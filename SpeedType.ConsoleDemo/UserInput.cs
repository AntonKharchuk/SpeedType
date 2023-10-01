using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedType.ConsoleDemo
{
    internal class UserInput : IUserInput
    {
        public void ChangeInputColor(ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
        }
    }
}
