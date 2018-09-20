using System;
using System.Drawing;
using System.IO;

namespace YPSamples.SimpleOCR
{
    static class Program
    {
        static void Main(string[] args)
        {
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "..");

            // TODO: use path from args
            var filePath = Path.Combine(rootPath, "TestData", "3.jpg");

            var (bmpImage, bmpWidth, bmpHeight) = LoadFromFile(filePath);

            var buffer = new byte[bmpHeight, bmpWidth];

            Recognizer.ConvertToBlackAndWhite(bmpImage, bmpHeight, bmpWidth, ref buffer);

            PrintArray(buffer);

            var result = Recognizer.Recognize(buffer);
            
            Console.WriteLine(result);

            // Save image to file
            //var outputFilePath = Path.Combine(rootPath, "output.jpg");
            //bmpImage.Save(outputFilePath);

            Console.ReadLine();
        }

        static (Bitmap, int, int) LoadFromFile(string filePath)
        {
            // Read image
            // For MacOS need native libgdiplus, more:
            // https://github.com/CoreCompat/CoreCompat/issues/3#issuecomment-277347158
            var image = Image.FromFile(filePath);
            var bmpImage = new Bitmap(image);
            return (bmpImage, bmpImage.Width, bmpImage.Height);
        }

        static void PrintArray(byte[,] array)
        {
            var height = array.GetUpperBound(0);
            var width = array.GetUpperBound(1);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    Console.Write(array[i, j] > 0 ? "O" : " ");
                }
                Console.WriteLine();
            }
        }
    }
}
