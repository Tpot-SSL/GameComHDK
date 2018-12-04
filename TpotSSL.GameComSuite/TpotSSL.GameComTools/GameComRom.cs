using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;

namespace TpotSSL.GameComTools {
    public enum GameComGame {
        FightersMegamix,
        SonicJam,
        Frogger,
        Centipede,
        ResidentEvil2,
        Henry,
        GameComInternet,
        TigerWebLink,
        MortalKombat,
        Jeopardy,
        Indy500,
        BatmanAndRobin,
        DukeNukem3D,
        Monopoly,
        WheelOfFortune,
        WilliamsArcade,
        QuizWiz,
        JurassicPark,
        Scrabble,
        TigerCasino
    }
  
    public class GameComRom : IDisposable {
        public int                  HeaderStartIndex = -1;
        public string               FilePath;

        /// <summary>
        /// Name specified in the header.
        /// </summary>
        public string               GameName;
        public ushort               GameId;

        /// <summary>
        /// The entire ROM has a game.com image.
        /// </summary>
        public GameComImage         FullImage;

        /// <summary>
        /// Game Icon X Coordinates
        /// </summary>
        public byte                 IconX;

        /// <summary>
        /// Game Icon Y Coordinates
        /// </summary>
        public byte                 IconY;

        /// <summary>
        /// Game Icon Bank Index
        /// </summary>
        public byte                 IconBankNo;

        /// <summary>
        /// Does the rom have a valid header.
        /// </summary>
        public bool                 IsValidRom;
        /// <summary>
        /// Game Icon as Image
        /// </summary>
        public Bitmap               GameIcon;

        public List<GameComBank>    MemoryBanks;

        public          int         SizeInBytes     => RawBytes.Length;
        public          byte[]      RawBytes        => FullImage.RawBytes;

        public const    string      TigerId         = "TigerDMGC";

        public static Dictionary<ushort, GameComGame> KnownGamesById = new Dictionary<ushort, GameComGame>(){
            {36646, GameComGame.FightersMegamix },  {37682, GameComGame.SonicJam },     {38680, GameComGame.Frogger },
            {45843, GameComGame.GameComInternet },  {50770, GameComGame.Indy500 },      {47124, GameComGame.MortalKombat },
            {49732, GameComGame.WheelOfFortune },   {37637, GameComGame.Monopoly },     {40481, GameComGame.Centipede },
            {35349, GameComGame.WilliamsArcade },   {57364, GameComGame.Scrabble },     {40498, GameComGame.Henry },
            {59714, GameComGame.JurassicPark },     {58155, GameComGame.DukeNukem3D },  {61498, GameComGame.ResidentEvil2 },
            {40497, GameComGame.BatmanAndRobin },   {42240, GameComGame.TigerWebLink }, {12657, GameComGame.Jeopardy },
            {58385, GameComGame.TigerCasino },      {53585, GameComGame.QuizWiz },
        };

        public static string GetGameName(GameComGame game){
            switch(game) {
                case GameComGame.FightersMegamix:
                    return "Fighters Megamix";
                case GameComGame.SonicJam:
                    return "Sonic Jam";
                case GameComGame.Frogger:
                    return "Frogger";
                case GameComGame.Centipede:
                    return "Centipede";
                case GameComGame.ResidentEvil2:
                    return "Resident Evil 2";
                case GameComGame.Henry:
                    return "Henry";
                case GameComGame.GameComInternet:
                    return "Game.Com Internet";
                case GameComGame.TigerWebLink:
                    return "Tiger Web Link";
                case GameComGame.MortalKombat:
                    return "Mortal Kombat";
                case GameComGame.Jeopardy:
                    return "Jeopardy";
                case GameComGame.Indy500:
                    return "Indy 500";
                case GameComGame.BatmanAndRobin:
                    return "Batman and Robin";
                case GameComGame.DukeNukem3D:
                    return "Duke Nukem 3D";
                case GameComGame.Monopoly:
                    return "Monopoly";
                case GameComGame.WheelOfFortune:
                    return "Wheel Of Fortune 1 & 2";
                case GameComGame.WilliamsArcade:
                    return "Williams Arcade Classics";
                case GameComGame.QuizWiz:
                    return "Quiz Wiz";
                case GameComGame.JurassicPark:
                    return "Jurassic Park";
                case GameComGame.Scrabble:
                    return "Scrabble";
                case GameComGame.TigerCasino:
                    return "Tiger Casino";
                default:
                    return "Unknown  ";
            }
        }

        /// <summary>
        /// Get known game name from index id.
        /// </summary>
        /// <param name="game">Game ID</param>
        /// <returns>Game Name</returns>
        public static string GetGameName(ushort game) => KnownGamesById.ContainsKey(game) ? GetGameName(KnownGamesById[game]) : "Unknown  ";

        /// <summary>
        /// Update the rom's raw bytes to match the current header info.
        /// Keep note that this does not write to any files.
        /// </summary>
        public void UpdateHeader() {
            // Header couldn't be found, so it can't be updated.
            if(HeaderStartIndex < 0)
                return;

            int index = HeaderStartIndex;

            // Update the bytes.
            FullImage.RawBytes[index]       = IconBankNo;
            FullImage.RawBytes[index + 1]   = IconX;
            FullImage.RawBytes[index + 2]   = IconY;

            // Convert the rom name into ascii bytes and apply to the array.
            byte[] nameBytes = Encoding.ASCII.GetBytes(GameName);

            for(int i = 0; i < 9; i++)
                FullImage.RawBytes[index+3+i] = nameBytes[i];
        }
        /// <summary>
        /// Save rom as binary file.
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveBinary(string filepath){
            UpdateHeader();
            FullImage.SaveBinary(filepath);
        }

