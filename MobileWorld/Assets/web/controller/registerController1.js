registerController = {
    init: function () {
        registerController.registerEvent();
    },
    registerEvent: function () {
        $('#btnSign').off('click').on('click', function () {
            if (validate()) {
                bootbox.alert("Đang đăng kí, vui lòng đợi trong giây lát!");
                registerController.signUp();
            } else {
                setTimeout(() => {
                    bootbox.hideAll();
                }, 2000);
            }
        });
    },
    signUp: function () {
        var user = {
            username: $('#txtUsername').val(),
            password: $('#txtPassword').val(),
            fullname: $('#txtName').val(),
            email: $('#txtEmail').val(),
            status: true
        }
        $.ajax({
            url: '/Login/Register',
            type: 'POST',
            data: {
                model: JSON.stringify(user)
            },
            dataType: 'json',
            success: function (response) {
                bootbox.hideAll();
                if (response.status) {
                    bootbox.alert("Tạo tài khoản thành công!");
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                } else {
                    bootbox.alert("Tài khoản đã tồn tại!");
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                }
            },
            error: function (error) {
                bootbox.alert("Có lỗi xảy ra!");
                window.setTimeout(function () {
                    bootbox.hideAll();
                }, 2000);
            }
        });
    }
}
const validate = () => {
    let username = $('#txtUsername').val();
    let password = $('#txtPassword').val();
    let rePassword = $('#txtRePassword').val();
    let email = $('#txtEmail').val();
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
    } else if (password != rePassword) {
        bootbox.alert("Xác nhận mật khẩu không đúng!");
        check = false;
    } else if (email == "") {
        bootbox.alert("Hãy nhập email!");
        check = false;
    } else if (!emailIsValid(email)) {
        bootbox.alert("Email không hợp lệ!");
        check = false;
    }
    return check;
}
const emailIsValid = (email) => {
    check = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
    return check;
}
registerController.init();