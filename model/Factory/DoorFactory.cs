using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model.Decorators;

namespace TempleOfDoom.model.Factory
{
    public class DoorFactory
    {
        public IDoor CreateDecoratedDoor(IEnumerable<DoorJson> doorProperties, Room room = null)
        {
            IDoor decoratedDoor = new Door();

            foreach (var doorProperty in doorProperties)
            {
                decoratedDoor = doorProperty.type switch
                {
                    "colored" when !string.IsNullOrEmpty(doorProperty.color) =>
                        new DoorColorDecorator(decoratedDoor, doorProperty.color),

                    "open on stones in room" =>
                        new DoorOnStonesDecorator(decoratedDoor, doorProperty.no_of_stones),

                    "open on odd" =>
                        new OpenOnOddDoorDecorator(decoratedDoor),

                    "toggle" =>
                        new DoorToggleDecorator(decoratedDoor),

                    "closing gate" =>
                        new ClosingGateDoorDecorator(decoratedDoor),

                    _ => decoratedDoor
                };
            }

            return decoratedDoor;
        }
    }
}
