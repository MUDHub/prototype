namespace MUDhub.Prototype.Server.Hubs.Models
{
    public class ActionRequestResult
    {
        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; } = null;
    }
}