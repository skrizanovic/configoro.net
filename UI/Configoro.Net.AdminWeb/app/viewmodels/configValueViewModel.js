
core.ConfigValuesViewModel = (function (core) {

    var configValues = ko.observableArray([]);

    function load() {

        configValues.removeAll();

        var id = core.EnvironmentsViewModel.Selected().EnvironmentId();
        var arg = { 'environmentId': id };

        core.DataService.GetConfigValues(arg,
            function(json) {
                $.each(json, function (i, t) {

                    var configValue = new ConfigValue();
                    configValue.update(t);

                    configValue.dirtyFlag.reset();

                    configValues.push(configValue);
                });
            });
    };

    this.edit = function (item, event) {

        core.App.SelectedView('valuesTmpl');

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Edit ConfigValue " + item.Name(),
            item,
            function () {
                var arg = { 'configValue': item };
                core.DataService.UpdateConfigValue(arg, function (result) {

                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    this.remove = function (parent, item, event) {

        var viewModel = new core.DialogViewModel.Create("confirm",
            "Delete ConfigValue " + item.Name(),
            "These operation cannot be reverted. Are you sure?",
            function () {

                var arg = { 'configValue': item };
                core.DataService.DeleteConfigValue(arg, function (result) {

                    var target = event.target;
                    var tr = $(target).closest('tr');
                    tr.fadeOut('slow', function () {
                        tr.remove();
                        parent.ConfigValues.remove(item);
                    });

                    core.ConfigurationSettingValues.RemoveAllByConfigValue(item.ConfigValueId());
                });
            });

        viewModel.open();
    };

    return {
          ConfigValues: configValues
        
        , Load: load
        , Delete: remove
        , Edit: edit
    };

} (core));
