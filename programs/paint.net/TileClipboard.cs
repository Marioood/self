// Name: Tile from Clipboard
// Submenu: Object
// Author: marioood
// Title: Tile from Clipboard
// Version: 1.0
// Desc: Repeats the contents of the clipboard.
// Keywords:
// URL:
// Help:
#region UICode
IntSliderControl OffsX = 0; // [0,256] Horizontal Offset
IntSliderControl OffsY = 0; // [0,256] Vertical Offset
#endregion

// Working surface
Surface wrk = null;

private Surface clipboardSurface = null;
private bool readClipboard = false;

protected override void OnDispose(bool disposing)
{
    if (disposing)
    {
        // Release any surfaces or effects you've created
        wrk?.Dispose(); wrk = null;
        clipboardSurface?.Dispose(); clipboardSurface = null;
    }

    base.OnDispose(disposing);
}

// This single-threaded function is called after the UI changes and before the Render function is called
// The purpose is to prepare anything you'll need in the Render function
void PreRender(Surface dst, Surface src)
{
    if (wrk == null)
    {
        wrk = new Surface(src.Size);
    }

    if (!readClipboard)
    {
        readClipboard = true;
        clipboardSurface = Services.GetService<IClipboardService>().TryGetSurface();
    }
    // Copy from the Clipboard to the wrk surface
    for (int y = 0; y < wrk.Size.Height; y++)
    {
        if (IsCancelRequested) return;
        for (int x = 0; x < wrk.Size.Width; x++)
        {
            if (clipboardSurface != null)
            {
                //wrk[x,y] = clipboardSurface.GetBilinearSample(x, y);
                //wrk[x,y] = clipboardSurface.GetBilinearSampleClamped(x, y);
                wrk[x,y] = clipboardSurface.GetBilinearSampleWrapped(x - OffsX, y + OffsY);
            }
            else
            {
                wrk[x,y] = Color.Transparent;
            }
        }
    }
}

// Here is the main multi-threaded render function
// The dst canvas is broken up into rectangles and
// your job is to write to each pixel of that rectangle
void Render(Surface dst, Surface src, Rectangle rect)
{
    // uint seed = RandomNumber.InitializeSeed(RandomNumberRenderSeed, rect.Location);

    // Step through each row of the current rectangle
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        // Step through each pixel on the current row of the rectangle
        for (int x = rect.Left; x < rect.Right; x++)
        {
            ColorBgra SrcPixel = src[x,y];
            ColorBgra WrkPixel = wrk[x,y];
            ColorBgra DstPixel = dst[x,y];

            ColorBgra CurrentPixel = WrkPixel;

            // TODO: Add additional pixel processing code here


            dst[x,y] = CurrentPixel;
        }
    }
}

