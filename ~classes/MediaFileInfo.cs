using Ans.Net8.Common;
using ImageMagick;

namespace Ans.Net8.Images
{

	public enum ImageSizeEnum
		: int
	{
		Unknown = 0,
		Extrasmall = 1, // 256
		Small = 2,      // 576
		Medium = 3,     // 768
		Large = 4,      // 992
		Extralarge = 5  // 1200
	}



	public class MediaFileInfo
	{

		/* ctors */


		public MediaFileInfo(
			FileInfo info)
		{
			FileInfo = info;
			Length = info.Length;
			ContentInfo = SuppIO.GetContentInfoFromExtention(Extension);
			Mimetype = ContentInfo.ContentType;
			if (ContentInfo.IsWebImage)
			{
				try
				{
					MagickImageInfo = new MagickImageInfo(info.FullName);
					Format = MagickImageInfo.Format.ToString().ToLower();
					var type1 = $"image/{Format}";
					if (Mimetype == "image/jpeg" && type1 != "image/jpeg")
					{
						Mimetype = Common._Consts.CONTENTINFO_BIN.ContentType;
						IsInvalidImage = true;
					}
					else
					{
						IsWebImage = true;
						IsJpeg = Format == "jpeg";
						Mimetype = type1;
						ResizeHelper = new ImageResizeHelper(
							MagickImageInfo.Width, MagickImageInfo.Height);
						_calcSize();
					}
				}
				catch (Exception)
				{
					Mimetype = Common._Consts.CONTENTINFO_BIN.ContentType;
					IsInvalidImage = true;
				}
			}
		}


		public MediaFileInfo(
			string path)
			: this(new FileInfo(path))
		{
		}


		/* readonly properties */


		public FileInfo FileInfo { get; }
		public ContentInfo ContentInfo { get; }
		public MagickImageInfo MagickImageInfo { get; }
		public ImageResizeHelper ResizeHelper { get; }
		public long Length { get; }
		public string Mimetype { get; }
		public string Format { get; }
		public bool IsWebImage { get; }
		public bool IsJpeg { get; }
		public bool IsInvalidImage { get; }

		public ImageSizeEnum MaxSize { get; private set; }
		public int SizeIndex { get; private set; }
		public bool HasSmall { get; private set; }
		public bool HasMedium { get; private set; }
		public bool HasLarge { get; private set; }
		public bool HasExtralarge { get; private set; }

		public string FullName
			=> FileInfo.FullName;

		public string Name
			=> FileInfo.Name;

		public string NameWithoutExtension
			=> Path.GetFileNameWithoutExtension(Name);

		public string Extension
			=> FileInfo.Extension;

		public string DirectoryPath
			=> FileInfo.Directory.FullName;

		public uint Width
			=> ResizeHelper?.Width ?? 0;

		public uint Height
			=> ResizeHelper?.Height ?? 0;

		public ImageOrientationEnum Orientation
			=> ResizeHelper?.Orientation ?? ImageOrientationEnum.Unknown;

		public float Ratio
			=> ResizeHelper?.Ratio ?? 0;

		public bool IsNearSquare
			=> ResizeHelper?.IsNearSquare ?? false;


		/* privates */


		private void _calcSize()
		{
			var min1 = Math.Min(Width, Height);

			MaxSize = ImageSizeEnum.Extralarge;
			SizeIndex = 5;
			HasSmall = true;
			HasMedium = true;
			HasLarge = true;
			HasExtralarge = true;
			if (min1 >= _Consts.SIZE_XL)
				return;

			HasExtralarge = false;
			MaxSize = ImageSizeEnum.Large;
			SizeIndex = 4;
			if (min1 >= _Consts.SIZE_LG)
				return;

			HasLarge = false;
			MaxSize = ImageSizeEnum.Medium;
			SizeIndex = 3;
			if (min1 >= _Consts.SIZE_MD)
				return;

			HasMedium = false;
			MaxSize = ImageSizeEnum.Small;
			SizeIndex = 2;
			if (min1 >= _Consts.SIZE_SM)
				return;

			HasSmall = false;
			MaxSize = ImageSizeEnum.Extrasmall;
			SizeIndex = 1;
		}

	}

}
