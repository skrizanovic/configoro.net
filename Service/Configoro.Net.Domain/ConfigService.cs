using System;
using System.Collections.Generic;
using System.Linq;
using Configoro.Net.Domain.Interface;


namespace Configoro.Net.Domain
{
    public class ConfigService : IConfigService
    {
        #region Environment Methods

        #region GetEnvironments

        /// <summary>
        /// Get the environments.
        /// </summary>
        /// <returns></returns>
        public List<Environment> GetEnvironments()
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    return db.Environment
                        .Include("ConfigValues")
                        .ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #region AddEnvironment

        /// <summary>
        /// Add the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        public Environment AddEnvironment(Environment environment)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var env = db.Environment.Add(environment);
                    db.SaveChanges();
                    return env;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region UpdateEnvironment

        /// <summary>
        /// Update the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        public Environment UpdateEnvironment(Environment environment)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var env = db.Environment.FirstOrDefault(
                        e => e.EnvironmentId == environment.EnvironmentId);

                    if (env != null)
                    {
                        env.EnvironmentName = environment.EnvironmentName;
                        env.IsActive = environment.IsActive;
                        db.SaveChanges();
                    }
                    return env;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region DeleteEnvironment

        /// <summary>
        /// Delete the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        public Environment DeleteEnvironment(Environment environment)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    if (environment.ConfigValues != null && environment.ConfigValues.Count > 0)
                    {
                        // First, remove mapping
                        foreach (var cv in environment.ConfigValues)
                        {
                            var csv = db.ConfigurationSettingValue.Where(e => 
                                e.ConfigValueId == cv.ConfigValueId).ToList();

                            if (csv.Count <= 0) continue;

                            db.ConfigurationSettingValue.RemoveRange(csv);
                            db.SaveChanges();
                        }

                        // Second, remove config values
                        foreach (var cv in environment.ConfigValues)
                        {
                            db.ConfigValue.Attach(cv);
                            db.ConfigValue.Remove(cv);
                        }
                        db.SaveChanges();
                    }

                    // Finally, remove environment
                    var envToDelete = new Environment {EnvironmentId = environment.EnvironmentId};
                    var env = db.Environment.Attach(envToDelete);
                    db.Environment.Remove(envToDelete);
                    db.SaveChanges();
                    return env;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #endregion

        #region ConfigValue Methods

        #region GetConfigValues

        /// <summary>
        /// Get the configuration values for the specified environment.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns></returns>
        public Response<IEnumerable<ConfigValue>> GetConfigValues(int environmentId)
        {
            Response<IEnumerable<ConfigValue>> response = new Response<IEnumerable<ConfigValue>>();
            try
            {
               

                using (var db = new ConfigClassContext())
                {
                    response.Entity=  db.ConfigValue
                        .Where(e => e.EnvironmentId == environmentId)
                        .OrderBy(p=>p.Name)
                        .ToList();

                   
                    //var selected = (from cv in db.ConfigValue
                    // join csv in db.ConfigurationSettingValue on cv.ConfigValueId equals csv.ConfigValueId
                    // join e in db.Environment on cv.EnvironmentId equals e.EnvironmentId
                    // where csv.ConfigurationSettingId == settingId && e.EnvironmentId == environmentId
                    // select cv).FirstOrDefault();

                    //var selectedValueId = selected != null ? selected.ConfigValueId : 0;

                    //var selectedValues = db.ConfigValue
                    //    .Where(e => e.EnvironmentId == environmentId)
                    //    .ToList();
                    //return new { selectedValueId, selectedValues };
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
            }
            return response;
        }

        #endregion

        #region AddConfigValue

        /// <summary>
        /// Add the configuration value.
        /// </summary>
        /// <param name="configValue">The configuration value.</param>
        /// <returns></returns>
        public ConfigValue AddConfigValue(ConfigValue configValue)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var cv = db.ConfigValue.Add(configValue);
                    db.SaveChanges();
                    return cv;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        #endregion

        #region UpdateConfigValue

        /// <summary>
        /// Update the configuration value.
        /// </summary>
        /// <param name="configValue">The configuration value.</param>
        /// <returns></returns>
        public ConfigValue UpdateConfigValue(ConfigValue configValue)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var cv = db.ConfigValue.FirstOrDefault(e =>
                           e.EnvironmentId == configValue.EnvironmentId 
                        && e.ConfigValueId == configValue.ConfigValueId);

                    if (cv != null)
                    {
                        cv.Name = configValue.Name;
                        cv.Value = configValue.Value;
                        cv.IsActive = configValue.IsActive;

                        db.SaveChanges();
                    }

                    return cv;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region DeleteConfigValue

        /// <summary>
        /// Delete the configuration value.
        /// </summary>
        /// <param name="configValue">The configuration value.</param>
        /// <returns></returns>
        public ConfigValue DeleteConfigValue(ConfigValue configValue)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var mapping = db.ConfigurationSettingValue.Where(e =>
                                            e.ConfigValueId == configValue.ConfigValueId).ToList();

                    if (mapping.Count > 0)
                    {
                        foreach (var entry in mapping)
                        {
                            db.ConfigurationSettingValue.Attach(entry);
                            db.ConfigurationSettingValue.Remove(entry);
                        }

                        db.SaveChanges();
                    }

                    var cv = db.ConfigValue.Attach(configValue);
                    db.ConfigValue.Remove(configValue);
                    db.SaveChanges();

                    return cv;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #endregion

        #region ConfigurationTemplate Methods

        #region GetTemplatesSettings

        /// <summary>
        /// Get the templates settings.
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationTemplate> GetTemplatesSettings()
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var t = db.ConfigurationTemplate
                        .Include("ConfigurationSettings")
                        .ToList();
                    return t;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region AddConfigurationTemplate

        /// <summary>
        /// Add the configuration template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public ConfigurationTemplate AddConfigurationTemplate(ConfigurationTemplate template)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var t = db.ConfigurationTemplate.Add(template);
                    db.SaveChanges();
                    return t;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region UpdateConfigurationTemplate

        /// <summary>
        /// Update the configuration template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public ConfigurationTemplate UpdateConfigurationTemplate(ConfigurationTemplate template)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var t = db.ConfigurationTemplate.FirstOrDefault(e =>
                           e.ConfigurationTemplateId == template.ConfigurationTemplateId);

