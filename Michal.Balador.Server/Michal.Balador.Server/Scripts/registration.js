var _d = [];
function init() {
    debugger;
    $(document).on('click', '#share form input[type=button]', function (e) {
        debugger;
        e.preventDefault();
        debugger;
        var idindex = $(this).data("index");

        var $form = $(this).parent();
        var formData = $form.serialize();
        ajax_token(formData, '/api/setToken', 'POST', 'application/x-www-form-urlencoded',function (got) {
            debugger;
            if (got.IsError) {
                alert(got.Message);
                return;
            }
            var tokenText = "frmToken_" + idindex;
            var signupText = "frmSignup_" + idindex;
            var okText = "ok_" + idindex;
            var errText = "err_" + idindex;

            formErr = document.getElementById(signupText);
            if (got.IsError) {
                alert(got.Message);
                //  formErr.style.display = "block";
                return;
            }
            formToken = document.getElementById(tokenText);
            formSignup = document.getElementById(signupText);
            formOkComplete = document.getElementById(okText);
            formSignup.style.display = "none";
            formOkComplete.style.display = "block";
            formToken.style.display = "none";
        }
        );

    });

    $(document).on('click', '#share form input[type=submit]', function (e) {
        e.preventDefault();
        debugger;

        var idindex = $(this).data("index");

        var $form = $(this).parent();
        var formData = $form.serialize();
        ajax_token(formData, '/api/setToken','POST' ,'application/x-www-form-urlencoded', function (got) {
                debugger;
                var tokenText = "frmToken_" + idindex;
                var signupText = "frmSignup_" + idindex;
                var okText = "ok_" + idindex;
                var errText = "err_" + idindex;
                formErr = document.getElementById(signupText);
                if (got.IsError) {
                    alert(got.Message);
                    //  formErr.style.display = "block";
                    return;
                }
                if (got.Result == null) {
                    alert("NULL!!!");
                    return;
                }
                formToken = document.getElementById(tokenText);
                formSignup = document.getElementById(signupText);
                formOkComplete = document.getElementById(okText);
                formSignup.style.display = "none";

                if (got.Result.IsTwoFactorAuthentication == true) {
                    formToken.style.display = "block";
                    return;
                }
                formOkComplete.style.display = "block";
        });

      
    });

    ajax_token(null, '/api/signup','GET', 'application/json; charset=utf-8', function (data) {
        debugger;
        var items = [];
        $.each(data, function (key, val) {
            _d.push(val);

            var $div_container = $('<div  />', { class: 'container', id: 'div_' + val.Id });
            var $div_row = $('<div  />', { class: 'row' });
            var completeText = $('<div />', { id: 'ok_' + val.Id, text: "Complete " + val.Message });

            completeText.appendTo($div_container);
            var errText = $('<div />', { id: 'err_' + val.Id, text: "Error on " + val.Message });
            errText.appendTo($div_container);
            completeText.hide();
            errText.hide();


            var frmTokenMessage = $('<div />', { text: val.Message });
            var $frmToken = $('<form />', { action: '/api/setToken', method: 'POST', id: "frmToken_" + val.Id });
            var fTokenHidden = $('<input />', { name: 'formType', type: 'hidden', placeholder: 'Name', value: val.Id });
            var $fieldToken = $('<input />', { name: "txtToken", placeholder: "Write Token", type: 'text' });
            var btnValidate = $('<input />', { type: 'button', value: 'Validate' });
            btnValidate.data("index", val.Id);
            $frmToken.append(frmTokenMessage, fTokenHidden, $fieldToken, $('<br />'), btnValidate).appendTo($div_container);
            $frmToken.hide();

            var $form = $('<form />', { action: '/api/signup', method: 'POST', id: "frmSignup_" + val.Id });
            $form.data("frmsignupid", val.Id);
            var frmSave = $('<div />', { text: val.Message });
            var frmRName = $('<input />', { name: 'formType', type: 'hidden', placeholder: 'Name', value: val.Id });
            var frmButton = $('<input />', { type: 'submit', value: 'Apply' });
            frmButton.data("index", val.Id);
            var fields = val.Fields;
            $form.show();

            for (var i = 0; i < fields.length; i++) {
                var field = fields[i];
                var $fieldForm = $('<input />', { name: field.Name, placeholder: field.Title, type: 'text' });
                $div_row.append($fieldForm, $('<br />'));
            }
            frmSave.append($div_row);
            $form.append(frmSave, $('<br />'), frmRName, frmButton).appendTo($div_container);
            var $hr = $('<hr />', { class: 'style16' });
            $div_container.append($form, $hr).appendTo($('#share'));
        });
    
    });
    
}