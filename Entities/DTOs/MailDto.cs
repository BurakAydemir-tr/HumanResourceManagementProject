using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MailDto
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
