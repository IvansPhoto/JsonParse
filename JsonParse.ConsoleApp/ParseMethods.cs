using System.Text.Json;

namespace JsonParse.ConsoleApp;

public static class ParseMethods
{
    public static async Task ReadAsyncParallel(string[] dir, string[] keys)
    {
        await Parallel.ForEachAsync(dir, AsyncParallel);

        async ValueTask AsyncParallel(string filePath, CancellationToken token)
        {
            FileStream file = null!;
            JsonDocument json = null!;
            try
            {
                file = File.OpenRead(filePath);
                json = await JsonDocument.ParseAsync(file, cancellationToken: token);
                var isFound = true;
                var currentNode = json.RootElement;
                Console.Write(string.Concat(
                    filePath.AsSpan(filePath.LastIndexOf('\\') + 1, filePath.Length - (filePath.LastIndexOf('\\') + 1)), ": "));

                foreach (var key in keys)
                {
                    if (currentNode.TryGetProperty(key.AsSpan(), out var node))
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
            catch (Exception)
            {
                Console.WriteLine($"The file '{filePath}' is not a JSON.");
            }
            finally
            {
                await file.DisposeAsync();
                json.Dispose();
            }
        }
    }
    
    public static async Task ReadAsync(string[] dir, string[] keys)
    {
        foreach (var filePath in dir)
        {
            FileStream file = null!;
            JsonDocument json = null!;
            try
            {
                file = File.OpenRead(filePath);
                json = await JsonDocument.ParseAsync(file);
                var isFound = true;
                var currentNode = json.RootElement;
                Console.Write(string.Concat(filePath.AsSpan(filePath.LastIndexOf('\\') + 1, filePath.Length - (filePath.LastIndexOf('\\') + 1)), ": "));

                foreach (var t in keys)
                {
                    if (currentNode.TryGetProperty(t.AsSpan(), out var node))
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
            catch(Exception)
            {
                Console.WriteLine($"The file '{filePath}' is not a JSON.");
            }
            finally
            {
                await file.DisposeAsync();
                json.Dispose();
            }
        }
    }

    public static void ReadSyncParallel(string[] directory, string[] keys)
    {
        Parallel.ForEach(directory, SyncParallel);

        void SyncParallel(string filePath)
        {
            FileStream file = null!;
            JsonDocument json = null!;
            try
            {
                file = File.OpenRead(filePath);
                json = JsonDocument.Parse(file);
                var isFound = true;
                var currentNode = json.RootElement;
                Console.Write(string.Concat(
                    filePath.AsSpan(filePath.LastIndexOf('\\') + 1,
                        filePath.Length - (filePath.LastIndexOf('\\') + 1)), ": "));

                foreach (var key in keys)
                {
                    if (currentNode.TryGetProperty(key.AsSpan(), out var node))
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
            catch (Exception)
            {
                Console.WriteLine($"The file '{filePath}' is not a JSON.");
            }
            finally
            {
                file.Dispose();
                json.Dispose();
            }
        }
    }

    public static void ReadSync(string[] dir, string[] keys)
    {
        foreach (var filePath in dir)
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

                foreach (var key in keys)
                {
                    if (currentNode.TryGetProperty(key.AsSpan(), out var node))
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
            catch(Exception)
            {
                Console.WriteLine($"The file '{filePath}' is not a JSON.");
            }
            finally
            {
                file.Dispose();
                json.Dispose();
            }
        }
    }
}