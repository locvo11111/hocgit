using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VChatCore.Dto
{
    public class NotificationDto
    {
        public string UserCode { get; set; }
        public List<string> Notifications { get; set; }
    }
}
