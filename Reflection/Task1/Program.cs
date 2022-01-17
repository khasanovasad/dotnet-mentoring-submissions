namespace Task1;

public class Program
{
    private static InMemoryConfigurationProvider? _fileConfigurationProvider;
    private static ConfigurationManagerConfigurationProvider? _configurationManagerConfigurationProvider;

    public static void Main(string[] args)
    {
        _fileConfigurationProvider = new InMemoryConfigurationProvider();
        _configurationManagerConfigurationProvider = new ConfigurationManagerConfigurationProvider();

        var model = new ModelWithProperties()
        {
            Prop1 = "1",
            Prop2 = "2",
        };
        
        WorkWithModel(model,
            (propertyInfo, settingName) => { _fileConfigurationProvider.Save(propertyInfo, settingName, model); },
            (propertyInfo, settingName) => { _configurationManagerConfigurationProvider.Save(propertyInfo, settingName, model); });
        
        // creating empty model to read from config
        var newModel = new ModelWithProperties();
        
        WorkWithModel(newModel,
            (propertyInfo, settingName) => { _fileConfigurationProvider.Load(propertyInfo, settingName, newModel); },
            (propertyInfo, settingName) => { _configurationManagerConfigurationProvider.Load(propertyInfo, settingName, newModel); });
        
        Console.WriteLine("model --> \n\tProp1: {0}, Prop2: {1}", model.Prop1, model.Prop2);
        Console.WriteLine("newModel --> \n\tProp1: {0}, Prop2: {1}", newModel.Prop1, newModel.Prop2);
    }

    private static void WorkWithModel(ModelWithProperties model, Action<PropertyInfo, string> fileConfigAction, Action<PropertyInfo, string> configManagerAction)
    {
        var properties = model.GetType().GetProperties();
        
        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes(true);
            
            foreach (var propertyAttribute in attributes)
            {
                if (propertyAttribute is ConfigurationItemAttribute attribute)
                {
                    if (attribute.ProviderType == typeof(InMemoryConfigurationProvider))
                    {
                        fileConfigAction(property, attribute.SettingName);
                    }
                    else if (attribute.ProviderType == typeof(ConfigurationManagerConfigurationProvider))
                    {
                        configManagerAction(property, attribute.SettingName);
                    }
                }
            }
        }
    }
}