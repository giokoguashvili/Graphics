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
    public static class Exts
    {
        public static Bitmap WithPixel(this Bitmap b, Point p, Color c)
        {
            b.SetPixel(p.X, p.Y, c);
            return b;
        }
    }
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

            var boxSize = new Size(this.pictureBox.Size.Width, this.pictureBox.Size.Height);
            var rectSize = new Size(100, 100);

            Point getCenter(Point p) => new Point(p.X / 2, p.Y / 2);

            var _center = getCenter(new Point(boxSize));
            var bitmap = new Bitmap(boxSize.Width, boxSize.Width);

            Func<Point, Point> to(Point center) => (p) => new Point(center.X + p.X, center.Y - p.Y);
            double toRadian(double a) => Math.PI * a / 180;
            (double, double) toBasis(int a) => (Math.Cos(toRadian(a)), Math.Sin(toRadian(a)));

            var angleBetweenBasisVectors = 90;
            var basisI = toBasis(angle);
            var basisJ = toBasis(angle + angleBetweenBasisVectors);

            Point rotate(Point p) => new Point(
                                        Convert.ToInt32(Math.Round(p.X * basisI.Item1 + p.Y * basisJ.Item1)),
                                        Convert.ToInt32(Math.Round(p.X * basisI.Item2 + p.Y * basisJ.Item2))
                                     );
            Enumerable
                .Range(0, rectSize.Width)
                .SelectMany(i => Enumerable.Range(0, rectSize.Height).Select(j => new Point(i, j)))
                .Select(rotate)
                .Select(to(_center))
                .Select(p => bitmap.WithPixel(p, Color.Red))
                .ToList();

            bitmap.WithPixel(_center, Color.Black);
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
