namespace Ans.Net8.Images
{

    public class ImageFileInfo
    {
        public bool IsInvalidImage { get; set; }
        public bool IsImage { get; set; }
        public bool IsJpeg { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Resizer Resizer { get; set; }
        public string Format { get; set; }
        public string Mimetype { get; set; }
    }

}