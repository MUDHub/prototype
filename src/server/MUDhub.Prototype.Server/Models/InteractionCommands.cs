using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Models
{
    public enum InteractionCommand
    {
        //Area Navigations
        GoWest,
        GoNorth,
        GoEast,
        GoSouth,
        JoinPortal, //Also JoinDungeon

        //Room Interactions
        InspectRoom,
        StartFight,
        TalkToNpc,
        PickItem,

        TalkToPlayer,
    }
}
