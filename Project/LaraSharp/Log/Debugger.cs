using System;
using System.Text.RegularExpressions;

namespace Adventofcode_day1.Log
{
    public class Debugger
    {
        private void PrintWithHighlight(string message, string prefix, ConsoleColor prefixColor, ConsoleColor messageColor)
        {
            Console.Write("[");
            Console.ForegroundColor = prefixColor;
            Console.Write(prefix);
            Console.ResetColor();
            Console.Write("] ");

            // Regular expression to match URLs
            var urlRegex = new Regex(@"(http[s]?://[\w\.-/]+)");
            var matches = urlRegex.Matches(message);
            int lastIndex = 0;

            foreach (Match match in matches)
            {
                // Print text before URL
                Console.ForegroundColor = messageColor;
                Console.Write(message.Substring(lastIndex, match.Index - lastIndex));
                Console.ResetColor();

                // Print URL in a special color (now changed to red)
                Console.ForegroundColor = ConsoleColor.Red; // Changed URL color to red
                Console.Write(match.Value);
                Console.ResetColor();

                lastIndex = match.Index + match.Length;
            }

            // Print remaining part of the message
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message.Substring(lastIndex));
            Console.ResetColor();
        }

        public void DebugError(string error, string prefix = "ERROR")
        {
            PrintWithHighlight(error, prefix, ConsoleColor.Red, ConsoleColor.White);
        }

        public void DebugInfo(string info, string prefix = "INFO")
        {
            PrintWithHighlight(info, prefix, ConsoleColor.Cyan, ConsoleColor.White);
        }

        public void DebugWarning(string warning, string prefix = "WARNING")
        {
            PrintWithHighlight(warning, prefix, ConsoleColor.Yellow, ConsoleColor.White);
        }

        public void DebugSuccess(string success, string prefix = "SUCCESS")
        {
            PrintWithHighlight(success, prefix, ConsoleColor.Green, ConsoleColor.White);
        }
    }
}
