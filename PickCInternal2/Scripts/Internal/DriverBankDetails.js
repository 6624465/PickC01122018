﻿//$(function () {
//    $('#frmBank').validate({
//        rules: {
//            txtBankName: {
//                required: true
//            },
//            txtBranch: {
//                required: true
//            },
//            txtAccNumber: {
//                required: true
//            },
//            txtAccType: {
//                required: true
//            }
//        }
//    });
//});

//function saveBankDetails() {
//    if (!$('#frmBank').valid())
//        return;

//    if (gIndex != -1) {
//        //var baseId=
//    }
//}

//var gIndex = -1;
//function EditBank(index) {
//    gIndex = index;
//}

//function DeleteBank(index) {

//}

$(function () {

    $('#frmBank').validate({
        rules: {
            txtBankName: {
                required: true
            },
            txtBranch: {
                required: true
            },
            txtAccNumber: {
                required: true
            },
            txtAccType: {
                required: true
            },
            txtIFSCcode:{
                required: true
            }
        },
        messages: {
            txtBankName: {
                required: ''
            },
            txtBranch: {
                required: ''
            },
            txtAccNumber: {
                required: ''
            },
            txtAccType: {
                required: ''
            },
            txtIFSCcode: {
                required: ''
            }
        }
    });
});
var gIndex = -1;
function EditBankDetails(index) {
    gIndex = index;

    var baseID = 'driver_BankDetails_' + index + '__';
    $('#txtBankName').val($('#' + baseID + 'BankName').val());
    $('#txtBranch').val($('#' + baseID + 'Branch').val());
    $('#txtAccNumber').val($('#' + baseID + 'AccountNumber').val());
    $('#txtAccType').val($('#' + baseID + 'AccountType').val());
    $('#bankDetailsModal').modal('show');

}
function DeleteBankDetails(index) {
    //var id = '#OPerator_BankDetails_' + index + '__IsActive';
    //$(id).val('False');trRow_Bank_
    debugger;
    var con=confirm("Are you sure..Do you want to delete?")
    if (con) {
        $('#trRow_Bank_' + index).remove();
        $('#txtBankName').val('');
        $('#txtBranch').val('');
        $('#txtAccNumber').val('');
        $('#txtAccType').val('');
        $('#txtIFSCcode').val('');
    }
    
}

function btnSaveBank() {

    if (!$('#frmBank').valid())
        return;

    if (gIndex != -1) {
        var baseID = 'driver_BankDetails_' + gIndex + '__';
        $('#' + baseID + 'BankName').val($('#txtBankName').val());
        $('#' + 'txtBankName_span_' + gIndex).text($('#txtBankName').val());

        $('#' + baseID + 'Branch').val($('#txtBranch').val());
        $('#' + 'txtBranch_span_' + gIndex).text($('#txtBranch').val());

        $('#' + baseID + 'AccountNumber').val($('#txtAccNumber').val());
        $('#' + 'txtAccNumber_span_' + gIndex).text($('#txtAccNumber').val());

        $('#' + baseID + 'AccountType').val($('#txtAccType').val());
        $('#' + 'txtAccType_span_' + gIndex).text($('#txtAccType').val());
    } else {
        var index = ($('#trBodyBank tr').length);
        var html = '<tr id="trRow_Bank_' + index + '">' +
            '<td>' +
            '<span id="txtBankName_span_' + index + '">' + $('#txtBankName').val() + '</span>' +
            '<input id="driver_BankDetails_' + index + '__BankName" name="driver.BankDetails[' + index + '].BankName" type="hidden"  value="' + $('#txtBankName').val() + '">' +
            '</td>' +
            '<td>' +
            '<span id="txtBranch_span_' + index + '">' + $('#txtBranch').val() + '</span>' +
            '<input id="driver_BankDetails_' + index + '__Branch" name="driver.BankDetails[' + index + '].Branch" type="hidden" value="' + $('#txtBranch').val() + '">' +
            '</td>' +
            '<td>' +
            '<span id="txtAccNumber_span_' + index + '">' + $('#txtAccNumber').val() + '</span>' +
            '<input id="driver_BankDetails_' + index + '__AccountNumber" name="driver.BankDetails[' + index + '].AccountNumber" type="hidden" value="' + $('#txtAccNumber').val() + '">' +
            '</td>' +
            '<td>' +
            '<span id="txtAccType_span_' + index + '">' + $('#txtAccType').val() + '</span>' +
            '<input id="driver_BankDetails_' + index + '__AccountType" name="driver.BankDetails[' + index + '].AccountType" type="hidden" value="' + $('#txtAccType').val() + '">' +
            '</td>' +
            '<td>' +
            '<a class="hand" onclick="EditBankDetails(\'' + index + '\')">Edit</a>&nbsp;|&nbsp;' +
            '<a class="hand" onclick="DeleteBankDetails(\'' + index + '\')">Delete</a>' +
            '</td>'
        '</tr>';
        $('#trBodyBank').append(html);
        $('#txtBankName').val('');
        $('#txtBranch').val('');
        $('#txtAccNumber').val('');
        $('#txtAccType').val('');
    }


    $('#bankDetailsModal').modal('hide');
    gIndex = -1;
}
function bankDetails(index) {
    gIndex = -1;
    $('#frmBank input[type="text"]').removeClass('error');
    $('#txtBankName, #txtBranch, #txtAccNumber, #txtAccType,#txtAccType').val('');
    $('#bankDetailsModal').modal('show');
}
//function bankDetails(index) {
//    $('#frmBank input[type="text"]').removeClass('error');
//    $('#txtBankName,#txtBranch,#txtAccNumber,#txtAccType').val('');
//    $('#bankDetailsModal').modal('show');
//}