using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsDW
{
    public interface IConsole
    {
        void Write(string message);
        void WriteLine(string message);
        string ReadLine();
        void Clear();
    }

    public class ConsoleWrapper : IConsole
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }

    public class ConsoleTestWrapper : IConsole
    {
        public List<String> LinesToRead = new();

        public void Write(string message)
        {
        }

        public void WriteLine(string message)
        {
        }

        public string ReadLine()
        {
            string result = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return result;
        }

        public void Clear()
        {
        }
    }
}
