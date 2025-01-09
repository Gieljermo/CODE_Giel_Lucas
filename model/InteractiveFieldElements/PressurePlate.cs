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
            // Itereer over elke deur in de lijst van deuren
            foreach (var currentDoor in connection.Doors)
            {
                // Controleer of de deur een toggable deur is (dekorator)
                if (currentDoor is DoorToggleDecorator toggleableDoor)
                {
                    // Wijzig de status van de deur
                    toggleableDoor.ChangeDoorStatus(entity, field.Room);
                    // Aangezien we alleen de eerste toggable deur willen wijzigen, breken we hier de loop
                    break;
                }
            }
        }


    }
}
