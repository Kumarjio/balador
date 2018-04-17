var _d = [];
function init() {

    $(document).on('click', '#share form input[type=button]', function (e) {
        debugger;
        e.preventDefault();
        debugger;
        var idindex = $(this).data("index");
        
        var $form = $(this).parent().parent().parent();// $(this).parent();
        var formData = $form.serialize();
        ajax_token(formData, '/api/setToken', 'POST', 'application/x-www-form-urlencoded', function (got) {
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

        var $form = $(this).parent().parent().parent();//    $(this).parent();
        var action = $form.attr('action');
        var formData = $form.serialize();
        ajax_token(formData, action, 'POST', 'application/x-www-form-urlencoded', function (got) {
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
            //if (got.Result == null) {
            //    alert("NULL!!!");
            //    return;
            //}
            formToken = document.getElementById(tokenText);
            formSignup = document.getElementById(signupText);
            formOkComplete = document.getElementById(okText);
            formSignup.style.display = "none";

            if (got.Result != null && got.Result.IsTwoFactorAuthentication == true) {
                formToken.style.display = "block";
                return;
            }
            formOkComplete.style.display = "block";
        });


    }); 

    //$(".mdl-checkbox").on("change", function () {
    $(document).on('change', '.mdl-checkbox', function (e) {
        debugger;
       // e.preventDefault();
        var idindex = $(this).data("index");
        var ischeck = $(this).hasClass("is-checked"); 
        var register_id = "register_id_" + idindex;
        var elembutton = document.getElementById(register_id);
        var check = $(this).children().first();
        if (check != null &&  check.length>0 &&  check[0].checked) {
            elembutton.removeAttribute("disabled");
        //    return $(this).children().first().attr("checked", true);
            
        } else {
            elembutton.setAttribute("disabled", true);
          //  return $(this).children().first().removeAttr("checked");
            
        }
    });

    $('#preloader').show();
    ajax_token(null, '/api/getMessagers', 'GET', 'application/json; charset=utf-8', function (data) {
        var items = [];
        $.each(data, function (key, val) {
            $('#preloader').hide();
            _d.push(val);

            var $div_container = $('<div  />', { class: 'card', id: 'div_' + val.Id });
            var $div_content = $('<div  />', { class: 'card-content' });
            var $div_row = $('<div  />', { class: 'row' });
            var $card__actions = $('<div  class="mdc-card__actions" />');
            var $card__actions_buttons = $('<div  class="mdc-card__action-buttons" />');
            var completeText = $('<div />', { id: 'ok_' + val.Id, text: "Complete " + val.Message });

            completeText.appendTo($div_container);
            var errText = $('<div />', { id: 'err_' + val.Id, text: "Error on " + val.Message });
            errText.appendTo($div_container);
            completeText.hide();
            errText.hide();
            $form = $('<form />', { action: '/api/signIn', method: 'POST', id: "frmSignup_" + val.Id });
            $form.data("frmsignupid", val.Id);
            var frmRName = $('<input />', { name: 'formType', type: 'hidden', placeholder: 'Name', value: val.Id });


            var frmTokenMessage = $('<div />', { text: val.Message });
            var $frmToken = $('<form />', { action: '/api/setToken', method: 'POST', id: "frmToken_" + val.Id });
            var fTokenHidden = $('<input />', { name: 'formType', type: 'hidden', placeholder: 'Name', value: val.Id });
            var $fieldToken = $('<input />', { name: "token", placeholder: "Write Token", type: 'text' });
            var btnValidate = $('<input />', { type: 'button', value: 'Validate' });
            btnValidate.data("index", val.Id);
            $frmToken.append(frmTokenMessage, fTokenHidden, $fieldToken, $('<br />'), btnValidate).appendTo($div_container);
            $frmToken.hide();

            if (!val.IsAlreadyRegister) {
                if (val.TwoFactorAuthentication) {
                    $form.attr('action', '/api/SignIn');
                }
                else {
                    $form.attr('action', '/api/setToken');
                }

                $form.data("frmsignupid", val.Id);
                var frmSave = $('<div />');//, { text: val.Message });
                var frmRName = $('<input />', { name: 'formType', type: 'hidden', placeholder: 'Name', value: val.Id });
                var frmButton = $('<input />', {
                    type: 'submit', value: 'Register',
                    Id: 'register_id_' + val.Id, class: 'mdc-button mdc-card__action mdc-card__action--button mdc-ripple-upgraded'
                });
               
                $card__actions_buttons.append(frmButton).appendTo($card__actions);

                if (val.IsAgreement) {
                   frmButton.attr('disabled',true);
                }
                frmButton.data("index", val.Id);
                var fields = val.Fields;
                $form.show();
                var $fieldaccept, $fieldacceptlabel; var $fieldForm;
                for (var i = 0; i < fields.length; i++) {
                    var field = fields[i];
                    if (field.FieldViewType != null && field.FieldViewType == "Aggrement") {
                        debugger;
                        $fieldaccept = $('<input/>', {  type: 'checkbox', id: field.Name + val.Id });
                        $fieldacceptlabel = $('<label for=' + field.Name + val.Id + ' />');
                        $fieldacceptlabel.text(val.Agreement);
                      
                        $fieldForm = $('<div />', { class: 'mdl-checkbox' }).append($fieldaccept, $fieldacceptlabel);
                      
                    }
                    else {
                        $fieldForm = $('<input />', { name: field.Name, placeholder: field.Title, type: 'text' });

                    }
                    debugger;
                    $fieldForm.data("index", val.Id);
                    $div_row.append($fieldForm, $('<br />'))
                }
                frmSave.append($div_row);
                $form.append(frmSave, $('<br />'), frmRName, $card__actions).appendTo($div_container);
               // $form.append(frmSave, $('<br />'), frmRName, frmButton).appendTo($div_container);
            }
            else {

                $form.attr('action', '/api/unRegister');

                var frmSave = $('<div />');//, { text: val.Message });
                var frmButton = $('<input />', {
                    type: 'submit', value: 'Unregister' ,class: 'mdc-button mdc-card__action mdc-card__action--button mdc-ripple-upgraded'
                });
                frmButton.data("index", val.Id);

                $form.show();
                frmSave.append($div_row);
                $card__actions_buttons.append(frmButton).appendTo($card__actions);

                $form.append(frmSave, $('<br />'), frmRName, $card__actions).appendTo($div_container);

            }
            var $title_row = $('<div class="row" />');
            var $logo= $('<div class="col s12 m2 l2" ><img src="'+val.Logo+'"/>');
            var $header_text = $('<div class="col s12 m6 l6" ><h3 style="text-align:left">' + val.Message + '</h3>');
            $title_row.append($logo, $header_text);
            // $div_container.append($form, $hr).appendTo($('#share'));
            $div_content.append($title_row,$form);
            $div_container.append($div_content).appendTo($('#share'));
        });

    });

}