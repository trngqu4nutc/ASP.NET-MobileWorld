controller = {
    init: () => {
        controller.registerEvent();
    },
    registerEvent: () => {
        $('#btnSign').off('click').on('click', function () {
            if (validate()) {
                var model = {
                    username: $('#txtUsername').val(),
                    email: $('#txtEmail').val()
                }
                bootbox.alert('Đang tiến hành xác thực, vui lòng đợi trong giây lát!')
                controller.authAccount(model);
            } else {
                setTimeout(() => {
                    bootbox.hideAll();
                }, 1500);
            }
        });
    },
    authAccount: (model) => {
        $.ajax({
            url: '/Login/AuthAccount',
            type: 'POST',
            data: { model: JSON.stringify(model) },
            dataType: 'json',
            success: (res) => {
                bootbox.hideAll();
                var msg = '';
                if (res.status == 1) {
                    msg = 'Xác thực thành công, hãy kiểm tra mật khẩu của bạn qua email!';
                } else if (res.status == 0) {
                    msg = 'Tài khoản không tồn tại!';
                } else if (res.status == -1) {
                    msg = 'Email xác thực không chính xác!';
                }
                bootbox.alert(msg);
                setTimeout(() => {
                    bootbox.hideAll();
                }, 2000);
            },
            error: (err) => {
                console.log(err);
            }
        });
    }
}
const validate = () => {
    let username = $('#txtUsername').val();
    let email = $('#txtEmail').val();
    let check = true;
    if (username.length == 0) {
        bootbox.alert('Hãy nhập tài khoản!');
        check = false;
    } else if (email.length == 0) {
        bootbox.alert('Hãy nhập email!');
        check = false;
    } else if (!emailIsValid(email)) {
        bootbox.alert('Email không hợp lệ!');
        check = false;
    }
    return check;
}
const emailIsValid = (email) => {
    check = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
    return check;
}
controller.init();