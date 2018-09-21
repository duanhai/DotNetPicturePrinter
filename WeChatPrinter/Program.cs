﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using System.Web.Http;
//using System.Web.Http.SelfHost;
using System.Net;
using System.Text;
using System.Management;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Web;

namespace WeChatPrinter
{
    public class Otherinfo
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int Code  { set; get; }
    }
    static class Program
    {
        private static HttpListener listener;

        private static Form1 fm1;
        private static string infomationStr=null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {



            //var config = new HttpSelfHostConfiguration("http://localhost:5000"); //配置主机
            //config.Routes.MapHttpRoute(    //配置路由
            //    "API Default", "api/{controller}/{id}",
            //    new { id = RouteParameter.Optional });

            //using (HttpSelfHostServer server = new HttpSelfHostServer(config)) //监听HTTP
            //{
            //    server.OpenAsync().Wait(); //开启来自客户端的请求


            //}

            if (listener == null)
            {
                listener = new HttpListener();
                var url = "http://+:8080/";
                listener.Prefixes.Add(url);
                listener.Start();
                listener.BeginGetContext(MainProcess, null);
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fm1 = new Form1();
            Application.Run(fm1);


        }

        private static void MainProcess(IAsyncResult ar)
        {
            var other = new Otherinfo();

            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(MainProcess, null);
            var response = context.Response;
            response.AddHeader("Server", "My Server V0.0.1");
            var request = context.Request;

            



            if (request.HttpMethod == "POST")
            {
                string text;
                using (var reader = new StreamReader(request.InputStream,
                                                     request.ContentEncoding))
                {
                    text = reader.ReadToEnd();
                    //var rawData = request.RawUrl;
                    //using System.Web and Add a Reference to System.Web
                    //解析 x-www-form-urlencode
                    Dictionary<string, string> postParams = new Dictionary<string, string>();

                    //post body 有值才解析你
                    if (text.Length > 0)
                    {
                        string[] rawParams = text.Split('&');
                        foreach (string param in rawParams)
                        {
                            string[] kvPair = param.Split('=');
                            string key = kvPair[0];
                            string value = HttpUtility.UrlDecode(kvPair[1]);
                            postParams.Add(key, value);
                        }
                    }
                   
                    

                    //打印逻辑
                    if (postParams.ContainsKey("imgUrl") && (request.RawUrl == "/api/print"))
                    {
                        //MessageBox.Show(postParams["imgUrl"]);

                        string imgUrl = postParams["imgUrl"];
                        if (imgUrl == null)
                        {
                            return;
                        }
                        //MessageBox.Show("imgUrl:\n" + imgUrl);
                        try
                        {
                            WebRequest webreq = WebRequest.Create(imgUrl);
                            WebResponse webres = webreq.GetResponse();
                            Stream stream = webres.GetResponseStream();
                            Image i;
                            i = Image.FromStream(stream);
                            stream.Close();
                            if (i == null)
                            {
                                infomationStr = "图片下载失败";
                                return;
                            }
                            else
                            {
                                string infoStr = PrintHelper.GetPrinterStatus(fm1.printDocument1.PrinterSettings.PrinterName);
                                if (infoStr == "准备就绪（Ready）")
                                {
                                    infomationStr = fm1.Test(imgUrl, i);
                                    other.Name = "打印机状态";
                                    other.Code = 200;
                                    other.Description = "正常";
                                }
                                else
                                {
                                    infomationStr = infoStr;
                                    other.Code = 400;
                                    other.Name = "打印机状态";
                                    other.Description = "打印机状态异常，请检查";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            infomationStr = ex.Message;

                        }



                        infomationStr += DateTime.Now.ToString();
                    }
                    //获取状态
                    else if(request.RawUrl == "/api/status")
                    {
                        infomationStr = PrintHelper.GetPrinterStatus(fm1.printDocument1.PrinterSettings.PrinterName);
                        infomationStr += DateTime.Now.ToString();
                        other = null;
                    }


                    //MessageBox.Show(postParams.ToString());
                }
            }else if (request.HttpMethod == "GET")
            {
                var path = request.Url.LocalPath;
                if (path.StartsWith("/") || path.StartsWith("\\"))
                    path = path.Substring(1);
                var sb = new StringBuilder("输入请求:");
                sb.AppendLine(path);
                var visit = path.Split(new char[] { '/', '\\' }, 2);
                if (visit.Length > 0)
                {


                    var cmd = visit[0].ToLower();
                    sb.AppendLine(string.Format("执行命令:{0}", cmd));
                    if (cmd == "print")
                    {
                        //fm1.Test("http://192.168.1.239:8000/h1.jpg");
                        string imgUrl = request.QueryString["imgUrl"];
                        if (imgUrl == null)
                        {
                            return;
                        }
                        //MessageBox.Show("imgUrl:\n" + imgUrl);
                        try
                        {
                            WebRequest webreq = WebRequest.Create(imgUrl);
                            WebResponse webres = webreq.GetResponse();
                            Stream stream = webres.GetResponseStream();
                            Image i;
                            i = Image.FromStream(stream);
                            stream.Close();
                            if (i == null)
                            {
                                infomationStr = "图片下载失败";
                                return;
                            }
                            else
                            {
                                string infoStr = PrintHelper.GetPrinterStatus(fm1.printDocument1.PrinterSettings.PrinterName);
                                if (infoStr == "准备就绪（Ready）")
                                {
                                    infomationStr = fm1.Test(imgUrl, i);
                                    other.Name = "打印机状态";
                                    other.Code = 200;
                                    other.Description = "正常";
                                }
                                else
                                {
                                    infomationStr = infoStr;
                                    other.Code = 400;
                                    other.Name = "打印机状态";
                                    other.Description = "打印机状态异常，请检查";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            infomationStr = ex.Message;

                        }



                        infomationStr += DateTime.Now.ToString();

                    }
                    else if (cmd == "status")
                    {
                        //MessageBox.Show(PrintHelper.GetPrinterStatus(fm1.printDocument1.PrinterSettings.PrinterName));
                        infomationStr = PrintHelper.GetPrinterStatus(fm1.printDocument1.PrinterSettings.PrinterName);
                        infomationStr += DateTime.Now.ToString();
                        other = null;

                    }
                    //sb.AppendLine(string.Format("另外有{0}个参数", visit.Length - 1 + request.QueryString.Count));
                }
                //var result = Encoding.UTF8.GetBytes(sb.ToString());

            }

            response.KeepAlive = true;

            response.ContentEncoding = System.Text.Encoding.UTF8;

            try

            {

                HttpHelper.WriteJson(response, 200, infomationStr,other);//默认输出

            }

            catch (ArgumentNullException ex)

            {

                HttpHelper.WriteJson(response, 0, ex.Message);

                Console.WriteLine("{0}", ex.Message);

                return;

            }

            catch (InvalidOperationException ex)

            {

                HttpHelper.WriteJson(response, 0, ex.Message);

                Console.WriteLine("{0}", ex.Message);

                return;

            }

            catch (Exception ex)

            {

                HttpHelper.WriteJson(response, 0, ex.Message);

                Console.WriteLine("{0}", ex.Message);

                return;

            }

            finally

            {
                infomationStr = "";


            }


        }





    }
}