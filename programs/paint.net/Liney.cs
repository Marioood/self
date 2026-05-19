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
IntSliderControl Amount1 = 0; // [0,100] Slider 1 Description
IntSliderControl Amount2 = 0; // [0,100] Slider 2 Description
IntSliderControl Amount3 = 0; // [0,100] Slider 3 Description
#endregion



byte CrudeRandom(int x, int y, int index) {
    return (byte)((x ^ (y * 13 + (x ^ (index * 67))) * 19847323359) * 2643 + ((x * 31) ^ (y * 2911111)));
}


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

