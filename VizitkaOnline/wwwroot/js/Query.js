async function DeleteById(id, password) {
    let response = await fetch(document.location.origin + '/api/Delete/' + id + "/" + password);
    let data = await response.text();
    alert(data);
    window.location.reload();
}
