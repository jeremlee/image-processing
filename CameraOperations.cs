using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace DIP_Activity
{
    static class CameraOperations
    {
        public static void greyScale(ref Bitmap input, ref Bitmap output)
        {
            Color pixel;
            int grey_value;

            for (int row = 0; row < input.Width; row++)
            {
                for (int col = 0; col < input.Height; col++)
                {
                    pixel = input.GetPixel(row, col);
                    grey_value = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    pixel = Color.FromArgb(grey_value, grey_value, grey_value);
                    output.SetPixel(row, col, pixel);
                }
            }
        }

        public static void invertColor(ref Bitmap input, ref Bitmap output)
        {
            Color pixel;
            int R_value, G_value, B_value;

            for (int row = 0; row < input.Width; row++)
            {
                for (int col = 0; col < input.Height; col++)
                {
                    pixel = input.GetPixel(row, col);
                    R_value = 255 - pixel.R;
                    G_value = 255 - pixel.G;
                    B_value = 255 - pixel.B;

                    pixel = Color.FromArgb(R_value, G_value, B_value);
                    output.SetPixel(row, col, pixel);
                }
            }
        }

        public static void sepia(ref Bitmap input, ref Bitmap output)
        {
            Color pixel;
            int R_value, G_value, B_value;

            for (int row = 0; row < input.Width; row++)
            {
                for (int col = 0; col < input.Height; col++)
                {
                    pixel = input.GetPixel(row, col);

                    R_value = (int)((pixel.R * .393) + (pixel.G * .769) + (pixel.B * .189));
                    G_value = (int)((pixel.R * .349) + (pixel.G * .686) + (pixel.B * .168));
                    B_value = (int)((pixel.R * .272) + (pixel.G * .534) + (pixel.B * .131));

                    if (R_value > 255)
                        R_value = 255;
                    if (G_value > 255)
                        G_value = 255;
                    if (B_value > 255)
                        B_value = 255;

                    pixel = Color.FromArgb(R_value, G_value, B_value);
                    output.SetPixel(row, col, pixel);
                }
            }
        }

        public static void histogram(ref Bitmap input, ref Bitmap output)
        {
            int[] histData = new int[256];
            Color pixel;
            int grey_value;

            for (int row = 0; row < input.Width; row++)
            {
                for (int col = 0; col < input.Height; col++)
                {
                    pixel = input.GetPixel(row, col);
                    grey_value = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    histData[grey_value]++;
                }
            }


            output = new Bitmap(256, 800);
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 800; y++)
                {
                    output.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(histData[x] / 5, output.Height - 1); y++)
                    output.SetPixel(x, (output.Height - 1) - y, Color.Black);
            }
        }

        public static void mirrorHorizontal(ref Bitmap background, ref Bitmap foreground)
        {
            if (background == null)
                return;

            foreground = new Bitmap(background.Width, background.Height);

            int width = background.Width;
            int height = background.Height;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    foreground.SetPixel(width - i - 1, j, background.GetPixel(i, j));
        }
        public static void mirrorVertical(ref Bitmap background, ref Bitmap foreground)
        {
            if (background == null) return;

            foreground = new Bitmap(background.Width, background.Height);

            int width = background.Width;
            int height = background.Height;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    foreground.SetPixel(i, height - j - 1, background.GetPixel(i, j));

        }
        public static void subtraction (ref Bitmap background, ref Bitmap foreground)
        {
            if (background == null || foreground == null)
                return;

            int greyGreen = 255 / 3;
            int threshold = 5;

            Color pixel;
            Bitmap? subtracted = new Bitmap(background.Width, background.Height);

            for (int i = 0; i < background.Width; i++)
            {
                if (i >= foreground.Width)
                    break;

                for (int j = 0; j < background.Height; j++)
                {
                    if (j >= foreground.Height)
                        break;

                    pixel = background.GetPixel(i, j);

                    subtracted.SetPixel(i, j,
                        Math.Abs((pixel.R + pixel.G + pixel.B) / 3 - greyGreen) < threshold ?
                        foreground.GetPixel(i, j) : pixel
                        );
                }
            }
            foreground = subtracted;
        }
    }
}
