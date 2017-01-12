

function ConfirmDeletion(message, headerMsg, postUrl, deletePosId, id, alertFalseMsg, alertTrueMsg, windowUrl) {
    $("#dialog-confirm").html(message);
    var deletedata ="{" + deletePosId + ":" + id + "}";  
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: headerMsg,
        buttons: {
            "Yes": { id: 'close', text: 'Yes', click: function () {
                $(this).dialog("close");
                $.ajax({
                    type: 'POST',
                    url: postUrl,
                    data: deletedata,
                    contentType: 'application/json',
                    success: function (data) {
                        if (data.toLowerCase() == "false") {
                            alert(alertFalseMsg);
                        }
                        else if (data.toLowerCase() == "true") {
                            alert(alertTrueMsg);
                            window.location = windowUrl;
                        }  
                    }
                });
            }, "class": "btn  btn-primary"
            },
            "No": { id: 'continue', text: 'No', click: function () { $(this).dialog("close"); }, "class": "btn  btn-primary" }
        }
    });
}



