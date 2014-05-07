
core.DataService = (function () {
    "use strict";

    var ajaxService = core.AjaxService;

    // Environment
    function getEnvironments(callback) {
        ajaxService.GetJson("GetEnvironments", null, callback);
    }

    function addEnvironment(args, callback) {
        ajaxService.PostJson("AddEnvironment", args, callback);
    }

    function updateEnvironment(args, callback) {
        ajaxService.PostJson("UpdateEnvironment", args, callback);
    }

    function deleteEnvironment(args, callback) {
        ajaxService.PostJson("DeleteEnvironment", args, callback);
    }

    // ConfigValue
    function getConfigValues(args, callback) {
        ajaxService.PostJson("GetConfigValues", args, callback);
    }

    function addConfigValue(args, callback) {
        ajaxService.PostJson("AddConfigValue", args, callback);
    }

    function updateConfigValue(args, callback) {
        ajaxService.PostJson("UpdateConfigValue", args, callback);
    }

    function deleteConfigValue(args, callback) {
        ajaxService.PostJson("DeleteConfigValue", args, callback);
    }

    // Template
    function getTemplatesSettings(callback) {
        ajaxService.GetJson("GetTemplatesSettings", null, callback);
    }

    function addConfigurationTemplate(args, callback) {
        ajaxService.PostJson("AddConfigurationTemplate", args, callback);
    }

    function updateConfigurationTemplate(args, callback) {
        ajaxService.PostJson("UpdateConfigurationTemplate", args, callback);
    }

    function deleteConfigurationTemplate(args, callback) {
        ajaxService.PostJson("DeleteConfigurationTemplate", args, callback);
    }
    // ConfigurationSetting
    function addConfigurationSetting(args, callback) {
        ajaxService.PostJson("AddConfigurationSetting", args, callback);
    }

    function updateConfigurationSetting(args, callback) {
        ajaxService.PostJson("UpdateConfigurationSetting", args, callback);
    }

    function deleteConfigurationSetting(args, callback) {
        ajaxService.PostJson("DeleteConfigurationSetting", args, callback);
    }
    // ConfigurationSettingValue
    function getConfigurationSettingValues(args, callback) {
        ajaxService.PostJson("GetConfigurationSettingValues", args, callback);
    }

    function addConfigurationSettingValue(args, callback) {
        ajaxService.PostJson("AddConfigurationSettingValue", args, callback);
    }

    function updateConfigurationSettingValue(args, callback) {
        ajaxService.PostJson("UpdateConfigurationSettingValue", args, callback);
    }

    function deleteConfigurationSettingValue(args, callback) {
        ajaxService.PostJson("DeleteConfigurationSettingValue", args, callback);
    }

    // ProcessorType
    function getProcessorTypes(args, callback) {
        ajaxService.PostJson("GetProcessorTypes", args, callback);
    }

    return {
          GetEnvironments: getEnvironments
        , AddEnvironment: addEnvironment
        , UpdateEnvironment: updateEnvironment
        , DeleteEnvironment: deleteEnvironment

        , GetConfigValues: getConfigValues
        , AddConfigValue: addConfigValue
        , UpdateConfigValue: updateConfigValue
        , DeleteConfigValue: deleteConfigValue

        , GetTemplatesSettings: getTemplatesSettings
        , AddConfigurationTemplate: addConfigurationTemplate
        , UpdateConfigurationTemplate: updateConfigurationTemplate
        , DeleteConfigurationTemplate: deleteConfigurationTemplate

        , AddConfigurationSetting: addConfigurationSetting
        , UpdateConfigurationSetting: updateConfigurationSetting
        , DeleteConfigurationSetting: deleteConfigurationSetting

        , GetConfigurationSettingValues: getConfigurationSettingValues
        , AddConfigurationSettingValue: addConfigurationSettingValue
        , UpdateConfigurationSettingValue: updateConfigurationSettingValue
        , DeleteConfigurationSettingValue: deleteConfigurationSettingValue

        , GetProcessorTypes: getProcessorTypes
    };

} (core));