using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpotSSL.GameComTools {
    public class GameComSystem {
        /// <summary>
        /// Memory address space.
        /// </summary>
        public readonly byte[] AddressMap;

        /// <summary>
        /// General purpose registers.
        /// </summary>
        public readonly GameComRegisters R;

        /// <summary>
        /// Interrupt enable registers.
        /// </summary>
        public readonly GameComRegisters IE;

        /// <summary>
        /// Interrupt request registers.
        /// </summary>
        public readonly GameComRegisters IR;

        /// <summary>
        /// PIO data registers.
        /// </summary>
        public readonly GameComRegisters P;

        /// <summary>
        /// MMU data registers.
        /// </summary>
        public readonly GameComRegisters MMU;

        /// <summary>
        /// Clock Timer rate (32 KHz).
        /// </summary>
        public const int SubClockRate  = 32768;

        /// <summary>
        /// System Clock rate (10MHz).
        /// </summary>
        public const int MainClockRate = 10000000;

        /// <summary>
        /// System configuration register.
        /// </summary>
        public byte SYS {
            get => AddressMap[0x0019];
            set => AddressMap[0x0019] = value;
        }

        /// <summary>
        /// Clock change register.
        /// </summary>
        public byte CKC {
            get => AddressMap[0x001A];
            set => AddressMap[0x001A] = value;
        }

        /// <summary>
        /// Control/Status register.
        /// </summary>
        public byte LCC {
            get => AddressMap[0x0030];
            set => AddressMap[0x0030] = value;
        }

        /// <summary>
        /// Display H-timing register.
        /// </summary>
        public byte LCH {
            get => AddressMap[0x0031];
            set => AddressMap[0x0031] = value;
        }

        /// <summary>
        /// Display V-timing register.
        /// </summary>
        public byte LCV {
            get => AddressMap[0x0032];
            set => AddressMap[0x0032] = value;
        }

        /// <summary>
        /// Controller register.
        /// </summary>
        public byte DMC {
            get => AddressMap[0x0034];
            set => AddressMap[0x0034] = value;
        }

        /// <summary>
        /// Source X-coordinate register.
        /// </summary>
        public byte DMX1 {
            get => AddressMap[0x0035];
            set => AddressMap[0x0035] = value;
        }

        /// <summary>
        /// Source Y-coordinate register.
        /// </summary>
        public byte DMY1 {
            get => AddressMap[0x0036];
            set => AddressMap[0x0036] = value;
        }

        /// <summary>
        /// Destination X-coordinate register.
        /// </summary>
        public byte DMX2 {
            get => AddressMap[0x0039];
            set => AddressMap[0x0039] = value;
        }

        /// <summary>
        /// Destination Y-coordinate register.
        /// </summary>
        public byte DMY2 {
            get => AddressMap[0x003A];
            set => AddressMap[0x003A] = value;
        }
        /// <summary>
        /// Palette register.
        /// </summary>
        public byte DMPL {
            get => AddressMap[0x003B];
            set => AddressMap[0x003B] = value;
        }

        public GameComSystem() {
            AddressMap = new byte[0xFFFF];

            R   = new GameComRegisters(this, 0x0,  0x10);
            IE  = new GameComRegisters(this, 0x10, 0x2);
            IR  = new GameComRegisters(this, 0x12, 0x2);
            P   = new GameComRegisters(this, 0x14, 0x4);
            MMU = new GameComRegisters(this, 0x24, 0x5);

        }
    }

    public class GameComVRAM {
        private readonly GameComSystem  _system;
        private readonly int            _start;
        public  const    byte           PixelWidth  = 200;
        public  const    byte           PixelHeight = 160;
        public  const    byte           Width       = PixelWidth/4;
        public  const    byte           Height      = PixelHeight;

        public GameComVRAM(GameComSystem system, int start) {
            _system     = system;
            _start      = start;
        }

        public byte this[int index] {
            get => _system.AddressMap[_start+index];
            set => _system.AddressMap[_start+index] = value;
        }

        public byte this[int x, int y] {
            get => _system.AddressMap[_start + x + y * Width];
            set => _system.AddressMap[_start + x + y * Width] = value;
        }

        public void RectCopy(GameComRom rom, byte bankno, byte x, byte y, byte w, byte h, byte destx, byte desty) => RectCopy(rom.MemoryBanks[bankno], x, y, w, h, destx, desty);

        public void RectCopy(GameComBank bank, byte x, byte y, byte w, byte h, byte destx, byte desty) {
            for(int iy=0; iy<h; ++iy) {
                for(int ix = 0; ix < w; ++ix) {
                    this[destx+ix, desty+iy] = bank[x+ix, y+iy];
                }
            }
        }

        public void RectCopy(GameComVRAM page, byte x, byte y, byte w, byte h, byte destx, byte desty) {
            for(int iy = 0; iy < h; ++iy) {
                for(int ix = 0; ix < w; ++ix) {
                    this[destx + ix, desty + iy] = page[x + ix, y + iy];
                }
            }
        }
    }

    public class GameComRegisters {
        private readonly GameComSystem  _system;
        private readonly int            _start;
        private readonly int            _count;

        public GameComRegisters(GameComSystem system, int start, int count) {
            _system     = system;
            _start      = start;
            _count      = count;
        }
        public byte this[int index] {
            get => _system.AddressMap[_start+index];
            set => _system.AddressMap[_start+index] = value;
        }
    }
}
