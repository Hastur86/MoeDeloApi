using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoeDeloApi.Logger;
using MoeDeloApi.Services;

namespace MoeDeloApi
{
    class Program
    {
        private static string ApiKey = "544cc416-8f6c-4e4e-9732-89faeb7e156b";
        private static string MainUrl = "https://restapi.moedelo.org";
        private static ILogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            MonyService monyService = new MonyService(MainUrl,ApiKey,Logger);

            string[] param = { "01.01.2025", "17.02.2025", "1", "16" };
            var entity = monyService.GetList(param);
            Logger.Log("Возвращена запись - "+ entity.Count);
            Console.ReadKey();
        }
    }
}
