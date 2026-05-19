// Name: Worley Noise
// Submenu: Noise
// Author: marioood
// Title: Worley Noise
// Version: 1.1
// Desc: Distance-based noise
// Keywords:
// URL:
// Help:
#region UICode
DoubleSliderControl ScaleX = 16; // [1,512] Horizontal Scale
DoubleSliderControl ScaleY = 16; // [1,512] Vertical Scale
IntSliderControl OctaveCount = 1; // [1,8] Octaves
ListBoxControl DistMode = 0; // Distance Mode|Euclidean (Circular)|Taxicab (Rhombic)|Chebychev (Square)
ListBoxControl OutputMode = 0; // Output Mode|Distance|Voronoi
IntSliderControl RadiusRandomnessInt = 0; // [0,100] Radius Randomness
IntSliderControl RandomnessXInt = 100; // [0,100] Horizontal Randomness
IntSliderControl RandomnessYInt = 100; // [0,100] Vertical Randomness
ReseedButtonControl NoiseSeed = 0; // Randomize
#endregion

private Func<double, double, double> DistFunction = null;
private double Divisor = 0;

private double RadiusRandomness;
private double RandomnessX;
private double RandomnessY;

byte CrudeRandom(int x, int y, int index) {
    return (byte)((x ^ (y * 13 + (x ^ (index * 67))) * 19847323359) * 2643 + ((x * 31) ^ (y * 2911111)));
}

double Worley(double x, double y, int index) {
    //TODO: optimize
    int y0 = (int)(y);
    int x0 = (int)(x);

    double shortestDist = double.PositiveInfinity;
    byte closestIndex = 0;

    for(int yi = -1; yi <= 1; yi++) {
        for(int xi = -1; xi <= 1; xi++) {
            //if(xi == 0 && yi == 0) continue;
            double xP = (CrudeRandom(x0 + xi, y0 + yi, index) / 255.0);
            xP *= RandomnessX;
            double yP = (CrudeRandom(x0 + xi, y0 + yi, index * 2 + 1) / 255.0);
            yP *= RandomnessY;
            double radiusMult = CrudeRandom(x0 + xi, y0 + yi, index * 3 + 2) / 255.0;
            radiusMult = radiusMult * RadiusRandomness + (1 - RadiusRandomness);

            double dist = DistFunction(xP + xi + (x0 - x), yP + yi + (y0 - y)) * radiusMult;
            if(dist < shortestDist) {
                shortestDist = dist;
                closestIndex = CrudeRandom(x0 + xi, y0 + yi, index * 4 + 3);
            }
        }
    }

    return OutputMode == 0 ? shortestDist / Divisor : closestIndex / 255.0;
}

double WorleyOctaves(double x, double y, int index, int octaves) {
    double scale = 1;
    double dist = Worley(x, y, index);
    for(int o = 1; o < octaves; o++) {
        scale /= 2;
        dist += Worley(x / scale, y / scale, index + o);
    }
    return dist / octaves;
}

void PreRender(Surface dst, Surface src)
{
    switch(DistMode) {
        case 0:
            DistFunction = (x, y) => Math.Sqrt(x * x + y * y);
            Divisor = Math.Sqrt(2);
            break;
        case 1:
            DistFunction = (x, y) => Math.Abs(x) + Math.Abs(y);
            Divisor = 2;
            break;
        case 2:
            DistFunction = (x, y) => Math.Max(Math.Abs(x), Math.Abs(y));
            Divisor = 1;
            break;
    }

    RadiusRandomness = RadiusRandomnessInt / 100.0;
    RandomnessX = RandomnessXInt / 100.0;
    RandomnessY = RandomnessYInt / 100.0;
}

void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra cur;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        for (int x = rect.Left; x < rect.Right; x++)
        {
            cur = src[x,y];
            byte col = (byte)(WorleyOctaves(x / ScaleX, y / ScaleY, NoiseSeed, OctaveCount) * 256);
            cur.R = col;
            cur.G = col;
            cur.B = col;

            dst[x,y] = cur;
        }
    }
}

