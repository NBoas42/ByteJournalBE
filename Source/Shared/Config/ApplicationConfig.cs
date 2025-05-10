using System.Collections.Generic;
using System.IO;

public class AppConfig{
    private readonly Dictionary<string, string> settings;

    public AppConfig(string configDir){
        settings = new Dictionary<string, string>();
        this.LoadSettings(configDir);
    }

    private void LoadSettings(string filePath){
        foreach (var line in File.ReadAllLines(filePath)){
            if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#")){
                continue;
            }

            var parts = line.Split('=', 2);
            if (parts.Length == 2){
                settings[parts[0].Trim()] = parts[1].Trim();
            }
        }
    }

    public string Get(string key)
    {
        return settings.TryGetValue(key, out var value) ? value : null;
    }

    public int GetInt(string key)
    {
        return int.TryParse(Get(key), out var value) ? value : 0;
    }
}