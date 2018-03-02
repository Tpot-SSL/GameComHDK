﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;

namespace TpotSSL.GameComTools {
  

    public class GameComImage :IDisposable{
        public byte[] RawBytes;
        public Bitmap Image;
        public int Width = 256;
        public int Height = 256;
        public static int DefaultFileSize = 16384;

        public static Color White => Color.FromArgb(255, 255, 255);
        public static Color Gray => Color.FromArgb(192, 192, 192);
        public static Color DarkGray => Color.FromArgb(128, 128, 128);
        public static Color Black => Color.FromArgb(0, 0, 0);

        public static Color GetCompatibleColor(Color color){
            var b = (int)Math.Round(color.GetBrightness()*255);
            if(b <= 64)
                return Black;
            if(b <= 144)
                return DarkGray;
            if(b <= 192)
                return Gray;

            return White;
        }

        public static List<Color> Palette = new List<Color>{White, Gray, DarkGray, Black};

        public static bool GetBit(byte b, byte bitIndex) => (b & (1 << bitIndex)) != 0;
        public static byte GetByte(byte p1, byte p2, byte p3, byte p4) => (byte)(p1 + (p2 << 2) + (p3 << 4) + (p4 << 6));
        public static byte GetByte(Color p1, Color p2, Color p3, Color p4) => (byte)((GetPixel(p4)) + (GetPixel(p3) << 2) + (GetPixel(p2) << 4) + (GetPixel(p1) << 6));
        public static byte GetPixel(Color col) => (byte)Palette.IndexOf(GetCompatibleColor(col));
        public static byte GetPixel(byte b, byte pIndex) => (byte)((GetBit(b, (byte)(pIndex*2)) ? 1 : 0) + (GetBit(b, (byte)(pIndex*2 + 1)) ? 2 : 0));
        public static Color GetColor(byte b, byte pIndex) => Palette[GetPixel(b, pIndex)];

        public static Bitmap MergeAlpha(string image, string alpha) => MergeAlpha(new Bitmap(image), new Bitmap(alpha));

