using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
namespace WeTeam.Infrastucture.Rest
{
    public class RestService
    {
        public static void Call(string url, string key, string text)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", "key=AIzaSyCUWTJKeBl0mBcI9uLWPLQf1EL2HsOACuo");
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = $@"{{
                   ""notification"": {{
                     ""title"": ""El probador {text} está esperándote!"",
                     ""body"": ""Recuerda que perderás la reserva si tardas."",
                     ""click_action"": ""http://localhost:8081"",
                     ""icon"": ""http://www.alt64.org/wiki/images/Firefox-128.png"",
                     ""vibrate"": [200, 100, 200, 100, 200, 100, 200],
                     ""data"": ""DETALLES DE LA RESERVA""
                   }},
                   ""to"": ""{key}""
                 }}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