                    t.TemplateName = template.TemplateName;
                    t.ConfigFile = template.ConfigFile;
                    t.ConfigType = template.ConfigType;
                    t.IsActive = template.IsActive;

                    db.SaveChanges();
                    return t;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region DeleteConfigurationTemplate

        /// <summary>
        /// Delete the configuration template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public ConfigurationTemplate DeleteConfigurationTemplate(ConfigurationTemplate template)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    if (template.ConfigurationSettings != null && template.ConfigurationSettings.Count > 0)
                    {
                        // First, remove mapping
                        foreach (var cs in template.ConfigurationSettings)
                        {
                            var csv = db.ConfigurationSettingValue.Where(e =>
                                e.ConfigurationSettingId == cs.ConfigurationSettingId).ToList();

                            if (csv.Count <= 0) continue;

                            db.ConfigurationSettingValue.RemoveRange(csv);
                            db.SaveChanges();
                        }

                        // Second, remove settings
                        foreach (var cs in template.ConfigurationSettings)
                        {
                            db.ConfigurationSetting.Attach(cs);
                            db.ConfigurationSetting.Remove(cs);
                        }
                        db.SaveChanges();
                    }

                    // Finally, remove template
                    var templToDelete = new ConfigurationTemplate { ConfigurationTemplateId = template.ConfigurationTemplateId };
                    var templ = db.ConfigurationTemplate.Attach(templToDelete);
                    db.ConfigurationTemplate.Remove(templToDelete);
                    db.SaveChanges();
                    return templ;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #endregion

        #region ConfigurationSetting Methods

        #region AddConfigurationSetting

        /// <summary>
        /// Add the configuration setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        public ConfigurationSetting AddConfigurationSetting(ConfigurationSetting setting)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var cs = db.ConfigurationSetting.Add(setting);
                    db.SaveChanges();
                    return cs;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region AddConfigurationSettingValue

        /// <summary>
        /// Adds the configuration setting value.
        /// </summary>
        /// <param name="configurationSettingId">The configuration setting identifier.</param>
        /// <param name="configValueId">The configuration value identifier.</param>
        /// <returns></returns>
        public ConfigurationSettingValue AddConfigurationSettingValue(int configurationSettingId, int configValueId)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var csv = db.ConfigurationSettingValue.Add(
                        new ConfigurationSettingValue
                        {
                            ConfigValueId = configValueId, ConfigurationSettingId = configurationSettingId
                        });
                    db.SaveChanges();
                    db.ConfigValue.Where(p => p.ConfigValueId == configValueId).FirstOrDefault();

                    return csv;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region UpdateConfigurationSetting

