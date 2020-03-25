using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public interface IChatClientContract
    {
        void ReceiveGlobalMessage(string message, string username);
        void ReceivePrivateMessage(string message, string username);
    }
}
