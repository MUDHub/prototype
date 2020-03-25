using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public interface IChatClientContract
    {
        Task ReceiveGlobalMessage(string message, string username);
        Task ReceivePrivateMessage(string message, string username);
    }
}
