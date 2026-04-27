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

    // Инверсия
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
    // Черно-белый
    class GrayScaleFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            Color resultColor = Color.FromArgb((int)Intensity, (int)Intensity, (int)Intensity);
            return resultColor;
        }
    }
    // Сепия
    class SepiaFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 10;
            double Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            Color resultColor = Color.FromArgb(Clamp((int)Intensity + 2 * k, 0, 255),
                Clamp((int)(Intensity + 0.5 * k), 0, 255),
                Clamp((int)Intensity - 1 * k, 0, 255));
            return resultColor;
        }
    }
    // Яркость
    class BrightFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 30;

            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + k, 0, 255),
                Clamp(sourceColor.G + k, 0, 255),
                Clamp(sourceColor.B + k, 0, 255));
            return resultColor;
        }
    }
    // Матричные фильтры
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
    // Размытие
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
    // Размытие по Гауссу
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
    // Собель 
    class SobelFilter : Filter
    {
        protected int[,] operatorX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        protected int[,] operatorY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = operatorX.GetLength(0) / 2;
            int radiusY = operatorY.GetLength(1) / 2;

            float resultRX = 0;
            float resultGX = 0;
            float resultBX = 0;
            float resultRY = 0;
            float resultGY = 0;
            float resultBY = 0;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultRX += neighborColor.R * operatorX[k + radiusX, l + radiusY];
                    resultGX += neighborColor.G * operatorX[k + radiusX, l + radiusY];
                    resultBX += neighborColor.B * operatorX[k + radiusX, l + radiusY];

                    resultRY += neighborColor.R * operatorY[k + radiusX, l + radiusY];
                    resultGY += neighborColor.G * operatorY[k + radiusX, l + radiusY];
                    resultBY += neighborColor.B * operatorY[k + radiusX, l + radiusY];
                }
            int resultR = (int)Math.Sqrt(resultRX * resultRX + resultRY * resultRY);
            int resultG = (int)Math.Sqrt(resultGX * resultGX + resultGY * resultGY);
            int resultB = (int)Math.Sqrt(resultBX * resultBX + resultBY * resultBY);


            return Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
        }
    }
    // Резкость
    class RezkostFilter : MatrixFilter
    {
        public RezkostFilter()
        {
            kernel = new float[3, 3];
            kernel[0, 0] = 0; kernel[0, 1] = -1; kernel[0, 2] = 0;
            kernel[1, 0] = -1; kernel[1, 1] = 5; kernel[1, 2] = -1;
            kernel[2, 0] = 0; kernel[2, 1] = -1; kernel[2, 2] = 0;

        }
    }

    // Фильтр "Серый мир"
    class GrayWorldFilter : Filter
    {
        // Нам не нужен calculateNewPixelColor, так как мы полностью меняем логику в processImage
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y) => Color.Black;

        public new Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            double avgR = 0, avgG = 0, avgB = 0;
            int totalPixels = sourceImage.Width * sourceImage.Height;

            // Шаг 1: Вычисляем средние значения каналов
            for (int i = 0; i < sourceImage.Width; i++)
            {
                if (worker.CancellationPending) return null;
                // Отчет о прогрессе (первые 50% на анализ)
                worker.ReportProgress((int)((float)i / sourceImage.Width * 50));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color c = sourceImage.GetPixel(i, j);
                    avgR += c.R;
                    avgG += c.G;
                    avgB += c.B;
                }
            }

            avgR /= totalPixels;
            avgG /= totalPixels;
            avgB /= totalPixels;

            // Среднее по всем каналам
            double avgAll = (avgR + avgG + avgB) / 3.0;

            // Коэффициенты масштабирования
            double kr = avgAll / avgR;
            double kg = avgAll / avgG;
            double kb = avgAll / avgB;

            // Шаг 2: Применяем коэффициенты к каждому пикселю
            for (int i = 0; i < sourceImage.Width; i++)
            {
                if (worker.CancellationPending) return null;
                // Отчет о прогрессе (вторые 50% на отрисовку)
                worker.ReportProgress(50 + (int)((float)i / sourceImage.Width * 50));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color c = sourceImage.GetPixel(i, j);
                    resultImage.SetPixel(i, j, Color.FromArgb(
                        Clamp((int)(c.R * kr), 0, 255),
                        Clamp((int)(c.G * kg), 0, 255),
                        Clamp((int)(c.B * kb), 0, 255)
                    ));
                }
            }

            return resultImage;
        }
    }
    // Гисторграмма (растяжение)
    class HistogramStretchFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y) => Color.Black;

        public new Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            int minR = 255, minG = 255, minB = 255;
            int maxR = 0, maxG = 0, maxB = 0;

            // ЭТАП 1: Поиск мин. и макс. значений по каждому каналу
            for (int i = 0; i < sourceImage.Width; i++)
            {
                if (worker.CancellationPending) return null;
                worker.ReportProgress((int)((float)i / sourceImage.Width * 50));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color c = sourceImage.GetPixel(i, j);
                    if (c.R < minR) minR = c.R;
                    if (c.G < minG) minG = c.G;
                    if (c.B < minB) minB = c.B;

                    if (c.R > maxR) maxR = c.R;
                    if (c.G > maxG) maxG = c.G;
                    if (c.B > maxB) maxB = c.B;
                }
            }

            // ЭТАП 2: Растяжение
            for (int i = 0; i < sourceImage.Width; i++)
            {
                if (worker.CancellationPending) return null;
                worker.ReportProgress(50 + (int)((float)i / sourceImage.Width * 50));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color c = sourceImage.GetPixel(i, j);

                    // Рассчитываем новые значения (с защитой от деления на ноль)
                    int newR = (maxR == minR) ? c.R : (c.R - minR) * 255 / (maxR - minR);
                    int newG = (maxG == minG) ? c.G : (c.G - minG) * 255 / (maxG - minG);
                    int newB = (maxB == minB) ? c.B : (c.B - minB) * 255 / (maxB - minB);

                    resultImage.SetPixel(i, j, Color.FromArgb(
                        Clamp(newR, 0, 255),
                        Clamp(newG, 0, 255),
                        Clamp(newB, 0, 255)
                    ));
                }
            }
            return resultImage;
        }
    }

    // Базовый класс для геометрических фильтров
    abstract class GeometricFilter : Filter
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Вычисляем новые координаты на основе формул
            int k = (int)calculateNewX(x, y);
            int l = (int)calculateNewY(x, y);

            // Проверка на выход за границы изображения
            if (k < 0 || k >= sourceImage.Width || l < 0 || l >= sourceImage.Height)
                return Color.Black;

            return sourceImage.GetPixel(k, l);
        }

        protected abstract double calculateNewX(int x, int y);
        protected abstract double calculateNewY(int x, int y);
    }

    // 1.1 Перенос (Shift)
    class ShiftFilter : GeometricFilter
    {
        protected override double calculateNewX(int x, int y) => x + 50;
        protected override double calculateNewY(int x, int y) => y;
    }

    // 1.2 Волны (Waves Type 1)
    class WavesFilter : GeometricFilter
    {
        protected override double calculateNewX(int x, int y) => x + 20 * Math.Sin(2 * Math.PI * y / 60);
        protected override double calculateNewY(int x, int y) => y;
    }

    // 1.3 Поворот (Rotate)
    class RotationFilter : GeometricFilter
    {
        private double angle;

        // Добавляем конструктор, чтобы принимать угол
        public RotationFilter(double angle)
        {
            this.angle = angle;
        }

        protected override double calculateNewX(int x, int y)
        {
            int x0 = 150; // Центр поворота
            int y0 = 150;
            return (x - x0) * Math.Cos(angle) - (y - y0) * Math.Sin(angle) + x0;
        }

        protected override double calculateNewY(int x, int y)
        {
            int x0 = 150;
            int y0 = 150;
            return (x - x0) * Math.Sin(angle) + (y - y0) * Math.Cos(angle) + y0;
        }
    }

    // 1.4 Эффект стекла (Glass)
    class GlassFilter : GeometricFilter
    {
        private Random rand = new Random();

        protected override double calculateNewX(int x, int y)
        {
            // Смещаем координату на случайное число в диапазоне [-0.5, 0.5]
            return x + (rand.NextDouble() - 0.5) * 10;
        }

        protected override double calculateNewY(int x, int y)
        {
            return y + (rand.NextDouble() - 0.5) * 10;
        }
    }
}
