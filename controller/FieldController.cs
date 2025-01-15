using Domain;
using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.adapter;
using TempleOfDoom.model.Factory;

namespace Controlllers
{
    public class FieldController
    {

        public List<Field> CreateFields(Room room, Player player)
        {
            List<Field> fields = new List<Field>();
            for (int y = 0; y < room.Height; y++)
            {
                for (int x = 0; x < room.Width; x++)
                {
                    Field field = new Field();
                    field.X = x;
                    field.Y = y;
                    field.Room = room;

                    SpecialTileBehaviourFactory factory = new SpecialTileBehaviourFactory();
                    if(room.SpecialFloorTiles != null)
                    {
                        var specialTileDef = room.SpecialFloorTiles.FirstOrDefault(tile => tile.x == x && tile.y == y);
                        if (specialTileDef != null)
                        {
                            field.SpecialTileBehaviour = factory.Create(specialTileDef.type);
                        }
                    }

                    if (room.Items != null)
                    {
                        var item = room.Items.Where(ri => ri.X == x).FirstOrDefault(ri => ri.Y == y);
                        if (item != null)
                        {
                            field.Item = item;
                        }
                    }

                    if(room.Enemies != null)
                    {
                        var enemy = room.Enemies.Where(re => re.X == x).FirstOrDefault(re => re.Y == y);
                        if (enemy != null)
                        {
                            if(enemy is OrthogonalEnemyAdapter adaptee)
                            {
                                adaptee.setCurrentField(field);
                            }
                        }
                    }

                    if (x > 0 && x < room.Width - 1 && y > 0 && y < room.Height - 1)
                    {
                        field.IsWall = false;
                    }
                    else
                    {
                        field.IsWall = true;
                    }
                    fields.Add(field);
                }
            }
            return fields;
        }

        public bool isMoveValid(Field nextField)
        {
            if (nextField == null)
            {
                return true;
            }
            if (nextField.IsWall)
            {
                return false;
            }
            return true;
        }
    }
}
