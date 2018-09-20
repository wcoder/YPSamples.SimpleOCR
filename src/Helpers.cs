using System.Collections.Generic;

namespace YPSamples.SimpleOCR
{
    public static class Helpers
    {
        public static IEnumerable<byte[,]> SplitArray(byte[,] array, int widthStep)
        {
            var height = array.GetUpperBound(0);
            var width = array.GetUpperBound(1);

            var parts = new List<byte[,]>();

            for (int start = 0, end = widthStep; end < width; start = end, end += widthStep)
            {
                var part = CutArray(array, height, start, end);
                parts.Add(part);
            }

            return parts;
        }

        public static byte[,] CutArray(byte[,] array, int height, int startWidth, int endWidth)
        {
            var tempBuffer = new byte[height, endWidth - startWidth];
            for (var i = 0; i < height; i++)
            for (int j = startWidth, k = 0; j < endWidth; j++, k++)
            {
                tempBuffer[i, k] = array[i, j];
            }
            return tempBuffer;
        }
    }
}