﻿$(function () {
    
    $('#frmVehicle').validate({
        rules: {
            VehicleRegistrationNo: {
				required: true,
				maxlength: 10,
				minlength: 10
            },
            VehicleType: {
                required: true
            },
            VehicleCategory: {
                required: true
            },
           Model: {
                required: true
            },
            Tonnage: {
                required: true
            }
           
        },
        messages: {
            VehicleRegistrationNo: {
                required: ''
            },
            VehicleType: {
                required: ''
            },
            VehicleCategory: {
                required: ''
            },
            Model: {
                required: ''
            },
            Tonnage: {
                required: ''
            }
        }

    });
});
var gIndex = -1;
function EditOperatorVehicle(index) {
    debugger;
    if (index != -1) {
        $('#VehicleRegistrationNo').attr('readonly', 'readonly');
    }
    //$('#registrationdoc').bind('change', function () {
    //    var filename = $('#' + baseID + 'registrationdoc').val();
    //    if (/^\s*$/.test(filename)) {
    //        $(".regDoc").removeClass('active');
    //        $("#registrationdocnoFile").text("No file chosen...");
    //    }
    //    else {
    //        $(".regDoc").addClass('active');
    //        $("#registrationdocnoFile").text(filename.replace("C:\\fakepath\\", ""));
    //    }
    //});

    gIndex = index;
    //OPerator_OperatorVehicle_0__VehicleRegistrationNo
    var baseID = 'OPerator_OperatorVehicle_' + index + '__';
    $('#VehicleRegistrationNo').val($('#' + baseID + 'VehicleRegistrationNo').val());
    $('#VehicleType').val($('#' + baseID + 'VehicleType').val());
    $('#VehicleCategory').val($('#' + baseID + 'VehicleCategory').val());
    var selectedCategory = {};
    selectedCategory.value = $('#' + baseID + 'VehicleCategory').val();
    var vehicleCategoryPromise = onChangeVehicleCategoryPromise(selectedCategory);
    $.when(vehicleCategoryPromise).done(function (result1) {
       
        $('#Model').removeAttr("disabled");
        $("#Model").html(""); // clear before appending new list
        $("<option value='-1'>Select item</option>").appendTo("#Model");
        $.each(result1, function (i, val) {
            
            $("#Model").append($('<option></option>').val(val.Text).html(val.Value));
        });
        $('#Model').val($('#' + baseID + 'Model').val());

        var modalObject = {};
        modalObject.value = $('#' + baseID + 'Model').val();

        var modalPromise = ModelChangedPromise(modalObject);
        $.when(modalPromise).done(function (modalResult) {            
            
            $('#Tonnage').val($('#' + baseID + 'Tonnage').val());
        });
    });   
    debugger;
    $('#frontimagedocFile').text($('#' + baseID + 'FrontImage').val());
    $('#hdnfrontimagedocFile').val($('#' + baseID + 'FrontImage').val());
    var FrontimageDocfile = $('#' + baseID + 'FrontImage').val();
    if (FrontimageDocfile == null || FrontimageDocfile == '')
    {
        $('#downFrontImage').hide();
    }
    else
    {
        $('#downFrontImage').show();
    }

    $('#backimagedocFile').text($('#' + baseID + 'BackImage').val());
    $('#hdnbackimagedocFile').val($('#' + baseID + 'BackImage').val());
    var backimagedocfile = $('#' + baseID + 'BackImage').val();
    if (backimagedocfile == null || backimagedocfile == '') {
        $('#downBackImage').hide();
    }
    else {
        $('#downBackImage').show();
    }

    $('#leftimagedocFile').text($('#' + baseID + 'LeftImage').val());
    $('#hdnleftimagedocFile').val($('#' + baseID + 'LeftImage').val());
    var leftimagedocfile = $('#' + baseID + 'LeftImage').val();
    if (leftimagedocfile == null || leftimagedocfile == '') {
        $('#downLeftImage').hide();
    }
    else {
        $('#downLeftImage').show();
    }

    $('#rightimagedocFile').text($('#' + baseID + 'RightImage').val());
    $('#hdnrightimagedocFile').val($('#' + baseID + 'RightImage').val());
    var rightimagedocfile = $('#' + baseID + 'RightImage').val();
    if (rightimagedocfile == null || rightimagedocfile == '') {
        $('#downRightImage').hide();
    }
    else {
        $('#downRightImage').show();
    }


    $('#registrationdocFile').text($('#' + baseID + 'registrationdoc').val());
    $('#hdnRegistrationdocFile').val($('#' + baseID + 'registrationdoc').val());
    var registrationdocFile = $('#' + baseID + 'registrationdoc').val();
    if (registrationdocFile == null || registrationdocFile == '')
    {
        $('#downregistrationdoc').hide();
    }
    else {
        $('#downregistrationdoc').show();
    }


    $('#insurancedocFile').text($('#' + baseID + 'insurancedoc').val());
    $('#hdnInsurancedocFile').val($('#' + baseID + 'insurancedoc').val());
    var insuranceDocFile = $('#' + baseID + 'insurancedoc').val();
    debugger;
    if (insuranceDocFile == null || insuranceDocFile == '') {
        $('#downinsurancedoc').hide();
    }
    else {
        $('#downinsurancedoc').show();
    }

    
    
    $('#pollutioncheckdocFile').text($('#' + baseID + 'pollutioncheckdoc').val());
    $('#hdnPollutioncheckdocFile').val($('#' + baseID + 'pollutioncheckdoc').val());
    var pollutioncheckDocFile = $('#' + baseID + 'pollutioncheckdoc').val();
    if (pollutioncheckDocFile == null || pollutioncheckDocFile == '') {
        $('#downpollutioncheckdoc').hide();
    }
    else {
        $('#downpollutioncheckdoc').show();
    }

    $('#othersdocFile').text($('#' + baseID + 'othersdoc').val());
    $('#hdnOthersdocFile').val($('#' + baseID + 'othersdoc').val());
   
    var othersdocFile = $('#' + baseID + 'othersdoc').val();
    if (othersdocFile == null || othersdocFile == '')
    {
        $('#downothersdoc').hide();
    }
    else {
        $('#downothersdoc').show();
    }

    //$('#hdnOperatorVehicleID').val($('#' + baseID + 'OperatorVehicleID').val());
    //var id = $('#' + baseID + 'OperatorVehicleID').val();
    //var id1 = $('#' + baseID + 'VehicleRegistrationNo').val()
    //var id2 = $('#' + baseID + 'FrontImage').val();

    $("#imgFrontImage").attr("src", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'FrontImage').val());
    $("#imgBackImage").attr("src", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'BackImage').val());
    $("#imgLeftImage").attr("src", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'LeftImage').val());
    $("#imgRightImage").attr("src", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'RightImage').val());

    $("#downFrontImage").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'FrontImage').val()).attr('download', 'FrontImage');
    $("#downBackImage").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'BackImage').val()).attr('download', 'BackImage');
    $("#downLeftImage").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'LeftImage').val()).attr('download', 'LeftImage');
    $("#downRightImage").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'RightImage').val()).attr('download', 'RightImage');

    $("#downregistrationdoc").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'registrationdoc').val()).attr('download', 'registrationdoc');
    $("#downinsurancedoc").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'insurancedoc').val()).attr('download', 'insurancedoc');
    $("#downpollutioncheckdoc").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'pollutioncheckdoc').val()).attr('download', 'pollutioncheckdoc');
    $("#downothersdoc").attr("href", "/VehicleAttachments/" + $('#' + baseID + 'OperatorVehicleID').val() + "/" + $('#' + baseID + 'VehicleRegistrationNo').val() + "/" + $('#' + baseID + 'othersdoc').val()).attr('download', 'othersdoc');

    //var regDoc2 = $('#registrationdoc');
    debugger;
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
    var con = confirm("Are you sure..Do you want to delete?");
    if (con) {
        $('#trRow_Vehicle_' + index).remove();
        $('#operatorVehicle_VehicleRegistrationNo').val('');
        $('#operatorVehicle_VehicleType').val('');
        $('#operatorVehicle_VehicleCategory').val('');
        $('#operatorVehicle_Model').val('');
        $('#operatorVehicle_Tonnage').val('');
    }
   // else {
    //    return false;
   // }
}
function btnSaveVehicle() {
    debugger;
    if ($('#frmVehicle').valid())
        $('#frmVehicle').submit();
    //if (gIndex != -1) {
    //    debugger;
    //    var baseID = 'OPerator_OperatorVehicle_' + gIndex + '__';
    //    $('#' + baseID + 'VehicleRegistrationNo').val($('#VehicleRegistrationNo').val());
    //    $('#' + 'operatorVehicle_VehicleRegistrationNo_span_' + gIndex).text($('#VehicleRegistrationNo').val());

    //    $('#' + baseID + 'VehicleType').val($('#VehicleType').val());
    //    $('#' + 'operatorVehicle_VehicleType_span_' + gIndex).text($('#VehicleType option:selected').text());

    //    $('#' + baseID + 'VehicleCategory').val($('#VehicleCategory').val());
    //    $('#' + 'operatorVehicle_VehicleCategory_span_' + gIndex).text($("#VehicleCategory option:selected").text());

    //    $('#' + baseID + 'Model').val($('#Model').val());
    //    $('#' + 'operatorVehicle_Model_span_' + gIndex).text($('#Model').val());

    //    $('#' + baseID + 'Tonnage').val($('#Tonnage').val());
    //    $('#' + 'operatorVehicle_Tonnage_span_' + gIndex).text($('#Tonnage').val());

    //} else {
    //    debugger;
    //    var index = ($('#trBodyVehicle tr').length);
    //    var html = '<tr id="trRow_Vehicle_' + index + '">' +
    //                    '<td>' +       
    //                        '<span id="operatorVehicle_VehicleRegistrationNo_span_' + index + '">' + $('#VehicleRegistrationNo').val() + '</span>' +
    //                        '<input class="vehicleNoCss" id="OPerator_OperatorVehicle_' + index + '__VehicleRegistrationNo" name="OPerator.OperatorVehicle[' + index + '].VehicleRegistrationNo" type="hidden" value="' + $('#operatorVehicle_VehicleRegistrationNo').val() + '">' +
    //                    '</td>' +
    //                    '<td>' +
    //                        '<span id="operatorVehicle_VehicleType_span_' + index + '">' + $("#VehicleType option:selected").text() + '</span>' +
    //                        '<input id="OPerator_OperatorVehicle_' + index + '__VehicleType" name="OPerator.OperatorVehicle[' + index + '].VehicleType" type="hidden" value="' + $("#operatorVehicle_VehicleType").val() + '">' +
    //                    '</td>' +
    //                    '<td>' +
    //                        '<span id="operatorVehicle_VehicleCategory_span_' + index + '">' + $("#VehicleCategory option:selected").text() + '</span>' +
    //                        '<input id="OPerator_OperatorVehicle_' + index + '__VehicleCategory" name="OPerator.OperatorVehicle[' + index + '].VehicleCategory" type="hidden" value="' + $("#operatorVehicle_VehicleCategory option:selected").val() + '">' +
    //                    '</td>' +
    //                     '<td>' +
    //                        '<span id="operatorVehicle_Model_span_' + index + '">' + $('#Model').val() + '</span>' +
    //                        '<input id="OPerator_OperatorVehicle_' + index + '__Model" name="OPerator.OperatorVehicle[' + index + '].Model" type="hidden" value="' + $('#operatorVehicle_Model').val() + '">' +
    //                    '</td>' +
    //                     '<td>' +
    //                        '<span id="operatorVehicle_Tonnage_span_' + index + '">' + $('#Tonnage').val() + '</span>' +
    //                        '<input id="OPerator_OperatorVehicle_' + index + '__Tonnage" name="OPerator.OperatorVehicle[' + index + '].Tonnage" type="hidden" value="' + $('#operatorVehicle_Tonnage').val() + '">' +
    //                    '</td>' +
    //                    '<td>' +
    //                     '<a class="hand" onclick="EditOperatorVehicle(\'' + index + '\')">Edit</a>&nbsp;|&nbsp;' +
    //                        '<a class="hand" onclick="DeleteOperatorVehicle(\'' + index + '\')">Delete</a>' +
    //                    '</td>'
    //    '</tr>';
    //    $('#trBodyVehicle').append(html);
    //    $('#operatorVehicle_VehicleRegistrationNo').val('');
    //    $('#operatorVehicle_VehicleType').val('');
    //    $('#operatorVehicle_VehicleCategory').val('');
    //    $('#operatorVehicle_Model').val('');
    //    $('#operatorVehicle_Tonnage').val('');
    //}


    //$('#VehicleModal').modal('hide');
    //gIndex = -1;
}
function AddVehicle(index) {
    debugger;
    gIndex = -1;
    $('#operatorVehicle_VehicleRegistrationNo, #operatorVehicle_VehicleType,#operatorVehicle_VehicleCategory, #operatorVehicle_Model, #operatorVehicle_Tonnage').val('');
    $('#VehicleModal').modal('show');

    if (index != -1) {
		$('#VehicleRegistrationNo').attr('readonly', 'readonly');
    }
}