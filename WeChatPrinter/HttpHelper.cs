using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeChatPrinter
{
    class HttpHelper
    {
        public static void WriteJson(HttpListenerResponse response, int status, string message, object sbj = null,string respTime="")

        {

            response.Headers["Access-Control-Allow-Origin"] = "*";

            response.ContentType = "application/json;charset=utf8";

            var data = new { code = status, msg = message, data = sbj,respTime=respTime };

            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            using (System.IO.Stream output = response.OutputStream)

            {

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(strJson);

                response.ContentLength64 = buffer.Length;

                output.Write(buffer, 0, buffer.Length);

                output.Flush();

            }

            response.Close();

        }
    }
}
