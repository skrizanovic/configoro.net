var core = {};

//ko.validation.rules.pattern.message = 'Invalid.';

ko.validation.configure({
      registerExtenders: true
    , messagesOnModified: true
    //, insertMessages: true
    , decorateElement: true
    , errorElementClass: 'inputError'
    , errorsAsTitle: true
    , parseInputAttributes: true
    //messageTemplate: null
});

ko.dirtyFlag = function (root, initiallyDirty) {
    var result = function () { },
    initialState = ko.observable(ko.toJSON(root)),
    isInitiallyDirty = ko.observable(initiallyDirty);

    result.isDirty = ko.computed(function () {
        return isInitiallyDirty() || initialState() !== ko.toJSON(root);
    });

    result.reset = function () {
        initialState(ko.toJSON(root));
        isInitiallyDirty(false);
    };

    return result;
};

core.App = (function (core) {

    var selectedMode = ko.observable();

    var selectedView = ko.observable();

    var displaySelectedMode = ko.computed(function () {
        return (selectedMode() === "Environments")
            ? "Switch To Templates"
            : "Switch To Environments";
    });

    function run() {
        $("#dialog").hide();
        $("#confirmDialog").hide();

        var p = new Promise(function (resolve, reject) {
            resolve();
        });

        p.then(function() {
            core.ProcessorTypes.Load();
        }).then(function () {
            core.EnvironmentsViewModel.Load();
        }).then(function () {
            core.ConfigurationTemplatesViewModel.Load();
        }).then(function () {
            core.ConfigurationSettingValues.Load();
        });

        selectedMode("Templates");
        ko.applyBindings(self, document.getElementById('appControlSection'));
    };

    function selectMode() {
        var sel = selectedMode();
        if (sel == "Environments") {
            selectedMode("Templates");
        } else {
            selectedMode("Environments");
        }
    };
    
    function isEnvironmentsMode() {
        return core.App.SelectedMode() == 'Environments';
    }

    function isTemplatesMode() {
        return core.App.SelectedMode() == 'Templates';
    }

    function configureValues() {
        core.App.SelectedView('valuesTmpl');

        core.ConfigValuesViewModel.Load();

        var viewModel = new core.DialogViewModel.Create("dialog",
            "Values",
            core.ConfigValuesViewModel,
            function () { core.App.SelectedView(undefined); },
            function () { core.App.SelectedView(undefined); }
        );

        viewModel.open();
    };

    return {
          SelectedMode: selectedMode
        , DisplaySelectedMode: displaySelectedMode
        , SelectedView: selectedView

        , Run: run
        , IsEnvironmentsMode: isEnvironmentsMode
        , IsTemplatesMode: isTemplatesMode
        , SelectMode: selectMode
        , ConfigureValues: configureValues
    };

}(core));
