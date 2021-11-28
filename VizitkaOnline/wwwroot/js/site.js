function CopyToClipboard() {
    var copyText = document.getElementById("PublicUserLink");
    navigator.clipboard.writeText(copyText.textContent);
    var notify = document.getElementById("notify");
    notify.classList.add("active");
    var notyfiType = document.getElementById("notifyType");
    notyfiType.classList.add("success");
    notyfiType.textContent = "Копирование прошло успешно!";

    setTimeout(() => {
        notyfiType.classList.remove("success");
        notify.classList.remove("active");
        notyfiType.textContent = "";
    }, 3000);
}

function autoSubmit() {
    document.getElementById("FileForm").submit();
}

// грузим картинки только после загрузки сайта
window.onload = () => {
    document.body.classList.add('loaded_hiding');
    window.setTimeout(function () {
        document.body.classList.add('loaded');
        document.body.classList.remove('loaded_hiding');
    }, 500);
}

