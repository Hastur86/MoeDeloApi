using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoeDeloApi.Logger;
using MoeDeloApi.Services;
using MoeDeloApi.Services.Repositories;

namespace MoeDeloApi
{
    class Program
    {
        private static string ApiKey = "544cc416-8f6c-4e4e-9732-89faeb7e156b";
        private static string MainUrl = "https://restapi.moedelo.org";
        private static ILogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            /*OperationRepository operationRepository = new OperationRepository(MainUrl, ApiKey, Logger);

            string[] param = { "01.01.2025", "17.12.2025", "1" };
            operationRepository.Update(param);

            string[] param1 = { "01.01.2025", "17.12.2025"};
            var entity = operationRepository.Get(param1);
            
            Logger.Log("Возвращена запись - "+ entity.Count);
            //operationRepository.Save();*/

            KontragentRepository kontragentRepository = new KontragentRepository(MainUrl, ApiKey, Logger);

            string[] param = { };
            kontragentRepository.Update(param);
            var entity = kontragentRepository.Get(param);

            Logger.Log("Возвращено записей - " + entity.Count);
            kontragentRepository.Save();

            Console.ReadKey();
        }
    }
}
