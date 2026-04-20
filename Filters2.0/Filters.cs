using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
namespace Filters2._0
{
    // Базовый класс для всех фильтров
    abstract class Filter
    {
        // Вспомогательная функция, чтобы цвета не выходили за пределы 0-255
        public int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                // Проверка на отмену
                if (worker.CancellationPending) return null;

                // Отправка прогресса (процент выполнения)
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    // Конкретный фильтр — Инверсия
    class InvertFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(
                255 - sourceColor.R,
                255 - sourceColor.G,
                255 - sourceColor.B
            );
            return resultColor;
        }
    }
}