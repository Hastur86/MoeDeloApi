using System.Collections.Generic;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public interface IMoeDeloRepository<T>
    {
        IMoeDeloDatabase<T> Db { get; set; }

        List<T> Entities { get; set; }

        List<T> Get(string[] args);
        bool Update(string[] args);
        bool Save();
    }
}