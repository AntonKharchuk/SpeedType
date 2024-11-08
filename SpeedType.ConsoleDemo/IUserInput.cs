using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedType
{
    public interface IUserInput
    {
        void ChangeInputColor(ConsoleColor consoleColor);
    }
    public interface ILemuelInput:IUserInput
    {
        public IEnumerable<string> GetWords();
    }
}
