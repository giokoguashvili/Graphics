using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rotation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Draw();
        }

        private void FillTextBox()
        {
            this.textBox1.Text = this.trackBar1.Value.ToString();
        }
        private Bitmap RectBitmap(int angle)
        {
            var width = this.pictureBox.Size.Width;
            var height = this.pictureBox.Size.Height;

            var rectW = 100;
            var rectH = 100;

            var x = width / 2;// - rectW / 2;
            var y = height / 2;// - rectH / 2;

            var bitmap = new Bitmap(width, height);

            Func<Point, Point> to = (p) => new Point(x + p.X, y - p.Y);

            Func<double, double> toRad = a => Math.PI * a / 180;

            var b = new double[][]
            {
                new double[] { Math.Cos(toRad(angle)), Math.Sin(toRad(angle)) },
                new double[] { Math.Cos(toRad(angle + 90)), Math.Sin(toRad(angle + 90)) },
            };


            Func<Point, Point> rotate = (p) => new Point(
                                                    Convert.ToInt32(Math.Round(p.X * b[0][0] + p.Y * b[0][1])),
                                                    Convert.ToInt32(Math.Round(p.X * b[1][0] + p.Y * b[1][1]))
                                               );
            Enumerable
                .Range(0, rectW)
                .SelectMany(i => Enumerable.Range(0, rectH).Select(j => new Point(i, j)))
                .Select(rotate)
                .Select(to)
                .Select(p =>
                {
                    bitmap
                        .SetPixel(p.X, p.Y, Color.Red);
                    return true;
                })
                .ToList();

            bitmap.SetPixel(x, y, Color.Black);
            return bitmap;
        }

        private void Draw()
        {
            FillTextBox();
            this.pictureBox.Image = RectBitmap(this.trackBar1.Value); 
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
