
var xlist = [{ "value": 1, "text": "Adana" }, { "value": 2, "text": "Adıyaman" }, { "value": 3, "text": "Afyon" }];


var myData = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('text'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    local: $.map(xlist, function (item) {
        return {
            value: item.value,
            text: item.text
        };
    })
});

myData.initialize();

var elt = $('input');
elt.tagsinput({
    itemValue: 'value',
    itemText: 'text',
    typeaheadjs: {
        name: 'myData',
        displayKey: 'text',
        source: myData.ttAdapter()
    }
});

/*window.alert(JSON.stringify(elt));*/



elt.tagsinput('add', { "value": 1, "text": "Adana" });
elt.tagsinput('add', { "value": 2, "text": "Adıyaman" });




