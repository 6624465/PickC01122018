$(function () {
    $('#frmAddress').validate({
        rules: {
            Address1: {
                required: true
            },
            Address2: {
                required: true
            },
            Address3: {
                required: true
            },
            Address4: {
                required: true
            },
            StateName: {
                required: true
            },
            CityName: {
                required: true
            },
            ZipCode: {
                required: true
            }
        }
    });
});

var gIndex = -1;
function EditAddress(index) {
    /*
    $.ajax({
        url: UrlAddress + '/Address?addressID=' + addressID,
        method: 'get',
        contentType: 'application/json',
        dataType: 'html',
        success: function (html) {
            $('#addressModalBody').html(html);
            $('#addressModal').modal('show');
        }
    }); */
    gIndex = index;
    var baseID = 'driver_AddressList_' + index + '__';
    $('#Address1').val($('#' + baseID + 'Address1').val());
    $('#Address2').val($('#' + baseID + 'Address2').val());
    $('#Address3').val($('#' + baseID + 'Address3').val());
    $('#Address4').val($('#' + baseID + 'Address4').val());
    $('#CityName').val($('#' + baseID + 'CityName').val());
    $('#StateName').val($('#' + baseID + 'StateName').val());
    $('#ZipCode').val($('#' + baseID + 'ZipCode').val());
    $('#addressModal').modal('show');

}

function btnSaveAddress() {
    debugger;
    if (!$('#frmAddress').valid())
        return;

    if (gIndex != -1) {
        var baseID = 'driver_AddressList_' + gIndex + '__';
        $('#' + baseID + 'Address1').val($('#Address1').val());
        $('#' + 'Address1_span_' + gIndex).text($('#Address1').val());

        $('#' + baseID + 'Address2').val($('#Address2').val());
        $('#' + 'Address2_span_' + gIndex).text($('#Address2').val());

        $('#' + baseID + 'Address3').val($('#Address3').val());
        $('#' + 'Address3_span_' + gIndex).text($('#Address3').val());

        $('#' + baseID + 'Address4').val($('#Address4').val());
        $('#' + 'Address4_span_' + gIndex).text($('#Address4').val());

        $('#' + baseID + 'CityName').val($('#CityName').val());
        $('#' + 'CityName_span_' + gIndex).text($('#CityName').val());

        $('#' + baseID + 'StateName').val($('#StateName').val());
        $('#' + 'StateName_span_' + gIndex).text($('#StateName').val());

        $('#' + baseID + 'ZipCode').val($('#ZipCode').val());
        $('#' + 'ZipCode_span_' + gIndex).val($('#ZipCode').val());
    } else {
        var index = $('.trRowCss').length;
        var html = '<tr id="trRow_' + index + '">' +
                        '<td>' +
                            '<span id="Address1_span_' + index + '">' + $('#Address1').val() + '</span>' +
                            '<input id="driver_AddressList_' + index + '__Address1" name="driver.AddressList[' + index + '].Address1" type="hidden" value="' + $('#Address1').val() + '">' +
                        '</td>' +
                        '<td>' +
                            '<span id="Address2_span_' + index + '">' + $('#Address2').val() + '</span>' +
                            '<input id="driver_AddressList_' + index + '__Address2" name="driver.AddressList[' + index + '].Address2" type="hidden" value="' + $('#Address2').val() + '">' +
                        '</td>' +
                        '<td>' +
                            '<input id="driver_AddressList_' + index + '__Address3" name="driver.AddressList[' + index + '].Address3" type="hidden" value="' + $('#Address3').val() + '">' +
                            '<input id="driver_AddressList_' + index + '__Address4" name="driver.AddressList[' + index + '].Address4" type="hidden" value="' + $('#Address4').val() + '">' +
                            '<input id="driver_AddressList_' + index + '__CityName" name="driver.AddressList[' + index + '].CityName" type="hidden" value="' + $('#CityName').val() + '">' +
                            '<input id="driver_AddressList_' + index + '__StateName" name="driver.AddressList[' + index + '].StateName" type="hidden" value="' + $('#StateName').val() + '">' +
                            '<input id="driver_AddressList_' + index + '__ZipCode" name="driver.AddressList[' + index + '].ZipCode" type="hidden" value="' + $('#ZipCode').val() + '">' +
                            '<a class="hand" onclick="EditAddress(\'' + index + '\')">Edit</a>&nbsp;|&nbsp;' +
                            '<a class="hand" onclick="DeleteAddress(\'' + index + '\')">Delete</a>' +
                        '</td>' +
                    '</tr>';
        $('#trBody').append(html);
    }


    $('#addressModal').modal('hide');
    gIndex = -1;
}

function AddAddress(index) {
    gIndex = -1;
    $('#Address1, #Address2, #Address3, #Address4, #CityName, #StateName, #ZipCode').val('');
    $('#addressModal').modal('show');
}

function DeleteAddress(index) {
    var id = '#driver_AddressList_' + index + '__IsActive';
    $(id).val('False');
    $('#trRow_' + index).css({
        color: 'red',
        'text-decoration': 'line-through',
        'font-style': 'italic'
    });
}

