
core.ConfigurationTemplatesViewModel = (function (core) {

    var self = this;

    var configurationTemplates = ko.observableArray([]);

    var selectedConfig = ko.observable();

    function load() {
        core.DataService.GetTemplatesSettings(function (json) {

            $.each(json.ConfigurationTemplates, function (i, t) {
                var templ = new ConfigurationTemplate()
                    .ConfigurationTemplateId(t.ConfigurationTemplateId)
                    .TemplateName(t.TemplateName)
                    .ConfigFile(t.ConfigFile)
                    .ConfigType(t.ConfigType)
                    .IsActive(t.IsActive);

                $.each(t.ConfigurationSettings, function (j, s) {
                    templ.addConfigurationSetting(s);
                });

                configurationTemplates.push(templ);
            });

            ko.applyBindings(self, document.getElementById('templatesSection'));
        });
    };

    function addTemplate(item, event) {

        core.App.SelectedView('templateTmpl');

        var data = new ConfigurationTemplate();
        var viewModel = new core.DialogViewModel.Create("dialog",
            "Add Template",
            data,
            function (t) {

                var newTemplate = new ConfigurationTemplate()
                    .ConfigurationTemplateId(t.ConfigurationTemplateId())
                    .TemplateName(t.TemplateName())
                    .ConfigFile(t.ConfigFile())
                    .ConfigType(t.ConfigType())
                    .IsActive(t.IsActive());

                var arg = { 'template': newTemplate };
                core.DataService.AddConfigurationTemplate(arg, function (result) {

                    newTemplate.ConfigurationTemplateId(result.ConfigurationTemplateId);
                    configurationTemplates.push(newTemplate);
                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    function editTemplate(item, event) {
        core.App.SelectedView('templateTmpl');

        var tmp = new ConfigurationTemplate()
            .ConfigurationTemplateId(item.ConfigurationTemplateId())
            .TemplateName(item.TemplateName())
            .ConfigFile(item.ConfigFile())
            .ConfigType(item.ConfigType())
            .IsActive(item.IsActive());

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Edit Template",
            tmp,
            function () {

                var arg = { 'template': tmp };
                core.DataService.UpdateConfigurationTemplate(arg, function (result) {

                    item
                        .TemplateName(result.TemplateName)
                        .ConfigFile(result.ConfigFile)
                        .ConfigType(result.ConfigType)
                        .IsActive(result.IsActive);
                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    function deleteTemplate(template, event) {

        var viewModel = new core.DialogViewModel.Create("confirm",
            "Delete Template",
            "These operation cannot be reverted. Are you sure?",
            function () {

                var arg = { 'template': template };
                core.DataService.DeleteConfigurationTemplate(arg, function (result) {

                    // Cleanup mapping
                    if (template.ConfigurationSettings().length > 0) {
                        template.ConfigurationSettings().forEach(function (item) {
                            core.ConfigurationSettingValues.RemoveAllByConfigurationSetting(item.ConfigurationSettingId());
                        });
                    }

                    var target = event.target;
                    var parent = $(target).closest('div');
                    var head = parent.prev('h3');
                    parent.add(head).fadeOut('slow', function () {
                        head.remove();
                        parent.remove();
                        configurationTemplates.remove(template);
                    });
                });
            });

        viewModel.open();
    };

    function addConfiguration(template, event) {

        core.App.SelectedView('settingTmpl');

        core.ConfigurationViewModel.Init();

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Add Setting",
            core.ConfigurationViewModel,
            function (result) {
                var newSetting = new ConfigurationSetting()
                    .ConfigurationTemplateId(template.ConfigurationTemplateId())
                    .ChangePropertyName(result.ConfigurationSetting().ChangePropertyName())
                    .XpathValue(result.ConfigurationSetting().XpathValue())
                    .ProcessorTypeId(core.ConfigurationViewModel.SelectedProcessorType().ProcessorTypeId());

                var arg = { 'setting': newSetting };
                core.DataService.AddConfigurationSetting(arg, function (response) {

                    newSetting.ConfigurationSettingId(response.ConfigurationSettingId);
                    template.ConfigurationSettings.push(newSetting);

                    // Update mapping
                    if (core.ConfigurationViewModel.SelectedValue() !== undefined) {
                        var configurationSettingId = newSetting.ConfigurationSettingId();
                        var configValueId = core.ConfigurationViewModel.SelectedValue().ConfigValueId();
                        core.ConfigurationSettingValues.Add(configurationSettingId, configValueId);
                    }
                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    function editConfiguration(configuration, event) {

        core.App.SelectedView('settingTmpl');

        core.ConfigurationViewModel.Init(configuration);

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Edit Setting",
            core.ConfigurationViewModel,
            function (data) {

                var setting = new ConfigurationSetting()
                    .ConfigurationSettingId(configuration.ConfigurationSettingId())
                    .ConfigurationTemplateId(configuration.ConfigurationTemplateId())
                    .ChangePropertyName(data.ConfigurationSetting().ChangePropertyName())
                    .XpathValue(data.ConfigurationSetting().XpathValue())
                    .ProcessorTypeId(core.ConfigurationViewModel.SelectedProcessorType().ProcessorTypeId());

                if (core.ConfigurationViewModel.SelectedValue() !== undefined) {
                    if (data.ConfigurationSettingValue() !== undefined) {
                        // update csv
                        data.ConfigurationSettingValue().ConfigValueId = data.SelectedValue().ConfigValueId();
                        core.ConfigurationSettingValues.Update(data.ConfigurationSettingValue());
                    } else {
                        // add csv
                        core.ConfigurationSettingValues.Add(
                            configuration.ConfigurationSettingId(), data.SelectedValue().ConfigValueId());
                    }
                } else {
                    if (data.ConfigurationSettingValue() !== undefined) {
                        // delete csv
                        core.ConfigurationSettingValues.Delete(data.ConfigurationSettingValue().ConfigurationSettingValueId);
                    }
                }


                //update the display
                //configuration.SelectedConfigValue(core.ConfigurationViewModel.SelectedValue().Name())
                configuration.recalcSelectedConfig();



                var arg = { 'setting': setting, 'environmentId': core.EnvironmentsViewModel.Selected().EnvironmentId() };
                core.DataService.UpdateConfigurationSetting(arg, function (result) {

                    configuration.ChangePropertyName(result.ChangePropertyName)
                    configuration.XpathValue(result.XpathValue)
                    configuration.ProcessorTypeId(result.ProcessorTypeId)
                    //configuration.SelectedConfigValue(result.ConfigValue)
                    configuration.recalcSelectedConfig();

                });

                core.App.SelectedView(undefined);
            },
            function () {
                core.App.SelectedView(undefined);
            });

        viewModel.open();
    };

    function deleteConfiguration(parent, item, event) {

        var viewModel = new core.DialogViewModel.Create("confirm",
            "Delete Setting",
            "These operation cannot be reverted. Are you sure?",
            function () {

                core.ConfigurationViewModel.Init(item);

                var arg = { 'setting': item };
                core.DataService.DeleteConfigurationSetting(arg, function (result) {

                    var target = event.target;
                    var tr = $(target).closest('tr');
                    tr.fadeOut('slow', function () {
                        tr.remove();
                        parent.ConfigurationSettings.remove(item);
                    });

                    var cv = core.ConfigurationViewModel.ConfigurationSettingValue();
                    if (cv != undefined) {
                        var configurationSettingValueId = core.ConfigurationViewModel.ConfigurationSettingValue().ConfigurationSettingValueId;
                        core.ConfigurationSettingValues.RemoveAllByConfigurationSettingValue(configurationSettingValueId);
                    }
                });
            });

        viewModel.open();
    };

    function selectConfig(data, selectedObj) {
        selectedConfig(data);

        //// highlight selected row via attr
        //$('#itemTable tbody tr').each(function() {
        //    $(this).removeAttr('bgcolor');
        //});
        //$(selectedObj.currentTarget).attr('bgcolor', '#CCCCCC');

        // highlight selected row via css
        $('#itemTable tr').each(function () {
            $(this).css('background-color', '');
        });
        $(selectedObj.currentTarget).css({
            'background-color': '#DDDDDD'
        });
    };

    return {
        ConfigurationTemplates: configurationTemplates
        , SelectedConfig: selectedConfig
        , SelectConfig: selectConfig

        , Load: load
        , AddTemplate: addTemplate
        , EditTemplate: editTemplate
        , DeleteTemplate: deleteTemplate
        , AddConfiguration: addConfiguration
        , EditConfiguration: editConfiguration
        , DeleteConfiguration: deleteConfiguration
    };

} (core));
