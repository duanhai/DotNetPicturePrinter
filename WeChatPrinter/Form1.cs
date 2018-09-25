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
            //WebRequest webreq = WebRequest.Create("http://xiaowechatprinter.duapp.com/GetUrl");
            //WebResponse webres = webreq.GetResponse();
            //Stream stream = webres.GetResponseStream();
            //string imgUrl = new StreamReader(stream, Encoding.GetEncoding("utf-8")).ReadToEnd();
            //stream.Close();

            if (null == imgUrl || !imgUrl.StartsWith("http"))
            {
                //MessageBox.Show("Not found imgUrl:\n" + imgUrl);
                e.Cancel = true;
                return;
            }
            //string imgUrl = "http://192.168.1.239:8000/h1.jpg";

            try
            {
                //WebRequest webreq = WebRequest.Create(imgUrl);
                //WebResponse webres = webreq.GetResponse();
                //Stream stream = webres.GetResponseStream();
                Image i;
                //i = Image.FromStream(stream);
                //stream.Close();
                //this.img = i;

                i = this.img;
                if (i == null)
                {
                    return;
                }
                Rectangle m = e.MarginBounds;

                /*
                int x = m.X;
                int y = m.Y;
                int width = (int)i.Width;
                int height = (int)i.Height;

                if ((double)i.Width / (double)i.Height > (double)m.Width / (double)m.Height) // image is wider
                {
                    m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                }
                else
                {
                    m.Width = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                }
                Rectangle destRect = new Rectangle(0, 0, i.Width, i.Height);

                //e.Graphics.DrawImage(i, m);
                */

                i = ScaleImage(i, 294, 420); //.Size = e.PageBounds.Size;
                Point loc = new Point(10, 15);
                //e.Graphics.DrawImage(i, 0,0,e.PageSettings.PrintableArea.Width,e.PageSettings.PrintableArea.Height);
                e.Graphics.DrawImage(i, loc);


                i.Dispose();
                //e.Graphics.DrawImage(i,16,16,i.Width,i.Height);
                //e.Graphics.DrawImage(i, destRect);

                /*
                //在图片中打印的矩形区域
                int oldX = e.MarginBounds.X;
                int oldY = e.MarginBounds.Y;
                int oldWidth = e.MarginBounds.Width;
                int oldHeight = e.MarginBounds.Height;
                int newX = e.MarginBounds.X;
                int newY = e.MarginBounds.Y;
                int newWidth = oldWidth;
                int newHeight = oldHeight;

                //理论的打印高度
                int tempHeight = oldWidth * img.Height / img.Width;

                //如果理论打印高度大于纸张的高度，也就是照片偏窄
                if (tempHeight > oldHeight)
                {
                    newHeight = oldHeight;
                    newWidth = newHeight * i.Width / i.Height;
                    newX = oldX + (oldWidth - newWidth) / 2;
                    newY = oldY;
                }

                //如果理论打印高度小于纸张的高度，也就是照片偏宽
                else
                {
                    newWidth = oldWidth;
                    newHeight = oldWidth * i.Height / i.Width;
                    newY = oldY + (oldHeight - newHeight) / 2;
                    newX = oldX;
                }


                Console.WriteLine("======================================");
                Console.WriteLine(e.MarginBounds);
                Console.WriteLine(newX + " - " + newY + " - " + newWidth + " - " + newHeight);
                Console.WriteLine("======================================");




                //将图像缩放到屏幕中心的位置
                e.Graphics.DrawImage(i, new Rectangle(newX, newY, newWidth, newHeight), new Rectangle(0, 0, i.Width, i.Height), GraphicsUnit.Pixel);
                */

                //printDocument1.OriginAtMargins = true;
                //double cmToUnits = 100 / 2.54;
                //e.Graphics.DrawImage(i, 0, 0, (float)(8.9 * cmToUnits), (float)(12.7 * cmToUnits));

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
    }
}
