using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDrip
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = args != null && args.Length > 2 ? args[2] : "*.*";

            if (args != null && args.Length < 2)
            {
                Console.WriteLine("please provide source and target directories");
                return;
            }

            var source = new DirectoryInfo(args[0]);
            var destination = new DirectoryInfo(args[1]);

            var files = Directory.GetFiles(source.FullName, pattern);

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
                    Console.WriteLine("FAILED!");
                }
                finally
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
