using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace TpotSSL.GameComTools {
   
    public static class GameComAssembly {
        /// <summary>
        /// Disassemble rom file into an assembly string. (Early W.I.P.)
        /// </summary>
        /// <param name="bytes">rom file as bytes.</param>
        /// <returns></returns>
        public static List<string> Disassemble(byte[] bytes) {
            List<string> lines = new List<string>();
            for(int i = 0; i < bytes.Length; ++i) {
                DisassembleInstruction(bytes, ref i, out string line);
                lines.Add(line);
            }
            return lines;
        }

        /// <summary>
        /// Assemble a rom from file. 
        /// This does not work. Very much a W.I.P.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static List<byte> Assemble(string file, int startIndex = 0) {
            throw new NotImplementedException();
            string[] asm                        = File.ReadAllLines(file);
            List<byte>              code        = new List<byte>();
            Dictionary<string, int> methods     = new Dictionary<string, int>();
            for(int i=0; i<asm.Length; ++i) {
                string line = asm[i];
                if(char.IsLetter(line[0])){
                    int index = 0;
                    while(char.IsLetter(line[index]))
                        index++;
                    methods.Add(line.Substring(0, index), startIndex+i);
                }
            }
            return code;
        }

        public static void DisassembleInstruction(byte[] bytes, ref int index, out string result) {
            result = GetInstructionName(bytes[index]);
            switch(bytes[index]) {
                case 0x0:
                case 0x1:
                case 0x2:
                case 0x3:
                case 0x4:
                case 0x5:
                case 0x6:
                case 0x7:
                case 0x8:
                case 0x9:
                case 0xA:
                case 0xB:
                case 0xC:
                case 0xD:
                case 0xE:
                case 0xF:
                    result += $" R{bytes[++index]}";
                    break;
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x46:
                case 0x47:
                case 0x48:
                    result += $" R{bytes[index+2]}, R{bytes[index + 1]}";
                    index += 2;
                    break;
                case 0x49:
                    result += $" {BitConverter.ToUInt16(bytes, ++index)}";
                    index++;
                    break;
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                    result += $" R{bytes[index + 2]}, #{bytes[index + 1]}";
                    index += 2;
                    break;
            }
        }

        public static string GetInstructionName(byte index) {
            switch(index) {
                case 0x0:
                    return "CLR";
                case 0x1:
                    return "NEG";
                case 0x2:
                    return "COM";
                case 0x3:
                    return "RR";
                case 0x4:
                    return "RL";
                case 0x5:
                    return "RRC";
                case 0x6:
                    return "RLC";
                case 0x7:
                    return "SRL";
                case 0x8:
                    return "INC";
                case 0x9:
                    return "DEC";
                case 0xA:
                    return "SRL";
                case 0xB:
                    return "SLL";
                case 0xC:
                    return "DA";
                case 0xD:
                    return "SWAP";
                case 0xE:
                    return "PUSH";
                case 0xF:
                    return "POP";

                case 0x10:
                case 0x20:
                case 0x30:
                case 0x40:
                case 0x50:
                    return "CMP";
                case 0x11:
                case 0x21:
                case 0x31:
                case 0x41:
                case 0x51:
                    return "ADD";
                case 0x12:
                case 0x22:
                case 0x32:
                case 0x42:
                case 0x52:
                    return "SUB";
                case 0x13:
                case 0x23:
                case 0x33:
                case 0x43:
                case 0x53:
                    return "ADC";
                case 0x14:
                case 0x24:
                case 0x34:
                case 0x44:
                case 0x54:
                    return "SBC";
                case 0x15:
                case 0x25:
                case 0x35:
                case 0x45:
                case 0x55:
                    return "AND";
                case 0x16:
                case 0x26:
                case 0x36:
                case 0x46:
                case 0x56:
                    return "OR";
                case 0x17:
                case 0x27:
                case 0x37:
                case 0x47:
                case 0x57:
                    return "XOR";
                case 0x18:
                    return "INCW";
                case 0x19:
                    return "DECW";
                case 0x1A:
                case 0x1B:
                    return "";
                case 0x1C:
                    return "BCLR";
                case 0x1D:
                    return "BSET";
                case 0x1E:
                    return "PUSHW";
                case 0x1F:
                    return "POPW";
                case 0x28:
                case 0x29:
                case 0x2E:
                case 0x38:
                case 0x39:
                case 0x48:
                case 0xB0:
                case 0xB1:
                case 0xB2:
                case 0xB3:
                case 0xB4:
                case 0xB5:
                case 0xB6:
                case 0xB7:
                case 0xB8:
                case 0xB9:
                case 0xBA:
                case 0xBB:
                case 0xBC:
                case 0xBD:
                case 0xBE:
                case 0xBF:
                case 0xC0:
                case 0xC1:
                case 0xC2:
                case 0xC3:
                case 0xC4:
                case 0xC5:
                case 0xC6:
                case 0xC7:
                case 0xC8:
                case 0xC9:
                case 0xCA:
                case 0xCB:
                case 0xCC:
                case 0xCD:
                case 0xCE:
                case 0xCF:
                    return "MOV";
                case 0x3F:
                case 0x49:
                    return "CALL";
                case 0xF0:
                    return "STOP";
                case 0xF1:
                    return "HALT";
                case 0xF8:
                    return "RET";
                case 0xF9:
                    return "IRET";
                case 0xFF:
                    return "NOP";
            }
            return "ILLEGAL";
        }
    }
}
