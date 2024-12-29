using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Decorators;
using Domain.Interfaces;
using TempleOfDoom.model;

namespace Domain
{
    public class PressurePlate : IItem
    {
        private string _type;
        public string Type { get => _type; set => _type = value; }
        public int Damage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int X { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Interact(Player player, Field field, Room room)
        {
            foreach (var connection in room.Connections)
            {
                var currentDoor = connection.Door;
                while (currentDoor != null)
                {
                    if (currentDoor is DoorToggleDecorator toggleableDoor)
                    {
                        toggleableDoor.OpenDoor(player, room);
                        break;
                    }

                    currentDoor = (currentDoor as BaseDoorDecorator)?._wrappee;
                }
            }
        }
    }
}
