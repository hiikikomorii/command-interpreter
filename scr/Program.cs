namespace ConsoleApp3;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

class Program
{
    
    static void Main()
    {
        Console.Title = "Hidlow_v01";
        Console.ForegroundColor = ConsoleColor.Blue;
        
        var commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);

        commands["exit"] = () => Environment.Exit(0);
        commands["clear"] = () =>
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Beep();
        };
        
        commands["time"] = () =>
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"Date: {now:dddd}, {now:dd.MM.yyyy}");
            Console.WriteLine($"Time: {now:HH:mm:ss}");
        };
        
        commands["ip"] = () =>
        {
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine(ip);
            }
        };
        
        commands["info"] = () =>
        {
            Console.WriteLine("OS:                      " + RuntimeInformation.OSDescription);
            Console.WriteLine("OS Arch:                 " + RuntimeInformation.OSArchitecture);
            Console.WriteLine("OS version:              " + Environment.OSVersion);
            Console.WriteLine("Largest Window Size:     " + Console.LargestWindowWidth + "x" + Console.LargestWindowHeight);
            Console.WriteLine("Processor Arch:          " + RuntimeInformation.ProcessArchitecture);
            Console.WriteLine("Processor count:         " + Environment.ProcessorCount);
            Console.WriteLine("Machine name:            " + Environment.MachineName);
            Console.WriteLine("User name:               " + Environment.UserName);
            Console.WriteLine("User profile:            " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

        };
        
        commands["help"] = () =>
        {
            Console.WriteLine("Commands:");
            foreach (var cmd in commands.Keys)
                Console.WriteLine($"- {cmd}");
        };

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (commands.TryGetValue(input, out var action))
                action();
            else
                Console.WriteLine("Unknown command. Type 'help'");
        }
    }
}