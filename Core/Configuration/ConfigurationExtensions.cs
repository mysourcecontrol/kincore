using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinectCore.Shared.Configuration
{
    /// <summary>
    /// This class adds extension methods to IConfiguration making it easier to pull out
    /// Kinect Core configuration options.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// The default section where settings are read from the IConfiguration object. 
        /// </summary>
        public const string DEFAULT_CONFIG_SECTION = "KinectCore";

        /// <summary>
        /// Constructs an KinectCoreOptions class with the options specifed in the "KinectCore" section in the IConfiguration object.
        /// </summary>
        /// <param name="config"></param>
        /// <returns>The KinectCoreOptions containing the values set in configuration system.</returns>
        public static KinectCoreOptions GetKinectCoreOptions(this IConfiguration config)
        {
            return GetKinectCoreOptions(config, DEFAULT_CONFIG_SECTION);
        }

        public static TConfig ConfigureKinectOptions<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }

        //public static KinectCoreOptions ConfigureDefaultKinectOptions(this IServiceCollection services, IConfiguration configuration)
        //{
        //    if (services == null) throw new ArgumentNullException(nameof(services));

        //    var config = new KinectCoreOptions();
        //    configuration.Bind(config);
        //    services.AddSingleton(config);
        //    return config;
        //}

        /// <summary>
        /// Constructs an KinectCoreOptions class with the options specifed in the "KinectCore" section in the IConfiguration object.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configSection">The config section to extract KinectCore options from.</param>
        /// <returns>The KinectCoreOptions containing the values set in configuration system.</returns>
        public static KinectCoreOptions GetKinectCoreOptions(this IConfiguration config, string configSection)
        {
            var options = new KinectCoreOptions();

            IConfiguration section;
            if (string.IsNullOrEmpty(configSection))
                section = config;
            else
                section = config.GetSection(configSection);

            if (section == null)
                return options;

            //var clientConfigTypeInfo = typeof(ClientConfig).GetTypeInfo();
            //foreach (var element in section.GetChildren())
            //{
            //    try
            //    {
            //        var property = clientConfigTypeInfo.GetDeclaredProperty(element.Key);
            //        if (property == null || property.SetMethod == null)
            //            continue;

            //        if (property.PropertyType == typeof(string) || property.PropertyType.GetTypeInfo().IsPrimitive)
            //        {
            //            var value = Convert.ChangeType(element.Value, property.PropertyType);
            //            property.SetMethod.Invoke(options.DefaultClientConfig, new object[] { value });
            //        }
            //        else if (property.PropertyType == typeof(TimeSpan) || property.PropertyType == typeof(Nullable<TimeSpan>))
            //        {
            //            var milliSeconds = Convert.ToInt64(element.Value);
            //            var timespan = TimeSpan.FromMilliseconds(milliSeconds);
            //            property.SetMethod.Invoke(options.DefaultClientConfig, new object[] { timespan });
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        throw new ConfigurationException($"Error reading value for property {element.Key}.", e)
            //        {
            //            PropertyName = element.Key,
            //            PropertyValue = element.Value
            //        };
            //    }
            //}

            //if (!string.IsNullOrEmpty(section["Profile"]))
            //{
            //    options.Profile = section["Profile"];
            //}
            //// Check legacy name if the new name isn't set
            //else if (!string.IsNullOrEmpty(section["AWSProfileName"]))
            //{
            //    options.Profile = section["AWSProfileName"];
            //}

            //if (!string.IsNullOrEmpty(section["ProfilesLocation"]))
            //{
            //    options.ProfilesLocation = section["ProfilesLocation"];
            //}
            //// Check legacy name if the new name isn't set
            //else if (!string.IsNullOrEmpty(section["AWSProfilesLocation"]))
            //{
            //    options.ProfilesLocation = section["AWSProfilesLocation"];
            //}

            //if (!string.IsNullOrEmpty(section["Region"]))
            //{
            //    options.Region = RegionEndpoint.GetBySystemName(section["Region"]);
            //}
            //// Check legacy name if the new name isn't set
            //else if (!string.IsNullOrEmpty(section["AWSRegion"]))
            //{
            //    options.Region = RegionEndpoint.GetBySystemName(section["AWSRegion"]);
            //}

            return options;
        }
    }
}
