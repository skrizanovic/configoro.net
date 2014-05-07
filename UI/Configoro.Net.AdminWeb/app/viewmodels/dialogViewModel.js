
//
// Generic dialog module
//
core.DialogViewModel = 
(function () {

    function dialogViewModel(dialogNodeName, title, data, saveCallback, cancelCallback) {
        var self = this;

        // Dialog title
        self.title = title;

        // Dialog data
        self.Data = data;

        // Dialog "is opened" flag
        self.isOpen = ko.observable(false);

        // Open dialog
        self.open = function () {
            ko.applyBindings(self, document.getElementById(dialogNodeName));
            self.isOpen(true);
        };

        // Save data and dismiss dialog
        self.save = function () {
            self.isOpen(false);
            if (saveCallback !== undefined) {
                saveCallback(self.Data);
            }
            ko.cleanNode(document.getElementById(dialogNodeName));
        };

        // Cancel and dismiss dialog
        self.cancel = function () {
            self.isOpen(false);
            if (cancelCallback !== undefined) {
                cancelCallback();
            }
            ko.cleanNode(document.getElementById(dialogNodeName));
        };

    }

    return {
        Create: dialogViewModel
    };
})()
