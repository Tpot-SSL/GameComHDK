using System;
using System.IO;

namespace TpotSSL.GameComTools.ImageRipper {
    public class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                Console.WriteLine("Must provide a filename. Usage: GCImageRip filename");
                return;
            }

            string filename = args[0];

             GameComImage.Convert(filename, true);
        }
    }
}
