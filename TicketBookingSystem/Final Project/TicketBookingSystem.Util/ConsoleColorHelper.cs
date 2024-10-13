using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Util
{
    public class ConsoleColorHelper
    {
       
        public static void SetHeadingColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        
        public static void SetSubheadingColor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

       
        public static void SetSuccessColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        
        public static void SetErrorColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        
        public static void SetInfoColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

       
        public static void ResetColor()
        {
            Console.ResetColor();
        }
    }

}
