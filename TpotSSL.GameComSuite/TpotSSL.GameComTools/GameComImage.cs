using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;

namespace TpotSSL.GameComTools {
    /// <summary>
    /// Game.com Image for graphics.
    /// </summary>
    public class GameComImage :IDisposable{
        /// <summary>
        /// Raw bytes of binary image.
        /// </summary>
        public byte[]       RawBytes;

        /// <summary>
        /// 32bpp Image representation.
        /// </summary>
        public Bitmap       Bitmap;

        /// <summary>
        /// Width in pixels.
        /// </summary>
        public int          Width           = 256;

        /// <summary>
        /// Height in pixels.
        /// </summary>
        public int          Height          = 256;

        public static int   DefaultFileSize = 16384;

        public static readonly Color White       = Color.FromArgb(255, 255, 255);
        public static readonly Color Gray        = Color.FromArgb(192, 192, 192);
        public static readonly Color DarkGray    = Color.FromArgb(128, 128, 128);
        public static readonly Color Black       = Color.FromArgb(0, 0, 0);

        /// <summary>
        /// Convert a standard 32bpp color into the closest match by brightness.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetCompatibleColor(Color color){
            //Get the brightness of the color, and convert it to a byte range.
            int brightness = (int)Math.Round(color.GetBrightness()*255);

            //Get the closest shade based on brightness.
            if(brightness <= 64)
                return Black;
            if(brightness <= 144)
                return DarkGray;
            if(brightness <= 192)
                return Gray;

            return White;
        }

        /// <summary>
        /// Default Game.com Color Palette
        /// </summary>
        public static List<Color> Palette = new List<Color>{White, Gray, DarkGray, Black};
        /// <summary>
        /// General purpose. Get a specified bit from a byte as boolean.
        /// </summary>
        /// <param name="sourceByte"></param>
        /// <param name="bitIndex"></param>
        /// <returns></returns>
        public static bool GetBit(byte sourceByte, byte bitIndex)                    => (sourceByte & (1 << bitIndex)) != 0;

        /// <summary>
        /// Get full byte from 4 pixels.
        /// </summary>
        /// <param name="pixel0">First Pixel</param>
        /// <param name="pixel1">Second Pixel </param>
        /// <param name="pixel2">Third Pixel</param>
        /// <param name="pixel3">Fourth Pixel</param>
        /// <returns></returns>
        public static byte GetByte(byte pixel0, byte pixel1, byte pixel2, byte pixel3)      => (byte)(pixel0 + (pixel1 << 2) + (pixel2 << 4) + (pixel3 << 6));

        /// <summary>
        /// Get full byte from 4 pixel colors.
        /// </summary>
        /// <param name="pixel0">First Pixel</param>
        /// <param name="pixel1">Second Pixel </param>
        /// <param name="pixel2">Third Pixel</param>
        /// <param name="pixel3">Fourth Pixel</param>
        /// <returns></returns>
        public static byte GetByte(Color pixel0, Color pixel1, Color pixel2, Color pixel3)  => (byte)((GetPixel(pixel3)) + (GetPixel(pixel2) << 2) + (GetPixel(pixel1) << 4) + (GetPixel(pixel0) << 6));
        public static byte GetPixel(Color col)                              => (byte)Palette.IndexOf(GetCompatibleColor(col));
        public static byte GetPixel(byte b, byte pIndex)                    => (byte)((GetBit(b, (byte)(pIndex*2)) ? 1 : 0) + (GetBit(b, (byte)(pIndex*2 + 1)) ? 2 : 0));
        public static Color GetColor(byte b, byte pIndex)                   => Palette[GetPixel(b, pIndex)];

        /// <summary>
        /// Creates a tranparent image from an alpha mask.
        /// </summary>
        public static Bitmap MergeAlpha(string image, string alpha) => MergeAlpha(new Bitmap(image), new Bitmap(alpha));

