using System;
using System.IO;
using System.Net;
using System.Text;
using MoeDeloApi.Logger;
using Newtonsoft.Json;

namespace MoeDeloApi.Services
{
    public class IMoeDeloApiGetCommand<T>
    {
        private string MainUrl = "";
        private string ApiKey = "";
        private ILogger Logger;

        public IMoeDeloApiGetCommand(string mainUrl, string apiKey, ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
        }

        public T Get(string[] parameters, string[] args)
        {
            try
            {
                string url = MainUrl;

                if (parameters != null && args != null)
                {
                    if(parameters.Length == 1) url = url + parameters[0] + args[0];
                    else
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (i == 0) url = url + "?" + parameters[i] + "=" + args[i];
                            else url = url + "&" + parameters[i] + "=" + args[i];
                        }
                    }
                }

                Logger.Log($"URL запроса: {url}");

                // Создаем WebRequest
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("md-api-key", ApiKey);
                request.Accept = "application/json";
                request.Timeout = 30000;

                // Получаем ответ
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    Console.WriteLine("Получен ответ. Статус: " + response.StatusCode);

                    // Используем UTF-8
                    Encoding encoding = Encoding.UTF8;

                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream, encoding))
                    {
                        string content = reader.ReadToEnd();
                        Console.WriteLine("Получено " + content.Length + " символов ответа");

                        // Парсим JSON ответ
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log("Ошибка вызова метода GET "
                           + MainUrl
                           + "с параметрами "
                           + string.Join(", ", args)
                           + e.Message);
                throw;
            }
        }
    }
}