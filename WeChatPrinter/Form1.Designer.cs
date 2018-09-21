using System.Drawing.Printing;

namespace WeChatPrinter
{
    partial class Form1
    {

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            int MARGIN_LEFT = 0;
            int MARGIN_RIGHT = 0;
            int MARGIN_TOP = 0;
            int MARGIN_BOTTOM = 0;
            this.button1 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 129);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "照片打印机";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument2_PrintPage);
            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("L 88X125mm", 350, 500);
            this.printDocument1.DefaultPageSettings.Margins = new Margins(MARGIN_LEFT, MARGIN_RIGHT, MARGIN_TOP, MARGIN_BOTTOM);

            //xxx
            //var resolution = new PrinterResolution();
            //resolution.Kind = PrinterResolutionKind.Custom;
            //resolution.X = 100;
            //resolution.Y = 100;
            //this.printDocument1.DefaultPageSettings.PrinterResolution = resolution;
            //打印开始前            // 
            // Form1

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 348);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "照片打印机";
            this.Text = "照片打印机";
            this.ResumeLayout(false);

        }

        public void reloadPrint()
        {
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument2_PrintPage);
        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Drawing.Printing.PrintDocument printDocument1;
    }
}

