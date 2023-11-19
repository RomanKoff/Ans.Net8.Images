using SixLabors.ImageSharp.Formats;

namespace Ans.Net8.Images
{

	public static partial class _e
	{

		/*
         * Resizer GetResizer(this Image image);
         * Image GetResizeInside(this Image image, int width, int height);
         * Image GetResizeAround(this Image image, int width, int height);
         * Image GetResizeAverage(this Image image, int side);
         * Image GetResizeAndCrop(this Image image, int width, int height, int cropX, int cropY, int cropWidth, int cropHeight);
         * Image GetResizeAroundAndCrop(this Image image, int width, int height, int cropX, int cropY, int cropWidth, int cropHeight);
         * Image GetResizeAroundAndCrop(this Image image, int width, int height, int cropWidth, int cropHeight, float divVertical, float divHorizontal);
         * 
         * void SaveResizeInside(this Image image, string filename, int width, int height, IImageEncoder encoder);
         * void SaveResizeAround(this Image image, string filename, int width, int height, IImageEncoder encoder);
         * void SaveResizeAverage(this Image image, string filename, int side, IImageEncoder encoder);
         * void SaveResizeAroundAndCrop(this Image image, string filename, int width, int height, int cropX, int cropY, int cropWidth, int cropHeight, IImageEncoder encoder);
         * void SaveResizeAroundAndCrop(this Image image, string filename, int width, int height, int cropWidth, int cropHeight, float divVertical, float divHorizontal, IImageEncoder encoder);
         */


		public static Resizer GetResizer(
			this Image image)
		{
			return new Resizer(image.Width, image.Height);
		}


		public static Image GetResizeInside(
			this Image image,
			int width,
			int height)
		{
			var r1 = image.GetResizer();
			r1.ScaleInside(width, height);
			return image.Clone(
				x => x.Resize(r1.NewWidth, r1.NewHeight));
		}


		public static Image GetResizeAround(
			this Image image,
			int width,
			int height)
		{
			var r1 = image.GetResizer();
			r1.ScaleAround(width, height);
			return image.Clone(
				x => x.Resize(r1.NewWidth, r1.NewHeight));
		}


		public static Image GetResizeAverage(
			this Image image,
			int side)
		{
			var r1 = image.GetResizer();
			r1.ScaleAverage(side);
			return image.Clone(
				x => x.Resize(r1.NewWidth, r1.NewHeight));
		}


		public static Image GetResizeAndCrop(
			this Image image,
			int width,
			int height,
			int cropX,
			int cropY,
			int cropWidth,
			int cropHeight)
		{
			return image.Clone(x => x
				.Resize(width, height)
				.Crop(new Rectangle(cropX, cropY, cropWidth, cropHeight)));
		}


		public static Image GetResizeAroundAndCrop(
			this Image image,
			int width,
			int height,
			int cropX,
			int cropY,
			int cropWidth,
			int cropHeight)
		{
			var r1 = image.GetResizer();
			r1.ScaleAround(width, height);
			return image.GetResizeAndCrop(
				r1.NewWidth, r1.NewHeight, cropX, cropY, cropWidth, cropHeight);
		}


		public static Image GetResizeAroundAndCrop(
			this Image image,
			int width,
			int height,
			int cropWidth,
			int cropHeight,
			float divVertical,
			float divHorizontal)
		{
			var r1 = image.GetResizer();
			r1.ScaleAround(width, height);
			int x1 = (int)((r1.NewWidth - cropWidth) / divHorizontal);
			int y1 = (int)((r1.NewHeight - cropHeight) / divVertical);
			return image.GetResizeAndCrop(
				r1.NewWidth, r1.NewHeight, x1, y1, cropWidth, cropHeight);
		}


		public static void SaveResizeInside(
			this Image image,
			string filename,
			int width,
			int height,
			IImageEncoder encoder)
		{
			using var copy1 = image.GetResizeInside(width, height);
			copy1.Save(filename, encoder);
		}


		public static void SaveResizeAround(
			this Image image,
			string filename,
			int width,
			int height,
			IImageEncoder encoder)
		{
			using var copy1 = image.GetResizeAround(width, height);
			copy1.Save(filename, encoder);
		}


		public static void SaveResizeAverage(
			this Image image,
			string filename,
			int side,
			IImageEncoder encoder)
		{
			using var copy1 = image.GetResizeAverage(side);
			copy1.Save(filename, encoder);
		}


		public static void SaveResizeAroundAndCrop(
			this Image image,
			string filename,
			int width,
			int height,
			int cropX,
			int cropY,
			int cropWidth,
			int cropHeight,
			IImageEncoder encoder)
		{
			using var copy1 = image.GetResizeAroundAndCrop(
				width, height, cropX, cropY, cropWidth, cropHeight);
			copy1.Save(filename, encoder);
		}


		public static void SaveResizeAroundAndCrop(
			this Image image,
			string filename,
			int width,
			int height,
			int cropWidth,
			int cropHeight,
			float divVertical,
			float divHorizontal,
			IImageEncoder encoder)
		{
			using var copy1 = image.GetResizeAroundAndCrop(
				width, height, cropWidth, cropHeight, divVertical, divHorizontal);
			copy1.Save(filename, encoder);
		}

	}

}