        /// <summary>
        /// Update the configuration setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        public ConfigurationSetting UpdateConfigurationSetting(ConfigurationSetting setting, int environmentId)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var cs = db.ConfigurationSetting.FirstOrDefault(e =>
                           e.ConfigurationSettingId == setting.ConfigurationSettingId);

                    if (cs != null)
                    {
                        cs.ChangePropertyName = setting.ChangePropertyName;
                        cs.XpathValue = setting.XpathValue;
                        cs.ProcessorTypeId = setting.ProcessorTypeId;
                        

                        db.SaveChanges();
                    }

                    var csv = db.ConfigurationSettingValue
                        .Include("ConfigurationSetting")
                        .Include("ConfigValue")
                        .Where(p => p.ConfigValue.EnvironmentId == environmentId && 
                            p.ConfigurationSettingId == setting.ConfigurationSettingId).ToList();



                    return cs;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #region DeleteConfigurationSetting

        /// <summary>
        /// Delete the configuration setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        public ConfigurationSetting DeleteConfigurationSetting(ConfigurationSetting setting)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var mapping = db.ConfigurationSettingValue.Where(e=>
                        e.ConfigurationSettingId==setting.ConfigurationSettingId).ToList();

                    if (mapping.Count > 0)
                    {
                        foreach (var entry in mapping)
                        {
                            db.ConfigurationSettingValue.Attach(entry);
                            db.ConfigurationSettingValue.Remove(entry);
                        }
                        db.SaveChanges();
                    }

                    var cs = db.ConfigurationSetting.Attach(setting);
                    db.ConfigurationSetting.Remove(setting);
                    db.SaveChanges();
                    return cs;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #endregion

        #region GetEnvironmentsSettingsByTemplateId

        /// <summary>
        /// Gets the environments settings by template identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns></returns>
        public List<ConfigurationSetting> GetEnvironmentsSettingsByTemplateId(int templateId)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    return
                        db.ConfigurationSetting
                            .Include("ConfigurationSettingValues")
                            .Include("ConfigurationSettingValues.ConfigValue")
                            .Include("ConfigurationSettingValues.ConfigValue.Environment")
                            .Where(p => p.ConfigurationTemplateId == templateId).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region GetSettings

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="environmentName">Name of the environment.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        public List<ConfigView> GetSettings(string environmentName, string templateName)
        {
            using (var db = new ConfigClassContext())
            {
                var q = from cv in db.Set<ConfigValue>()
                    join cs in db.Set<ConfigurationSettingValue>() on cv.ConfigValueId equals cs.ConfigValueId
                    where cv.Environment.EnvironmentName== environmentName &&
                          cs.ConfigurationSetting.ConfigurationTemplate.TemplateName == templateName
                    select new ConfigView
                    {
                        Environment = cv.Environment.EnvironmentName,
                        Property = cs.ConfigurationSetting.ChangePropertyName,
                        value = cv.Value,
                        xPath = cs.ConfigurationSetting.XpathValue,
                        ProcessorTypeId = cs.ConfigurationSetting.ProcessorTypeId
                    };

                var obj = q.ToList();

                return obj;
            }
        }

        #endregion

        #region GetConfigurationSettingValues

        /// <summary>
        /// Gets the configuration setting values.
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationSettingValue> GetConfigurationSettingValues()
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    return db.ConfigurationSettingValue.Include("ConfigValue").ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #region UpdateConfigurationSetting

        /// <summary>
        /// Update the configuration setting.
        /// </summary>
        /// <param name="configurationSettingValue">The configuration setting value.</param>
        /// <returns></returns>
        public ConfigurationSettingValue UpdateConfigurationSettingValue(ConfigurationSettingValue configurationSettingValue)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var csv = db.ConfigurationSettingValue.FirstOrDefault(e =>
                            e.ConfigurationSettingValueId == configurationSettingValue.ConfigurationSettingValueId);

                    if (csv != null)
                    {
                        csv.ConfigurationSettingId = configurationSettingValue.ConfigurationSettingId;
                        csv.ConfigValueId = configurationSettingValue.ConfigValueId;
                        db.SaveChanges();
                    }
                    csv = db.ConfigurationSettingValue.Include("ConfigValue").FirstOrDefault(e =>
                                                e.ConfigurationSettingValueId == configurationSettingValue.ConfigurationSettingValueId);
                    return csv;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion

        #region DeleteConfigurationSettingValue

        /// <summary>
        /// Deletes the configuration setting value.
        /// </summary>
        /// <param name="configurationSettingValueId">The configuration setting value identifier.</param>
        /// <returns></returns>
        public ConfigurationSettingValue DeleteConfigurationSettingValue(int configurationSettingValueId)
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    var csv = new ConfigurationSettingValue
                    {
                        ConfigurationSettingValueId = configurationSettingValueId
                    };

                    csv = db.ConfigurationSettingValue.Attach(csv);
                    csv = db.ConfigurationSettingValue.Remove(csv);
                    db.SaveChanges();
                    return csv;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion

        #region GetProcessorTypes

        /// <summary>
        /// Gets the processor types.
        /// </summary>
        /// <returns></returns>
        public List<ProcessorType> GetProcessorTypes()
        {
            try
            {
                using (var db = new ConfigClassContext())
                {
                    return db.ProcessorType.ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        #endregion
    }
}
