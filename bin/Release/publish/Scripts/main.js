var swal = function () { };
swal.Icon = {
    "success": "success",
    "error": "error",
    "warning": "warning",
}


swal.Show = function (icon, msg = "", html = false, showCancelButton = false) {
    let confirmButtonColor = "#3085d6";
    let confirmButtonText = "OK";
    if (msg.includes("確認刪除")) {
        confirmButtonColor = "#dd3333";
        confirmButtonText = "確認刪除"
    }
    return Swal.fire({
        icon: icon,
        text: (html ? '' : msg),
        html: (html ? msg : ''),
        //title: Messager.Title[icon],
        showCancelButton: showCancelButton,
        confirmButtonColor: confirmButtonColor,
        confirmButtonText: confirmButtonText
    });
}
