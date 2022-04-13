using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button_Click(object sender, EventArgs e)
        {
            
        }

        Image[] resim =
        {
            Properties.Resources.emoji,
            Properties.Resources.mayin,
        };


        PictureBox[] Parca = new PictureBox[1];//karakterimizi oluşturuyoruz
        PictureBox bos = new PictureBox(); //ekranda en altta karakterimizi 1 kere göstermek için picturebox oluşturuyoruz.
        int SonrakiDeger = 0; // karakterin ilk elemanını belirliyoruz.
        int SonDeger = 0; // karakterin baş elemanını yani resmin olacağı elemanı belirliyoruz.
        bool OyunDurumu = false; // oyunun başlaması için gerekli değişkeni oluşturuyoruz.
        char Yon = (char)0; //yönümüzü belirtecek değişkeni oluşturuyoruz.
        bool Carpma = false; //mayına çarptığımızı kontrol etmek için gerekli değişkeni oluşturuyoruz.
        int sayac1 = 0;

        private void KarakterOlustur() //karakterimizi oluşturmak için gerekli fonksiyon.
        {
            Parca = new PictureBox[1];
            for (int i = 0; i < Parca.Length; i++)
            {
                Parca[i] = new PictureBox();
                Parca[i].Width = 20;
                Parca[i].Height = 20;
                Parca[i].Top = 400;
                Parca[i].Left = 180;
                Parca[i].BackColor = Color.White;
                
                pictureBox3.Controls.Add(Parca[i]);
            }
            SonDeger = Parca.Length - 1;
            Parca[SonDeger].Image = resim[0];
        }

        private void YolEkle() //ekranımıza(pictureBox3) karakterimizin parçalarını ekliyoruz.
        {
            Array.Resize(ref Parca, Parca.Length + 1);
            Parca[Parca.Length - 1] = new PictureBox();
            Parca[Parca.Length - 1].Width = 20;
            Parca[Parca.Length - 1].Height = 20;
            Parca[Parca.Length - 1].Top = -20; 
            Parca[Parca.Length - 1].Left = -20; 
            Parca[Parca.Length -1].BackColor = Color.White;
            pictureBox3.Controls.Add(Parca[Parca.Length - 1]);
            sayac1++;
        }
        int sayac = 0;
        private void CarpmaKontrol() //sınırları aştığını kontrol eden fonksiyon.
        {
            for (int i = 0; i < Parca.Length; i++)
            {

                float mutlakX = Math.Abs((Parca[SonDeger].Left + (Parca[SonDeger].Width / 2)) - (Parca[i].Left + (Parca[i].Width / 2)));
                float mutlakY = Math.Abs((Parca[SonDeger].Top + (Parca[SonDeger].Height / 2)) - (Parca[i].Top + (Parca[i].Height / 2)));
                float farkGenislik = (Parca[SonDeger].Width / 2) + (Parca[i].Width / 2);
                float farkYukselik = (Parca[SonDeger].Height / 2) + (Parca[i].Height / 2);
            }

            if (Parca[SonDeger].Left > pictureBox3.Width - Parca[SonDeger].Width)
            {
                MessageBox.Show("Sağ tarafa çok gittin.");
                Carpma = true;
            }
            else if (Parca[SonDeger].Top > pictureBox3.Height - Parca[SonDeger].Height)
            {
                MessageBox.Show("Aşağı tarafa çok gittin");
                Carpma = true;
            }
            else if (Parca[SonDeger].Top < 1)
            {
                Parca[SonDeger].Image = resim[0];
                Parca[SonDeger - 1].Image = null;
                timer1.Enabled = false; //zamanı durduruyoruz.
                kaydet(); //dosyaya kaydet fonksiyonunu çağırıyoruz.
                if(radioButton1.Checked)
                {
                    for (int j = 0; j < 40; j++)
                    {
                        Mayinlar[j].Visible = true;
                    }
                }
                else if(radioButton2.Checked)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        Mayinlar1[j].Visible = true;
                    }
                }
                else if(radioButton3.Checked)
                {
                    for (int j = 0; j < 80; j++)
                    {
                        Mayinlar2[j].Visible = true;
                    }
                }
                MessageBox.Show("Kazandınız"); //ekrana kazandınız yazısını bastırıyoruz.
                Carpma = true; //çarptığını belirtiyotuz.
                Application.Restart(); //uygulamayı yeniden başlatıyoruz.
            }
            else if (Parca[SonDeger].Left < 0)
            {
                MessageBox.Show("Sol tarafa çok gittin.");
                Carpma = true;
            }
            
        }

        int SolaGit = 0, YukariGit = 0; // karakterin ilerlemesi için gerekli değişkenler.
        private void KarakterHareket(int top, int left) //karakteri hareket ettiren fonksiyon.
        {
            Parca[SonrakiDeger].Left = Parca[SonDeger].Left + left;
            Parca[SonrakiDeger].Top = Parca[SonDeger].Top + top;

            if (SonrakiDeger == Parca.Length - 1)
            {
                SonrakiDeger = 0;
                SonDeger = Parca.Length - 1;
            }
            else
            {
                SonrakiDeger++;
                SonDeger = SonrakiDeger - 1;
            }
        }

        Random r = new Random(); //random sayı için değişkeni oluşturuyoruz.
        PictureBox[] Mayinlar = new PictureBox[40]; //kolay seviye için 40 tane mayın olduğu için 40 elemanlı dizi oluşturduk.
        PictureBox degisken_mayin;
        int[] MayinlarTop = new int[40];
        int[] MayinlarLeft = new int[40];
        int Score = 0;

        private void MayinOlustur()//kolay seviye için mayın oluşturma fonksiyonu.
        {
            for (int i = 0; i < 40; i++)
            {
                MayinlarTop[i] = 20 * r.Next(0, 20);
                MayinlarLeft[i] = 20 * r.Next(0, 20);
                degisken_mayin = new PictureBox();
                degisken_mayin.Width = 20;
                degisken_mayin.Height = 20;
                degisken_mayin.Left = MayinlarLeft[i];
                degisken_mayin.Top = MayinlarTop[i];
                degisken_mayin.BackColor = Color.Red;
                degisken_mayin.Image = resim[1];
                Mayinlar[i] = degisken_mayin;
                pictureBox3.Controls.Add(Mayinlar[i]); //mayını ekrana ekliyor.
                Mayinlar[i].Visible = false;
            }
            for (int i = 0; i < 40; i++)//aynı konuma atılan mayınları değiştirmek için döngü oluşturduk.
            {
                for (int j = 0; j < 40; j++)
                {
                    if (MayinlarLeft[i] == MayinlarLeft[j] && MayinlarTop[i] == MayinlarTop[j] && i != j || MayinlarLeft[i] == 180 && MayinlarTop[i] == 380)//mayınlar aynı konumdaysa ve karakterin başlangıç konumunda mayın varsa koşula giriyor.
                    {
                        degisken_mayin = new PictureBox();
                        degisken_mayin.Top = MayinlarTop[i];
                        degisken_mayin.Left = MayinlarLeft[i];
                        degisken_mayin.BackColor = Color.Yellow;
                        degisken_mayin.Image = null;
                        pictureBox3.Controls.Remove(Mayinlar[i]);//mayını ekrandan siliyor.
                        MayinlarTop[i] = 20 * r.Next(0, 20);
                        MayinlarLeft[i] = 20 * r.Next(0, 20);

                        degisken_mayin.Width = 20;
                        degisken_mayin.Height = 20;
                        degisken_mayin.Left = MayinlarLeft[i];
                        degisken_mayin.Top = MayinlarTop[i];
                        degisken_mayin.BackColor = Color.Red;
                        degisken_mayin.Image = resim[1];
                        Mayinlar[i] = degisken_mayin;
                        pictureBox3.Controls.Add(Mayinlar[i]);
                        Mayinlar[i].Visible = false;
                    }
                }
            }

        }

        PictureBox[] Mayinlar1 = new PictureBox[50];
        PictureBox degisken_mayin1;
        int[] MayinlarTop1 = new int[50];
        int[] MayinlarLeft1 = new int[50];
        private void MayinOlustur1()//orta seviye için mayın oluşturan fonksiyon
        {
            for (int i = 0; i < 50; i++)
            {
                MayinlarTop1[i] = 20 * r.Next(0, 20);
                MayinlarLeft1[i] = 20 * r.Next(0, 20);
                degisken_mayin1 = new PictureBox();
                degisken_mayin1.Width = 20;
                degisken_mayin1.Height = 20;
                degisken_mayin1.Left = MayinlarLeft1[i];
                degisken_mayin1.Top = MayinlarTop1[i];
                degisken_mayin1.BackColor = Color.Red;
                degisken_mayin1.Image = resim[1];
                Mayinlar1[i] = degisken_mayin1;
                pictureBox3.Controls.Add(Mayinlar1[i]);
                Mayinlar1[i].Visible = false;
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (MayinlarLeft1[i] == MayinlarLeft1[j] && MayinlarTop1[i] == MayinlarTop1[j] && i != j || MayinlarLeft1[i] == 180 && MayinlarTop1[i] == 380)
                    {
                        degisken_mayin1 = new PictureBox();
                        degisken_mayin1.Top = MayinlarTop1[i];
                        degisken_mayin1.Left = MayinlarLeft1[i];
                        degisken_mayin1.BackColor = Color.Yellow;
                        degisken_mayin1.Image = null;
                        pictureBox3.Controls.Remove(Mayinlar1[i]);
                        MayinlarTop1[i] = 20 * r.Next(0, 20);
                        MayinlarLeft1[i] = 20 * r.Next(0, 20);

                        degisken_mayin1.Width = 20;
                        degisken_mayin1.Height = 20;
                        degisken_mayin1.Left = MayinlarLeft1[i];
                        degisken_mayin1.Top = MayinlarTop1[i];
                        degisken_mayin1.BackColor = Color.Red;
                        degisken_mayin1.Image = resim[1];
                        Mayinlar1[i] = degisken_mayin1;
                        pictureBox3.Controls.Add(Mayinlar1[i]);
                        Mayinlar1[i].Visible = false;
                    }
                }
            }

        }

        PictureBox[] Mayinlar2 = new PictureBox[80];
        PictureBox degisken_mayin2;
        int[] MayinlarTop2 = new int[80];
        int[] MayinlarLeft2 = new int[80];
        private void MayinOlustur2()//zor seviye için mayın oluşturma fonksiyonu
        {
            for (int i = 0; i < 80; i++)
            {
                MayinlarTop2[i] = 20 * r.Next(0, 20);
                MayinlarLeft2[i] = 20 * r.Next(0, 20);
                degisken_mayin2 = new PictureBox();
                degisken_mayin2.Width = 20;
                degisken_mayin2.Height = 20;
                degisken_mayin2.Left = MayinlarLeft2[i];
                degisken_mayin2.Top = MayinlarTop2[i];
                degisken_mayin2.BackColor = Color.Red;
                degisken_mayin2.Image = resim[1];
                Mayinlar2[i] = degisken_mayin2;
                pictureBox3.Controls.Add(Mayinlar2[i]);
                Mayinlar2[i].Visible = false;
            }
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    if (MayinlarLeft2[i] == MayinlarLeft2[j] && MayinlarTop2[i] == MayinlarTop2[j] && i != j || MayinlarLeft2[i] == 180 && MayinlarTop2[i] == 380)
                    {
                        degisken_mayin2 = new PictureBox();
                        degisken_mayin2.Top = MayinlarTop2[i];
                        degisken_mayin2.Left = MayinlarLeft2[i];
                        degisken_mayin2.BackColor = Color.Yellow;
                        degisken_mayin2.Image = null;
                        pictureBox3.Controls.Remove(Mayinlar2[i]);
                        MayinlarTop2[i] = 20 * r.Next(0, 20);
                        MayinlarLeft2[i] = 20 * r.Next(0, 20);

                        degisken_mayin2.Width = 20;
                        degisken_mayin2.Height = 20;
                        degisken_mayin2.Left = MayinlarLeft2[i];
                        degisken_mayin2.Top = MayinlarTop2[i];
                        degisken_mayin2.BackColor = Color.Red;
                        degisken_mayin2.Image = resim[1];
                        Mayinlar2[i] = degisken_mayin2;
                        pictureBox3.Controls.Add(Mayinlar2[i]);
                        Mayinlar2[i].Visible = false;
                    }
                }
            }

        }

        private void kaydet()//dosyaya kaydetme fonksiyonu.
        {
            if (kayıtkontrol != 0)//koşulu sağlarsa eğer dosya kaydedecek.
            {
                using (StreamWriter yaz = File.AppendText(@".\veriler.txt"))
                {
                    yaz.WriteLine(label7.Text + " " + label6.Text);
                }
            }
        }
        private void oku()//dosyada ki verileri okumak için gerekli fonksiyon.
        {
            using (StreamReader oku = new StreamReader(@".\veriler.txt"))
            {
                while (oku.Peek() >= 0)//her bir satırı okumak için gerekli döngü.
                {
                    richTextBox1.Text += oku.ReadLine()+"\n";
                    
                }
                MessageBox.Show(richTextBox1.Text);
            }
        }

        
        private void MayinCarpmaKontrol()//kolay seviye için mayına çarptığını kontrol eden fonksiyon.
        {
            for(int i=0;i<40;i++)
            {
                float mutlakX = Math.Abs((Parca[SonDeger].Left + (Parca[SonDeger].Width / 2)) - (MayinlarLeft[i] + (21 / 2)));
                float mutlakY = Math.Abs((Parca[SonDeger].Top + (Parca[SonDeger].Height / 2)) - (MayinlarTop[i] + (21 / 2)));
                float farkGenislik = (Parca[SonDeger].Width / 2) + (21 / 2);
                float farkYukselik = (Parca[SonDeger].Height / 2) + (21 / 2);

                if ((farkGenislik > mutlakX) && (farkYukselik > mutlakY))//mayına çarptıysa koşulu sağla.
                {
                    timer1.Enabled = false;
                    for(int j=0;j<40;j++)
                    {
                        Mayinlar[j].Visible = true;
                    }
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("Yeni Oyun", "Kaybettiniz", MessageBoxButtons.YesNo);//messageBox ile ekrana seçenek sunan kod.
                    if (dialog == DialogResult.Yes)//evet e basılınca koşulu sağla.
                    {
                        kaydet();
                        Application.Restart();
                    }
                    else
                    {
                        kaydet();
                        this.Close();
                    }
                }
                else //mayına çarpmadıysa koşulu sağla.
                {
                    YolEkle();//karaktere eleman ekleme fonksiyonunu çağırıyoruz.
                    
                }
            }
            
        }

        private void MayinCarpmaKontrol1()//orta seviye için mayına çarptığını kontrol eden fonksiyon.
        {
            for (int i = 0; i < 50; i++)
            {
                float mutlakX = Math.Abs((Parca[SonDeger].Left + (Parca[SonDeger].Width / 2)) - (MayinlarLeft1[i] + (21 / 2)));
                float mutlakY = Math.Abs((Parca[SonDeger].Top + (Parca[SonDeger].Height / 2)) - (MayinlarTop1[i] + (21 / 2)));
                float farkGenislik = (Parca[SonDeger].Width / 2) + (21 / 2);
                float farkYukselik = (Parca[SonDeger].Height / 2) + (21 / 2);
                if ((farkGenislik > mutlakX) && (farkYukselik > mutlakY))
                {
                    timer1.Enabled = false;
                    for (int j = 0; j < 50; j++)
                    {
                        Mayinlar1[j].Visible = true;
                    }
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("Yeni Oyun", "Kaybettiniz", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        kaydet();
                        Application.Restart();
                    }
                    else
                    {
                        kaydet();
                        this.Close();
                    }
                }
                else
                {
                    YolEkle();
                    
                }
            }

        }

        private void MayinCarpmaKontrol2()//zor seviye için mayına çarptığını kontrol eden fonksiyon.
        {
            for (int i = 0; i < 80; i++)
            {
                float mutlakX = Math.Abs((Parca[SonDeger].Left + (Parca[SonDeger].Width / 2)) - (MayinlarLeft2[i] + (21 / 2)));
                float mutlakY = Math.Abs((Parca[SonDeger].Top + (Parca[SonDeger].Height / 2)) - (MayinlarTop2[i] + (21 / 2)));
                float farkGenislik = (Parca[SonDeger].Width / 2) + (21 / 2);
                float farkYukselik = (Parca[SonDeger].Height / 2) + (21 / 2);
                
                if ((farkGenislik > mutlakX) && (farkYukselik > mutlakY))
                {
                    timer1.Enabled = false;
                    for (int j = 0; j < 80; j++)
                    {
                        Mayinlar2[j].Visible = true;
                    }
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("Yeni Oyun", "Kaybettiniz", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        kaydet();
                        Application.Restart();
                    }
                    else 
                    {
                        kaydet();
                        this.Close();
                    }
                }
                else
                {
                    YolEkle();
                }
            }

        }

        int mayin_kontrol=0;
        private void MayinKontrol()//kolay seviye için karakterin bulunduğu konumun etrafında kaç tane mayın olduğunu kontrol eden fonksiyon.
        {
            mayin_kontrol = 0;
            for (int i = 0; i < 40; i++)
            {
                int top = Parca[SonDeger].Top;
                int left = Parca[SonDeger].Left;
                if (MayinlarTop[i] == top + 20 && MayinlarLeft[i] == left)
                {
                    mayin_kontrol++;
                }
                else if (MayinlarTop[i] == top -20 && MayinlarLeft[i] == left)
                {
                    mayin_kontrol++;
                }
                else if (MayinlarTop[i] == top && MayinlarLeft[i] == left + 20)
                {
                    mayin_kontrol++;
                }
                else if (MayinlarTop[i] == top && MayinlarLeft[i] == left - 20)
                {
                    mayin_kontrol++;
                }
            }
            label4.Text = mayin_kontrol.ToString();
        }

        int mayin_kontrol1 = 0;
        private void MayinKontrol1()//orta seviye için karakterin bulunduğu konumun etrafında kaç tane mayın olduğunu kontrol eden fonksiyon.
        {
            mayin_kontrol1 = 0;
            for (int i = 0; i < 50; i++)
            {
                int top = Parca[SonDeger].Top;
                int left = Parca[SonDeger].Left;
                if (MayinlarTop1[i] == top + 20 && MayinlarLeft1[i] == left)
                {
                    mayin_kontrol1++;
                }
                else if (MayinlarTop1[i] == top - 20 && MayinlarLeft1[i] == left)
                {
                    mayin_kontrol1++;
                }
                else if (MayinlarTop1[i] == top && MayinlarLeft1[i] == left + 20)
                {
                    mayin_kontrol1++;
                }
                else if (MayinlarTop1[i] == top && MayinlarLeft1[i] == left - 20)
                {
                    mayin_kontrol1++;
                }
            }
            label4.Text = mayin_kontrol1.ToString();
        }

        int mayin_kontrol2 = 0;
        private void MayinKontrol2()//zor seviye için karakterin bulunduğu konumun etrafında kaç tane mayın olduğunu kontrol eden fonksiyon.
        {
            mayin_kontrol2 = 0;
            for (int i = 0; i < 80; i++)
            {
                int top = Parca[SonDeger].Top;
                int left = Parca[SonDeger].Left;
                if (MayinlarTop2[i] == top + 20 && MayinlarLeft2[i] == left)
                {
                    mayin_kontrol2++;
                }
                else if (MayinlarTop2[i] == top - 20 && MayinlarLeft2[i] == left)
                {
                    mayin_kontrol2++;
                }
                else if (MayinlarTop2[i] == top && MayinlarLeft2[i] == left + 20)
                {
                    mayin_kontrol2++;
                }
                else if (MayinlarTop2[i] == top && MayinlarLeft2[i] == left - 20)
                {
                    mayin_kontrol2++;
                }
            }
            label4.Text = mayin_kontrol2.ToString();
        }

        int kontrol = 0;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)//program çalıştığında kolay seviye seçili ise.
        {
            for (int i = 0; i < 40; i++)//kolay seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol != 0)
                {

                    degisken_mayin = new PictureBox();

                    degisken_mayin.Top = MayinlarTop[i];
                    degisken_mayin.Left = MayinlarLeft[i];
                    degisken_mayin.BackColor = Color.Yellow;
                    degisken_mayin.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            for (int i = 0; i < 50; i++)//orta seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol1 != 0)
                {
                    degisken_mayin1 = new PictureBox();

                    degisken_mayin1.Top = MayinlarTop1[i];
                    degisken_mayin1.Left = MayinlarLeft1[i];
                    degisken_mayin1.BackColor = Color.Yellow;
                    degisken_mayin1.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar1[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            for (int i = 0; i < 80; i++)//zor seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol2 != 0)
                {
                    degisken_mayin2 = new PictureBox();

                    degisken_mayin2.Top = MayinlarTop2[i];
                    degisken_mayin2.Left = MayinlarLeft2[i];
                    degisken_mayin2.BackColor = Color.Yellow;
                    degisken_mayin2.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar2[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            bos = new PictureBox();
            bos.Width = 20;
            bos.Height = 20;
            bos.Top = 380;
            bos.Left = 180;
            bos.BackColor = Color.White;
            bos.Image = resim[0];

            pictureBox3.Controls.Add(bos);

            SolaGit = 16; YukariGit = 0;
            Yon = 'R';
            Carpma = false; //çarpma durumunu false yapıyoruz.
            OyunDurumu = true; //oyunu aktif hale getiriyoruz.
            SonrakiDeger = 0;

            KarakterOlustur(); //karakteri oluşturuyoruz.
            YolEkle(); //karakteri ekrana ekliyoruz.
            MayinOlustur(); //kolay seviye mayın oluşturuyoruz.
            kontrol++;

        }
        int kontrol1 = 0;
        private void radioButton2_CheckedChanged(object sender, EventArgs e)//program çalıştığında orta seviye seçili ise.
        {

            for (int i = 0; i < 40; i++)//kolay seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol != 0)
                {
                    degisken_mayin = new PictureBox();

                    degisken_mayin.Top = MayinlarTop[i];
                    degisken_mayin.Left = MayinlarLeft[i];
                    degisken_mayin.BackColor = Color.Yellow;
                    degisken_mayin.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            for (int i = 0; i < 50; i++)//orta seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol1 != 0)
                {
                    degisken_mayin1 = new PictureBox();

                    degisken_mayin1.Top = MayinlarTop1[i];
                    degisken_mayin1.Left = MayinlarLeft1[i];
                    degisken_mayin1.BackColor = Color.Yellow;
                    degisken_mayin1.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar1[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            for (int i = 0; i < 80; i++)//zor seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol2 != 0)
                {
                    degisken_mayin2 = new PictureBox();

                    degisken_mayin2.Top = MayinlarTop2[i];
                    degisken_mayin2.Left = MayinlarLeft2[i];
                    degisken_mayin2.BackColor = Color.Yellow;
                    degisken_mayin2.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar2[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            bos = new PictureBox();
            bos.Width = 20;
            bos.Height = 20;
            bos.Top = 380;
            bos.Left = 180;
            bos.BackColor = Color.White;
            bos.Image = resim[0];

            pictureBox3.Controls.Add(bos);

            SolaGit = 16; YukariGit = 0;
            Yon = 'R';
            Carpma = false;
            OyunDurumu = true;
            SonrakiDeger = 0;

            KarakterOlustur();
            YolEkle();
            MayinOlustur1();
            kontrol1++;
        }
        int kontrol2 = 0;
        private void radioButton3_CheckedChanged(object sender, EventArgs e)//program çalıştığında zor seviye seçili ise.
        {


            for (int i = 0; i < 40; i++)//kolay seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol != 0)
                {
                    degisken_mayin = new PictureBox();

                    degisken_mayin.Top = MayinlarTop[i];
                    degisken_mayin.Left = MayinlarLeft[i];
                    degisken_mayin.BackColor = Color.Yellow;
                    degisken_mayin.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            for (int i = 0; i < 50; i++)//orta seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol1 != 0)
                {
                    degisken_mayin1 = new PictureBox();

                    degisken_mayin1.Top = MayinlarTop1[i];
                    degisken_mayin1.Left = MayinlarLeft1[i];
                    degisken_mayin1.BackColor = Color.Yellow;
                    degisken_mayin1.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar1[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            for (int i = 0; i < 80; i++)//zor seviyeden kalan önceki mayınları siliyor.
            {
                if (kontrol2 != 0)
                {
                    degisken_mayin2 = new PictureBox();

                    degisken_mayin2.Top = MayinlarTop2[i];
                    degisken_mayin2.Left = MayinlarLeft2[i];
                    degisken_mayin2.BackColor = Color.Yellow;
                    degisken_mayin2.Image = null;
                    pictureBox3.Controls.Remove(Mayinlar2[i]);
                    Parca[SonDeger].Image = null;
                }
            }

            bos = new PictureBox();
            bos.Width = 20;
            bos.Height = 20;
            bos.Top = 380;
            bos.Left = 180;
            bos.BackColor = Color.White;
            bos.Image = resim[0];

            pictureBox3.Controls.Add(bos);

            SolaGit = 16; YukariGit = 0;
            Yon = 'R';
            Carpma = false;
            OyunDurumu = true;
            SonrakiDeger = 0;

            KarakterOlustur();
            YolEkle();
            MayinOlustur2();
            kontrol2++;
        }

        private void button7_Click(object sender, EventArgs e)//sağa git butonu.
        {
            if (OyunDurumu) //oyun aktifse koşulu sağlıyor.
            {
                timer1.Enabled = true; //zamanı çalıştırıyoruz.
                if (sayac1 == 0)
                {
                    for (int i = 0; i < 2; i++)//karakteri ekrana getirmek için gerekli dongü.
                    {
                        YolEkle();
                    }
                }
                if(bos.BackColor == Color.White) //ekrana eklenen bos picturebox ını siliyoruz.
                {
                    bos.BackColor = Color.SkyBlue;
                    bos.Image = null;
                    pictureBox3.Controls.Remove(bos);
                }

                if (Parca.Length == 2)//karakteri ekrana getirmek için gerekli koşul.
                {
                    SolaGit = 0; YukariGit = 0;
                    YukariGit = -20; //yukarı git komutu.
                    Yon = 'U'; //yön değişkenini U yapıyoruz.
                    KarakterHareket(YukariGit, SolaGit); //karakteri hareket ettiriyoruz.
                    YolEkle();//karaktere ekleme yapıyoruz.
                }
                //karakter ekrandaysa aşağıdaki kodları çalıştır.
                SolaGit = 0; YukariGit = 0;
                SolaGit = 20;
                Yon = 'R';
                KarakterHareket(YukariGit, SolaGit);
                YolEkle();
                CarpmaKontrol();//butona her basıldığında kenara çarpma kontrolu yaptıran koşul.
                if (radioButton1.Checked)
                {
                    MayinCarpmaKontrol();
                    MayinKontrol();
                }
                else if(radioButton2.Checked)
                {
                    MayinCarpmaKontrol1();
                    MayinKontrol1();
                }
                else if(radioButton3.Checked)
                {
                    MayinCarpmaKontrol2();
                    MayinKontrol2();
                }
                
                for (int i = 0; i < Parca.Length - 2; i++)//karakterin her zaman son değerine resim atıyor.
                {
                    Parca[SonDeger].Image = resim[0];
                    Parca[i].Image = null;
                }
                for(int i=0;i<Parca.Length-1;i++)
                {
                    if(Parca[SonDeger].Left==Parca[i].Left&&Parca[SonDeger].Top==Parca[i].Top)
                    {
                        Parca[i].Image = resim[0];
                    }
                }
                if (Carpma)//kenara fazla gittiyse gittiği yönün tersine çalışan koşul.
                {
                    SolaGit = 0; YukariGit = 0;
                    SolaGit = -20;
                    Yon = 'L';
                    KarakterHareket(YukariGit, SolaGit);
                    Parca[SonDeger-2].Image = resim[0];
                }
                Carpma = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)//sola git butonu.
        {
            //burdaki kodların hepsi sağa git butonu kodlarının hepsiyle aynı mantıkta çalışıyor.
            if (OyunDurumu)
            {
                timer1.Enabled = true;
                if (sayac1 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        YolEkle();
                    }
                }

                if (bos.BackColor == Color.White)
                {
                    bos.BackColor = Color.SkyBlue;
                    bos.Image = null;
                    pictureBox3.Controls.Remove(bos);
                }

                if (Parca.Length == 2)
                {
                    SolaGit = 0; YukariGit = 0;
                    YukariGit = -20;
                    Yon = 'U';
                    KarakterHareket(YukariGit, SolaGit);
                    YolEkle();
                }

                SolaGit = 0; YukariGit = 0;
                SolaGit = -20;
                Yon = 'L';
                KarakterHareket(YukariGit, SolaGit);
                YolEkle();
                CarpmaKontrol();
                if (radioButton1.Checked)
                {
                    MayinCarpmaKontrol();
                    MayinKontrol();
                }
                else if (radioButton2.Checked)
                {
                    MayinCarpmaKontrol1();
                    MayinKontrol1();
                }
                else if (radioButton3.Checked)
                {
                    MayinCarpmaKontrol2();
                    MayinKontrol2();
                }

                for (int i = 0; i < Parca.Length - 2; i++)
                {
                    Parca[SonDeger].Image = resim[0];
                    Parca[i].Image = null;
                }
                for (int i = 0; i < Parca.Length - 1; i++)
                {
                    if (Parca[SonDeger].Left == Parca[i].Left && Parca[SonDeger].Top == Parca[i].Top)
                    {
                        Parca[i].Image = resim[0];
                    }
                }
                if (Carpma)
                {
                    SolaGit = 0; YukariGit = 0;
                    SolaGit = 20;
                    Yon = 'R';
                    KarakterHareket(YukariGit, SolaGit);
                    Parca[SonDeger - 2].Image = resim[0];
                }
                Carpma = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)//yukarı git butonu.
        {
            //burdaki kodların hepsi sağa git butonu kodlarının hepsiyle aynı mantıkta çalışıyor.
            if (OyunDurumu)
            {
                timer1.Enabled = true;
                if (sayac1==0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        YolEkle();
                    }
                }

                if (bos.BackColor == Color.White)
                {
                    bos.BackColor = Color.SkyBlue;
                    bos.Image = null;
                    pictureBox3.Controls.Remove(bos);
                }

                if (Parca.Length == 2)
                {
                    SolaGit = 0; YukariGit = 0;
                    YukariGit = -20;
                    Yon = 'U';
                    KarakterHareket(YukariGit, SolaGit);
                    YolEkle();
                }

                SolaGit = 0; YukariGit = 0;
                YukariGit = -20;
                Yon = 'U';
                KarakterHareket(YukariGit, SolaGit);
                YolEkle();
                CarpmaKontrol();
                if (radioButton1.Checked)
                {
                    MayinCarpmaKontrol();
                    MayinKontrol();
                }
                else if (radioButton2.Checked)
                {
                    MayinCarpmaKontrol1();
                    MayinKontrol1();
                }
                else if (radioButton3.Checked)
                {
                    MayinCarpmaKontrol2();
                    MayinKontrol2();
                }
                for (int i = 0; i < Parca.Length - 2; i++)
                {
                    Parca[SonDeger].Image = resim[0];
                    Parca[i].Image = null;
                }
                for (int i = 0; i < Parca.Length - 1; i++)
                {
                    if (Parca[SonDeger].Left == Parca[i].Left && Parca[SonDeger].Top == Parca[i].Top)
                    {
                        Parca[i].Image = resim[0];
                    }
                }
                if (Carpma)
                {
                    SolaGit = 0; YukariGit = 0;
                    YukariGit = 20;
                    Yon = 'D';
                    KarakterHareket(YukariGit, SolaGit);
                    Parca[SonDeger - 2].Image = resim[0];
                }
                Carpma = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)//aşağı git butonu.
        {
            //burdaki kodların hepsi sağa git butonu kodlarının hepsiyle aynı mantıkta çalışıyor.
            if (OyunDurumu)
            {
                timer1.Enabled = true;
                if (sayac1 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        YolEkle();
                    }
                }

                if (bos.BackColor == Color.White)
                {
                    bos.BackColor = Color.SkyBlue;
                    bos.Image = null;
                    pictureBox3.Controls.Remove(bos);
                }

                if (Parca.Length == 2)
                {
                    SolaGit = 0; YukariGit = 0;
                    YukariGit = -20;
                    Yon = 'U';
                    KarakterHareket(YukariGit, SolaGit);
                    YolEkle();
                }

                SolaGit = 0; YukariGit = 0;
                YukariGit = 20;
                Yon = 'D';
                KarakterHareket(YukariGit, SolaGit);
                YolEkle();
                CarpmaKontrol();
                if (radioButton1.Checked)
                {
                    MayinCarpmaKontrol();
                    MayinKontrol();
                }
                else if (radioButton2.Checked)
                {
                    MayinCarpmaKontrol1();
                    MayinKontrol1();
                }
                else if (radioButton3.Checked)
                {
                    MayinCarpmaKontrol2();
                    MayinKontrol2();
                }

                for (int i=0;i<Parca.Length-2;i++)
                {
                    Parca[SonDeger].Image = resim[0];
                    Parca[i].Image = null;
                }
                for (int i = 0; i < Parca.Length - 1; i++)
                {
                    if (Parca[SonDeger].Left == Parca[i].Left && Parca[SonDeger].Top == Parca[i].Top)
                    {
                        Parca[i].Image = resim[0];
                    }
                }
                if (Carpma)
                {
                    SolaGit = 0; YukariGit = 0;
                    YukariGit = -20;
                    Yon = 'U';
                    KarakterHareket(YukariGit, SolaGit);
                    Parca[SonDeger - 2].Image = resim[0];
                }
                Carpma = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)//süre için gerekli kodlar.
        {
            label6.Text = ((Convert.ToString(dakika) + ":") + Convert.ToString(saniye));
            if ((saniye == 59))//saniye 59  olduğunda koşula giriyor.
            {
                saniye = 0;//saniyeyi sıfırlıyor.
                dakika = dakika + 1;//dakikayı 1 arttırıyor.
                if (dakika == 60)
                {
                    saniye = 0;
                    dakika = 0;
                }
            }
            saniye = saniye + 1;//her seferinde saniye 1 artıyor.
        }
        int dakika = 0;
        int saniye = 1;
        int kayıtkontrol=0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            label7.Text = textBox1.Text;
            MessageBox.Show(textBox1.Text + " kaydedildi.");
            kayıtkontrol++;//kişiyi kaydet butonuna basılınca dosya kaydetme için gerekli değişken.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            oku();//skorları görüntüle butonuna basınca çalışan dosyadan oku fonksiyonu.
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Created By Muhammet Hüseyin Salcı\n\nOynanış: Butonlara bastığınızda süre başlıyor\nve karakterimizi gizli olan mayınlara basmadan\nen üst noktaya getirmeye çalışıyorsunuz.\n\nÜst kısımda yazan maynlara yakınlık bölümünde\nbulunduğunuz konumda etrafınızda kaç mayın\nolduğunu gösteriyor.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            timer1.Interval = 1000; //timer kaçar kaçar artacak belirliyoruz.
        }
    }
}
