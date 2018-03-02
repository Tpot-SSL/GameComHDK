using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpotSSL.GameComTools.FMColorFix {
    class Program {
        static void Main(string[] args) {
            if(args.Length < 1) {
                Console.WriteLine("Must provide a filename. Usage: GCImageRip filename");
                return;
            }

            string filename = args[0];

            GameComImage.FixColor(filename, GameComGame.FightersMegamix);
        }
    }
}
