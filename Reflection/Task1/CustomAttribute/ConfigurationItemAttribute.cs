namespace Task1.CustomAttribute;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class ConfigurationItemAttribute : Attribute
{
    public string SettingName { get; set; }
    public Type ProviderType { get; set; }

    public ConfigurationItemAttribute(string settingName, Type providerType)
    {
        SettingName = settingName;
        ProviderType = providerType;
    }
}