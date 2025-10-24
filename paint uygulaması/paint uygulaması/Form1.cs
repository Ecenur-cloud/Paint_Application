using System.Drawing.Drawing2D;

namespace paint_uygulaması
{
    public partial class Form1 : Form
    {
        Graphics g;
        Point p1, p2;
        Pen kalem;
        Brush firca;
        string sekil = "Çizgi";

        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            KalemFircaAyarla();
        }
        //visible false yapıldığında görünmüyo panel 
        //bütün numero seysilerin max min değerlerini değiştirdik.
        //hepsine isim verdikkarışmasın diye fontlarını ayarladık
        void KalemFircaAyarla()
        {
            Color c1 = Color.FromArgb((int)nR1.Value, (int)nG1.Value, (int)nB1.Value);
            kRenk.BackColor = c1;
            kalem = new Pen(c1, (float)nkalınlık.Value);

            Color c2 = Color.FromArgb((int)nR2.Value, (int)nG2.Value, (int)nB2.Value);
            fRenk.BackColor = c2;
            firca = new SolidBrush(c2);

            Color c3 = Color.FromArgb((int)nR3.Value, (int)nG3.Value, (int)nB3.Value);
            ikincirenk.BackColor = c3;

            if (Gradient.Checked == true)
                firca = new LinearGradientBrush(new Point(0, 0), new Point(100, 100), c2, c3);
            else
                firca= new SolidBrush(c2);
        }
        private void nR1_ValueChanged(object sender, EventArgs e)
        {
            KalemFircaAyarla();
        }



        private void Rbcizgi_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            sekil = rb.Text;
            label1.Text = "şekil seç:" + sekil; //sectiniz kısmının yanına seçtiğin seçeneği yazıyo

        }

        private void Gradient_CheckedChanged(object sender, EventArgs e)
        {
            if (Gradient.Checked == true)
                panel2.Enabled = true;
            else
                panel2.Enabled = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            p1 = e.Location;

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            p2 = e.Location;
            Rectangle rect = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            int a = p2.X - p1.X;
            int b = p2.Y - p1.Y;
            switch (sekil)
            {

                case "cizgi":
                    g.DrawLine(kalem, p1, p2);
                    break;
                case "Dikdortgen":
                    g.FillRectangle(firca, rect);
                    g.DrawRectangle(kalem, rect);
                    break;
                case "Elips":
                    g.FillEllipse(firca, rect);
                    g.DrawEllipse(kalem, rect); 
                    break;
                case "cember":
                    int c=(int) Math.Sqrt(a*a+b*b);//ondalıklı gelebilir diye int yaptık
                    Rectangle rc = new Rectangle(p1.X - c, p1.Y - c, c * 2, c * 2);
                    g.FillEllipse(firca, rc);
                    g.DrawEllipse(kalem, rc);
                    g.DrawLine (kalem, p1, p2);//merkezinden cizgi olusturuyo tıklayıp bıraktıgığım yeri gösteriyo
                    break;
                case "kare":
                    int kenar=(a>b) ? a : b;
                    Rectangle kare= new Rectangle(p1.X,p1.Y,kenar,kenar);
                    g.FillRectangle(firca, kare);
                    g.DrawRectangle(kalem, kare);
                    g.DrawLine(kalem, p1, p2); //başlangıc bitişleri gösteriyo
                    break;

            }
        }
        

        
    }
}
