//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;

//namespace Further.Abp.LineNotify
//{
//    public class LineNotifyLoggerHandler: DelegatingHandler
//    {
//        private readonly ILogger<LineNotifyLoggerHandler> logger;
//        private readonly IOptions<ServiceDefinitionOptions> options;

//        public LineNotifyLoggerHandler(
//            HttpClientHandler innerHandler,
//            ILogger<LineNotifyLoggerHandler> logger,
//            IOptions<ServiceDefinitionOptions> options)
//            :base(innerHandler)
//        {
//            this.logger = logger;
//            this.options = options;
//        }
//        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
//        {
//            //if (options.Value.EnableLogger)
//            //{
//            //    this.logger.LogInformation($"準備發送訊息至Line Notify， RequestContent: {request.Content}");
//            //}

//            var response = await base.SendAsync(request, cancellationToken);

//            //if (options.Value.EnableLogger)
//            //{
//            //    var message = await response.Content.ReadAsStringAsync();
                

//            //    if (response.IsSuccessStatusCode)
//            //    {
//            //        this.logger.LogInformation($"發送訊息至Line Notify成功， ResponseContent: {message}");
//            //    }
//            //    else
//            //    {
//            //        this.logger.LogError($"發送訊息至Line Notify失敗， ResponseContent: {message}");
//            //    }
//            //}
//            return response;
//        }
//    }
//}
