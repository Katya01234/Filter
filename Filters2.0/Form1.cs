namespace Filters2._0
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создаем диалог для открытия файла
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png;*.gif|All files|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // загружаем изображение в Bitmap
                image = new Bitmap(dialog.FileName);
                // отображаем изображение в PictureBox
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void точечныеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void инверсияtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filter)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
            {
                image = newImage;
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar2.Value = 0;
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
