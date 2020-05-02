var homeConfig = {
    page: 1,
    pageIndex: 1,
    pageSize: 5
}
var homeController = {
    init: function () {
        homeController.loadData();
    },
    registerEvent: function () {
        $('#formData').validate({
            rules: {
                username: {
                    required: true,
                    minlength: 5
                },
                password: {
                    required: true,
                    minlength: 5
                }
            },
            messages: {
                username: {
                    required: "Hãy nhập tên tài khoản!",
                    minlength: "Tên tài khoản phải trên 5 kí tự!"
                },
                password: {
                    required: "Hãy nhập mật khẩu!",
                    minlength: "Mật khẩu phải trên 5 kí tự!"
                }
            }
        });
        $('.btnEdit').off('click').on('click', function () {
            var id = $(this).data('id');
            homeController.loadDetail(id);
            $('.pass').hide();
            $('#exampleModal').modal('show');
        });
        $('#btnAdd').off('click').on('click', function () {
            $('#exampleModal').modal('show');
            $('.pass').show();
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#formData').valid()) {
                homeController.saveUser();
                $('#exampleModal').modal('hide');
                homeController.resetForm();
                $('.pass').hide();
            }
        });
        $('#btnCancel').off('click').on('click', function () {
            $('#exampleModal').modal('hide');
            $('.pass').hide();
            homeController.resetForm();
            $('.form-control').removeClass('error');
            $('.error').hide();
        });
        $('#btnSeach').off('click').on('click', function () {
            homeController.loadData(true);
        });
        $('#btnReset').off('click').on('click', function () {
            homeController.resetForm();
            homeController.loadData(true);
        });
    },
    saveUser: function () {
        var model = {
            id: $('#txtId').val(),
            username: $('#txtUsername').val(),
            fullname: $('#txtName').val(),
            phonenumber: $('#txtPhone').val(),
            email: $('#txtEmail').val(),
            address: $('#txtAddress').val(),
            status: $('#slActive').val() == 'false' ? false : true
        };
        if ($('#txtPassword').val() != '') {
            model.password = $('#txtPassword').val();
        }
        $.ajax({
            url: '/User/Save',
            type: 'POST',
            data: {
                model: JSON.stringify(model)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    if (model.id === '0') {
                        homeController.loadData(true);
                        bootbox.alert("Thêm thành công!");
                        window.setTimeout(function () {
                            bootbox.hideAll();
                        }, 2000);
                    } else {
                        homeController.loadData();
                        bootbox.alert("Sửa thành công!");
                        window.setTimeout(function () {
                            bootbox.hideAll();
                        }, 2000);
                    }
                } else {
                    if (model.id === '0') {
                        bootbox.alert("Thêm thất bại!");
                        window.setTimeout(function () {
                            bootbox.hideAll();
                        }, 2000);
                    } else {
                        bootbox.alert("Sửa thất bại!");
                        window.setTimeout(function () {
                            bootbox.hideAll();
                        }, 2000);
                    }
                }
            },
            error: function (error) {
                console.log(error);
                bootbox.alert("Sửa thất bại!");
            }
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/User/LoadDetail',
            type: 'GET',
            data: { id },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    $('#txtId').val(data.id);
                    $('#txtUsername').val(data.username);
                    $('#txtName').val(data.fullname);
                    $('#txtPhone').val(data.phonenumber);
                    $('#txtAddress').val(data.address);
                    $('#txtEmail').val(data.email);
                    $('#slActive').val(data.status ? 'true' : 'false');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    },
    resetForm: function () {
        $('#txtId').val('0');
        $('#txtUsername').val('');
        $('#txtPassword').val('');
        $('#txtName').val('');
        $('#txtPhone').val('');
        $('#txtAddress').val('');
        $('#txtEmail').val('');
        $('#slActive').val('');
        $('#txtSeach').val('');
        $('#slSeach').val('');
    },
    loadData: function (changePageSize) {
        $.ajax({
            url: '/User/LoadData',
            type: 'GET',
            data: {
                name: $('#txtSeach').val(),
                status: $('#slSeach').val(),
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            id: item.id,
                            username: item.username,
                            fullname: item.fullname,
                            email: item.email,
                            phonenumber: item.phonenumber,
                            address: item.address,
                            status: item.status == false ? "<a style=\"margin-left: 40%; color: red;\"><i class=\"fas fa-user-lock\"></i></a>" : "<a style=\"margin-left: 40%; color: green;\"><i class=\"fas fa-check\"></i></a>",
                            role: item.role
                        });
                    });
                    $('#tblData').html(html);
                    homeController.paging(response.totalRow, changePageSize);
                    homeController.registerEvent();
                }
            },
            error: function (error) {
                console.log(error);
            }
        })
    },
    paging: function (totalRow, changePageSize) {
        var totalPage = Math.ceil(totalRow / homeConfig.pageSize);
        //load lai totalPage
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            visiblePages: 5,
            first: "Đầu",
            last: "Cuối",
            next: "Tiếp",
            prev: "Trước",
            onPageClick: function (event, page) {
                homeConfig.pageIndex = page;
                setTimeout(function () {
                    homeController.loadData();
                }, 10);
            }
        });
    }
}
homeController.init()