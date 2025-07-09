using Ans.Net8.Common;
using ImageMagick;

namespace Ans.Net8.Images
{

	public class ImageHelper
		: IDisposable
	{

		/* ctor */


		public ImageHelper(
			MediaFileInfo info)
		{
			Info = info;
			if (Info.IsWebImage)
			{
				Image = new MagickImage(info.FileInfo.FullName);
				if (Info.IsJpeg)
					Exif = Image.GetExifProfile();
			}
		}


		/* disposing */


		private bool _disposedValue;
		protected virtual void Dispose(
			bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing && Info.IsWebImage)
					Image.Dispose();
				_disposedValue = true;
			}
		}


		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}


		/* readonly properties */


		public MediaFileInfo Info { get; }
		public MagickImage Image { get; }
		public IExifProfile Exif { get; }

		public string ExifOrientation
			=> Exif?.GetValue(ExifTag.Orientation).ToString();


		/* properties */


		public string Modify { get; set; }
		public ImageShiftEnum ShiftDefault { get; set; }

	}

}
