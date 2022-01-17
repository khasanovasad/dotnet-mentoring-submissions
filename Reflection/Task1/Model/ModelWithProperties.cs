namespace Task1.Model;

public class ModelWithProperties
{
    [ConfigurationItem("SettingForProp1", typeof(ConfigurationManagerConfigurationProvider))]
    public string Prop1 { get; set; }
    
    [ConfigurationItem("SettingForProp2", typeof(InMemoryConfigurationProvider))]
    public string Prop2 { get; set; }
    
}