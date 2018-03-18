﻿$('document').ready(function () {
    debugger;
    $(".dropdown-button").dropdown();
    var url = window.location.pathname;
    var filename = url.substring(url.lastIndexOf('/') + 1)
    if (filename == null || filename == '' || filename == 'index.html')
    {
        if (sessionStorage.token != null) {
            $('.clogin').hide();
            $('.show_a').show();
            $('.show_aherf').text("Hello " + localStorage.getItem("user") + ".. ");
        }
        else {
            $('.clogin').show();
            $('.show_a').hide();
        }
        debugger;
     
        return;
    }

    if (filename != 'login.html') {

        if (sessionStorage.token === undefined && filename != 'login.html') 
            window.location.href = "login.html";
   
        else {
            $('#txtuser').text("Hello " + localStorage.getItem("user") + " ");
            init();
        }
    }

    /* login submit */
    $("#loginsubmit").click(function () {
        debugger;
        var alerttextempty = "Field can't be empty!";
        var url = "/token";
        var data = $("#login-form").serialize();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                debugger;
                if (response != null && response.access_token != null) {
                    sessionStorage.setItem('token', response.token_type + " " + response.access_token);
                    sessionStorage.setItem('secret', response["m:secret"]);
                    localStorage.setItem('user', response.userName);
                    setTimeout('window.location.href = "index.html"', 1000);
                }
            }
        });
        return false;
    });
});
function ajax_token(data, url,typ,contentType,callback) {
    debugger;
    
     var access_token = sessionStorage.token;
    $.ajax({
       // type: "POST",
        type: typ,
        url: url,
        data: data,
        headers: { "Authorization": access_token },
        //contentType: "application/json; charset=utf-8",
        contentType: contentType,
     //   dataType: "json",
        success: function (data, status) {
            debugger;
            callback(data, status)
            //alert(data.Message ? data.Message : status);
        },
        error: function (errMsg) {
            debugger;
            alert(errMsg.responseJSON.Message);
            setTimeout('window.location.href = "login.html"', 1000);
        }
    });
    return false;
}