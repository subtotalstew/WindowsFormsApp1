﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    
    public partial class MainForm : Form
    {
        public MainForm()
        { 
            InitializeComponent();
            label3.Text = "Gamma =" + hScrollBar1.Value.ToString();
            hScrollBar1.Visible = false;
            label3.Visible = false;
            
        }

        private void openbutton_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (checkBox1.Checked)
            {

                pictureBox2.Image.Dispose(); //обязательно чтобы убивать картинку, иначе адовый жор памяти
                pictureBox2.Image = AdjustGamma(pictureBox1.Image, hScrollBar1.Value);
                label3.Text = "Gamma =" + (hScrollBar1.Value / 10).ToString();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = AdjustGamma(pictureBox1.Image, hScrollBar1.Value);
                hScrollBar1.Visible = true;
                label3.Visible = true;
            }
            catch
            {
                MessageBox.Show("Сначала необходимо загрузить изображение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //checkBox1.Checked = false;
            }
        }
        public Bitmap AdjustGamma(Image image, float gamma)
        {
            // Устанавливаем гамма-значение объекта ImageAttributes.
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetGamma(gamma / 100);

            // Нарисуем изображение на новом растровом изображении
            // при применении нового значения гаммы
            Point[] points =
            {
        new Point(0, 0),
        new Point(image.Width, 0),
        new Point(0, image.Height),
    };
            Rectangle rect =
                new Rectangle(0, 0, image.Width, image.Height);

            // Создаем растровое изображение результата.
           
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            // Вернуть результат.
            
            return bm;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form2 f2 = new Form2())
            {
                f2.ShowDialog(this);
            }
        }

        public void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG) |*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG |All files (*.*) |*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    pictureBox2.Image = pictureBox1.Image;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void гистограммаЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form3 f3 = new Form3(this.pictureBox2.Image))
            {
                f3.ShowDialog(this);
            }
        }

        private void сохранитьФайлКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Created)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "*.bmp";
                sfd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG) |*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG |All files (*.*) |*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName;
                    pictureBox2.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                MessageBox.Show("Файл сохранен", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Невозможно сохранить выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
