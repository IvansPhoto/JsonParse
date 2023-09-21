using System.Diagnostics;
using JsonParse.ConsoleApp;

Console.WriteLine("Enter a path to a JSON file. Press Enter to use the current directory.");
var path = Console.ReadLine();
if (string.IsNullOrEmpty(path))
    path = Directory.GetCurrentDirectory();

var directory = Directory.GetFiles(path).Where(SelectJson).ToArray();

string[] keys;
do
{
    Console.WriteLine("Enter Keys with ',' separator.");
    keys = Console.ReadLine()!.Split(',').Select(TrimStr).ToArray();
} while (keys.Length is 0);

SetupMethod:
string? method;
do
{
    Console.WriteLine(@"Enter a method to parse:\n S - for Sync\n SP - for Sync parallel\n A - for Async\n AP - for Async parallel\n .");
    method = Console.ReadLine();
} while (string.IsNullOrEmpty(method));

var watch = new Stopwatch();
watch.Start();

switch (method)
{
    case "S":
        ParseMethods.ReadSync(directory, keys);
        break;
    case "SP":
        ParseMethods.ReadSyncParallel(directory, keys);
        break;
    case "A":
        await ParseMethods.ReadAsync(directory, keys);
        break;
    case "AP":
        await ParseMethods.ReadAsyncParallel(directory, keys);
        break;
    default:
        Console.WriteLine("The letter is wrong.");
        goto SetupMethod;
}

watch.Stop();
Console.WriteLine($"Elapsed time: {watch.Elapsed} Press Enter to exit.");
Console.ReadLine();


string TrimStr(string str) => str.Trim();

bool SelectJson(string str) => str.Contains(".json", StringComparison.InvariantCultureIgnoreCase) &&
                               str.IndexOf(".json", StringComparison.InvariantCultureIgnoreCase) == str.Length - 5;


           