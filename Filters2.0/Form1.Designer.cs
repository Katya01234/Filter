namespace Filters2._0
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            menuStrip1 = new MenuStrip();
            назадToolStripMenuItem = new ToolStripMenuItem();
            файлToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            сохранитьКакToolStripMenuItem = new ToolStripMenuItem();
            фильтрыToolStripMenuItem = new ToolStripMenuItem();
            точечныеToolStripMenuItem = new ToolStripMenuItem();
            инверсияToolStripMenuItem = new ToolStripMenuItem();
            чернобелыйToolStripMenuItem = new ToolStripMenuItem();
            сепияToolStripMenuItem = new ToolStripMenuItem();
            яркостьToolStripMenuItem = new ToolStripMenuItem();
            серыйМирToolStripMenuItem = new ToolStripMenuItem();
            растяжениеГистограммыToolStripMenuItem = new ToolStripMenuItem();
            геометрическиеToolStripMenuItem = new ToolStripMenuItem();
            переносToolStripMenuItem = new ToolStripMenuItem();
            поворотToolStripMenuItem = new ToolStripMenuItem();
            волныToolStripMenuItem = new ToolStripMenuItem();
            эффектСтеклаToolStripMenuItem = new ToolStripMenuItem();
            матричныеToolStripMenuItem = new ToolStripMenuItem();
            размытиеToolStripMenuItem = new ToolStripMenuItem();
            размытиеПоГауссуToolStripMenuItem = new ToolStripMenuItem();
            собельToolStripMenuItem = new ToolStripMenuItem();
            резкостьToolStripMenuItem = new ToolStripMenuItem();
            тиснениеToolStripMenuItem = new ToolStripMenuItem();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            progressBar2 = new ProgressBar();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();

            // pictureBox1
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Location = new Point(12, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(781, 369);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;

            // menuStrip1
            menuStrip1.Items.AddRange(new ToolStripItem[] { назадToolStripMenuItem, файлToolStripMenuItem, фильтрыToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(805, 28);
            menuStrip1.TabIndex = 1;

            // назадToolStripMenuItem
            назадToolStripMenuItem.Name = "назадToolStripMenuItem";
            назадToolStripMenuItem.Size = new Size(65, 24);
            назадToolStripMenuItem.Text = "Назад";
            назадToolStripMenuItem.Click += назадToolStripMenuItem_Click;

            // файлToolStripMenuItem
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { открытьToolStripMenuItem, сохранитьКакToolStripMenuItem });
            файлToolStripMenuItem.Text = "Файл";

            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += открытьToolStripMenuItem_Click;
            сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            сохранитьКакToolStripMenuItem.Click += сохранитьКакToolStripMenuItem_Click;

            // фильтрыToolStripMenuItem
            фильтрыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { точечныеToolStripMenuItem, геометрическиеToolStripMenuItem, матричныеToolStripMenuItem });
            фильтрыToolStripMenuItem.Text = "Фильтры";

            // точечныеToolStripMenuItem
            точечныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                инверсияToolStripMenuItem,
                чернобелыйToolStripMenuItem,
                сепияToolStripMenuItem,
                яркостьToolStripMenuItem,
                серыйМирToolStripMenuItem,
                растяжениеГистограммыToolStripMenuItem
            });
            точечныеToolStripMenuItem.Text = "Точечные";

            инверсияToolStripMenuItem.Text = "Инверсия";
            инверсияToolStripMenuItem.Click += инверсияtoolStripMenuItem1_Click;
            чернобелыйToolStripMenuItem.Text = "Черно-белый";
            чернобелыйToolStripMenuItem.Click += чернобелыйToolStripMenuItem_Click;
            сепияToolStripMenuItem.Text = "Сепия";
            сепияToolStripMenuItem.Click += сепияToolStripMenuItem_Click;
            яркостьToolStripMenuItem.Text = "Яркость";
            яркостьToolStripMenuItem.Click += яркостьToolStripMenuItem_Click;
            серыйМирToolStripMenuItem.Text = "Серый мир";
            серыйМирToolStripMenuItem.Click += серыйМирToolStripMenuItem_Click;
            растяжениеГистограммыToolStripMenuItem.Text = "Растяжение гистограммы";
            растяжениеГистограммыToolStripMenuItem.Click += растяжениеГистограммыToolStripMenuItem_Click;

            // геометрическиеToolStripMenuItem
            геометрическиеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                переносToolStripMenuItem,
                поворотToolStripMenuItem,
                волныToolStripMenuItem,
                эффектСтеклаToolStripMenuItem
            });
            геометрическиеToolStripMenuItem.Text = "Геометрические";

            переносToolStripMenuItem.Text = "Перенос";
            переносToolStripMenuItem.Click += переносToolStripMenuItem_Click;
            поворотToolStripMenuItem.Text = "Поворот";
            поворотToolStripMenuItem.Click += поворотToolStripMenuItem_Click;
            волныToolStripMenuItem.Text = "Волны";
            волныToolStripMenuItem.Click += волныToolStripMenuItem_Click;
            эффектСтеклаToolStripMenuItem.Text = "Стекло";
            эффектСтеклаToolStripMenuItem.Click += эффектСтеклаToolStripMenuItem_Click;

            // матричныеToolStripMenuItem
            матричныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                размытиеToolStripMenuItem,
                размытиеПоГауссуToolStripMenuItem,
                собельToolStripMenuItem,
                резкостьToolStripMenuItem,
                тиснениеToolStripMenuItem

            });
            матричныеToolStripMenuItem.Text = "Матричные";

            размытиеToolStripMenuItem.Text = "Размытие";
            размытиеToolStripMenuItem.Click += размытиеToolStripMenuItem_Click;
            размытиеПоГауссуToolStripMenuItem.Text = "Размытие по Гауссу";
            размытиеПоГауссуToolStripMenuItem.Click += размытиеПоГауссуToolStripMenuItem_Click;
            собельToolStripMenuItem.Text = "Собель";
            собельToolStripMenuItem.Click += собельToolStripMenuItem_Click;
            резкостьToolStripMenuItem.Text = "Резкость";
            резкостьToolStripMenuItem.Click += резкостьToolStripMenuItem_Click;
            тиснениеToolStripMenuItem.Text = "Тиснение";
            тиснениеToolStripMenuItem.Click += тиснениеToolStripMenuItem_Click;

            // backgroundWorker1
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

            // progressBar2
            progressBar2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar2.Location = new Point(12, 409);
            progressBar2.Size = new Size(648, 29);

            // button2 (Отмена)
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(666, 409);
            button2.Size = new Size(127, 29);
            button2.Text = "Отмена";
            button2.Click += button2_Click;

            // Form1
            ClientSize = new Size(805, 450);
            Controls.Add(button2);
            Controls.Add(progressBar2);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Photo Editor 2.0";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem назадToolStripMenuItem;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem фильтрыToolStripMenuItem;
        private ToolStripMenuItem точечныеToolStripMenuItem;
        private ToolStripMenuItem инверсияToolStripMenuItem;
        private ToolStripMenuItem чернобелыйToolStripMenuItem;
        private ToolStripMenuItem сепияToolStripMenuItem;
        private ToolStripMenuItem яркостьToolStripMenuItem;
        private ToolStripMenuItem серыйМирToolStripMenuItem;
        private ToolStripMenuItem растяжениеГистограммыToolStripMenuItem;
        private ToolStripMenuItem геометрическиеToolStripMenuItem;
        private ToolStripMenuItem переносToolStripMenuItem;
        private ToolStripMenuItem поворотToolStripMenuItem;
        private ToolStripMenuItem волныToolStripMenuItem;
        private ToolStripMenuItem эффектСтеклаToolStripMenuItem;
        private ToolStripMenuItem матричныеToolStripMenuItem;
        private ToolStripMenuItem размытиеToolStripMenuItem;
        private ToolStripMenuItem размытиеПоГауссуToolStripMenuItem;
        private ToolStripMenuItem собельToolStripMenuItem;
        private ToolStripMenuItem резкостьToolStripMenuItem;
        private ToolStripMenuItem тиснениеToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ProgressBar progressBar2;
        private Button button2;
    }
}