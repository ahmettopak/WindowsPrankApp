using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrankApp
{
    public partial class Form1 : Form
    {
        //public Form1()
        //{
        //    InitializeComponent();
        //    this.KeyPreview = true;
        //    this.BackColor = Color.Magenta; // Formun rengi herhangi bir renk olabilir, burada örnek olması için Magenta seçilmiştir
        //    this.TransparencyKey = Color.Magenta;

        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);

        //}
        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.X)
        //    {
        //        // Ctrl+X tuş kombinasyonuna basıldığında uygulamayı kapat
        //        this.Close();
        //    }
        //}
        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    //e.Cancel = true;
        //}

        // Windows API fonksiyonlarına erişim için gerekli P/Invoke tanımlamaları
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        public Form1()
        {
            InitializeComponent();

            // Formun arka planını masaüstünün ekran görüntüsü ile ayarla
            SetDesktopAsBackground();
        }

        private void SetDesktopAsBackground()
        {
            // Masaüstü penceresinin boyutlarını ve konumunu al
            RECT desktopRect;
            GetWindowRect(GetDesktopWindow(), out desktopRect);

            // Masaüstünün ekran görüntüsünü al
            Bitmap desktopScreenshot = new Bitmap(desktopRect.Right - desktopRect.Left, desktopRect.Bottom - desktopRect.Top);
            Graphics g = Graphics.FromImage(desktopScreenshot);
            g.CopyFromScreen(desktopRect.Left, desktopRect.Top, 0, 0, desktopScreenshot.Size);

            // Formun arka planına masaüstünün ekran görüntüsünü ayarla
            this.BackgroundImage = desktopScreenshot;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

    }
}
