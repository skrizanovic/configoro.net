
function Environment() {
    var self = this;

    self.EnvironmentId = ko.observable(0);
    self.EnvironmentName = ko.observable("");
    self.IsActive = ko.observable(false);
    self.ConfigValues = ko.observableArray([]);

    self.dirtyFlag = new ko.dirtyFlag(self);

    self.addConfigValue = function(entry) {
        self.ConfigValues.push(
            new ConfigValue()
                .ConfigValueId(entry.ConfigValueId)
                .EnvironmentId(entry.EnvironmentId)
                .Name(entry.Name)
                .Value(entry.Value)
                .IsActive(entry.IsActive));
    };
};

function ConfigValue() {
    var self = this;

    self.ConfigValueId = ko.observable();
    self.EnvironmentId = ko.observable();
    self.Name = ko.observable();
    self.Value = ko.observable();
    self.IsActive = ko.observable();

    self.dirtyFlag = new ko.dirtyFlag(self);

    self.update = function (data) {
        self.ConfigValueId(data.ConfigValueId || 0);
        self.EnvironmentId(data.EnvironmentId || 0);
        self.Name(data.Name || "New name");
        self.Value(data.Value || "New value");
        self.IsActive(data.IsActive || false);
    };
};

function ConfigurationTemplate() {
    var self = this;

    self.ConfigurationTemplateId = ko.observable(0),
    self.TemplateName = ko.observable("").extend({ required: true, message: 'Please supply template name.' }),
    self.ConfigFile = ko.observable("").extend({ required: true, message: 'Please supply configuration file name.' }),
    self.ConfigType = ko.observable(1), // Currently only type 1 supported
    self.IsActive = ko.observable(false);
    self.ConfigurationSettings = ko.observableArray([]);

    self.TemplateStatus = ko.computed(function() {
        return self.IsActive() ? self.TemplateName() : self.TemplateName() + " (Inactive)";
    }, self);

    self.addConfigurationSetting = function (configurationSetting) {
        self.ConfigurationSettings.push(
            new ConfigurationSetting()
                .ConfigurationSettingId(configurationSetting.ConfigurationSettingId)
                .ConfigurationTemplateId(configurationSetting.ConfigurationTemplateId)
                .ChangePropertyName(configurationSetting.ChangePropertyName)
                .XpathValue(configurationSetting.XpathValue)
                .ProcessorTypeId(configurationSetting.ProcessorTypeId));
    };
};

function ConfigurationSetting() {
    var self = this;

    self.ConfigurationSettingId = ko.observable(0);
    self.ConfigurationTemplateId = ko.observable(0),
    self.ProcessorTypeId = ko.observable(0);
    self.ChangePropertyName = ko.observable("").extend({ required: true, message: 'Please supply property name.' });
    self.XpathValue = ko.observable("").extend({ required: true, message: 'Please supply value.' });
    self.SelectedConfigValue = ko.computed(function () {
        var matchConfigurationSettingValue = ko.utils.arrayFirst(core.ConfigurationSettingValues.Map(), function (element) {
            if (core.EnvironmentsViewModel.Selected() != undefined)
                return self.ConfigurationSettingId() === element.ConfigurationSettingId && core.EnvironmentsViewModel.Selected().EnvironmentId() == element.EnvironmentId;
            else
                return self.ConfigurationSettingId() === element.ConfigurationSettingId;
        });

        if (matchConfigurationSettingValue === null) {
            return "Unspecified";
        }

        var matchConfigValue = ko.utils.arrayFirst(core.EnvironmentsViewModel.Selected().ConfigValues(), function (element) {
            return matchConfigurationSettingValue.ConfigValueId === element.ConfigValueId();
        });

        if (matchConfigValue === null || matchConfigValue === undefined) {
            return "Unspecified";
        }

        return matchConfigValue.Name();
    });

    self.dirtyFlag = new ko.dirtyFlag(self);
};

function ProcessorType() {
    var self = this;

    self.ProcessorTypeId = ko.observable(0);
    self.Name = ko.observable("");
};

function SettingConfigValue() {
    var self = this;

    self.ConfigurationSettingValueId = 0;
    self.ConfigurationSettingId = 0;
    self.ConfigValueId = 0;
};
