namespace Task1.ConfigurationProvider;

public class InMemoryConfigurationProvider : IConfigurationProvider
{
    private Dictionary<string, string> _configs = new();
    
    public void Save(PropertyInfo propertyInfo, string settingName, ModelWithProperties specifiedObject)
    {
        if (_configs.ContainsKey(settingName))
        {
            _configs[settingName] = propertyInfo.GetValue(specifiedObject).ToString();
            return;
        }

        _configs.Add(settingName, propertyInfo.GetValue(specifiedObject).ToString());
    }

    public void Load(PropertyInfo propertyInfo, string settingName, ModelWithProperties specifiedObject)
    {
        if (_configs.ContainsKey(settingName))
        {
            propertyInfo.SetValue(specifiedObject, _configs[settingName]);
            return;
        }

        Console.WriteLine("Error reading from config");
    }
}