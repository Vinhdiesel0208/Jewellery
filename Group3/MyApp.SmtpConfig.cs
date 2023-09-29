using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group3.SmtpConfig
{
    public class SmtpConfig
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
