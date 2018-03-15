$('document').ready(function () {
    debugger;
    /* set un input in log in page */
    var url = window.location.pathname;
    var filename = url.substring(url.lastIndexOf('/') + 1)

    if (filename === 'login.html') {

        $('#un').val(localStorage.getItem("un"));

    }

    if (filename != 'login.html') {

        if (sessionStorage.login === undefined && filename != 'login.html') {

            window.location.href = "login.html";
        }
        else {
            var token = sessionStorage.getItem("login");
            var unval = localStorage.getItem("un");

            ///////Validate user token

            //var link19 = "http://demo.btogather.com/aspjson/index.asp?fcase=18&un="+unval+"&token="+token;
            var link19 = JSON_URL + "?fcase=18&un=" + unval + "&token=" + token;

            $.getJSON(link19, function (results) {
                $.each(results, function (index) {

                    if (results[index].tok != 1) {
                        window.location.href = "login.html";
                    }
                    {
                        localStorage.setItem('Lang', results[index].lang);



                    }

                });

            });


        }
    }

    /* login submit */
    $("#loginsubmit").click(function () {

        debugger;
        var alerttextempty = "Field can't be empty!";
        if ($('#un').val() == '') {


            $("#un").css('background-color', 'OrangeRed');
            $("#error").html('<div class="btn btn-warning" style="margin-bottom:10px;">  &nbsp; ' + alerttextempty + ' </div>');
            return false;
        }

        if ($('#ps').val() == '') {
            $("#ps").css('background-color', 'OrangeRed');
            $("#error").html('<div class="btn btn-warning" style="margin-bottom:10px;">  &nbsp; ' + alerttextempty + ' </div>');

            return false;
        }

        var data = $("#login-form").serialize();

        $.ajax({
            type: 'POST',
            //url: 'http://demo.btogather.com/aspjson/index.asp?fcase=10',
            url: JSON_URL + '?fcase=10',
            data: data,
            beforeSend: function () {
                $("#error").fadeOut();
                $("#un").css('background-color', '#fff');
                $("#ps").css('background-color', '#fff');
                $("button").html('<span class="glyphicon glyphicon-transfer" class="btn btn-warning"></span> &nbsp; sending ...');
            },
            success: function (response) {
                if (response == "2") {

                    var alerttext = 'Inputs are incorrect.Please try again!';
                    $("#error").fadeIn(1000, function () {
                        $("#error").html('<div class="btn btn-warning" style="margin-bottom:10px;">  &nbsp; ' + alerttext + ' </div>');
                        $("button").html('<span class="glyphicon glyphicon-log-in"></span> &nbsp; Sign In');
                    });

                }
                else {

                    sessionStorage.setItem('login', response);
                    localStorage.setItem('un', $('#un').val());


                    var ComID = localStorage.getItem("ComID");
                    $("button").html('<img src="../build/images/btn-ajax-loader.gif" /> &nbsp; Signing In ...');
                    setTimeout(' window.location.href = "index.html?ComID=' + ComID + '"; ', 4000);
                }
            }
        });
        return false;
    });
    /* login submit */

    var url = window.location.pathname;
    var filename = url.substring(url.lastIndexOf('/') + 1)
    if (filename != 'login.html') {
        var UserName = localStorage.getItem("un");


        //var link18 = "http://demo.btogather.com/aspjson/index.asp?fcase=17&UserName="+UserName;
        var link18 = JSON_URL + "?fcase=17&UserName=" + UserName;


        $.getJSON(link18, function (results) {
            $.each(results, function (index) {

                $("#ComLinks").append('<li id="ComLinksid"><a href="index.html?ComID=' + results[index].comid + '&SupplierID=' + results[index].Name + '" >' + results[index].FullName + '</a></li>');
            });

        });
    }


});
