using System;
using System.Collections.Generic;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public interface IMoeDeloListEntity<T>
    {
        List<T> GetList(string[] args);
    }
}