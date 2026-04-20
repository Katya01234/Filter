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
    }
}
