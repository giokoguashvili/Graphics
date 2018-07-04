using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Draw(int n, int count)
        {
            var b = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            b.SetPixel(100, 100, Color.Red);

            var g = Graphics.FromImage(b);
            g.DrawEllipse(new Pen(Color.Black), new RectangleF(100, 100, 200, 200));

            double toRadian(double a) => Math.PI * a / 180;
            //var count = 100;
            //var n = 2;
            Func<double, PointF> point = (i) =>
            {
                var angle = (360 / (1.0 * count)) * i;
                var radA = toRadian(angle);
                var x = (float)Math.Cos(radA) * 100 + 200;
                var y = (float)Math.Sin(radA) * 100 + 200;
                return new PointF(x, y);
            };

            Enumerable
                .Range(0, count)
                .Select(i =>
                {
                    var pF = point(i);

                    pF.X -= 2;
                    pF.Y -= 2;
                    g.DrawEllipse(new Pen(Color.Red), new RectangleF(pF, new Size(4, 4)));

                    g.DrawLine(new Pen(Color.Green), point(i), point(i * n));
                    return true;
                })
                .ToList();

            this.pictureBox1.Image = b;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Draw(this.trackBar1.Value, this.trackBar2.Value);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Draw(this.trackBar1.Value, this.trackBar2.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Draw(this.trackBar1.Value, this.trackBar2.Value);
        }
    }
}
