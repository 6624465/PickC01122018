$(function () {
    $('#frmAttachments').validate({
        rules: {
            fileAttachment: {
                required:true
            }
        }
    });
});