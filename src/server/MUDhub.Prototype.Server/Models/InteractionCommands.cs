using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Models
{
    [Flags]
    public enum InteractionCommand
    {
        //Area Navigations
        GoWest = 1,
        GoNorth = 2,
        GoEast = 4,
        GoSouth = 8,
        JoinPortal = 16, //Also JoinDungeon

        //Room Interactions
        InspectRoom = 32,
        StartFight = 64,
        TalkToNpc = 128,
        PickItem = 256,

        //TalkToPlayer,
    }
}
