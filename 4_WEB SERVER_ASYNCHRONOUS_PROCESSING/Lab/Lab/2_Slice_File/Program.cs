
namespace _2_Slice_File
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            while (n <= 50)
            {
                string videoPath = Console.ReadLine();
                string destination = Console.ReadLine();
                int pieces = int.Parse(Console.ReadLine());

                SliceAsync(videoPath, destination, pieces);

                Console.WriteLine("Anything else?");
                n++;
            }
        }

        private static void SliceAsync(string sourceFile, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }

        private static void Slice(string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var sourse = new FileStream(sourceFile, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                long partLenght = (sourse.Length / parts);
                long currentByte = 0;

                for (int currentPart = 1; currentPart < parts; currentPart++)
                {
                    string filePath = string.Format("{0}/Part-{1}{2}", destinationPath, currentPart, fileInfo.Extension);

                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[4096];

                        while (currentByte <= partLenght * currentPart)
                        {
                            int readBytesCount = sourse.Read(buffer, 0, buffer.Length);

                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }

                    Console.WriteLine("Slice complete.");
                }
            }
        }
    }
}
