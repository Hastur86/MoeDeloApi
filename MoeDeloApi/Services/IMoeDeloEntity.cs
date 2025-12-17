using System.Collections.Generic;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public interface IMoeDeloEntity<T>
    {
        string MainUrl { get; set; }
        string ApiKey { get; set; }
        ILogger Logger { get; set; }

        T Get(string id);
        List<T> GetList(string[] args);
        bool Set(T entity);
    }
}