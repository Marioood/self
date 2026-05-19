// Name: Wave Pass
// Submenu: Color
// Author: marioood
// Title: Wave Pass
// Version: 1.0
// Desc: Puts pixels through a wave function.
// Keywords:
// URL:
// Help:
#region UICode
DoubleSliderControl Period = 8; // [0,100] Period
ListBoxControl WaveType = 0; // Wave|Cosine|Triangle|Sawtooth|Square
#endregion

double Tri(double n) {
    double flip = (int)(n) % 2 == 0 ? -2 : 2;
    double offs = (int)(n) % 2 == 0 ? 1 : -1;
    return n % 1 * flip + offs;
}

double Saw(double n) {
    return n % 1 * -2 + 1;
}

double Square(double n) {
    return (int)(n + 0.5) % 2 == 0 ? 1 : -1;
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

            /*if((int)(y / 16) % 4 == 0) {
                cur.G = (byte)((Tri(x / Period) / 2 + 0.5) * 255);
            } else if((int)(y / 16) % 4 == 1) {
                cur.G = (byte)((Math.Cos(x * Math.PI / Period) / 2 + 0.5) * 255);
            } else if((int)(y / 16) % 4 == 2) {
                cur.G = (byte)((Saw(x / Period) / 2 + 0.5) * 255);
            } else {
                cur.G = (byte)((Square(x / Period) / 2 + 0.5) * 255);
            }*/

            switch(WaveType) {
                case 0:
                    cur.R = (byte)((Math.Cos(cur.R / 255.0 * Math.PI * Period) / 2 + 0.5) * 255);
                    cur.G = (byte)((Math.Cos(cur.G / 255.0 * Math.PI * Period) / 2 + 0.5) * 255);
                    cur.B = (byte)((Math.Cos(cur.B / 255.0 * Math.PI * Period) / 2 + 0.5) * 255);
                    break;
                case 1:
                    cur.R = (byte)((Tri(cur.R / 255.0 * Period) / 2 + 0.5) * 255);
                    cur.G = (byte)((Tri(cur.G / 255.0 * Period) / 2 + 0.5) * 255);
                    cur.B = (byte)((Tri(cur.B / 255.0 * Period) / 2 + 0.5) * 255);
                    break;
                case 2:
                    cur.R = (byte)((Saw(cur.R / 255.0 * Period) / 2 + 0.5) * 255);
                    cur.G = (byte)((Saw(cur.G / 255.0 * Period) / 2 + 0.5) * 255);
                    cur.B = (byte)((Saw(cur.B / 255.0 * Period) / 2 + 0.5) * 255);
                    break;
                case 3:
                    cur.R = (byte)((Square(cur.R / 255.0 * Period) / 2 + 0.5) * 255);
                    cur.G = (byte)((Square(cur.G / 255.0 * Period) / 2 + 0.5) * 255);
                    cur.B = (byte)((Square(cur.B / 255.0 * Period) / 2 + 0.5) * 255);
                    break;
            }


            dst[x,y] = cur;
        }
    }
}

