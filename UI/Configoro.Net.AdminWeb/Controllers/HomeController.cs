
using System.Linq;
using System.Web.Mvc;
using Configoro.Net.Domain;
using Configoro.Net.AdminWeb.Models;
using Environment = Configoro.Net.Domain.Environment;
using Configoro.Net.AdminWeb.Transformer;


namespace Configoro.Net.AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        #region _service

        /// <summary>
        /// The service
        /// </summary>
        private readonly ConfigService _service = new ConfigService();

        #endregion

        #endregion

        #region Environment Methods

        #region GetEnvironments

        /// <summary>
        /// Get the environments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetEnvironments()
        {
            var env = _service.GetEnvironments();
            //remove circular refence from object tree
            env.ForEach(cv => cv.ConfigValues.ToList()
                .ForEach(e => e.Environment = null));

            var model = new EnvironmentsViewModel {Environments = env};
            return new JsonResult {Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        #endregion

        #region AddEnvironment

        /// <summary>
        /// Add the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddEnvironment(Environment environment)
        {
            var env = _service.AddEnvironment(environment);
            return new JsonResult {Data = env, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        #endregion

        #region UpdateEnvironment

        /// <summary>
        /// Update the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateEnvironment(Environment environment)
        {
            var env = _service.UpdateEnvironment(environment);
            return new JsonResult {Data = env, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        #endregion

        #region DeleteEnvironment

        /// <summary>
        /// Delete the environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteEnvironment(Environment environment)
        {
            var env = _service.DeleteEnvironment(environment);
            return new JsonResult {Data = env, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        #endregion

        #endregion

        #region ConfigValue Methods

        #region GetConfigValues

        /// <summary>
        /// Get the configuration values.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetConfigValues(int environmentId)
        {
            var result = _service.GetConfigValues(environmentId);
            if (result.IsErrored == false)
            {
                return new JsonResult { Data = result.Entity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                Response.StatusCode = 404;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        #endregion

        #region AddConfigValue

        /// <summary>
        /// Add the configuration value.
        /// </summary>
        /// <param name="configValue">The configuration value.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddConfigValue(ConfigValue configValue)
        {
            var cv = _service.AddConfigValue(configValue);
            return new JsonResult { Data = cv, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region UpdateConfigValue

        /// <summary>
        /// Update the configuration value.
        /// </summary>
        /// <param name="configValue">The configuration value.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateConfigValue(ConfigValue configValue)
        {
            var cv = _service.UpdateConfigValue(configValue);
            return new JsonResult { Data = cv, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region DeleteConfigValue

        /// <summary>
        /// Delete the configuration value.
        /// </summary>
        /// <param name="configValue">The configuration value.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteConfigValue(ConfigValue configValue)
        {
            var cv = _service.DeleteConfigValue(configValue);
            return new JsonResult { Data = cv, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #endregion

        #region ConfigurationTemplate Methods

        #region GetTemplatesSettings

        /// <summary>
        /// Get the templates settings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTemplatesSettings()
        {
            var env = _service.GetTemplatesSettings();
            //remove circular refence from object tree
            env.ForEach(p => p.ConfigurationSettings.ToList()
                .ForEach(r => r.ConfigurationTemplate = null));

            var model = new TemplateViewModel {ConfigurationTemplates = env};
            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region AddConfigurationTemplate

        /// <summary>
        /// Add the configuration template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddConfigurationTemplate(ConfigurationTemplate template)
        {
            var t = _service.AddConfigurationTemplate(template);
            return new JsonResult { Data = t, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region UpdateConfigurationTemplate

        /// <summary>
        /// Update the configuration template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateConfigurationTemplate(ConfigurationTemplate template)
        {
            var t = _service.UpdateConfigurationTemplate(template);
            return new JsonResult { Data = t, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region DeleteConfigurationTemplate

        /// <summary>
        /// Delete the configuration template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteConfigurationTemplate(ConfigurationTemplate template)
        {
            var t = _service.DeleteConfigurationTemplate(template);
            return new JsonResult { Data = t, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
        [HttpPost]
        public JsonResult AddConfigurationSetting(ConfigurationSetting setting)
        {
            var cs = _service.AddConfigurationSetting(setting);
            return new JsonResult { Data = cs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region UpdateConfigurationSetting

        /// <summary>
        /// Update the configuration setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateConfigurationSetting(ConfigurationSetting setting, int environmentId)
        {
            var cs = _service.UpdateConfigurationSetting(setting, environmentId).ToConfigurationSettingViewModel();
            return new JsonResult { Data = cs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region DeleteConfigurationSetting

        /// <summary>
        /// Delete the configuration setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteConfigurationSetting(ConfigurationSetting setting)
        {
            var cs = _service.DeleteConfigurationSetting(setting);
            return new JsonResult { Data = cs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #endregion

        #region ConfigurationSettingValue Methods

        #region GetConfigurationSettingValues

        /// <summary>
        /// Gets the configuration setting values.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetConfigurationSettingValues()
        {
            var result = _service.GetConfigurationSettingValues().ToConfigSettingViewModel();

            var recs = result.Where(p => p.EnvironmentId == 1 && p.ConfigurationSettingId == 2070).ToList();
            return new JsonResult {Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        #endregion

        #region AddConfigurationSettingValue

        /// <summary>
        /// Adds the configuration setting value.
        /// </summary>
        /// <param name="configurationSettingId">The configuration setting identifier.</param>
        /// <param name="configValueId">The configuration value identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddConfigurationSettingValue(int configurationSettingId, int configValueId)
        {
            var result = _service.AddConfigurationSettingValue(configurationSettingId, configValueId).ToConfigSettingViewModel();
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region UpdateConfigurationSettingValue

        /// <summary>
        /// Updates the configuration setting value.
        /// </summary>
        /// <param name="configurationSettingValue">The configuration setting value.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateConfigurationSettingValue(ConfigurationSettingValue configurationSettingValue)
        {
            var cs = _service.UpdateConfigurationSettingValue(configurationSettingValue).ToConfigSettingViewModel();
            return new JsonResult { Data = cs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region DeleteConfigurationSettingValue

        /// <summary>
        /// Deletes the configuration setting value.
        /// </summary>
        /// <param name="configurationSettingValueId">The configuration setting value identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteConfigurationSettingValue(int configurationSettingValueId)
        {
            var result = _service.DeleteConfigurationSettingValue(configurationSettingValueId);
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #endregion

        #region ProcessorTypes Methods

        #region GetProcessorTypes

        /// <summary>
        /// Gets the processor types.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProcessorTypes()
        {
            var result = _service.GetProcessorTypes();
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #endregion

        #region Index

        /// <summary>
        /// Default action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Templates";
            ViewBag.Message = "Use this page to modify global config settings";
            return View();
        }

        #endregion

        #region About

        /// <summary>
        /// About action.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        #endregion

        #region Contact

        /// <summary>
        /// Contact action.
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #endregion
    }
}
