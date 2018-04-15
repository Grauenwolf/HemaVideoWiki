function sayHello() {
    var compiler = document.getElementById("compiler").value;
    var framework = document.getElementById("framework").value;
    return "Hello from " + compiler + " and " + framework + "!";
}
function readValue(controlName) {
    var control = $('#' + controlName)[0];
    return control.value;
}
function addVideo(sectionKey, url, description, author, startTime) {
    var newVideo = {
        "sectionKey": sectionKey,
        "url": url,
        "description": description,
        "author": author,
        "startTime": startTime,
    };
    $.ajax({
        type: "POST",
        url: "/api/book/addVideo",
        // The key needs to match your method's input parameter (case-sensitive).
        data: JSON.stringify(newVideo),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { location.reload(); },
        error: function (errMsg, errObj) {
            alert(errMsg);
            alert(errObj);
        }
    });
}
function search(bookKey, guardKey, techniqueKey, footworkKey, targetKey) {
    window.location.href = "/demo/search?bookKey=" + bookKey + "&guardKey=" + guardKey + "&techniqueKey=" + techniqueKey + "&footworkKey=" + footworkKey + "&targetKey=" + targetKey;
}
//# sourceMappingURL=file.js.map