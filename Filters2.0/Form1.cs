using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Filters2._0
{
    public partial class Form1 : Form
    {
        // Основное изображение и стек для реализации Undo
        private Bitmap image;
        private Stack<Bitmap> history = new Stack<Bitmap>();

        public Form1()
        {
            InitializeComponent();

            // Включаем поддержку Drag & Drop и горячих клавиш
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.AllowDrop = true;
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += PictureBox_DragEnter;
            pictureBox1.DragDrop += PictureBox_DragDrop;
        }

        /// <summary>
        /// Общий метод для запуска любого фильтра
        /// </summary>
        private void ApplyFilter(Filter filter)
        {
            if (image == null)
            {
                MessageBox.Show("Сначала загрузите изображение!");
                return;
            }

            if (!backgroundWorker1.IsBusy)
            {
                // Визуальный отклик: блокируем меню на время обработки
                menuStrip1.Enabled = false;
                backgroundWorker1.RunWorkerAsync(filter);
            }
        }

        #region Файл (Открыть / Сохранить)

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Очистка старых ресурсов перед загрузкой нового фото
                    if (image != null) image.Dispose();
                    while (history.Count > 0) history.Pop().Dispose();

                    image = new Bitmap(dialog.FileName);
                    pictureBox1.Image = image;
                    pictureBox1.Refresh();
                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null) return;

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveDialog.Title = "Save an Image File";

                if (saveDialog.ShowDialog() == DialogResult.OK && saveDialog.FileName != "")
                {
                    using (System.IO.FileStream fs = (System.IO.FileStream)saveDialog.OpenFile())
                    {
                        var format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        if (saveDialog.FilterIndex == 2) format = System.Drawing.Imaging.ImageFormat.Bmp;
                        if (saveDialog.FilterIndex == 3) format = System.Drawing.Imaging.ImageFormat.Gif;

                        image.Save(fs, format);
                    }
                }
            }
        }

        #endregion

        #region BackgroundWorker (Фоновая обработка)

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Работаем с копией, чтобы не мешать основному потоку отрисовки
            Bitmap source = (Bitmap)image.Clone();
            Filter filter = (Filter)e.Argument;

            Bitmap result = filter.processImage(source, backgroundWorker1);

            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                result?.Dispose();
            }
            else
            {
                e.Result = result;
            }
            source.Dispose(); // Чистим временную копию
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && e.Result != null)
            {
                // Кладем текущее состояние в историю ПЕРЕД обновлением
                history.Push(image);

                image = (Bitmap)e.Result;
                pictureBox1.Image = image;
            }

            progressBar2.Value = 0;
            menuStrip1.Enabled = true; // Разблокируем интерфейс
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        #endregion

        #region Обработчики фильтров

        private void инверсияtoolStripMenuItem1_Click(object sender, EventArgs e) => ApplyFilter(new InvertFilter());
        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new GrayScaleFilter());
        private void сепияToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new SepiaFilter());
        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new BrightFilter());
        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new BlurFilter());
        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new GaussianFilter());
        private void собельToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new SobelFilter());
        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(new RezkostFilter());

        #endregion

        #region Управление (Undo / Hotkeys / DragDrop)

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (history.Count > 0)
            {
                Bitmap toDelete = image;
                image = history.Pop();
                pictureBox1.Image = image;
                toDelete.Dispose(); // Удаляем старый Bitmap из памяти
                pictureBox1.Refresh();
            }
            else
            {
                MessageBox.Show("История пуста!");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) назадToolStripMenuItem_Click(sender, e);
        }

        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyFilter(new GrayWorldFilter());
        }

        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                try
                {
                    if (image != null) image.Dispose();
                    while (history.Count > 0) history.Pop().Dispose();

                    image = new Bitmap(files[0]);
                    pictureBox1.Image = image;
                    pictureBox1.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке: " + ex.Message);
                }
            }
        }

        #endregion
    }
}