        /// <summary>
        /// Creates a tranparent image from an alpha mask.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Bitmap MergeAlpha(Bitmap image, Bitmap alpha){
            Bitmap bitmap = image.Clone() as Bitmap;
            for(int y = 0; y < image.Height; ++y){
                for(int x = 0; x < image.Width; ++x){
                    Color c = GetCompatibleColor(image.GetPixel(x, y));
                    if(c != White)
                        continue;

                    Color c2 = GetCompatibleColor(alpha.GetPixel(x, y));
                    if(c2 != White)
                        continue;

                    bitmap?.SetPixel(x, y, Color.Transparent);
                }
            }
            return bitmap;
        }

        /// <summary>
        /// Fighters Megamix Color Fix
        /// </summary>
        /// <param name="filename"></param>
        public static void FixColor(string filename){
            Color sColor = DarkGray;
            Color dColor = White;

            Bitmap      image       = new Bitmap(filename);
            BitmapData  imageData   = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

            unsafe{
                int i = 0;
                for(int y = 0; y < image.Height; y++){
                    byte* row = (byte*)imageData.Scan0 + (y*imageData.Stride);
                    for(int x = 0; x < image.Width; x ++){

                        byte    b1 = row[(x)*4];
                        Color   c = GetCompatibleColor(Color.FromArgb(b1, b1, b1));

                        if(c == sColor){
                            row[x*4]      = dColor.R;
                            row[x*4 + 1]  = dColor.R;
                            row[x*4 + 2]  = dColor.R;
                        }

                        i++;
                    }
                }
            }

            image.UnlockBits(imageData);

            string outputPath = Path.GetDirectoryName(filename) + "\\output";
            Directory.CreateDirectory(outputPath);

            image.Save(outputPath + "\\" + Path.GetFileName(filename));
        }
        /// <summary>
        /// Create a game.com image from a file. For both images and binary!
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="autoflip"></param>
        /// <returns></returns>
        public static GameComImage FromFile(string filename, bool autoflip = true){
            string ext = Path.GetExtension(filename)?.ToLower();

            switch(ext){
                // From Binary:
                case ".bin":
                    return new GameComImage(File.ReadAllBytes(filename), autoflip);

                // From Image:
                case ".png":
                case ".bmp":
                case ".gif":
                    return new GameComImage(new Bitmap(filename), autoflip);
            }
            return null;
        }

        /// <summary>
        /// Split a game.com image that contains multiple banks into individual bank objects.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static List<GameComBank> SplitBanks(GameComImage image) {
            List<GameComBank> list = new List<GameComBank>();

            // Todo: This implementation is kinda lazy.
            // It should /not/ be using LINQ.
            // In the future replace this with a proper rectangle copy.
            // Would be nice to have a constructor for this object that takes another instance, and rectangle coordinates or a Rectangle object.
            // Which should also use an unsafe context for copying the image data instead of creating an image clone.
            // Using LINQ might actually be slower than just using the image, and generating the bytes again too.
            // This implementation really is awful.
            int b = 0;
            for(int y = 0; y < image.Bitmap.Height; y += 256) {
                for(int x = 0; x < image.Bitmap.Width; x += 256) {
                    // Clone the image with a crop region.
                    Bitmap imageTemp = image.Bitmap.Clone(new Rectangle(x, y, 256, 256), PixelFormat.Format24bppRgb);

                    // Create the gcom bank and image objects in a really slow awful way.
                    // Do not do this.
                    // And lets hope that it's replaced by the next couple commits hot diggity damn.
                    list.Add(new GameComBank(new GameComImage(image.RawBytes.Skip(b).Take(GameComBank.SizeInBytes).ToArray(), imageTemp), x, y, b));
                    
                    // Keep track of what byte position we're at for the linq commands. 
                    // Yeah I know, I hate this as much as you do.
                    b += GameComBank.SizeInBytes;
                }

                // Force a garbage collection heap because ooh boy there's a lot to clean.
                // When rewriting this function, this won't be neccesary.
                GC.Collect();
            }

            // After the worlds worst use of query commands, return the list of banks.
            return list;
        }

        /// <summary>
        /// Split a game.com image that contains multiple banks into individual bank images.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static List<GameComImage> Split(GameComImage image){
            List<GameComImage>  list = new List<GameComImage>();
            int b                    = 0;

            // Todo: This implementation is kinda lazy.
            // It should /not/ be using LINQ.
            // In the future replace this with a proper rectangle copy.
            // Would be nice to have a constructor for this object that takes another instance, and rectangle coordinates or a Rectangle object.
            // Which should also use an unsafe context for copying the image data instead of creating an image clone.
            // Using LINQ might actually be slower than just using the image, and generating the bytes again too.
            // This implementation really is awful.
            for(int y = 0; y < image.Bitmap.Height; y += 256){
                for(int x = 0; x < image.Bitmap.Width; x += 256){
                    Bitmap imageTemp = image.Bitmap.Clone(new Rectangle(x, y, 256, 256), PixelFormat.Format24bppRgb);
                    list.Add(new GameComImage(image.RawBytes.Skip(b).Take(GameComBank.SizeInBytes).ToArray(), imageTemp));

                    b += GameComBank.SizeInBytes;
                    
                }
                GC.Collect();
            }

            return list;
        }

        /// <summary>
        /// Convert a gcom image from image to bin and vice versa. Works on png, bmp, and gif. Though the output is assumed as png.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="multiimage"></param>
        public static void Convert(string filename, bool multiimage = false){
            string ext = Path.GetExtension(filename)?.ToLower();

            switch(ext){
                case ".bin":
                    if(multiimage)
                        new GameComImage(File.ReadAllBytes(filename)).SaveImages(Path.ChangeExtension(filename, ".png"));
                    else
                        new GameComImage(File.ReadAllBytes(filename)).SaveImage(Path.ChangeExtension(filename, ".png"));
                    break;

                case ".png":
                case ".bmp":
                case ".gif":
                    new GameComImage(new Bitmap(filename)).SaveBinary(Path.ChangeExtension(filename, ".BIN"));
                    break;
            }
        }

        /// <summary>
        /// Create game.com image from bitmap.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="autoflip"></param>
        public GameComImage(Bitmap image, bool autoflip = true) {

            // Flip and rotate the image so it matches the gcom's weird way of storing image data.
            image.RotateFlip(RotateFlipType.Rotate270FlipY);

            Width                   = image.Width;
            Height                  = image.Height;

            // Lock the image for reading.
            BitmapData imageData    = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            // Create raw byte array.
            byte[] bytes = new byte[(Width*Height)/4];
            unsafe{
                int i = 0;

                // Cycle through the image and grab the color data.
                // Keep in mind gcom graphics are 2bpp, so 4 pixels are stored in a single byte.
                for(int xx = 0; xx <= Width - 256; xx += 256){
                    for(int y = 0; y < Height; ++y){
                        byte* row = (byte*)imageData.Scan0 + (y*imageData.Stride);
                        for(int x = 0; x < 256; x += 4){
                            // Get the bytes for the first color channel for the next 4 pixels.
                            byte    b1 = row[(x + xx)     * 4];
                            byte    b2 = row[(x + xx + 1) * 4];
                            byte    b3 = row[(x + xx + 2) * 4];
                            byte    b4 = row[(x + xx + 3) * 4];

                            // Create colors from those bytes, using the same value for each color channel.
                            Color   c1 = Color.FromArgb(b1, b1, b1);
                            Color   c2 = Color.FromArgb(b2, b2, b2);
                            Color   c3 = Color.FromArgb(b3, b3, b3);
                            Color   c4 = Color.FromArgb(b4, b4, b4);

                            // Get byte value from 4 pixels.
                            bytes[i] = GetByte(c1, c2, c3, c4);
                            i++;
                        }
                    }
                }
            }
            //Finalize the image.
            image.UnlockBits(imageData);

            // If the resulting image should keep it's orientation, flip it back.
            // This might actually cause problems though.
            // Because the image is being flipped after the fact, and the bytes are unaffected.
            // Should probably look into this and test it. 
            // TODO
            if(autoflip == false)
                image.RotateFlip(RotateFlipType.Rotate270FlipY);

            RawBytes    = bytes;
            Bitmap      = image;
        }

        private GameComImage(byte[] bytes, Bitmap image){
            RawBytes    = bytes;
            Bitmap      = image;
        }

        /// <summary>
        /// Create a game.com image from a byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="autoflip"></param>
        public GameComImage(byte[] bytes, bool autoflip = true) {

            // Determine the width and height of the image when it's bigger than a single bank.
            if(bytes.Length > DefaultFileSize){
                Height = 256*(bytes.Length/DefaultFileSize);
                while(Height > 8192){
                    Height  /= 2;
                    Width   *= 2;
                }
            }

            // Create Image object,
            Bitmap      image        = new Bitmap(Width, Height);

            //Lock the image for writing.
            BitmapData  imageData    = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

            unsafe{
                int i = 0;
                // Go through the image pixel by pixel, 4 pixels at a time.
                for(int xx = 0; xx <= Width - 256; xx += 256){
                    for(int y = 0; y < Height; y++){
                        // Get the byte index of the current row of pixels.
                        byte* row = (byte*)imageData.Scan0 + (y*imageData.Stride);
                        for(int x = 0; x < 256; x += 4){
                            // Get the nearest colors for each pixel.
                            // Don't forget that 4 game.com pixels are stored in a single byte.
                            Color c1 = GetColor(bytes[i], 3);
                            Color c2 = GetColor(bytes[i], 2);
                            Color c3 = GetColor(bytes[i], 1);
                            Color c4 = GetColor(bytes[i], 0);

                            // Set all the pixels in BGRA format.
                            // We use red for each channel because the fact that we're using grayscale, it doesn't matter, and assures we get a gray under any circumstances.

                            // First pixel 
                            row[(x + xx)*4]         = c1.R;
                            row[(x + xx)*4 + 1]     = c1.R;
                            row[(x + xx)*4 + 2]     = c1.R;
                            row[(x + xx)*4 + 3]     = 255;

                            // Second pixel
                            row[(x + xx + 1)*4]     = c2.R;
                            row[(x + xx + 1)*4 + 1] = c2.R;
                            row[(x + xx + 1)*4 + 2] = c2.R;
                            row[(x + xx + 1)*4 + 3] = 255;

                            //Third pixel
                            row[(x + xx + 2)*4]     = c3.R;
                            row[(x + xx + 2)*4 + 1] = c3.R;
                            row[(x + xx + 2)*4 + 2] = c3.R;
                            row[(x + xx + 2)*4 + 3] = 255;

                            //Forth pixel
                            row[(x + xx + 3)*4]     = c4.R;
                            row[(x + xx + 3)*4 + 1] = c4.R;
                            row[(x + xx + 3)*4 + 2] = c4.R;
                            row[(x + xx + 3)*4 + 3] = 255;
                            i++;
                        }
                    }
                }
            }

            // Finalize the image.
            image.UnlockBits(imageData);

            // Game.com images are stored in an awkward way, flipping and rotating them upright makes it easier to work with.
            if(autoflip)
                image.RotateFlip(RotateFlipType.Rotate270FlipY);

            //Put height in a temporary variable.
            int t       = Height;
            //Swap width and height.
            Height      = Width;
            Width       = t;

            //Apply variables.
            RawBytes    = bytes;
            Bitmap      = image;
        }

        /// <summary>
        /// Save as multiple images when the image includes multiple banks.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveImages(string filename){
            int i           = 0;
            string ext      = Path.GetExtension(filename);
            string fname    = Path.GetFileNameWithoutExtension(filename);

            // Go through the current image in 256x256 chunks.
            for(int y = 0; y < Bitmap.Height; y += 256){
                for(int x = 0; x < Bitmap.Width; x += 256){

                    // Clone cropped instances.
                    Bitmap image = Bitmap.Clone(new Rectangle(x, y, 256, 256), PixelFormat.Format32bppArgb);

                    // Save Image file
                    image.Save(fname + i + ext);

                    // Delete image
                    image.Dispose();
                    image = null;
                   
                    i++;
                }
                GC.Collect();
            }
        }
        /// <summary>
        /// Save as Image.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveImage(string filename)  => Bitmap.Save(filename);

        /// <summary>
        /// Save as game.com binary.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveBinary(string filename) => File.WriteAllBytes(filename, RawBytes);

        public void Dispose(){
            Bitmap      = null;
            RawBytes    = null;

            GC.SuppressFinalize(this);
        }
    }
}
