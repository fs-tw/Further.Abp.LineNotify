using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class StatusResult
    {
        public int status { get; set; }
        public string message { get; set; }
        public string targetType { get; set; }

        public string target { get; set; }
    }


}
