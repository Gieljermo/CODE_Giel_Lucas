using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Decorators;
using Domain.Interfaces;
using TempleOfDoom.model;
using TempleOfDoom.model.Adapter;
using TempleOfDoom.model.Interfaces;

namespace Domain
{
    public class PressurePlate : Item
    {

        public override char? GetSymbol() => 'T';
        public PressurePlate(string type, IPosition position)
           : base(type, position)
        {
            this.Type = type;
        }

        public override void Interact(IEntity entity, Field field)
        {
            foreach (var connection in field.Room.Connections)
            {
                ChangeStatusOfToggleDoors(entity, field, connection);
            }

           
        }

        private void ChangeStatusOfToggleDoors(IEntity entity, Field field, Connection connection)
        {
            foreach (var currentDoor in connection.Doors)
            {
                if (currentDoor is DoorToggleDecorator toggleableDoor)
                { 
                    toggleableDoor.ChangeDoorStatus(entity, field.Room);
                    break;
                }
            }
        }


    }
}