        /// <summary>
        /// Overwrite rom binary with current data.
        /// </summary>
        public void SaveBinary() => SaveBinary(FilePath);

        /// <summary>
        /// Replace one of the banks with a new Image.
        /// </summary>
        /// <param name="bankIndex"></param>
        /// <param name="image"></param>
        public void ReplaceBank(int bankIndex, GameComImage image){
            GameComBank bank = MemoryBanks[bankIndex];
            using(Graphics g = Graphics.FromImage(FullImage.Bitmap))
                g.DrawImage(image.Bitmap, bank.X, bank.Y);
            
            for(int i = 0; i < image.RawBytes.Length; i++)
                FullImage.RawBytes[bank.ByteStart + i] = image.RawBytes[i];
        }

        /// <summary>
        /// Create rom object from file.
        /// </summary>
        /// <param name="filePath"></param>
        public GameComRom(string filePath){
            FullImage       = new GameComImage(File.ReadAllBytes(filePath));
            MemoryBanks     = GameComImage.SplitBanks(FullImage);

            GrabHeaderData(SearchForHeader());
            FilePath = filePath;
        }

        public Bitmap RefreshIcon(byte x, byte y) {
            IconX       = x;
            IconY       = y;

            return RefreshIcon();
        }

        public Bitmap RefreshIcon(byte bankNo) {
            IconBankNo  = bankNo;

            return RefreshIcon();
        }

        public Bitmap RefreshIcon(byte x, byte y, byte bankNo) {
            IconX       = x;
            IconY       = y;
            IconBankNo  = bankNo;

            return RefreshIcon();
        }

        /// <summary>
        /// Refresh icon image with current header info.
        /// </summary>
        /// <returns></returns>
        public Bitmap RefreshIcon() {
            if(IconBankNo >= MemoryBanks.Count)
                return new Bitmap(64, 64);

            return GameIcon = MemoryBanks[IconBankNo].Image.Bitmap.Clone(new Rectangle(IconX, IconY, 64, 64), PixelFormat.Format32bppArgb);
        }

        /// <summary>
        /// Fill this instance's variables with the header data.
        /// </summary>
        /// <param name="index"></param>
        public void GrabHeaderData(int index){
            if(index < 0)
                return;

            IconBankNo  = RawBytes[index];
            IconX       = RawBytes[index + 1];
            IconY       = RawBytes[index + 2];

            GameName    = Encoding.ASCII.GetString(RawBytes, index+3, 9);

            RefreshIcon();
            GameId              = BitConverter.ToUInt16(RawBytes, index + 13);

            IsValidRom            = true;
            HeaderStartIndex    = index;
        }

        /// <summary>
        /// Find ROM Header data and return the location.
        /// </summary>
        /// <returns>Location of header.</returns>
        public int SearchForHeader(){
            string tigerId = Encoding.ASCII.GetString(RawBytes, 5, 9);
            if(tigerId == TigerId)
                return 14;

            tigerId = Encoding.ASCII.GetString(RawBytes, 0x40005, 9);
            return tigerId == TigerId ? 0x4000E : -1;
        }

        public void Dispose(){
            FullImage.Dispose();
            MemoryBanks.Clear();

            FullImage   = null;
            MemoryBanks = null;

            GC.SuppressFinalize(this);
        }
    }

    public class GameComBank {
        /// <summary>
        /// X position in the ROM.
        /// </summary>
        public          int             X;

        /// <summary>
        /// Y position in the ROM.
        /// </summary>
        public          int             Y;

        /// <summary>
        /// Byte index the bank starts at in the rom.
        /// </summary>
        public          int             ByteStart;

        /// <summary>
        /// Image data for this bank.
        /// </summary>
        public          GameComImage    Image;

        public static   int             SizeInBytes => 16384;

        /// <summary>
        /// Raw byte array.
        /// </summary>
        public          byte[]          RawBytes    => Image.RawBytes;

        /// <summary>
        /// Create from game.com image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="byteStart"></param>
        public GameComBank(GameComImage image, int x, int y, int byteStart){
            Image       = image;
            X           = x;
            Y           = y;
            ByteStart   = byteStart;
        }
        /// <summary>
        /// Create from bitmap image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="byteStart"></param>
        public GameComBank(Bitmap image, int x, int y, int byteStart) {
            Image = new GameComImage(image);
            X = x;
            Y = y;
            ByteStart = byteStart;
        }
        /// <summary>
        /// Create from bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="byteStart"></param>
        public GameComBank(byte[] bytes, int x, int y, int byteStart) {
            Image = new GameComImage(bytes);
            X = x;
            Y = y;
            ByteStart = byteStart;
        }

        /// <summary>
        /// Get Byte at index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index] {
            get => RawBytes[ByteStart + index];
            set => RawBytes[ByteStart + index] = value;
        }
        /// <summary>
        /// Get byte at rectangle position. (This does not grab pixels. Each byte has 4 pixels.)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public byte this[int x, int y] {
            get => RawBytes[ByteStart + x + y * 64];
            set => RawBytes[ByteStart + x + y * 64] = value;
        }


    }
}
