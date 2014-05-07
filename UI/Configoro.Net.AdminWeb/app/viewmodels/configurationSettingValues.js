
core.ConfigurationSettingValues = (function (core) {

    var map = ko.observableArray([]);

    function load() {

        core.DataService.GetConfigurationSettingValues({}, function (result) {

            $.each(result, function (i, csv) {
                var val = new SettingConfigValue();
                val.ConfigurationSettingValueId = csv.ConfigurationSettingValueId;
                val.ConfigurationSettingId = csv.ConfigurationSettingId;
                val.ConfigValueId = csv.ConfigValueId;
                val.EnvironmentId = csv.EnvironmentId;

                map.push(val);
            });
        });
    };

    function add(configurationSettingId, configValueId) {
        var arg = {
            'configurationSettingId': configurationSettingId,
            'configValueId': configValueId
        };

        core.DataService.AddConfigurationSettingValue(arg, function (result) {
            map.push(result);
        });
    }

    function update(value) {

        var arg = { 'configurationSettingValue': value };
        core.DataService.UpdateConfigurationSettingValue(arg, function (result) {

            var found = ko.utils.arrayFirst(map(), function (element) {
                return value.ConfigurationSettingValueId === element.ConfigurationSettingValueId;
            });

            if (found != undefined) {
                found.ConfigValueId = value.ConfigValueId;
            }
        });
    }

    function remove(configurationSettingValueId) {

        var arg = { 'configurationSettingValueId': configurationSettingValueId };
        core.DataService.DeleteConfigurationSettingValue(arg, function (result) {

            var found = ko.utils.arrayFirst(map(), function (element) {
                return configurationSettingValueId === element.ConfigurationSettingValueId;
            });

            if (found != undefined) {
                map.remove(found);
            }
        });
    }

    function removeAllByConfigValue(configValueId) {
        ko.utils.arrayForEach(map(), function (element) {
            if (configValueId === element.ConfigValueId) {
                map.remove(element);
            }
        });
    }

    function removeAllByConfigurationSetting(configurationSettingId) {
        ko.utils.arrayForEach(map(), function (element) {
            if (configurationSettingId === element.ConfigurationSettingId) {
                map.remove(element);
            }
        });
    }

    function removeAllByConfigurationSettingValue(configurationSettingValueId) {
        ko.utils.arrayForEach(map(), function (element) {
            if (configurationSettingValueId === element.ConfigurationSettingValueId) {
                map.remove(element);
            }
        });
    }

    return {
        Map: map

        , Load: load
        , Add: add
        , Update: update
        , Delete: remove

        , RemoveAllByConfigValue: removeAllByConfigValue
        , RemoveAllByConfigurationSetting: removeAllByConfigurationSetting
        , RemoveAllByConfigurationSettingValue: removeAllByConfigurationSettingValue
    };

} (core));