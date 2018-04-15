function sayHello() {
	const compiler = (document.getElementById("compiler") as HTMLInputElement).value;
	const framework = (document.getElementById("framework") as HTMLInputElement).value;
	return `Hello from ${compiler} and ${framework}!`;
}

function readValue(controlName: string): string {
	var control = <HTMLSelectElement>$('#' + controlName)[0];
	return control.value;
}

function addVideo(sectionKey: number, url: string, description: string, author: string, startTime: string): void {
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

function search(bookKey: number, guardKey: number, techniqueKey: number, footworkKey: number, targetKey: number): void {
	window.location.href = "/demo/search?bookKey=" + bookKey + "&guardKey=" + guardKey + "&techniqueKey=" + techniqueKey + "&footworkKey=" + footworkKey + "&targetKey=" + targetKey;
}