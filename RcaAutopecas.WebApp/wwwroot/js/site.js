document.addEventListener('DOMContentLoaded', function () {
    var successToast = document.getElementById('success-toast');
    if (successToast) {
        setTimeout(function () {
            var bsAlert = new bootstrap.Alert(successToast);
            bsAlert.close();
        }, 5000);
    }
});