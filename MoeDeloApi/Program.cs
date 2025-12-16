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

            var entity = monyService.Get("784196140");
            Logger.Log("Возвращена запись - "+ entity.Contractor.Name);
            Console.ReadKey();
        }
    }
}
