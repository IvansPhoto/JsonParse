using System.Text.Json;

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

var watch = new System.Diagnostics.Stopwatch();
watch.Start();

foreach (var filePath in directory)
{
    FileStream file = null!;
    JsonDocument json = null!;
    try
    {
        file = File.OpenRead(filePath);
        json = JsonDocument.Parse(file);
        var isFound = true;
        var currentNode = json.RootElement;
        Console.Write(string.Concat(filePath.AsSpan(filePath.LastIndexOf('\\') + 1, filePath.Length - (filePath.LastIndexOf('\\') + 1)), ": "));

        foreach (var t in keys)
        {
            if (currentNode.TryGetProperty(t, out var node))
            {
                currentNode = node;
            }
            else
            {
                isFound = false;
                break;
            }
        }

        if (isFound)
            Console.WriteLine(currentNode);
        else
            Console.WriteLine("None");
    }
    finally
    {
        file!.Dispose();
        json!.Dispose();
    }
}

watch.Stop();
Console.WriteLine($"Elapsed time: {watch.Elapsed} Press Enter to exit.");
Console.ReadLine();


string TrimStr(string str) => str.Trim();

bool SelectJson(string str) => str.Contains(".json", StringComparison.InvariantCultureIgnoreCase) &&
                               str.IndexOf(".json", StringComparison.InvariantCultureIgnoreCase) == str.Length - 5;