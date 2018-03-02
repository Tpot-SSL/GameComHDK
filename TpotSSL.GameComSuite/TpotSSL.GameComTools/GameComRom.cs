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
        public string               Filename;

        public string               GameName;
        public ushort               GameId;

        public GameComImage         FullImage;

        public byte                 IconX;
        public byte                 IconY;
        public byte                 IconBankNo;

        public bool                 ValidRom;

        public Bitmap               GameIcon;

        public List<GameComBank>    MemoryBanks;


        public          int         SizeInBytes     => RawBytes.Length;
        public          byte[]      RawBytes        => FullImage.RawBytes;

        public static   string      TigerId         => "TigerDMGC";

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

        public static string GetGameName(ushort game) => KnownGamesById.ContainsKey(game) ? GetGameName(KnownGamesById[game]) : "Unknown  ";

        public void UpdateHeader() {
            if(HeaderStartIndex < 0)
                return;

            int index = HeaderStartIndex;

            FullImage.RawBytes[index]       = IconBankNo;
            FullImage.RawBytes[index + 1]   = IconX;
            FullImage.RawBytes[index + 2]   = IconY;

            byte[] nameBytes = Encoding.ASCII.GetBytes(GameName);

            for(int i = 0; i < 9; i++)
                FullImage.RawBytes[index+3+i] = nameBytes[i];
        }

        public void SaveBinary(string filename){
            UpdateHeader();
            FullImage.SaveBinary(filename);
        }

        public void SaveBinary() {
            UpdateHeader();
            FullImage.SaveBinary(Filename);
        }

        public void ReplaceBank(int bankIndex, byte[] bytes, Bitmap image){
            GameComBank bank = MemoryBanks[bankIndex];
            using(Graphics g = Graphics.FromImage(FullImage.Bitmap))
                g.DrawImage(image, bank.X, bank.Y);
            
            for(int i = 0; i < bytes.Length; i++)
                FullImage.RawBytes[bank.ByteStart + i] = bytes[i];
        }

        public GameComRom(string filename){
            FullImage       = new GameComImage(File.ReadAllBytes(filename));
            MemoryBanks     = GameComImage.SplitBanks(FullImage);

            GrabHeaderData(SearchForHeader());
            Filename = filename;
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

        public Bitmap RefreshIcon() {
            if(IconBankNo >= MemoryBanks.Count)
                return new Bitmap(64, 64);

            return GameIcon = MemoryBanks[IconBankNo].Image.Bitmap.Clone(new Rectangle(IconX, IconY, 64, 64), PixelFormat.Format32bppArgb);
        }

        public void GrabHeaderData(int index){
            if(index < 0)
                return;

            IconBankNo  = (RawBytes[index]);
            IconX       = RawBytes[index + 1];
            IconY       = RawBytes[index + 2];

            GameName    = Encoding.ASCII.GetString(RawBytes, index+3, 9);

            RefreshIcon();
            GameId              = BitConverter.ToUInt16(RawBytes, index + 13);

            ValidRom            = true;
            HeaderStartIndex    = index;
        }

        public int SearchForHeader(){
            string tigerId = Encoding.ASCII.GetString(RawBytes, 5, 9);
            if(tigerId == TigerId)
                return 14;

            tigerId = Encoding.ASCII.GetString(RawBytes, 0x40005, 9);
            if(tigerId == TigerId)
                return 0x4000E;

            return -1;
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
        public          int             X;
        public          int             Y;
        public          int             ByteStart;

        public          GameComImage    Image;

        public static   int             SizeInBytes => 16384;
        public          byte[]          RawBytes    => Image.RawBytes;

        public GameComBank(GameComImage image, int x, int y, int byteStart){
            Image       = image;
            X           = x;
            Y           = y;
            ByteStart   = byteStart;
        }

        public GameComBank(Bitmap image, int x, int y, int byteStart) {
            Image       = new GameComImage(image);
            X           = x;
            Y           = y;
            ByteStart   = byteStart;
        }

        public GameComBank(byte[] bytes, int x, int y, int byteStart) {
            Image       = new GameComImage(bytes);
            X           = x;
            Y           = y;
            ByteStart   = byteStart;
        }
    }
}
