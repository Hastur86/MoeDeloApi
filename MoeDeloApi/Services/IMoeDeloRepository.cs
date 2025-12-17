using System.Collections.Generic;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public interface IMoeDeloRepository<T>
    {
        string MainUrl { get; set; }
        string ApiKey { get; set; }
        ILogger Logger { get; set; }
        IMoeDeloDatabase<T> Db { get; set; }

        List<T> Entities { get; set; }

        List<T> Get(string[] args);
        bool Update(string[] args);
        bool Save();
    }
}