
core.ConfigurationViewModel = (function(core) {

    var configurationSetting = ko.observable();

    var selectedValue = ko.observable();

    var selectedProcessorType = ko.observable();

    var configurationSettingValue = ko.observable();

    function init(item) {

        var tmp = new ConfigurationSetting();

        if (item != null) {

            tmp.ConfigurationSettingId(item.ConfigurationSettingId());
            tmp.ChangePropertyName(item.ChangePropertyName());
            tmp.XpathValue(item.XpathValue());
            tmp.ProcessorTypeId(item.ProcessorTypeId());
        } else {
            tmp.ProcessorTypeId(core.ProcessorTypes.Map()[0]);
        }
        configurationSetting(tmp);

        var matchProcessor = ko.utils.arrayFirst(core.ProcessorTypes.Map(), function (element) {
            return tmp.ProcessorTypeId() === element.ProcessorTypeId();
        });
        selectedProcessorType(matchProcessor);

        var matchConfigurationSettingValue = ko.utils.arrayFirst(core.ConfigurationSettingValues.Map(), function (element) {
            return tmp.ConfigurationSettingId() === element.ConfigurationSettingId && core.EnvironmentsViewModel.Selected().EnvironmentId() == element.EnvironmentId;
        });
        if (matchConfigurationSettingValue !== null) {
            configurationSettingValue(matchConfigurationSettingValue);
        } else {
            configurationSettingValue(undefined);
        }

        if (matchConfigurationSettingValue !== null) {
            var matchConfigValue = ko.utils.arrayFirst(core.EnvironmentsViewModel.Selected().ConfigValues(), function (element) {
                return matchConfigurationSettingValue.ConfigValueId === element.ConfigValueId();
            });
            selectedValue(matchConfigValue);
        } else {
            selectedValue(undefined);
        }
    };

    return {
          ConfigurationSetting: configurationSetting
        , SelectedValue: selectedValue
        , SelectedProcessorType: selectedProcessorType
        , ConfigurationSettingValue: configurationSettingValue

        , Init: init
    };

} (core));
