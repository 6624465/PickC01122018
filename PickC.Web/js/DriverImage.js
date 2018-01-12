$(function () {
    $('#frmImgRegister').validate({
        rules: {
            DriverName: {
                required: true
            },
            MobileNo: {
                required: true
            }
        }
    });
});
function Clear() {
    $('#DriverName').val('');
    $('#MobileNo').val('');
}

function btnSaveImageRegistration() {
    if (!$('#frmImgRegister').valid())
        return;
    else {
        var DriverName = $('#DriverName').val();
        var MobileNo = $('#MobileNo').val();
        var obj = {
            DriverName: DriverName,
            MobileNo: MobileNo
        }
        $.ajax({
            url: baseUrl + 'Home/GetFile',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(obj),
            success: function (res) {
                $('#DriverName').val('');
                $('#MobileNo').val('');
                $('#Registration').modal('hide');
                $('#DriverDescription').modal('show');
            },
            error: function (err) {
               
            }
        });
    }
} 

$('#MobileNo').change(function (e) {
    var mobileNumberRegExp = /^[789]\d{9}$/,
        mobileNumber = e.currentTarget ? e.currentTarget.value : '';

    if (mobileNumberRegExp.test(mobileNumber) == false) {
        alert('Please enter valid phonenumber & it should contails 10 digits and starts with (7 or 8 or 9).');
        $('#MobileNo').val("");
        return false;
    }
});
//function DownloadImage() {
//    var DriverName = $('#DriverName').val();
//    var MobileNo = $('#MobileNo').val();
//    var obj = {
//        DriverName:DriverName,
//        MobileNo:MobileNo
//    }
//    $.ajax({
//        url: baseUrl + '/Home/GetFile',
//        method: 'POST',
//        contentType: 'application/pdf',
//        data: JSON.stringify(obj),
//        success: function (res) {
//           
//        },
//        error: function (err) {
//           
//        }
//    });
//}