namespace Task1.ConfigurationProvider;

public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
{
    public void Save(PropertyInfo propertyInfo, string settingName, ModelWithProperties specifiedObject)
    {
        try
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if (settings[settingName] == null)
            {
                settings.Add(settingName, propertyInfo.GetValue(specifiedObject).ToString());
            }
            else
            {
                settings[settingName].Value = propertyInfo.GetValue(specifiedObject).ToString();
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error writing app settings");
        }
    }

    public void Load(PropertyInfo propertyInfo, string settingName, ModelWithProperties specifiedObject)
    {
        try  
        {  
            var appSettings = ConfigurationManager.AppSettings;
            propertyInfo.SetValue(specifiedObject, appSettings[settingName] ?? "Not Found");
        }  
        catch (ConfigurationErrorsException)  
        {  
            Console.WriteLine("Error reading app settings");  
        }   
    }
}