$(function () {
    $('#frmDriver').validate({
        rules: {
            operatorDriverList_DriverName: {
                required: true
            },
            operatorDriverList_DriverLicenseNo: {
                required: true
            },
            operatorDriverList_DriverMobileNo: {
                required: true
            },
            operatorDriverList_VehicleNo: {
                required: true
            }
        }
    });
});
var gIndex = -1;
function EditOperatorDriver(index) {
    debugger;
    gIndex = index;
   // operatorDriverList_DriverName_span_0
    var baseID = 'OPerator_OperatorDriverList_' + index + '__';
    //$('#operatorDriverList_DriverName option').filter(function () {
    //    return this.text == $('#' + baseID + 'operatorDriverList_DriverName').val();
    //}).attr('selected', true);

    var vehicles = $('.vehicleNoCss');
    var newVehicleArr = new Array();

    if (vehicles.length > 0) {
        $.each(vehicles, function (index, item) {
            //OPerator_OperatorVehicle_-1__VehicleRegistrationNo 
            var vehicle = $('#OPerator_OperatorVehicle_' + index + '__VehicleRegistrationNo').val();
            newVehicleArr.push(vehicle);
        });
    }
    $.each(newVehicleArr, function (key, value) {
        $('#operatorDriverList_VehicleNo')
             .append($('<option class="new">', { value: value })
             .text(value));
    });

    //$('#operatorVehicle_VehicleType option').filter(function () {
    //    return this.text == $('#' + baseID + 'VehicleType').val();
    //}).attr('selected', true);

    //$('#operatorVehicle_Model option').filter(function () {
    //    return this.text == $('#' + baseID + 'Model').val();
    //}).prop('selected', true);
    $('#operatorDriverList_DriverName option').filter(function () {
        return this.text == $('#' + baseID + 'DriverName').val();
    }).attr('selected', true);
    $('#operatorDriverList_DriverLicenseNo').val($('#' + baseID + 'DriverLicenseNo').val());
    $('#operatorDriverList_DriverMobileNo').val($('#' + baseID + 'DriverMobileNo').val());
    $('#operatorDriverList_VehicleNo option').filter(function () {
        return this.text == $('#' + baseID + 'VehicleattachedNo').val();
    }).attr('selected', true);
    //$('#operatorDriverList_VehicleNo').val($('#' + baseID + 'VehicleNo').val());
    $('#DriverModal').modal('show');

}
function DeleteOperatorDriver(index) {
    //var id = '#driver_OperatorDriverList_' + index + '__IsActive';
    //$(id).val('False');
    //$('#trRow_' + index).css({
    //    color: 'red',
    //    'text-decoration': 'line-through',
    //    'font-style': 'italic'
    //});
    $('#trRow_Driver_' + index).remove();
    $('#operatorDriverList_DriverName').val('');
    $('#operatorDriverList_DriverLicenseNo').val('');
    $('#operatorDriverList_DriverMobileNo').val('');
    $('#operatorDriverList_VehicleNo').val('');
}
function btnSavedriver() {
    if (!$('#frmDriver').valid())
        return;

    if (gIndex != -1) {
        var baseID = 'OPerator_OperatorDriverList_' + gIndex + '__';
        $('#' + baseID + 'operatorDriverList_DriverName').val($('#operatorDriverList_DriverName').val());
        $('#' + 'operatorDriverList_DriverName_span_' + gIndex).text($("#operatorDriverList_DriverName option:selected").text());

        $('#' + baseID + 'operatorDriverList_DriverLicenseNo').val($('#operatorDriverList_DriverLicenseNo').val());
        $('#' + 'operatorDriverList_DriverLicenseNo_span_' + gIndex).text($('#operatorDriverList_DriverLicenseNo').val());

        $('#' + baseID + 'operatorDriverList_DriverMobileNo').val($('#operatorDriverList_DriverMobileNo').val());
        $('#' + 'operatorDriverList_DriverMobileNo_span_' + gIndex).text($('#operatorDriverList_DriverMobileNo').val());

        $('#' + baseID + 'operatorDriverList_VehicleNo').val($('#operatorDriverList_VehicleNo').val());
        $('#' + 'operatorDriverList_VehicleNo_span_' + gIndex).text($('#operatorDriverList_VehicleNo').val());
    } else {
        var index = ($('#trBodyDriver tr').length);
        var html = '<tr id="trRow_Driver_' + index + '">' +
                        '<td>' +
                            '<span id="operatorDriverList_DriverName_span_' + index + '">' + $('#operatorDriverList_DriverName option:selected').text() + '</span>' +
                            '<input id="OPerator_OperatorDriverList_' + index + '__DriverName" name="OPerator[' + index + '].DriverName" type="hidden"  value="' + $('#operatorDriverList_DriverName').val() + '">' +
                        '</td>' +
                        '<td>' +
                            '<span id="operatorDriverList_DriverLicenseNo_span_' + index + '">' + $('#operatorDriverList_DriverLicenseNo').val() + '</span>' +
                            '<input id="OPerator_OperatorDriverList_' + index + '__DriverLicenseNo" name="OPerator[' + index + '].DriverLicenseNo" type="hidden" value="' + $('#operatorDriverList_DriverLicenseNo').val() + '">' +
                        '</td>' +
                         '<td>' +
                            '<span id="operatorDriverList_DriverMobileNo_span_' + index + '">' + $('#operatorDriverList_DriverMobileNo').val() + '</span>' +
                            '<input id="OPerator_OperatorDriverList_' + index + '__DriverMobileNo" name="OPerator[' + index + '].DriverMobileNo" type="hidden" value="' + $('#operatorDriverList_DriverMobileNo').val() + '">' +
                        '</td>' +
                         '<td>' +
                            '<span id="operatorDriverList_VehicleNo_span_' + index + '">' + $('#operatorDriverList_VehicleNo').val() + '</span>' +
                            '<input id="OPerator_OperatorDriverList_' + index + '__VehicleNo" name="OPerator[' + index + '].VehicleattachedNo" type="hidden" value="' + $('#operatorDriverList_VehicleNo').val() + '">' +
                        '</td>' +
                        '<td>' +
                         '<a class="hand" onclick="EditOperatorDriver(\'' + index + '\')">Edit</a>&nbsp;|&nbsp;' +
                            '<a class="hand" onclick="DeleteOperatorDriver(\'' + index + '\')">Delete</a>' +
                        '</td>'
                    '</tr>';
                    $('#trBodyDriver').append(html);
                    $('#operatorDriverList_DriverName').val('');
                    $('#operatorDriverList_DriverLicenseNo').val('');
                    $('#operatorDriverList_DriverMobileNo').val('');
                    $('#operatorDriverList_VehicleNo').val('');
    }


    $('#DriverModal').modal('hide');
    gIndex = -1;
}
function AddDriver(index) {

    gIndex = -1;
    $('#operatorDriverList_DriverName, #operatorDriverList_DriverLicenseNo, #operatorDriverList_DriverMobileNo, #operatorDriverList_VehicleNo').val('');
    $('#DriverModal').modal('show');
    var vehicles = $('.vehicleNoCss');
    var newVehicleArr = new Array();

    if (vehicles.length > 0) {
        $.each(vehicles, function (index, item) {
            //OPerator_OperatorVehicle_-1__VehicleRegistrationNo 
            var vehicle = $('#OPerator_OperatorVehicle_' + index + '__VehicleRegistrationNo').val();
            newVehicleArr.push(vehicle);
        });
    }
    $.each(newVehicleArr, function (key, value) {
        $('#operatorDriverList_VehicleNo')
             .append($('<option class="new">', { value: value })
             .text(value));
    });
}