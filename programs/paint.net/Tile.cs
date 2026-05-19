// Name: Tile Grid
// Submenu: Object
// Author: marioood
// Title: Tile Grid
// Version: 1.0
// Desc: Tiles 
// Keywords:
// URL:
// Help:
#region UICode
IntSliderControl TileWidth = 16; // [1,256] Tile Width
IntSliderControl TileHeight = 16; // [1,256] Tile Height
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra cur;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        for (int x = rect.Left; x < rect.Right; x++)
        {
            cur = src[x % TileWidth,y % TileHeight];

            dst[x,y] = cur;
        }
    }
}

