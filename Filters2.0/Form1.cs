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
            // Создаем экземпляр фильтра
            Filter filter = new InvertFilter();
            // Применяем его к картинке, которая загружена в приложение
            Bitmap resultImage = filter.processImage(image);
            // Отображаем результат в PictureBox
            pictureBox1.Image = resultImage;
            // Обновляем PictureBox
            pictureBox1.Refresh();
        }
    }
}
