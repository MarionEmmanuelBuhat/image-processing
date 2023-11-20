﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace image_processing {
    static public class ImageProcessing {
        static public void displayError() {
            MessageBox.Show("Base image does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public Bitmap basicCopy(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, pixel);
                }
            }
            return processedImage;
        }

        static public Bitmap greyScale(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    processedImage.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            }
            return processedImage;
        }

        static public Bitmap colorInversion(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, Color.FromArgb(
                        255 - pixel.R, 
                        255 - pixel.G, 
                        255 - pixel.B));
                }
            }
            return processedImage;
        }

        static public Bitmap histogram(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap greyImage = greyScale(baseImage);

            Color pixel;
            int[] histogramData = new int[256];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    pixel = greyImage.GetPixel(x, y);
                    histogramData[pixel.R]++;
                }
            }

            // set the bg to white
            Bitmap processedImage = new Bitmap(256, height);
            for (int x = 0; x < 256; x++) {
                for (int y = 0; y < height; y++) {
                    processedImage.SetPixel(x, y, Color.White);
                }
            }
            // plot the data
            for (int x = 0; x < 256; x++) {
                for (int y = 0; y < Math.Min(histogramData[x] / 5, height); y++) {
                    processedImage.SetPixel(x, height - 1 - y, Color.Black);
                }
            }
            return processedImage;
        }

        static public Bitmap sepia(Bitmap baseImage) {
            int width = baseImage.Width;
            int height = baseImage.Height;
            Bitmap processedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    int R = pixel.R;
                    int G = pixel.G;
                    int B = pixel.B;
                    int _R = Math.Min(255, (int)(0.393 * R + 0.769 * G + 0.189 * B));
                    int _G = Math.Min(255, (int)(0.349 * R + 0.686 * G + 0.168 * B));
                    int _B = Math.Min(255, (int)(0.272 * R + 0.534 * G + 0.131 * B));
                    processedImage.SetPixel(x, y, Color.FromArgb(_R, _G, _B));
                }
            }
            return processedImage;
        }
    }
}