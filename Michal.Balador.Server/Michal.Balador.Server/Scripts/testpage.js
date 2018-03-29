
function send() {
    debugger;
    var da = $("#testform").serialize(); 
    debugger;
    alert(da);
    ajax_token(da, '/api/SendToClient', 'POST', 'application/x-www-form-urlencoded',
        function (data) {
            debugger;
            alert(data.Message ? data.Message : status);
            setTimeout('window.location.href = "index.html"', 100);
        },
        function (errMsg) {
            alert(errMsg.responseJSON.Message);
        });
}

function init() {
    debugger;
    $("#send").click(function () {
        debugger;
        send();
        
    });

}