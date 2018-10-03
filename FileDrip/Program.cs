using System;
using System.Threading;
using System.IO;

namespace FileDrip
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("USAGE: FileDrip <source folder> <destination folder> [delay in seconds, default = 1]");
                return;
            }

            var source = new DirectoryInfo(args[0]);
            if (!Directory.Exists(source.FullName))
            {
                Console.WriteLine("ERROR: source directory does not exist: {0}", source.FullName);
                return;
            }

            var destination = new DirectoryInfo(args[1]);
            if (!Directory.Exists(destination.FullName))
            {
                Console.WriteLine("ERROR: destination directory does not exist: {0}", destination.FullName);
                return;
            }

            if (source.FullName == destination.FullName)
            {
                Console.WriteLine("ERROR: source and destination directories must be different");
                return;
            }

            int delay = 1;
            if (args.Length > 2)
            {
                bool test = int.TryParse(args[2], out delay);

                if (test == false)
                {
                    Console.WriteLine("ERROR: delay option should be a valid whole number");
                    return;
                }
            }

            var files = Directory.GetFiles(source.FullName);

            foreach (var f in files)
            {
                var fi = new FileInfo(f);

                try
                {
                    Console.Write($"{fi.FullName}... ");
                    File.Move(fi.FullName, Path.Combine(destination.FullName, fi.Name));
                    Console.WriteLine("OK!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("FAILED: {0}", e.ToString());
                }
                finally
                {
                    Thread.Sleep(delay * 1000);
                }
            }
        }
    }
}
