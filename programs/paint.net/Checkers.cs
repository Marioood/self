// Name: Checkers
// Submenu: Render
// Author: marioood
// Title: Checkers
// Version: 1.0
// Desc:
// Keywords:
// URL:
// Help:
#region UICode
IntSliderControl ScaleX = 16; // [1,256] Horizontal Scale
IntSliderControl ScaleY = 16; // [1,256] Vertical Scale
IntSliderControl ShiftX = 0; // [0,256] Horizontal Offset
IntSliderControl ShiftY = 0; // [0,256] Vertical Offset
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra primaryColor = EnvironmentParameters.PrimaryColor;
    ColorBgra secondaryColor = EnvironmentParameters.SecondaryColor;

    ColorBgra cur;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        for (int x = rect.Left; x < rect.Right; x++)
        {
            cur = src[x,y];
            int c = ((int)((x + ShiftX * ((int)(y / ScaleY) % 2)) / ScaleX) + (int)((y + ShiftY * ((int)(x / ScaleX) % 2)) / ScaleY));
            cur = c % 2 == 0 ? primaryColor : secondaryColor;

            dst[x,y] = cur;
        }
    }
}
