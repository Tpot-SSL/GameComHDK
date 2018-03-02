using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpotSSL.GameComTools.ApplyAlpha {
    class Program {
        static void Main(string[] args){
            if(args.Length < 2){
                Console.WriteLine("Must provide 2 filenames. Usage: GCApplyAlpha filename alpha_filename");
                return;
            }

            string filename = args[0];
            string filename2 = args[1];

            GameComImage.MergeAlpha(filename, filename2).Save(Path.GetFileNameWithoutExtension(filename)+"_a"+".png");
        }
    }
}
