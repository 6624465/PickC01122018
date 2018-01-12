$(function () {
    $('#frmVehicle').validate({
        rules: {
            operatorVehicle_VehicleRegistrationNo: {
                required: true
            },
            operatorVehicle_VehicleType: {
                required: true
            },
            operatorVehicle_VehicleCategory: {
                required: true
            },
            operatorVehicle_Model: {
                required: true
            },
            operatorVehicle_Tonnage: {
                required: true
            }
        }
    });
});
var gIndex = -1;
function EditOperatorVehicle(index) {
    debugger
    gIndex = index;
    //OPerator_OperatorVehicle_0__VehicleRegistrationNo
    var baseID = 'OPerator_OperatorVehicle_' + index + '__';
    $('#operatorVehicle_VehicleRegistrationNo').val($('#' + baseID + 'VehicleRegistrationNo').val());
    $('#operatorVehicle_VehicleType').val($('#' + baseID + 'VehicleType').val());
    $('#operatorVehicle_VehicleCategory').val($('#' + baseID + 'VehicleCategory').val());
    var selectedCategory = {};
    selectedCategory.value = $('#' + baseID + 'VehicleCategory').val();
    onChangeVehicleCategory(selectedCategory);
    var el = {};
    el.value = $('#' + baseID + 'Model').val();
    ModelChanged(el);
     $('#operatorVehicle_Model').val($('#' + baseID + 'Model').val());
   
    $('#operatorVehicle_Tonnage').val($('#' + baseID + 'Tonnage').val());
    //$('#operatorVehicle_VehicleType option').filter(function () {
    //    return this.text == $('#' + baseID + 'VehicleType').val();
    //}).attr('selected', true);

    //$('#operatorVehicle_VehicleCategory option').filter(function () {
    //    return this.text == $('#' + baseID + 'VehicleCategory').val();
    //}).attr('selected', true);

    //$('#operatorVehicle_Model option').filter(function () {
    //    return this.text == $('#' + baseID + 'Model').val();
    //}).attr('selected', true);

  //  $('#operatorVehicle_Tonnage').val($('#' + baseID + 'Tonnage').val());
    $('#VehicleModal').modal('show');

}
function DeleteOperatorVehicle(index) {
    //var id = '#vehicle_OperatorVehicleList_' + index + '__IsActive';
    //$(id).val('False');
    //$('#trRow_' + index).css({
    //    color: 'red',
    //    'text-decoration': 'line-through',
    //    'font-style': 'italic'
    //});
    $('#trRow_Vehicle_' + index).remove();
    $('#operatorVehicle_VehicleRegistrationNo').val('');
    $('#operatorVehicle_VehicleType').val('');
    $('#operatorVehicle_VehicleCategory').val('');
    $('#operatorVehicle_Model').val('');
    $('#operatorVehicle_Tonnage').val('');
}
function btnSaveVehicle() {
    if (!$('#frmVehicle').valid())
        return;
    if (gIndex != -1) {
        debugger;
        var baseID = 'OPerator_OperatorVehicle_' + gIndex + '__';
        $('#' + baseID + 'VehicleRegistrationNo').val($('#operatorVehicle_VehicleRegistrationNo').val());
        $('#' + 'operatorVehicle_VehicleRegistrationNo_span_' + gIndex).text($('#operatorVehicle_VehicleRegistrationNo').val());

        $('#' + baseID + 'VehicleType').val($('#operatorVehicle_VehicleType').val());
        $('#' + 'operatorVehicle_VehicleType_span_' + gIndex).text($('#operatorVehicle_VehicleType option:selected').text());

        $('#' + baseID + 'VehicleCategory').val($('#operatorVehicle_VehicleCategory').val());
        $('#' + 'operatorVehicle_VehicleCategory_span_' + gIndex).text($("#operatorVehicle_VehicleCategory option:selected").text());

        $('#' + baseID + 'Model').val($('#operatorVehicle_Model').val());
        $('#' + 'operatorVehicle_Model_span_' + gIndex).text($('#operatorVehicle_Model').val());

        $('#' + baseID + 'Tonnage').val($('#operatorVehicle_Tonnage').val());
        $('#' + 'operatorVehicle_Tonnage_span_' + gIndex).text($('#operatorVehicle_Tonnage').val());

    } else {
        debugger;
        var index = ($('#trBodyVehicle tr').length);
        var html = '<tr id="trRow_Vehicle_' + index + '">' +
                        '<td>' +
                            '<span id="operatorVehicle_VehicleRegistrationNo_span_' + index + '">' + $('#operatorVehicle_VehicleRegistrationNo').val() + '</span>' +
                            '<input class="vehicleNoCss" id="OPerator_OperatorVehicle_' + index + '__VehicleRegistrationNo" name="OPerator.OperatorVehicle[' + index + '].VehicleRegistrationNo" type="hidden" value="' + $('#operatorVehicle_VehicleRegistrationNo').val() + '">' +
                        '</td>' +
                        '<td>' +
                            '<span id="operatorVehicle_VehicleType_span_' + index + '">' + $("#operatorVehicle_VehicleType option:selected").text() + '</span>' +
                            '<input id="OPerator_OperatorVehicle_' + index + '__VehicleType" name="OPerator.OperatorVehicle[' + index + '].VehicleType" type="hidden" value="' + $("#operatorVehicle_VehicleType").val() + '">' +
                        '</td>' +
                        '<td>' +
                            '<span id="operatorVehicle_VehicleCategory_span_' + index + '">' + $("#operatorVehicle_VehicleCategory option:selected").text() + '</span>' +
                            '<input id="OPerator_OperatorVehicle_' + index + '__VehicleCategory" name="OPerator.OperatorVehicle[' + index + '].VehicleCategory" type="hidden" value="' + $("#operatorVehicle_VehicleCategory option:selected").val() + '">' +
                        '</td>' +
                         '<td>' +
                            '<span id="operatorVehicle_Model_span_' + index + '">' + $('#operatorVehicle_Model').val() + '</span>' +
                            '<input id="OPerator_OperatorVehicle_' + index + '__Model" name="OPerator.OperatorVehicle[' + index + '].Model" type="hidden" value="' + $('#operatorVehicle_Model').val() + '">' +
                        '</td>' +
                         '<td>' +
                            '<span id="operatorVehicle_Tonnage_span_' + index + '">' + $('#operatorVehicle_Tonnage').val() + '</span>' +
                            '<input id="OPerator_OperatorVehicle_' + index + '__Tonnage" name="OPerator.OperatorVehicle[' + index + '].Tonnage" type="hidden" value="' + $('#operatorVehicle_Tonnage').val() + '">' +
                        '</td>' +
                        '<td>' +
                         '<a class="hand" onclick="EditOperatorVehicle(\'' + index + '\')">Edit</a>&nbsp;|&nbsp;' +
                            '<a class="hand" onclick="DeleteOperatorVehicle(\'' + index + '\')">Delete</a>' +
                        '</td>'
        '</tr>';
        $('#trBodyVehicle').append(html);
        $('#operatorVehicle_VehicleRegistrationNo').val('');
        $('#operatorVehicle_VehicleType').val('');
        $('#operatorVehicle_VehicleCategory').val('');
        $('#operatorVehicle_Model').val('');
        $('#operatorVehicle_Tonnage').val('');
    }


    $('#VehicleModal').modal('hide');
    gIndex = -1;
}
function AddVehicle(index) {
    gIndex = -1;
    $('#operatorVehicle_VehicleRegistrationNo, #operatorVehicle_VehicleType,#operatorVehicle_VehicleCategory, #operatorVehicle_Model, #operatorVehicle_Tonnage').val('');
    $('#VehicleModal').modal('show');
}