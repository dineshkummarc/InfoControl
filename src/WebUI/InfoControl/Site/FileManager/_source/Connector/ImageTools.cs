/*
 * CKFinder
 * ========
 * http://www.ckfinder.com
 * Copyright (C) 2007-2008 Frederico Caldeira Knabben (FredCK.com)
 *
 * The software, this file and its contents are subject to the CKFinder
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of CKFinder.
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security;
using ImageManipulation;

namespace CKFinder.Connector
{
    internal class ImageTools
    {
        private ImageTools() {}

        public static bool IsImageExtension(string extension)
        {
            switch (extension.ToLower())
            {
                case "jpg":
                case "jpeg":
                case "gif":
                case "png":
                case "bmp":
                    return true;
            }
            return false;
        }

        public static bool ValidateImage(string filePath)
        {
            Image sourceImage;

            try
            {
                sourceImage = Image.FromFile(filePath);
                sourceImage.Dispose();
                return true;
            }
            catch
            {
                // This is not a valid image. Do nothing.
                return false;
            }
        }

        public static bool ResizeImage(string sourceFile, string targetFile, int maxWidth, int maxHeight, bool preserverAspectRatio, int quality)
        {
            Image sourceImage;

            try
            {
                sourceImage = Image.FromFile(sourceFile);
            }
            catch (OutOfMemoryException)
            {
                // This is not a valid image. Do nothing.
                return false;
            }

            // If 0 is passed in any of the max sizes it means that that size must be ignored,
            // so the original image size is used.
            maxWidth = maxWidth == 0
                           ? sourceImage.Width
                           : maxWidth;
            maxHeight = maxHeight == 0
                            ? sourceImage.Height
                            : maxHeight;

            if (sourceImage.Width <= maxWidth && sourceImage.Height <= maxHeight)
            {
                sourceImage.Dispose();

                if (sourceFile != targetFile)
                    File.Copy(sourceFile, targetFile);

                return true;
            }

            Size oSize;
            if (preserverAspectRatio)
            {
                // Gets the best size for aspect ratio resampling
                oSize = GetAspectRatioSize(maxWidth, maxHeight, sourceImage.Width, sourceImage.Height);
            }
            else
                oSize = new Size(maxWidth, maxHeight);

            Image oResampled;

            if (sourceImage.PixelFormat == PixelFormat.Indexed || sourceImage.PixelFormat == PixelFormat.Format1bppIndexed || sourceImage.PixelFormat == PixelFormat.Format4bppIndexed || sourceImage.PixelFormat == PixelFormat.Format8bppIndexed)
                oResampled = new Bitmap(oSize.Width, oSize.Height, PixelFormat.Format24bppRgb);
            else
                oResampled = new Bitmap(oSize.Width, oSize.Height, sourceImage.PixelFormat);

            // Creates a Graphics for the oResampled image
            Graphics oGraphics = Graphics.FromImage(oResampled);

            // The Rectangle that holds the Resampled image size
            Rectangle oRectangle;

            // High quality resizing
            if (quality > 80)
            {
                oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // If HighQualityBicubic is used, bigger Rectangle is required to remove the white border
                oRectangle = new Rectangle(-1, -1, oSize.Width + 1, oSize.Height + 1);
            }
            else
                oRectangle = new Rectangle(0, 0, oSize.Width, oSize.Height);

            // Place a white background (for transparent images).
            oGraphics.FillRectangle(new SolidBrush(Color.White), oRectangle);

            // Draws over the oResampled image the resampled Image
            oGraphics.DrawImage(sourceImage, oRectangle);

            sourceImage.Dispose();

            String extension = Path.GetExtension(targetFile).ToLower();

            if (extension == ".jpg" || extension == ".jpeg")
            {
                ImageCodecInfo oCodec = GetJpgCodec();

                if (oCodec != null)
                {
                    var aCodecParams = new EncoderParameters(1);
                    aCodecParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                    oResampled.Save(targetFile, oCodec, aCodecParams);
                }
                else
                    oResampled.Save(targetFile);
            }
            else
            {
                switch (extension)
                {
                    case ".gif":
                        try
                        {
                            // Use a proper palette
                            var quantizer = new OctreeQuantizer(255, 8);
                            using (Bitmap quantized = quantizer.Quantize(oResampled))
                            {
                                quantized.Save(targetFile, ImageFormat.Gif);
                            }
                        }
                        catch (SecurityException)
                        {
                            // The calls to Marshal might fail in Medium trust, save the image using the default palette
                            oResampled.Save(targetFile, ImageFormat.Png);
                        }
                        break;

                    case ".png":
                        oResampled.Save(targetFile, ImageFormat.Png);
                        break;

                    case ".bmp":
                        oResampled.Save(targetFile, ImageFormat.Bmp);
                        break;
                }
            }
            oGraphics.Dispose();
            oResampled.Dispose();

            return true;
        }

        private static ImageCodecInfo GetJpgCodec()
        {
            ImageCodecInfo[] aCodecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo oCodec = null;

            for (int i = 0; i < aCodecs.Length; i++)
            {
                if (aCodecs[i].MimeType.Equals("image/jpeg"))
                {
                    oCodec = aCodecs[i];
                    break;
                }
            }

            return oCodec;
        }

        private static Size GetAspectRatioSize(int maxWidth, int maxHeight, int actualWidth, int actualHeight)
        {
            // Creates the Size object to be returned
            var oSize = new Size(maxWidth, maxHeight);

            // Calculates the X and Y resize factors
            float iFactorX = maxWidth/(float) actualWidth;
            float iFactorY = maxHeight/(float) actualHeight;

            // If some dimension have to be scaled
            if (iFactorX != 1 || iFactorY != 1)
            {
                // Uses the lower Factor to scale the opposite size
                if (iFactorX < iFactorY)
                {
                    oSize.Height = (int) Math.Round(actualHeight*iFactorX);
                }
                else if (iFactorX > iFactorY)
                {
                    oSize.Width = (int) Math.Round(actualWidth*iFactorY);
                }
            }

            if (oSize.Height <= 0) oSize.Height = 1;
            if (oSize.Width <= 0) oSize.Width = 1;

            // Returns the Size
            return oSize;
        }
    }
}