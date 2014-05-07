//(function() {

// Accordion suff
ko.bindingHandlers.accordion = {
    init: function (element, valueAccessor) {
        var options = valueAccessor() || {};
        setTimeout(function () {
            $(element).accordion(options);
        }, 0);

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).accordion("destroy");
        });
    },
    update: function (element, valueAccessor) {
        var options = valueAccessor() || {};
        if (typeof $(element).data("ui-accordion") != "undefined") {
            $(element).accordion("destroy").accordion(options);
        }
    }
};

// Dialogs stuff

ko.bindingHandlers.dialog = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var options = ko.utils.unwrapObservable(valueAccessor()) || {};
        //do in a setTimeout, so the applyBindings doesn't bind twice from element being copied and moved to bottom
        setTimeout(function () {
            options.close = function () {
                allBindingsAccessor().dialogVisible(false);
            };

            $(element).dialog(options);
        }, 0);

        //handle disposal (not strictly necessary in this scenario)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).dialog("destroy");
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var shouldBeOpen = ko.utils.unwrapObservable(allBindingsAccessor().dialogVisible),
            $el = $(element),
            dialog = $el.data("uiDialog") || $el.data("dialog");

        //don't call open/close before initialization
        if (dialog) {
            $el.dialog(shouldBeOpen ? "open" : "close");
        }
    }
};

ko.bindingHandlers.dialogcmd = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).button().click(function () {
            var options = ko.utils.unwrapObservable(valueAccessor());
            $('#' + options.id).dialog(options.cmd || 'open');
        });
    }
};

//})();
