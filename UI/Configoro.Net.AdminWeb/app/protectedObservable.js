//wrapper to an observable that requires accept/cancel
ko.protectedObservable = function (initialValue) {
    //private variables
    var actualValue = ko.observable(initialValue),
        tempValue = initialValue;

    //computed observable that we will return
    var result = ko.dependentObservable({
        read: actualValue,
        write: function (newValue) {
            tempValue = newValue;
        }
    });

    //if different, commit temp value
    result.commit = function () {
        if (tempValue !== actualValue()) {
            actualValue(tempValue);
        }
    };

    //force subscribers to take original
    result.reset = function () {
        actualValue.valueHasMutated();
        tempValue = actualValue();
    };

    return result;
};
