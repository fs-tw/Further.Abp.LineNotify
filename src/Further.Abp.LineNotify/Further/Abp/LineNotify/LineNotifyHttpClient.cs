using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class LineNotifyHttpClient : LineNotifyHttpClientBase
    {
        public LineNotifyHttpClient(
            IOptions<ServiceDefinitionOptions> options,
            IHttpClientFactory httpClientFactory)
            : base(options, httpClientFactory)
        {
        }
    }
}
