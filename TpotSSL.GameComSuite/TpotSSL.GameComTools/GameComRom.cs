using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;

namespace TpotSSL.GameComTools {

    /// <summary>
    /// Known Game.com software releases.
    /// </summary>
    public enum GameComGame {
        /// <summary> Fighters MegaMix [Id: 36646]</summary>
        FightersMegamix,
        /// <summary> Sonic JAM [Id: 37682]</summary>
        SonicJam,
        /// <summary> Frogger [Id: 38680]</summary>
        Frogger,
        /// <summary> Centipede [Id: 40481]</summary>
        Centipede,
        /// <summary> Resident Evil 2 [Id: 61498]</summary>
        ResidentEvil2,
        /// <summary> Henry [Id: 40498]</summary>
        Henry,
        /// <summary> Game.com Internet [Id: 45843]</summary>
        GameComInternet,
        /// <summary> Tiger Web Link [Id: 42240]</summary>
        TigerWebLink,
        /// <summary> Mortal Kombat Trilogy [Id: 47124]</summary>
        MortalKombat,
        /// <summary> Jeopardy [Id: 12657]</summary>
        Jeopardy,
        /// <summary> Indy 500  [Id: 50770]</summary>
        Indy500,
        /// <summary> Batman And Robin [Id: 40497]</summary>
        BatmanAndRobin,
        /// <summary> Duke Nukem 3D [Id: 58155]</summary>
        DukeNukem3D,
        /// <summary> Monopoly [Id: 37637]</summary>
        Monopoly,
        /// <summary> Wheel Of Fortune 1 and 2 [Id: 49732]</summary>
        WheelOfFortune,
        /// <summary> William's Arcade Classics [Id: 35349]</summary>
        WilliamsArcade,
        /// <summary> Quiz Wiz [Id: 53585]</summary>
        QuizWiz,
        /// <summary> Jurassic Park [Id: 59714]</summary>
        JurassicPark,
        /// <summary> Scrabble [Id: 57364]</summary>
        Scrabble,
        /// <summary> Tiger Casino [Id: 58385]</summary>
        TigerCasino
    }
  
    /// <summary>
    /// Game.com ROM file and info.
    /// </summary>
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

        /// <summary>
        /// 16KB Memory bank objects.
        /// </summary>
        public List<GameComBank>    MemoryBanks;

        /// <summary>
        /// Rom size in bytes.
        /// </summary>
        public          int         SizeInBytes     => RawBytes.Length;
        /// <summary>
        /// Array of raw byte data.
        /// </summary>
        public          byte[]      RawBytes        => FullImage.RawBytes;

        /// <summary>
        /// Game.com rom magic number.
        /// </summary>
        public const    string      TigerId         = "TigerDMGC";

        /// <summary>
        /// Known game.com software by id.
        /// </summary>
        public static Dictionary<ushort, GameComGame> KnownGamesById = new Dictionary<ushort, GameComGame>(){
            {36646, GameComGame.FightersMegamix },  {37682, GameComGame.SonicJam },     {38680, GameComGame.Frogger },
            {45843, GameComGame.GameComInternet },  {50770, GameComGame.Indy500 },      {47124, GameComGame.MortalKombat },
            {49732, GameComGame.WheelOfFortune },   {37637, GameComGame.Monopoly },     {40481, GameComGame.Centipede },
            {35349, GameComGame.WilliamsArcade },   {57364, GameComGame.Scrabble },     {40498, GameComGame.Henry },
            {59714, GameComGame.JurassicPark },     {58155, GameComGame.DukeNukem3D },  {61498, GameComGame.ResidentEvil2 },
            {40497, GameComGame.BatmanAndRobin },   {42240, GameComGame.TigerWebLink }, {12657, GameComGame.Jeopardy },
            {58385, GameComGame.TigerCasino },      {53585, GameComGame.QuizWiz },
        };

        /// <summary>
        /// Get name of game from known games.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
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

            // Convert the game name into an ascii bytes array.
            // Todo: Some error checking. The name can currently be set with non-ascii characters. A problem waiting to happen.
            byte[] nameBytes = Encoding.ASCII.GetBytes(GameName);

            // Apply the newly converted bytes to the byte array
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
            // Grab the bank object for convenience.
            GameComBank bank = MemoryBanks[bankIndex];

            // Draw the new bank to the full rom image.
            using(Graphics g = Graphics.FromImage(FullImage.Bitmap))
                g.DrawImage(image.Bitmap, bank.X, bank.Y);
            
            // Then apply the changes to the bytes.
            for(int i = 0; i < image.RawBytes.Length; i++)
                FullImage.RawBytes[bank.ByteStart + i] = image.RawBytes[i];
        }

        /// <summary>
        /// Create rom object from file.
        /// </summary>
        /// <param name="filePath"></param>
        public GameComRom(string filePath){
            // Create an image out of the entire rom, for the HDK.
            FullImage       = new GameComImage(File.ReadAllBytes(filePath));

            // Then split the large image up into 256x256 pixel banks.
            MemoryBanks     = GameComImage.SplitBanks(FullImage);

            // Search for the header location, and grab header data!
            GrabHeaderData(SearchForHeader());
            FilePath = filePath;
        }

        /// <summary>
        /// Move icon position and refresh.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Bitmap RefreshIcon(byte x, byte y) {
            IconX       = x;
            IconY       = y;

            return RefreshIcon();
        }

        /// <summary>
        /// Change Icon bank and refresh.
        /// </summary>
        /// <param name="bankNo"></param>
        /// <returns></returns>
        public Bitmap RefreshIcon(byte bankNo) {
            IconBankNo  = bankNo;

            return RefreshIcon();
        }

        /// <summary>
        /// Move icon, change bank, and refresh.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bankNo"></param>
        /// <returns></returns>
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
        /// <param name="index">Location of header in bytes</param>
        public void GrabHeaderData(int index){
            // If location of the header given is invalid, then exit.
            // Maybe the header couldn't be found, or it isn't a valid rom.
            if(index < 0)
                return;

            // Set icon data from the bytes in the header.
            IconBankNo              = RawBytes[index];
            IconX                   = RawBytes[index + 1];
            IconY                   = RawBytes[index + 2];

            // Set the game's name to bytes from the header converted into an ascii string.
            // Please note that gamecom game's have a name that is always exactly 9 characters in length.
            GameName                = Encoding.ASCII.GetString(RawBytes, index+3, 9);

            // Refresh icon image.
            RefreshIcon();

            // Set the game id from header bytes. (The value is an unsigned short, so it has to be converted from two bytes.
            GameId                  = BitConverter.ToUInt16(RawBytes, index + 13);

            // We found the header, and all is good, so clearly this rom is valid.
            IsValidRom              = true;

            // Keep the position of the header for future reference.
            HeaderStartIndex        = index;
        }

        /// <summary>
        /// Find ROM Header data and return the location.
        /// </summary>
        /// <returns>Location of header.</returns>
        public int SearchForHeader(){
            // Grab the magic number from the expected location
            string tigerId = Encoding.ASCII.GetString(RawBytes, 5, 9);

            // If it was found return the location of the header.
            if(tigerId == TigerId)
                return 14;

            // If not, try at another common location.
            tigerId = Encoding.ASCII.GetString(RawBytes, 0x40005, 9);

            // If it was found return the header location, if not return -1.
            return tigerId == TigerId ? 0x4000E : -1;
        }
        /// <summary>
        /// Dispose this instance.
        /// </summary>
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

        /// <summary>
        /// Size of bank in bytes.
        /// </summary>
        public const    int             SizeInBytes = 16384;

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
