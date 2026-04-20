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

    class GrayScaleFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb((int)Intensity, (int)Intensity, (int)Intensity);
            return resultColor;
        }
    }



    class MatrixFilter : Filter
    {
        protected float[,] kernel = null;
        public MatrixFilter() { }
        protected MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            int kernelSize = kernel.GetLength(0);
            int radius = kernelSize / 2;
            for (int j = -radius; j <= radius; j++)
            {
                for (int i = -radius; i <= radius; i++)
                {
                    int pixelX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int pixelY = Clamp(y + j, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(pixelX, pixelY);
                    float kernelValue = kernel[j + radius, i + radius];
                    resultR += neighborColor.R * kernelValue;
                    resultG += neighborColor.G * kernelValue;
                    resultB += neighborColor.B * kernelValue;
                }
            }
            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255)
            );
        }

    }
    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeY, sizeX];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
                }
            }

        }
    }

    class GaussianFilter : MatrixFilter
    {
        public GaussianFilter()
        {
            createGaussianKernel(3, 2); 
        }
        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];
            float norm = 0;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    kernel[i + radius, j + radius] = (float)Math.Exp(-(i * i + j * j) / (2 * sigma * sigma));
                    norm += kernel[i + radius, j + radius];
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    kernel[i, j] /= norm;
            }
        }
    }
}
