using ManyConsole;
using System;

namespace VeInteractive.TechTest.OneTimePassword.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleCommandDispatcher.DispatchCommand(ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program)), args, Console.Out);
        }
    }
}
