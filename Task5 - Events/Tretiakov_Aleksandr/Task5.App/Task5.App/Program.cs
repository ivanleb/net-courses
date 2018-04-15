using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.Core;
using Task5.App.Implementations;
using Task5.Core.Models;

namespace Task5.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var messager = new ConsoleMessenger();
            var inputProcess = new ConsoleProcessUserInput(messager);
            var consoleHandler = new ConsoleBoardHandler()
            {
                Board = new Board {SizeX = 100, SizeY = 20},
                Hero = new Hero() {PositionX = 10, PositionY = 10, Symbol = "+"},
                Bombs = new List<Bomb>()
                {
                    new Bomb() {PositionX = 12, PositionY = 15, Symbol = "x"},
                    new Bomb() {PositionX = 50, PositionY = 15, Symbol = "x"},
                    new Bomb() {PositionX = 56, PositionY = 11, Symbol = "x"},
                    new Bomb() {PositionX = 34, PositionY = 5, Symbol = "x"},
                    new Bomb() {PositionX = 8, PositionY = 19, Symbol = "x"}
                }
            };
            new AppLogic(messager, consoleHandler, inputProcess).Run();
        }
    }
}
