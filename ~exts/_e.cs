using Ans.Net8.Common;
using ImageMagick;

namespace Ans.Net8.Images
{

	public static partial class _e
	{

		/* functions */


		public static ImageResizeHelper GetResizer(
			this MagickImage image)
		{
			return new ImageResizeHelper(image.Width, image.Height);
		}


		public static IMagickImage GetResizeInside(
			this MagickImage image,
			uint width,
			uint height,
			bool noIncrease,
			bool useMaskBackground)
		{
			var r1 = image.GetResizer();
			r1.ScaleInside(width, height, noIncrease);
			var clone1 = image.Clone();
			clone1.Resize(r1.NewWidth, r1.NewHeight);
			if (useMaskBackground)
			{
				using var back1 = new MagickImage(
					"pattern:CHECKERBOARD");
				clone1.Tile(back1, CompositeOperator.DstOver);
			}
			return clone1;
		}


		public static IMagickImage GetResizeAround(
			this MagickImage image,
			uint width,
			uint height)
		{
			var r1 = image.GetResizer();
			r1.ScaleAround(width, height);
			var clone1 = image.Clone();
			clone1.Resize(r1.NewWidth, r1.NewHeight);
			return clone1;
		}


		public static IMagickImage GetResizeAverage(
			this MagickImage image,
			uint side)
		{
			var r1 = image.GetResizer();
			r1.ScaleAverage(side);
			var clone1 = image.Clone();
			clone1.Resize(r1.NewWidth, r1.NewHeight);
			return clone1;
		}


		public static IMagickImage GetResizeAndCrop(
			this MagickImage image,
			uint width,
			uint height,
			uint cropX,
			uint cropY,
			uint cropWidth,
			uint cropHeight)
		{
			var clone1 = image.Clone();
			clone1.Resize(width, height);
			clone1.Crop(new MagickGeometry((int)cropX, (int)cropY, cropWidth, cropHeight));
			return clone1;
		}


		public static IMagickImage GetResizeAroundAndCrop(
			this MagickImage image,
			uint width,
			uint height,
			uint cropX,
			uint cropY,
			uint cropWidth,
			uint cropHeight)
		{
			var r1 = image.GetResizer();
			r1.ScaleAround(width, height);
			return image.GetResizeAndCrop(
				r1.NewWidth, r1.NewHeight,
				cropX, cropY, cropWidth, cropHeight);
		}


		public static IMagickImage GetResizeAroundAndCrop(
			this MagickImage image,
			uint width,
			uint height,
			uint cropWidth,
			uint cropHeight,
			ImageShiftEnum shiftVertical,
			ImageShiftEnum shiftHorizontal)
		{
			var r1 = image.GetResizer();
			r1.ScaleAround(width, height);
			var cropX = ImageResizeHelper.GetCropStart(r1.NewWidth, cropWidth, shiftVertical);
			var cropY = ImageResizeHelper.GetCropStart(r1.NewHeight, cropHeight, shiftHorizontal);
			return image.GetResizeAndCrop(
				r1.NewWidth, r1.NewHeight,
				cropX, cropY, cropWidth, cropHeight);
		}


		/* methods */


		public static void SaveResizeInside(
			this MagickImage image,
			string filename,
			uint width,
			uint height,
			uint quality,
			bool noIncrease,
			bool useMaskBackground)
		{
			using var copy1 = image.GetResizeInside(
				width, height, noIncrease, useMaskBackground);
			copy1.Quality = quality;
			copy1.Write(filename);
		}


		public static void SaveResizeAround(
			this MagickImage image,
			string filename,
			uint width,
			uint height,
			uint quality)
		{
			using var copy1 = image.GetResizeAround(width, height);
			copy1.Quality = quality;
			copy1.Write(filename);
		}


		public static void SaveResizeAverage(
			this MagickImage image,
			string filename,
			uint side,
			uint quality)
		{
			using var copy1 = image.GetResizeAverage(side);
			copy1.Quality = quality;
			copy1.Write(filename);
		}


		public static void SaveResizeAroundAndCrop(
			this MagickImage image,
			string filename,
			uint width,
			uint height,
			uint cropX,
			uint cropY,
			uint cropWidth,
			uint cropHeight,
			uint quality)
		{
			using var copy1 = image.GetResizeAroundAndCrop(
				width, height,
				cropX, cropY, cropWidth, cropHeight);
			copy1.Quality = quality;
			copy1.Write(filename);
		}


		public static void SaveResizeAroundAndCrop(
			this MagickImage image,
			string filename,
			uint width,
			uint height,
			uint cropWidth,
			uint cropHeight,
			ImageShiftEnum shiftVertical,
			ImageShiftEnum shiftHorizontal,
			uint quality)
		{
			using var copy1 = image.GetResizeAroundAndCrop(
				width, height,
				cropWidth, cropHeight, shiftVertical, shiftHorizontal);
			copy1.Quality = quality;
			copy1.Write(filename);
		}

	}

}
