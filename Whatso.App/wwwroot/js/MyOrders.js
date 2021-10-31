<script>
    $(document).ready(function () {
        var Title = "@TempData["DownloadMessageTitle"]"
    var Message = "@TempData["DownloadMessage"]"
    var URl = "@TempData["WhitelabelDownloadURL"]"
    if (Message != null && Message != "") {
        swal({
            title: Title,
            type: 'info',
            html:
                Message,
            showCloseButton: true,
            showCancelButton: true,
            focusConfirm: false,
            confirmButtonText:
                'Download',
            cancelButtonText:
                'Cancel',
            padding: '2em'
        }).then(function (result) {
            if (result.value) {
                location.href = URl;
            }
        })
    }
    });
    $("#CompanyLOGO").change(function (e) {
        let img = new Image()
    img.src = window.URL.createObjectURL(e.target.files[0])
        img.onload = () => {
            if (img.height === 183 && img.width === 400) {
        $("#btnwhitelabel").removeAttr('disabled');
            }
    else {
        swal("Error", "Height and Width Does Not Match For Company LOGO", "error");
    $("#btnwhitelabel").attr('disabled', 'disabled');
            }
        }
    });
    $("#CompanyICON").change(function (e) {
        let img = new Image()
    img.src = window.URL.createObjectURL(e.target.files[0])
        img.onload = () => {
            if (img.height === 35 && img.width == 35) {
        $("#btnwhitelabel").removeAttr('disabled');
            }
    else {
        swal("Error", "Height and Width Does Not Match For Company ICON", "error");
    $("#btnwhitelabel").attr('disabled', 'disabled');

            }
        }
    });
</script>