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
            }
        }
    });
});

function saveBankDetails() {
    if (!$('#frmBank').valid())
        return;

    if (gIndex != -1) {
        var baseId=
    }
}

var gIndex = -1;
function EditBank(index) {
    gIndex = index;
}

function DeleteBank(index) {

}