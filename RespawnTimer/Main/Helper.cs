using System.IO;
using System.IO.Compression;
using System.Net;
using Exiled.API.Features;
using RespawnTimer.API.Features;

namespace RespawnTimer.Main;

public class Helper
{
    public static string RespawnTimerDirectoryPath => Path.Combine(Paths.Configs, "RespawnTimer");

    public static void FileManager()
    {
        if (!Directory.Exists(RespawnTimerDirectoryPath))
        {
            // Log.Warn("RespawnTimer directory does not exist. Creating...");
            Log.Info("RespawnTimer directory does not exist. Creating...");
            Directory.CreateDirectory(RespawnTimerDirectoryPath);
        }

        string exampleTimerDirectory = Path.Combine(RespawnTimerDirectoryPath, "ExampleTimer");
        if (!Directory.Exists(exampleTimerDirectory))
            Helper.DownloadExampleTimer(exampleTimerDirectory);
    }

    public static void ReloadTimer()
    {
        if (RespawnTimer.Instance.Config.Timers.IsEmpty())
        {
            Log.Error("Timer list is empty!");
            return;
        }

        TimerManager.CachedTimers.Clear();

        foreach (string name in RespawnTimer.Instance.Config.Timers.Values)
        {
            TimerManager.AddTimer(name);
        }
    }
    
    public static void DownloadExampleTimer(string exampleTimerDirectory)
    {
        string exampleTimerZip = exampleTimerDirectory + ".zip";
        string exampleTimerTemp = exampleTimerDirectory + "_Temp";

        using WebClient client = new();

        // Log.Warn("Downloading ExampleTimer.zip...");
        Log.Info("Downloading ExampleTimer.zip...");
        string url = $"https://github.com/Saskyc/RespawnTimer/releases/download/v{RespawnTimer.Instance.Version}/ExampleTimer.zip";
        try
        {
            client.DownloadFile(url, exampleTimerZip);
        }
        catch (WebException e)
        {
            if (e.Response is HttpWebResponse response)
                Log.Error($"Error while downloading ExampleTimer.zip: {(int)response.StatusCode} {response.StatusCode}");
                
            return;
        }

        Log.Info("ExampleTimer.zip has been downloaded!");

        // Log.Warn("Extracting...");
        Log.Info("Extracting...");
        ZipFile.ExtractToDirectory(exampleTimerZip, exampleTimerTemp);
        Directory.Move(Path.Combine(exampleTimerTemp, "ExampleTimer"), exampleTimerDirectory);

        Directory.Delete(exampleTimerTemp);
        File.Delete(exampleTimerZip);

        Log.Info("Done!");
    }
}