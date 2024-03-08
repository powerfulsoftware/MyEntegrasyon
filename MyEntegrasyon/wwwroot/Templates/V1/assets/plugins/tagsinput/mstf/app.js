var country_list = ["Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua & Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia & Herzegovina", "Botswana", "Brazil"];
var city_list = ["Amsterdam", "Auckland", "London", "Paris", "Washington", "New York", "Los Angeles", "Sydney", "Melbourne", "Canberra", "Beijing", "New Delhi", "Kathmandu", "Cairo", "Cape Town", "Kinshasa"];

var myData = new Bloodhound({
  datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
  queryTokenizer: Bloodhound.tokenizers.whitespace,
  local: $.map(country_list, function(item) {
      return {
        value: item
      };
    })
});
myData.initialize();

$('#tags').tagsinput({
  typeaheadjs: {
    name: 'value',
    displayKey: 'value',
    valueKey: 'value',
    source: myData.ttAdapter()
  }
});

changeList = function(val) {
  source = (val === 'cities') ? city_list : country_list;
  myData.clear(); // First remove all suggestions from the search index
  myData.local = $.map(source, function(item) {
    return {
      value: item
    };
  });
  myData.initialize(true); // Finally reinitialise the bloodhound suggestion engine
};
