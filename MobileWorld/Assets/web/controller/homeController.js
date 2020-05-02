homeController = {
    init: function () {
        homeController.registerEvent();
    },
    registerEvent: function () {
        $('#login').off('click').on('click', function () {
            if ($('#login').html() == 'Đăng nhập') {
                window.location.href = '/Login';
            } else {
                homeController.logout();
            }
        });
    },
    logout: function () {
        $.ajax({
            url: '/Login/Logout',
            type: 'POST',
            data: {},
            dataType: 'json',
            success: (res) => {
                if (res.status) {
                    bootbox.alert('Đăng xuất thành công!');
                    setTimeout(() => {
                        bootbox.hideAll();
                        window.location.href = "/Home";
                    }, 1500);
                    $('#login').html('Đăng nhập').attr('color', 'white');
                    $('#greeting').html('Xin chào');
                }
            }
        });
    }
}
homeController.init();