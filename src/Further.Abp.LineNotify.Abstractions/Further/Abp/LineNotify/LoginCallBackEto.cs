using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class LoginCallBackEto
    {
        public string State { get; set; } = null!;
        public string ConfiguratorsName { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string ResultUrl { get; set; } = null!;
    }
}
