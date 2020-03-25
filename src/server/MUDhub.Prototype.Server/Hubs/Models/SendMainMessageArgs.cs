using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs.Models
{
    public class SendMainMessageArgs
    {
        public ChannelScope Scope { get; set; } = ChannelScope.Public;
        public string Message { get; set; } = string.Empty;

    }
}
