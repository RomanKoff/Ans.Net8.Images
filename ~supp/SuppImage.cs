using Ans.Net8.Common;
using SixLabors.ImageSharp.Formats;

namespace Ans.Net8.Images
{

    public static class SuppImage
    {

        /*
		 * ImageFileInfo GetFileInfo(string path, string extension);
		 * ImageFileInfo GetFileInfo(FileInfo file);
		 * 
         * void ResizeInside(string sourcefile, string resultfile, int width, int height, IImageEncoder encoder);
		 * void ResizeAround(string sourcefile, string resultfile, int width, int height, IImageEncoder encoder);
		 * void ResizeAverage(string sourcefile, string resultfile, int side, IImageEncoder encoder);
		 * void ResizeAroundAndCrop(string sourcefile, string resultfile, int width, int height, int cropX, int cropY, int cropWidth, int cropHeight, IImageEncoder encoder);
		 * void ResizeAroundAndCrop(string sourcefile, string resultfile, int width, int height, int cropWidth, int cropHeight, float divVertical, float divHorizontal, IImageEncoder encoder);
         */


        /* functions */


        public static ImageFileInfo GetFileInfo(
            string path,
            string extension)
        {
            var info1 = new ImageFileInfo();
            try
            {
                var image1 = Image.Identify(path);
                info1.Width = image1.Width;
                info1.Height = image1.Height;
                info1.Format = image1.Metadata.DecodedImageFormat.Name.ToLower();
                info1.Mimetype = image1.Metadata.DecodedImageFormat.DefaultMimeType;
                info1.IsImage = true;
                info1.IsJpeg = info1.Format == "jpeg";
                info1.Resizer = new Resizer(image1.Width, image1.Height);
            }
            catch (Exception)
            {
                var ci1 = SuppIO.GetContentInfo(extension);
                if (ci1.IsWebImage)
                {
                    info1.Mimetype = _Consts.CONTENTINFO_BIN.ContentType;
                    info1.IsInvalidImage = true;
                }
                else
                {
                    info1.Mimetype = ci1.ContentType;
                }
                return info1;
            }
            return info1;
        }


        public static ImageFileInfo GetFileInfo(
            FileInfo file)
        {
            return GetFileInfo(file.FullName, file.Extension);
        }


        /* methods */


        public static void ResizeInside(
            string sourcefile,
            string resultfile,
            int width,
            int height,
            IImageEncoder encoder)
        {
            using var image1 = Image.Load(sourcefile);
            image1.SaveResizeInside(
                resultfile, width, height, encoder);
        }


        public static void ResizeAround(
            string sourcefile,
            string resultfile,
            int width,
            int height,
            IImageEncoder encoder)
        {
            using var image1 = Image.Load(sourcefile);
            image1.SaveResizeAround(
                resultfile, width, height, encoder);
        }


        public static void ResizeAverage(
            string sourcefile,
            string resultfile,
            int side,
            IImageEncoder encoder)
        {
            using var image1 = Image.Load(sourcefile);
            image1.SaveResizeAverage(
                resultfile, side, encoder);
        }


        public static void ResizeAroundAndCrop(
            string sourcefile,
            string resultfile,
            int width,
            int height,
            int cropX,
            int cropY,
            int cropWidth,
            int cropHeight,
            IImageEncoder encoder)
        {
            using var image1 = Image.Load(sourcefile);
            image1.SaveResizeAroundAndCrop(
                resultfile,
                width, height,
                cropX, cropY,
                cropWidth, cropHeight,
                encoder);
        }


        public static void ResizeAroundAndCrop(
            string sourcefile,
            string resultfile,
            int width,
            int height,
            int cropWidth,
            int cropHeight,
            float divVertical,
            float divHorizontal,
            IImageEncoder encoder)
        {
            using var image1 = Image.Load(sourcefile);
            image1.SaveResizeAroundAndCrop(
                resultfile,
                width, height,
                cropWidth, cropHeight,
                divVertical, divHorizontal,
                encoder);
        }

    }

}
