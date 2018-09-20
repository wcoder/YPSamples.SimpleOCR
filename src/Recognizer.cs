using System.Drawing;
using System.Linq;

namespace YPSamples.SimpleOCR
{
    public static class Recognizer
    {
        public static void ConvertToBlackAndWhite(
            Bitmap bmpImage,
            int bmpHeight,
            int bmpWidth,
            ref byte[,] buffer)
        {
            for (var i = 0; i < bmpHeight; i++)
            for (var j = 0; j < bmpWidth; j++)
            {
                var c = bmpImage.GetPixel(j, i);
                var bw = (byte)(new[] { c.R, c.G, c.B }.Max() > 210 ? 255 : 0);

                //bmpImage.SetPixel(j, i, Color.FromArgb(bw, bw, bw));

                buffer[i, j] = (byte)(bw > 0 ? 0 : 1);
            }
        }

        public static string Recognize(byte[,] buffer)
        {
            // Split to fragments with 8px width
            var numbers = Helpers.SplitArray(buffer, 8);

            // Recognize each fragment based some metrics
            return string.Join("", numbers.Select(RecognizeNumber));
        }

        public static string RecognizeNumber(byte[,] a)
        {
            if (a[8, 0] == 1 &&
                a[8, 7] == 1 &&
                a[5, 3] == 1 &&
                a[8, 4] == 1 &&
                a[11,4] == 1 &&
                a[12,4] == 0
                )
                return "+";

            if (a[3, 2] == 1 &&
                a[3, 5] == 1 &&
                a[7, 3] == 0 &&
                a[7, 7] == 1 &&
                a[7, 1] == 1 &&
                a[10,1] == 1 &&
                a[12,6] == 0 &&
                a[9, 3] == 0 &&
                a[12,2] == 1
                )
                return "0";

            if (a[3, 2] == 0 &&
                a[3, 5] == 0 &&
                a[6, 4] == 1 &&
                a[12,4] == 1 &&
                a[11,3] == 1
                )
                return "1";

            if (a[3, 1] == 1 &&
                a[3, 5] == 1 &&
                a[6, 6] == 1 &&
                a[7, 3] == 0 &&
                a[9, 3] == 1 &&
                a[12,0] == 1 &&
                a[12,6] == 1
                )
                return "2";

            if (a[3, 1] == 1 &&
                a[3, 6] == 0 &&
                a[7, 2] == 1 &&
                a[6, 1] == 0 &&
                a[9, 1] == 0 &&
                a[10,6] == 1 &&
                a[12,1] == 1 &&
                a[12,5] == 1
                )
                return "3";

            if (a[5, 4] == 1 &&
                a[10,7] == 1 &&
                a[10,0] == 1 &&
                a[12,5] == 1
                )
                return "4";

            if (a[3, 1] == 1 &&
                a[3, 6] == 1 &&
                a[5, 1] == 1 &&
                a[5, 6] == 0 &&
                a[7, 1] == 1 &&
                a[7, 5] == 1 &&
                a[12,1] == 1 &&
                a[12,5] == 1
                )
                return "5";

            if (a[3, 1] == 0 &&
                a[3, 6] == 1 &&
                a[4, 3] == 1 &&
                a[7, 1] == 1 &&
                a[8, 5] == 1 &&
                a[12,1] == 0 &&
                a[12,6] == 0
                )
                return "6";

            if (a[3, 0] == 1 &&
                a[3, 7] == 1 &&
                a[7, 4] == 1 &&
                a[12,2] == 1 &&
                a[12,6] == 0
                )
                return "7";

            if (a[3, 2] == 1 &&
                a[7, 3] == 1 &&
                a[5, 1] == 1 &&
                a[5, 6] == 1 &&
                a[10,1] == 1 &&
                a[10,6] == 1 &&
                a[12,2] == 1
                )
                return "8";

            if (a[3, 2] == 1 &&
                a[3, 5] == 1 &&
                a[7, 3] == 0 &&
                a[7, 7] == 1 &&
                a[7, 1] == 1 &&
                a[10,1] == 0 &&
                a[12,6] == 0 &&
                a[9, 3] == 0 &&
                a[12,2] == 1
                )
                return "9";

            return "?";
        }
    }
}