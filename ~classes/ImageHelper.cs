using SixLabors.ImageSharp;

namespace Ans.Net8.Images
{

	public class ImageHelper
		: IDisposable
	{

		private readonly Image _image;
		private readonly Resizer _resizer;


		/* ctor */


		public ImageHelper(
			string filename)
		{
			_image = Image.Load(filename);
			_resizer = new Resizer(_image.Width, _image.Height);
			ImageProp = new()
			{
				Mimetype = _image.Metadata.DecodedImageFormat.DefaultMimeType,
				Width = _image.Width,
				Height = _image.Height,
				Orientation = _resizer.Orientation,
				IsNearSquare = _resizer.IsNearSquare,
			};
			if (ImageProp.Mimetype == "image/jpeg")
				JpegProp = new()
				{
					MaxSize = _getMaxSize(_image.Width, _image.Height),
				};
		}


		/* readonly properties */


		public ImageFileProp ImageProp { get; private set; }
		public JpegFileProp JpegProp { get; private set; }


		/* disposing */


		private bool disposedValue;

		protected virtual void Dispose(
			bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
					_image.Dispose();
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}


		/* privates */


		private static ImageSizeEnum _getMaxSize(
			int width,
			int height)
		{
			// todo
			return ImageSizeEnum.Unknown;
		}

	}



	public class ImageFileProp
	{
		public string Mimetype { get; set; }
		public ImageOrientationEnum Orientation { get; set; }
		public bool IsNearSquare { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}



	public class JpegFileProp
	{
		public ImageSizeEnum MaxSize { get; set; }
		public string Modify { get; set; }
		public ImageShiftEnum ShiftDefault { get; set; }
	}



	public enum ImageSizeEnum
		: int
	{
		Unknown = 0,
		Extrasmall = 1,
		Small = 2,
		Medium = 3,
		Large = 4,
		Extralarge = 5
	}



	public enum ImageShiftEnum
		: int
	{
		None = 0,
		Start = 1,
		StartMiddle = 2,
		EndMiddle = 3,
		End = 4
	}

}
