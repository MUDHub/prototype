using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Models
{
    public class GameAction
    {
        public InteractionCommand Command { get; set; }
        public string TargetUid { get; set; } = string.Empty;

        public string DesciptionMessage { get; set; } = string.Empty;

    }
}
