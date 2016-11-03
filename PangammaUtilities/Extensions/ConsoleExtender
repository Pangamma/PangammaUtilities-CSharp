using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangammaUtilities.Extensions
{
    public static class ConsoleExtender
    {
        public static void PrintLine(this object o, ConsoleColor? textColor = null, ConsoleColor? bgColor = null)
        {
            Print(o+"\r\n", textColor, bgColor);
        }
        public static void Print(this object o,ConsoleColor? textColor = null, ConsoleColor? bgColor = null)
        {
            textColor = textColor ?? Console.ForegroundColor;
            bgColor = bgColor ?? Console.BackgroundColor;
            var origColorT = Console.ForegroundColor;
            var origColorB = Console.BackgroundColor;
            Console.ForegroundColor = textColor.Value;
            Console.BackgroundColor = bgColor.Value;
            Console.Write(o);
            Console.ForegroundColor = origColorT;
            Console.BackgroundColor = origColorB;
        }
    }
}
