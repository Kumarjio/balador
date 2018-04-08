
function send() {
    debugger;
    var da = $("#testform").serialize(); 
   // debugger;
  //  alert(da);
    ajax_token(da, '/api/SendToClient', 'POST', 'application/x-www-form-urlencoded',
        function (data) {
            debugger;
            alert(data.Message ? data.Message : status);
            window.location.assign("index.html");
        },
        function (errMsg) {
            alert(errMsg.responseJSON.Message);
        });
}

function init() {
    debugger;
    var selectBox = document.getElementById('MesssageType');

    for (var i = 0, l = ms.length; i < l; i++) {
        var option = ms[i];
        selectBox.options.add(new Option(option, option));

    }

    $("#send").click(function (event) {
        debugger;
        event.preventDefault();
        send();
        
    });

}