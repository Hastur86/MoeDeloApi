using System.Collections.Generic;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public interface IMoeDeloDatabase<T>
    {
        ILogger Logger { get; set; }

        List<T> Load();
        bool Save(List<T> data);
    }
}