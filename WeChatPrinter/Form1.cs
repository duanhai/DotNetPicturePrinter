using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Drawing.Printing;
namespace WeChatPrinter
{
    public partial class Form1 : Form
    {
        public string imgUrl;
        public Image img=null;
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            //double ratio = 0.85;
            //double ratioYY = 1.0;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        public Form1()
        {
            InitializeComponent();
            //printDocument1.Print();
            initPrinter();
            CheckForIllegalCrossThreadCalls = false;
            string myIpAddress = IpHelper.GetLocalIP();
            this.label1.Text = "ip地址： " + myIpAddress;

        }

       
        private void initPrinter()
        {
            //xxx
            //var resolution = new PrinterResolution();
            //resolution.Kind = PrinterResolutionKind.Custom;
            //resolution.X = 100;
            //resolution.Y = 100;
            //this.printDocument1.DefaultPageSettings.PrinterResolution = resolution;
            //打印开始前            // 
            // Form1
            int MARGIN_LEFT = 0;
            int MARGIN_RIGHT = 0;
            int MARGIN_TOP = 0;
            int MARGIN_BOTTOM = 0;
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument2_PrintPage);
            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("L 88X125mm", 350, 500);
            this.printDocument1.DefaultPageSettings.Margins = new Margins(MARGIN_LEFT, MARGIN_RIGHT, MARGIN_TOP, MARGIN_BOTTOM);
        }
        private void PrintDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            if (null == imgUrl || !imgUrl.StartsWith("http"))
            {
                //MessageBox.Show("Not found imgUrl:\n" + imgUrl);
                e.Cancel = true;
                return;
            }
            //string imgUrl = "http://192.168.1.239:8000/h1.jpg";

            try
            {

                Image i;
   

                i = this.img;
                this.pictureBox1.Image = i;
                if (i == null)
                {
                    return;
                }
                Rectangle m = e.MarginBounds;


                i = ScaleImage(i, 294, 420); //.Size = e.PageBounds.Size;
                Point loc = new Point(10, 15);
                //e.Graphics.DrawImage(i, 0,0,e.PageSettings.PrintableArea.Width,e.PageSettings.PrintableArea.Height);
                e.Graphics.DrawImage(i, loc);


                i.Dispose();


            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
            



            //e.Graphics.DrawImage(image, new Point(1, 1));

        }

        //
        // 摘要:
        //     引发 System.Drawing.Printing.PrintDocument.BeginPrint 事件。 之后调用 System.Drawing.Printing.PrintDocument.Print
        //     调用方法之前打印文档的第一页。
        //
        // 参数:
        //   e:
        //     包含事件数据的 System.Drawing.Printing.PrintEventArgs。
        protected virtual void OnBeginPrint(PrintEventArgs e)
        {
            MessageBox.Show("打印开始");
        }


        public void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        public string  PreviewDialog(string imgUrlstr, Image theImg = null)
        {
            if (imgUrlstr == "")
            {
                return "imgUrl 为空";
            }

            this.imgUrl = imgUrlstr;
            this.img = theImg;
            reloadPrint();
            try
            {
                var printPriview = new PrintPreviewDialog
                {
                    Document = printDocument1,
                    WindowState = FormWindowState.Maximized
                };
                printPriview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印错误，请检查打印设置！");
            }

            return "";
        }

        public string Test(string imgUrlstr,Image theImg=null)
        {
            if (imgUrlstr == "")
            {
                return "imgUrl 为空";
            }
            this.imgUrl = imgUrlstr;
            this.img = theImg;
            this.richTextBox1.Text = imgUrl;
            reloadPrint();
            //MessageBox.Show(PrintHelper.GetPrinterStatus(printDocument1.PrinterSettings.PrinterName));

            //MessageBox.Show("imgUrl:\n" + imgUrl);

            //执行该方法才会进入打印对应的回调函数

            if (this.img!=null)
            {
                printDocument1.Print();
                return "发送打印指令成功";
            }else
            {
                return "打印机下载图片失败";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
