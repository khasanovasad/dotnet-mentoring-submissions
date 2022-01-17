namespace Task1.ConfigurationProvider;

public interface IConfigurationProvider
{
    void Save(PropertyInfo propertyInfo, string settingName, ModelWithProperties specifiedObject);
    void Load(PropertyInfo propertyInfo, string settingName, ModelWithProperties specifiedObject);
}