        public static Bitmap MergeAlpha(Bitmap image, Bitmap alpha){
            Bitmap bitmap = image.Clone() as Bitmap;
            for(int y = 0; y < image.Height; y += 1){
                for(int x = 0; x < image.Width; x += 1){
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

        public static void FixColor(string filename, GameComGame game){
            var sColor = DarkGray;
            var dColor = White;

            var image = new Bitmap(filename);
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

            unsafe{
                int i = 0;
                for(int y = 0; y < image.Height; y++){
                    byte* row = (byte*)imageData.Scan0 + (y*imageData.Stride);
                    for(int x = 0; x < image.Width; x ++){

                        var b1 = row[(x)*4];
                        var c = GetCompatibleColor(Color.FromArgb(b1, b1, b1));
                        if(c == sColor){
                            row[(x)*4] = dColor.R;
                            row[(x)*4 + 1] = dColor.R;
                            row[(x)*4 + 2] = dColor.R;
                        }
                        i++;
                    }
                }

            }

            image.UnlockBits(imageData);
            var output = Path.GetDirectoryName(filename) + "\\output";
            Directory.CreateDirectory(output);
            image.Save(output + "\\" + Path.GetFileName(filename));
        }

        public static GameComImage FromFile(string filename, bool autoflip = true){
            string ext = Path.GetExtension(filename)?.ToLower();

            switch(ext){
                case ".bin":
                    return new GameComImage(File.ReadAllBytes(filename), autoflip);

                case ".png":
                case ".bmp":
                case ".gif":
                    return new GameComImage(new Bitmap(filename), autoflip);
            }
            return null;
        }

        public static List<GameComBank> SplitBanks(GameComImage image) {
            List<GameComBank> list = new List<GameComBank>();
            int b = 0;
            for(int y = 0; y < image.Image.Height; y += 256) {
                for(int x = 0; x < image.Image.Width; x += 256) {
                    var imageTemp = image.Image.Clone(new Rectangle(x, y, 256, 256), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    list.Add(new GameComBank(new GameComImage(image.RawBytes.Skip(b).Take(GameComBank.SizeInBytes).ToArray(), imageTemp), x, y, b));
                    b += GameComBank.SizeInBytes;
                    GC.Collect();
                }
            }

            return list;
        }

        public static List<GameComImage> Split(GameComImage image){
            List<GameComImage> list = new List<GameComImage>();
            int b = 0;
            for(int y = 0; y < image.Image.Height; y += 256){
                for(int x = 0; x < image.Image.Width; x += 256){
                    var imageTemp = image.Image.Clone(new Rectangle(x, y, 256, 256), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    list.Add(new GameComImage(image.RawBytes.Skip(b).Take(GameComBank.SizeInBytes).ToArray(), imageTemp));
                    b += GameComBank.SizeInBytes;
                    GC.Collect();
                }
            }

            return list;
        }

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

        public GameComImage(Bitmap image, bool autoflip = true) {
                image.RotateFlip(RotateFlipType.Rotate270FlipY);

            Width = image.Width;
            Height = image.Height;
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            byte[] bytes = new byte[(Width*Height)/4];
            unsafe{
                int i = 0;
                for(int xx = 0; xx <= Width - 256; xx += 256){
                    for(int y = 0; y < Height; y++){
                        byte* row = (byte*)imageData.Scan0 + (y*imageData.Stride);
                        for(int x = 0; x < 256; x += 4){

                            var b1 = row[(x + xx)*4];
                            var b2 = row[(x + xx + 1)*4];
                            var b3 = row[(x + xx + 2)*4];
                            var b4 = row[(x + xx + 3)*4];
                            var c1 = Color.FromArgb(b1, b1, b1);
                            var c2 = Color.FromArgb(b2, b2, b2);
                            var c3 = Color.FromArgb(b3, b3, b3);
                            var c4 = Color.FromArgb(b4, b4, b4);
                            bytes[i] = GetByte(c1, c2, c3, c4);
                            i++;
                        }
                    }
                }
            }
            image.UnlockBits(imageData);
            if(autoflip == false)
                image.RotateFlip(RotateFlipType.Rotate270FlipY);

            RawBytes = bytes;
            Image = image;
        }

        public GameComImage(byte[] bytes, Bitmap image){
            RawBytes = bytes;
            Image = image;
        }

        public GameComImage(byte[] bytes, bool autoflip = true) {
            if(bytes.Length > DefaultFileSize){
                Height = 256*(bytes.Length/DefaultFileSize);
                while(Height > 8192){
                    Height /= 2;
                    Width *= 2;
                }
            }

            Bitmap image = new Bitmap(Width, Height);
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            unsafe{
                int i = 0;
                for(int xx = 0; xx <= Width - 256; xx += 256){
                    for(int y = 0; y < Height; y++){
                        byte* row = (byte*)imageData.Scan0 + (y*imageData.Stride);
                        for(int x = 0; x < 256; x += 4){
                            var c1 = GetColor(bytes[i], 3);
                            var c2 = GetColor(bytes[i], 2);
                            var c3 = GetColor(bytes[i], 1);
                            var c4 = GetColor(bytes[i], 0);

                            row[(x + xx)*4] = c1.R;
                            row[(x + xx)*4 + 1] = c1.R;
                            row[(x + xx)*4 + 2] = c1.R;
                            row[(x + xx)*4 + 3] = 255;


                            row[(x + xx + 1)*4] = c2.R;
                            row[(x + xx + 1)*4 + 1] = c2.R;
                            row[(x + xx + 1)*4 + 2] = c2.R;
                            row[(x + xx + 1)*4 + 3] = 255;


                            row[(x + xx + 2)*4] = c3.R;
                            row[(x + xx + 2)*4 + 1] = c3.R;
                            row[(x + xx + 2)*4 + 2] = c3.R;
                            row[(x + xx + 2)*4 + 3] = 255;


                            row[(x + xx + 3)*4] = c4.R;
                            row[(x + xx + 3)*4 + 1] = c4.R;
                            row[(x + xx + 3)*4 + 2] = c4.R;
                            row[(x + xx + 3)*4 + 3] = 255;
                            i++;
                        }
                    }
                }
            }
            image.UnlockBits(imageData);
            if(autoflip)
                image.RotateFlip(RotateFlipType.Rotate270FlipY);

            var t = Height;
            Height = Width;
            Width = t;
            RawBytes = bytes;
            Image = image;
        }

        public void SaveImages(string filename){
            int i = 0;
            string ext = Path.GetExtension(filename);
            string fname = Path.GetFileNameWithoutExtension(filename);
            for(int y = 0; y < Image.Height; y += 256){
                for(int x = 0; x < Image.Width; x += 256){
                    var image = Image.Clone(new Rectangle(x, y, 256, 256), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    image.Save(fname + i + ext);
                    image.Dispose();
                    image = null;
                    GC.Collect();
                    i++;
                }
            }
        }

        public void SaveImage(string filename) => Image.Save(filename);
        public void SaveBinary(string filename) => File.WriteAllBytes(filename, RawBytes);
        public void Dispose(){
            Image = null;
            RawBytes = null;

            GC.SuppressFinalize(this);
        }
    }
}