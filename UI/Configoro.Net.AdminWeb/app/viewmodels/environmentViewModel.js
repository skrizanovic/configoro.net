
core.EnvironmentsViewModel = (function (core) {

    var environments = ko.observableArray([]);

    // Nothing selected by default
    var selected = ko.observable();

    selected.subscribe(function (newEnvironment) {
        core.ConfigValuesViewModel.Load();
    });

    var selectedEnvironmentName = ko.computed(function () {
        return (selected() === undefined)
            ? ""
            : selected().EnvironmentName();
    });

    var isDirty = ko.computed(function () {
        if (selected() === undefined) {
            return false;
        }

        return (selected().dirtyFlag.isDirty()) ? true : false;
    });

    function load() {
        core.DataService.GetEnvironments(function (result) {
            $.each(result.Environments, function (i, env) {
                var environment = new Environment()
                    .EnvironmentId(env.EnvironmentId)
                    .EnvironmentName(env.EnvironmentName)
                    .IsActive(env.IsActive);


                var sortedConfig = env.ConfigValues.sort(function (a, b) {
                    if (a.Name > b.Name)
                        return 1;
                    if (a.Name < b.Name)
                        return -1;
                    // a must be equal to b
                    return 0;
                });

                $.each(sortedConfig, function (j, s) {
                    environment.addConfigValue(s);
                });

                environment.dirtyFlag.reset();

                environments.push(environment);
            });

            selected(environments()[0]);

            ko.applyBindings(self, document.getElementById('environmentsSection'));
        });
    };

    function add() {

        core.App.SelectedView('environmentTmpl');

        var env = new Environment();

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Create New Environment",
            env,
            function () {
                core.DataService.AddEnvironment(env, function (result) {

                    env.EnvironmentId(result.EnvironmentId);
                    environments.push(env);
                    selected(env);
                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            }
        );

        viewModel.open();
    };

    function edit(environment, event) {

        core.App.SelectedView('environmentTmpl');

        var env = new Environment()
            .EnvironmentId(environment.EnvironmentId())
            .EnvironmentName(environment.EnvironmentName())
            .IsActive(environment.IsActive());

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Edit Environment",
            env,
            function () {

                var arg = { 'environment': env };
                core.DataService.UpdateEnvironment(arg, function (result) {

                    environment
                        .EnvironmentName(result.EnvironmentName)
                        .IsActive(result.IsActive);
                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    function remove(environment, event) {

        var viewModel = new core.DialogViewModel.Create("confirm",
            "Delete Environment " + environment.EnvironmentName(),
            "These operation cannot be reverted. Are you sure?",
            function () {

                var arg = { 'environment': environment };
                core.DataService.DeleteEnvironment(arg, function (result) {

                    // Cleanup mapping
                    if (environment.ConfigValues().length > 0) {
                        environment.ConfigValues().forEach(function (item) {
                            core.ConfigurationSettingValues.RemoveAllByConfigValue(item.ConfigValueId());
                        });
                    }

                    var target = event.target;
                    var parent = $(target).closest('div');
                    var head = parent.prev('h3');
                    parent.add(head).fadeOut('slow', function () {
                        head.remove();
                        parent.remove();
                        environments.remove(environment);
                    });
                });
            });

        viewModel.open();
    };

    function addValue(environment, event) {

        core.App.SelectedView('valuesTmpl');

        var cv = new ConfigValue().EnvironmentId(environment.EnvironmentId());

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Add Configuration Value",
            cv,
            function (result) {

                var arg = { 'configValue': cv };
                core.DataService.AddConfigValue(arg, function (response) {

                    cv.ConfigValueId(response.ConfigValueId);
                    environment.ConfigValues.push(cv);
                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    function SortConfigValues(environment, event) {
        return environment.ConfigValues();
    }

    return {
        Environments: environments
        , Selected: selected
        , IsDirty: isDirty
        , SelectedEnvironmentName: selectedEnvironmentName

        , Load: load
        , Add: add
        , Edit: edit
        , Delete: remove
        , AddValue: addValue
        , SortedConfigValues: SortConfigValues
    };

} (core));
