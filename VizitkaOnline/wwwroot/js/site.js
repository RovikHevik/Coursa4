function CopyToClipboard() {
    var copyText = document.getElementById("PublicUserLink");
    navigator.clipboard.writeText(copyText.textContent);
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