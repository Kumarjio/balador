
function subscribe(url,filter) {
    debugger;
    var fromdata = JSON.stringify({
        WebHookUri: url,
        Secret: sessionStorage.secret,
        Description: " WebHook!",
        Filters: [filter]
    });
        ajax_token(fromdata, '/api/balador/registrations', 'POST', 'application/json; charset=utf-8',
        function (data) {
            debugger;
            alert(data.Message ? data.Message : status);
        },
        function (errMsg) {
            alert(errMsg.responseJSON.Message);
        });
}

function getAllWebhooks() {
    debugger;

    ajax_token(null, '/api/balador/registrations', 'GET', 'application/json; charset=utf-8', function (data) {
        debugger;
        var items = data;
        $.each(items, function (key, val) {
            debugger;
            var webhook = items[key];
            if (webhook!=null && webhook.Filters != null && webhook.Filters[0] != null) {
                if (webhook.Filters[0] == 'preUpdate') {
                   // $("#preSendUri").MaterialTextfield.checkDirty();
                   // $("#preSendUri").MaterialTextfield.change(webhook.WebHookUri);
                    //$("#preSendUri").get(0).MaterialTextfield.change('test');
                    $("#preSendId").val(webhook.Id);
                   $("#preSendUri").val(webhook.WebHookUri);
                }
                else {
                    $("#postSendId").val(webhook.Id);
                    $("#postSendUri").val(webhook.WebHookUri);
                }
                Materialize.updateTextFields();
            }
        });

    });
    }
function unsubscribe(id) {
    debugger;
    var deferred = $.Deferred();
    ajax_token(null, '/api/balador/registrations/' + id, 'DELETE', 'application/json; charset=utf-8',
        function (data) {
            debugger;
            alert(data.Message ? data.Message : status);
            deferred.resolve(resp);
        },
        function (errMsg) {
            debugger;
            alert(errMsg.responseJSON.Message);
            deferred.reject();
        });
    return deferred.promise();
}

function init() {
    $("#sendPre").click(function () {
        debugger;
        var preSendId=$("#preSendId").val();
        if (preSendId != null && preSendId!='') {
            var chained = unsubscribe(preSendId).then(subscribe($("#preSendUri").val(), "preUpdate")).fail(function (merr) {
                debugger;
            });
        }
        else {
            subscribe($("#preSendUri").val(), "preUpdate");
        }
     
        
    });
    $("#sendPost").click(function () {
        debugger;
        //unsubscribe($("#postSendId").val());
        //subscribe( $("#postSendUri").val(), "postUpdate")
        var postSendId = $("#postSendId").val();
        if (postSendId != null && postSendId != '') {
            var chained = unsubscribe(postSendId).then(subscribe($("#postSendUri").val(), "postUpdate")).fail(function (merr) {
                debugger;
            });
        }
        else {
            subscribe($("#postSendUri").val(), "postUpdate");
        }
    });
    getAllWebhooks();
}