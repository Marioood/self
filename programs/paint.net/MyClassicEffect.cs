// Name:
// Submenu:
// Author:
// Title:
// Version:
// Desc:
// Keywords:
// URL:
// Help:
#region UICode
ListBoxControl RedChoice = 0; // Red Channel|Red|Green|Blue|Alpha|None
ListBoxControl GreenChoice = 1; // Green Channel|Red|Green|Blue|Alpha|None
ListBoxControl BlueChoice = 2; // Blue Channel|Red|Green|Blue|Alpha|None
ListBoxControl AlphaChoice = 3; // Alpha Channel|Red|Green|Blue|Alpha|None
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra currentPixel;
    byte[] channels = new byte[5];

    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            currentPixel = src[x,y];
            channels[0] = currentPixel.R;
            channels[1] = currentPixel.G;
            channels[2] = currentPixel.B;
            channels[3] = currentPixel.A;

            currentPixel.R = channels[RedChoice];
            currentPixel.G = channels[GreenChoice];
            currentPixel.B = channels[BlueChoice];
            currentPixel.A = channels[AlphaChoice];
            dst[x,y] = currentPixel;
        }
    }
}
