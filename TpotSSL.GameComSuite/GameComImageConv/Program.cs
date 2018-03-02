using System;
using System.IO;

namespace TpotSSL.GameComTools.ImageConv {
    class Program {
        static void Main(string[] args){
            if(args.Length < 1){
                Console.WriteLine("Must provide a filename. Usage: GCImageConv filename");
                return;
            }

            string filename = args[0];
            string ext = Path.GetExtension(filename);

            GameComImage.Convert(filename);
        }
    }
}
