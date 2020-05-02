var loginController = {
    init: function () {
        loginController.registerEvent();
    },
    registerEvent: function () {
        $('#btnSign').off('click').on('click', function () {
            if (validate()) {
                bootbox.alert("Đang đăng nhập!");
                window.setTimeout(function () {
                    bootbox.hideAll();
                }, 1500);
                loginController.login();
            } else {
                setTimeout(() => {
                    bootbox.hideAll();
                }, 2000);
            }
        });
    },
    login: function () {
        var user = {
            username: $('#txtUsername').val(),
            password: $('#txtPassword').val()
        }
        $.ajax({
            url: '/Login/Signin',
            type: 'POST',
            data: {
                model: JSON.stringify(user)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status == 1) {
                    if (response.role == 2 || response.role == 3) {
                        window.location.href = '/Admin/HomeAdmin/Index';
                    } else {
                        window.location.href = '/Home/Index';
                    }
                } else if (response.status == 0) {
                    bootbox.alert("Tài khoản không tồn tại!");
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                } else if (response.status == -1) {
                    bootbox.alert("Tài khoản đã bị khóa!");
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                } else if (response.status == -2) {
                    bootbox.alert("Mật khẩu không đúng!");
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}
loginController.init();
const validate = () => {
    let username = $('#txtUsername').val();
    let password = $('#txtPassword').val();
    let check = true;
    if (username == "" && password == "") {
        bootbox.alert("Hãy nhập tài khoản và mật khẩu!");
        check = false;
    } else if (username == "") {
        bootbox.alert("Hãy nhập tài khoản!");
        check = false;
    } else if (password == "") {
        bootbox.alert("Hãy nhập mật khẩu!");
        check = false;
    }
    return check;
}