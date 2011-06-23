///<reference path="~/Scripts/jquery-1.5.2-vsdoc.js" />
///<reference path="~/Account/SecurityService.asmx/jsdebug" />
$(document).ready(function () {
    $("#txtUserName").blur(function () {
        var userName = $("#txtUserName").val();
        $.ajax("/Account/SecurityService.asmx/UserNameAvailable", {
            type: "POST",
            data: { userName: userName },
            dataType: "xml",
            success: function (data, textStatus, xhr) {
                if (data.p) {
                    data = data.p;
                }
                if (Boolean.parse(typeof(data.text) !== typeof(undefined) ? data.text : data.childNodes[0].textContent)) {
                    $("#spnUserNameAvaliable").text("This user name is available.").removeClass("notAvailable").addClass("available");
                    $("#btnOK").removeAttr("disabled");
                } else {
                    $("#spnUserNameAvaliable").text("This user name is already in use. Please, choose another one.").removeClass("available").addClass("notAvailable");
                    $("#btnOK").attr("disabled", "disabled");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("An error has been raised.\nPlease, check your Internet connection.\nIf it works, then it could have been a problem at the server.\n"+ textStatus + "\n" + errorThrown);
            }
        });
    });
});