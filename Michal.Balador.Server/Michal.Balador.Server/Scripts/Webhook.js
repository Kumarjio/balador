function subscribePreUpdate(url,secret) {
    debugger;
    //var access_token = "bearer ZzOcyv05FectwOiAgvjjVHkGi4sIBlcNsPTNjkTEzXOB4vx8U2YGu1XYwKsPyeSJI-tmzgJNsHftFeRkLZWW-8F6AV_euwN94Nc0i4B-y0sNtnWhZO1irNTcGJ_ZAGg-vVUOmfMX38E6P32Njh_VztKV0Qi7MfYpfCy5sipctn8IjMApKftg8kq0kzyEob1ZKxnkjWHi47AKq8pmgtfVQsIqbn1HcSAqs8nXbvAE5ky30PLJ0v64z2c9ocsxZr6izs_9trzypWM0pcYq8G3BI04x_7-LFzL0qYwrAANxIeMTTWjuyWZ29KBAwoo-Q1KUrQIRVAzDm4O8gqeEKFptq4T1hNsin8B3g0Y0xOLawCpGyIptIMZQuJyZ5cdVGvQ1NkF45t9ufEjoWCgSMoEF6-_RBXvsGd9BnZOeMC0RTwwpJfUJavPL66pJJMbXTVACmlqMva-pehSvpL_Os5N1f0UJC41IjFJp4H0SC9-87E5_DoO5m0tATVtzGwHk0sjU-P5VsW8kz9UqFwfm5XRshT8Bqin9hCoOnNl8OagY3YvouKcCpDvScXHMV9dW-vFh";
    var access_token = "bearer " + $("#txtToken").val();
    $.ajax({
        type: "POST",
        url: "/api/balador/registrations",
        data: JSON.stringify({
            WebHookUri: "http://localhost:1945/api/PreUpdate",
            Secret: "12345678901234567890123456789012",
            Description: "My first WebHook!",
            Filters: ["preUpdate"]
        }),
        headers: { "Authorization": access_token },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {
            debugger;
            alert(data.Message ? data.Message : status);
        },
        error: function (errMsg) {
            debugger;
            alert(errMsg.responseJSON.Message);
        }
    });
    return false;
}
function subscribePostUpdate() {
    debugger;
    //var access_token = "bearer ZzOcyv05FectwOiAgvjjVHkGi4sIBlcNsPTNjkTEzXOB4vx8U2YGu1XYwKsPyeSJI-tmzgJNsHftFeRkLZWW-8F6AV_euwN94Nc0i4B-y0sNtnWhZO1irNTcGJ_ZAGg-vVUOmfMX38E6P32Njh_VztKV0Qi7MfYpfCy5sipctn8IjMApKftg8kq0kzyEob1ZKxnkjWHi47AKq8pmgtfVQsIqbn1HcSAqs8nXbvAE5ky30PLJ0v64z2c9ocsxZr6izs_9trzypWM0pcYq8G3BI04x_7-LFzL0qYwrAANxIeMTTWjuyWZ29KBAwoo-Q1KUrQIRVAzDm4O8gqeEKFptq4T1hNsin8B3g0Y0xOLawCpGyIptIMZQuJyZ5cdVGvQ1NkF45t9ufEjoWCgSMoEF6-_RBXvsGd9BnZOeMC0RTwwpJfUJavPL66pJJMbXTVACmlqMva-pehSvpL_Os5N1f0UJC41IjFJp4H0SC9-87E5_DoO5m0tATVtzGwHk0sjU-P5VsW8kz9UqFwfm5XRshT8Bqin9hCoOnNl8OagY3YvouKcCpDvScXHMV9dW-vFh";
    var access_token = "bearer " + $("#txtToken").val();
    $.ajax({
        type: "POST",
        url: "/api/balador/registrations",
        data: JSON.stringify({
            WebHookUri: "http://localhost:1945/api/PostUpdate",
            Secret: "12345678901234567890123456789012",
            Description: "My first WebHook!",
            Filters: ["postUpdate"]
        }),
        headers: { "Authorization": access_token },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {
            debugger;
            alert(data.Message ? data.Message : status);
        },
        error: function (errMsg) {
            debugger;
            alert(errMsg.responseJSON.Message);
        }
    });
    return false;
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

                }
                else {

                }
            }
         
        });

    });
}