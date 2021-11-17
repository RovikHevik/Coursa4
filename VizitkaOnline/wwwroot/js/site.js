function CopyToClipboard() {
    var copyText = document.getElementById("PublicUserLink");
    navigator.clipboard.writeText(copyText.textContent);
}

function autoSubmit() {
    document.getElementById("FileForm").submit();
}