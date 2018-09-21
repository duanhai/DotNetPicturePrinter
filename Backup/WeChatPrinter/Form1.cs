using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WeChatPrinter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            WebRequest webreq = WebRequest.Create("http://xiaowechatprinter.duapp.com/GetUrl");
            WebResponse webres = webreq.GetResponse();
            Stream stream = webres.GetResponseStream();
            string imgUrl = new StreamReader(stream, Encoding.GetEncoding("utf-8")).ReadToEnd();
            stream.Close();

            if (null == imgUrl || !imgUrl.StartsWith("http"))
            {
                MessageBox.Show("Not found imgUrl:\n" + imgUrl);
                e.Cancel = true;
                return;
            }

            webreq = WebRequest.Create(imgUrl);
            webres = webreq.GetResponse();
            stream = webres.GetResponseStream();
            Image image;
            image = Image.FromStream(stream);
            stream.Close();
            e.Graphics.DrawImage(image, new Point(1,1));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}
