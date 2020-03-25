using System.Security.Principal;

namespace MUDhub.Prototype.Server.Services.Models
{
    public class NavigationResult
    {


        public NavigationResult(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }


        public string Message { get; }

        public bool Succeeded { get; }
    }
}