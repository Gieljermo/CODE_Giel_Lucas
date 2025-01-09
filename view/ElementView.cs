using Domain;
using Domain.Decorators;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempleOfDoom.model;
using TempleOfDoom.model.Enums;
using TempleOfDoom.model.Interfaces;

namespace GameView
{
    public class ElementView
    {
        public void DrawItem(IInteractiveFieldElement item)
        {
            if (item is IDrawable drawableItem)
            {
                string colorName = drawableItem.GetColor();
                if (Enum.TryParse(colorName, true, out ConsoleColor consoleColor))
                {
                    Console.ForegroundColor = consoleColor;
                }
                Console.Write($" {drawableItem.GetSymbol()}");
                Console.ResetColor();
            }
        }

        public void DrawPortal(Portal item)
        {
            if (item is IDrawable drawableItem)
            {
                string colorName = drawableItem.GetColor();
                if (Enum.TryParse(colorName, true, out ConsoleColor consoleColor))
                {
                    Console.ForegroundColor = consoleColor;
                }
                Console.Write($" {drawableItem.GetSymbol()}");
                Console.ResetColor();
            }
        }

        public void DrawDoor(IDoor door)
        {
            if (IsValidColor(door.GetColor()) && door.GetSymbol() != null)
            {
                Console.ForegroundColor = ParseColor(door.GetColor());
                Console.Write($" {door.GetSymbol()}");
            }
        }

        private bool IsValidColor(string colorName)
        {
            return Enum.TryParse(colorName, true, out ConsoleColor _);
        }

        private ConsoleColor ParseColor(string colorName)
        {
            Enum.TryParse(colorName, true, out ConsoleColor consoleColor);
            return consoleColor;
        }

    }
}
