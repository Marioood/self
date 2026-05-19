// Name: Worley Noise
// Submenu: Noise
// Author: marioood
// Title: Worley Noise
// Version: 1.0
// Desc: Distance-based noise
// Keywords:
// URL:
// Help:
#region UICode
DoubleSliderControl ScaleX = 16; // [1,512] Horizontal Scale
DoubleSliderControl ScaleY = 16; // [1,512] Vertical Scale
IntSliderControl OctaveCount = 1; // [1,8] Octaves
ReseedButtonControl NoiseSeed = 0; // Randomize
#endregion

byte CrudeRandom(int x, int y, int index) {
    return (byte)((x ^ (y * 13 + (x ^ (index * 67))) * 19847323359) * 2643 + ((x * 31) ^ (y * 2911111)));
}

double Worley(double x, double y, int index) {
    //TODO: optimize
    int y0 = (int)(y + 0.5);
    int x0 = (int)(x + 0.5);

    double shortestDist = double.PositiveInfinity;

    for(int yi = -1; yi <= 1; yi++) {
        for(int xi = -1; xi <= 1; xi++) {
            double xP = (CrudeRandom(x0 + xi, y0 + yi, index) / 255.0) - 0.5;
            double yP = (CrudeRandom(x0 + xi, y0 + yi, index * 2 + 1) / 255.0) - 0.5;
            double xSqr = xP + xi + (x0 - x);
            double ySqr = yP + yi + (y0 - y);
            double dist = Math.Sqrt(xSqr * xSqr + ySqr * ySqr);
            if(dist < shortestDist)
                shortestDist = dist;
        }
    }

    return shortestDist / Math.Sqrt(2);
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

