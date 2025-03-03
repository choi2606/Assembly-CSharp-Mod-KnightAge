using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class ConfigManager
{
    private Dictionary<string, AccountConfig> _configs;

    public ConfigManager()
    {
        _configs = new Dictionary<string, AccountConfig>();
    }

    public void Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _configs = JsonConvert.DeserializeObject<Dictionary<string, AccountConfig>>(json);
        }
    }

    public AccountConfig GetConfig(string accountId)
    {
        if (_configs.TryGetValue(accountId, out var config))
        {
            return config;
        }
        return null;
    }

    public void SetConfig(string accountId, AccountConfig config)
    {
        _configs[accountId] = config;
    }

    public void Save(string filePath)
    {
        string json = JsonConvert.SerializeObject(_configs, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
