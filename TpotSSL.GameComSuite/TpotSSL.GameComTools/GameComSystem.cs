using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpotSSL.GameComTools {
    /// <summary>
    /// Object for emulating and controlling a virtual game.com system.
    /// </summary>
    public class GameComSystem {
        /// <summary>
        /// Memory address space.
        /// 0x0000-0x007F: Registers
        /// 0x0080-0x03FF: RAM
        /// 0x0400-0x0FFF: Reserved Area
        /// 0x1000-0x9FFF: ROM
        /// 0xA000-0xDFFF: VRAM
        /// 0xE000-0xFFFF: Extended RAM
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
        /// Processor status register 0.
        /// </summary>
        public byte PS0 {
            get => AddressMap[0x001E];
            set => AddressMap[0x001E] = value;
        }

        /// <summary>
        /// Processor status register 1.
        /// Bit 0: Interrupt Enable.
        /// </summary>
        public byte PS1 {
            get => AddressMap[0x001F];
            set => AddressMap[0x001F] = value;
        }

        /// <summary>
        /// LCD Control/Status register.
        /// Bit  7:   Display ON/OFF.
        /// Bit  6:   Current framebuffer.
        /// Bit  5-4: Grayscale gradition control. (11 is the default)
        /// Bits 3-1: LCDC/DMA clock bits
        /// Bit  0:   Normal White bar bit (0 = white, 1 = black)
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
        /// Bits 7 to 6 : Dot data color 0
        /// Bits 5 to 4 : Dot data color 1
        /// Bits 3 to 2 : Dot data color 2
        ///  Bits 1 to 0 : Dot data color 3
        /// </summary>
        public byte DMPL {
            get => AddressMap[0x003B];
            set => AddressMap[0x003B] = value;
        }

        public GameComSystem() {
            // Create address space.
            AddressMap = new byte[0xFFFF];

            // Set up register objects.
            // These mainly just exist for convenience.
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

        /// <summary>
        /// Width in pixels.
        /// </summary>
        public  const    byte           PixelWidth  = 200;

        /// <summary>
        /// Height in pixels.
        /// </summary>
        public  const    byte           PixelHeight = 160;

        /// <summary>
        /// Width in bytes.
        /// </summary>
        public  const    byte           Width       = PixelWidth/4;

        /// <summary>
        /// Height in bytes.
        /// </summary>
        public  const    byte           Height      = PixelHeight;

        public GameComVRAM(GameComSystem system, int startingByte) {
            _system     = system;
            _start      = startingByte;
        }

        /// <summary>
        /// Get byte at index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index] {
            get => _system.AddressMap[_start+index];
            set => _system.AddressMap[_start+index] = value;
        }

        /// <summary>
        /// Get byte at rectangle position. Please note that this does not grab pixels, and each byte contains 4 pixels.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public byte this[int x, int y] {
            get => _system.AddressMap[_start + x + y * Width];
            set => _system.AddressMap[_start + x + y * Width] = value;
        }

        /// <summary>
        /// Rectangle copy pixels from ROM object.
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="bankno"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="destx"></param>
        /// <param name="desty"></param>
        public void RectCopy(GameComRom rom, byte bankno, byte x, byte y, byte w, byte h, byte destx, byte desty) => RectCopy(rom.MemoryBanks[bankno], x, y, w, h, destx, desty);

        /// <summary>
        /// Rectangle copy pixels from Bank object.
        /// </summary>
        /// <param name="bank">Bank object</param>
        /// <param name="x">Source X</param>
        /// <param name="y">Source Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="destx">Destination X</param>
        /// <param name="desty">Destination Y</param>
        public void RectCopy(GameComBank bank, byte x, byte y, byte w, byte h, byte destx, byte desty) {
            // Cycle through the bytes in the rectangle and apply them.
            for(int iy = 0; iy < h; ++iy) {
                for(int ix = 0; ix < w; ++ix) {
                    this[destx + ix, desty + iy] = bank[x + ix, y + iy];
                }
            }
        }

        public void RectCopy(GameComVRAM page, byte x, byte y, byte w, byte h, byte destx, byte desty) {
            // Cycle through the bytes in the rectangle and apply them.
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
