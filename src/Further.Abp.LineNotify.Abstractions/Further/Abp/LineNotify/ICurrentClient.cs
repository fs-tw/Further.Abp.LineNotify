using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public interface ICurrentClient
    {
        string ClientKey { get; set; }
        IDisposable SetCurrent(string clientKey);
    }
}
