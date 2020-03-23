namespace MUDhub.Prototype.Server.Controllers
{
    public class CreateRoomArgs
    {
        public (string Name, int X, int Y)[] Rooms { get; set; }

    }
}