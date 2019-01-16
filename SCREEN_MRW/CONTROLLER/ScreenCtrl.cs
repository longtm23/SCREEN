using Newtonsoft.Json;
using SCREEN_MRW.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SCREEN_MRW.CONTROLLER
{
    public class ScreenCtrl
    {
        public bool GetConfig(string host)
        {
            bool isCheck = false;
            try
            {
                var url = host + "/api/config";
                var jsonSetting = getRequest(host, url);
                var data = JsonConvert.DeserializeObject<Config>(jsonSetting);
                if (data != null)
                {
                    isCheck = true;
                }
            }
            catch
            {
                isCheck = false;
            }
            return isCheck;
        }
        private static string getRequest(string host, string url)
        {
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 3);
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            string res = "";
            if (response.IsSuccessStatusCode)
            {
                res = response.Content.ReadAsStringAsync().Result;
            }
            return res;
        }
    }
}
