using Ans.Net8.Common;
using SixLabors.ImageSharp;

namespace Ans.Net8.Images
{

	public class Resizer
    {

        /* ctor */


        public Resizer(
            int width,
            int height)

        {
            Width = width;
            Height = height;
            Ratio = (float)width / height;
            if (Width == Height)
            {
                IsNearSquare = true;
                Orientation = ImageOrientationEnum.Square;
            }
            else
            {
                IsNearSquare = (Ratio < 1.1 && Ratio > 0.9);
                Orientation = Ratio > 1
                    ? ImageOrientationEnum.Landscape
                    : ImageOrientationEnum.Portrait;
            }
        }


        /* readonly properties */


        public int Width { get; private set; }
        public int Height { get; private set; }
        public float Ratio { get; private set; }
        public ImageOrientationEnum Orientation { get; private set; }
        public bool IsNearSquare { get; private set; }
        public int NewWidth { get; private set; }
        public int NewHeight { get; private set; }

        public Size NewSize
            => new(NewWidth, NewHeight);


        /* methods */


        /// <summary>
        /// Масштабировать
        /// </summary>
        public void Scale(
            float ratio)
        {
            NewWidth = SuppMath.RoundToInt(Width * ratio);
            NewHeight = SuppMath.RoundToInt(Height * ratio);
        }


        /// <summary>
        /// Масштабировать внутри
        /// </summary>
        public void ScaleInside(
            float width,
            float height)
        {
            float r1 = Math.Min((width / Width), (height / Height));
            Scale(r1);
        }


        /// <summary>
        /// Масштабировать снаружи
        /// </summary>
        public void ScaleAround(
            float width,
            float height)
        {
            float r1 = Math.Max((width / Width), (height / Height));
            Scale(r1);
        }


        /// <summary>
        /// Масштабировать усреднением
        /// </summary>
        public void ScaleAverage(
            int side)
        {
            float w1 = side * Width / Height;
            float h1 = side * Height / Width;
            NewWidth = SuppMath.RoundToInt((side + w1) / 2);
            NewHeight = SuppMath.RoundToInt((side + h1) / 2);
        }


        /// <summary>
        /// Масштабировать до ширины
        /// </summary>
        public void ScaleToWidth(
            int width)
        {
            float r1 = (float)width / Width;
            NewWidth = width;
            NewHeight = SuppMath.RoundToInt(Height * r1);
        }


        /// <summary>
        /// Масштабировать до высоты
        /// </summary>
        public void ScaleToHeight(
            int height)
        {
            float r1 = (float)height / Height;
            NewWidth = SuppMath.RoundToInt(Width * r1);
            NewHeight = height;
        }


        /* functions */


        public Size GetScale(
            float ratio)
        {
            Scale(ratio);
            return NewSize;
        }


        public Size GetScaleInside(
            float width,
            float height)
        {
            ScaleInside(width, height);
            return NewSize;
        }


        public Size GetScaleAround(
            float width,
            float height)
        {
            ScaleAround(width, height);
            return NewSize;
        }


        public Size GetScaleAverage(
            int side)
        {
            ScaleAverage(side);
            return NewSize;
        }


        public Size GetScaleToWidth(
            int width)
        {
            ScaleToWidth(width);
            return NewSize;
        }


        public Size GetScaleToHeight(
            int height)
        {
            ScaleToHeight(height);
            return NewSize;
        }

    }



    public enum ImageOrientationEnum
        : int
    {
        Unknown = 0,
        Landscape = 1,
        Portrait = 2,
        Square = 3
    }

}
