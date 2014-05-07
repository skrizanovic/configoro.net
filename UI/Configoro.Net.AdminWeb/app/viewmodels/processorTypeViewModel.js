
core.ProcessorTypes = (function (core) {

    var map = ko.observableArray([]);

    function load() {

        core.DataService.GetProcessorTypes( {}, function (result) {
            $.each(result, function (i, t) {

                var pType = new ProcessorType()
                    .ProcessorTypeId(t.ProcessorTypeId)
                    .Name(t.Name);
                map.push(pType);
            });
        });
    }

    return {
          Map: map

        , Load: load
    };

} (core));