var config = {
    brand: '',
    page: 1,
    pageSize: 5
}
var controller = {
    init: function () {
        controller.LoadData();
    },
    registerEvent: function () {
        $('#formData').validate({
            rules: {
                brand: {
                    required: true
                },
                pictureurl: {
                    required: true
                }
            },
            messages: {
                brand: {
                    required: 'Hãy nhập tên hãng'
                },
                pictureurl: {
                    required: 'Hãy nhập đường dẫn ảnh'
                }
            }
        });
        $('.btnEdit').off('click').on('click', function () {
            controller.ResetForm();
            controller.LoadDetail($(this).data('id'));
            $('#exampleModal').modal('show');
        });
        $('#btnAdd').off('click').on('click', function () {
            controller.ResetForm();
            $('#exampleModal').modal('show');
        });
        $('#btnCancel').off('click').on('click', function () {
            $('#exampleModal').modal('hide');
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#formData').valid()) {
                controller.SaveBrand();
                $('#exampleModal').modal('hide');
            }
        });
        $('.btnDelete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm({
                message: "<h5>Xóa hãng này?</h5>",
                buttons: {
                    confirm: {
                        label: "Yes",
                        className: "btn btn-success"
                    },
                    cancel: {
                        label: "Cancel",
                        className: "btn btn-danger"
                    }
                },
                callback: function (result) {
                    if (result) {
                        controller.DeleteBrand(id);
                    }
                }
            });
        });
        $('#btnSeach').off('click').on('click', function () {
            controller.LoadData(true);
        });
        $('#btnReset').off('click').on('click', function () {
            $('#txtSeach').val('');
            controller.LoadData(true);
        });
    },
    LoadDetail: function (id) {
        $.ajax({
            url: '/Brand/LoadDetail',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: (res) => {
                $('#id').val(res.data.id);
                $('#brand').val(res.data.brand);
                $('#pictureurl').val(res.data.pictureurl);
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    DeleteBrand: function (id) {
        $.ajax({
            url: '/Brand/DeleteBrand',
            type: 'GET',
            data: { id: id },
            success: (res) => {
                var msg = '';
                if (res.status) {
                    msg = 'Xóa thành công!';
                    controller.LoadData(true);
                } else {
                    msg = 'Xóa thất bại!';
                }
                bootbox.alert(msg);
                setTimeout(() => {
                    bootbox.hideAll();
                }, 2000);
            }
        });
    },
    SaveBrand: function () {
        var model = {
            id: $('#id').val(),
            brand: $('#brand').val(),
            pictureurl: $('#pictureurl').val()
        }
        $.ajax({
            url: '/Brand/SaveBrand',
            type: 'POST',
            data: {
                model: JSON.stringify(model)
            },
            success: (res) => {
                var msg = '';
                if (res.status == 1) {
                    if ($('#id').val() == 0) {
                        msg = 'Thêm';
                        controller.LoadData(true);
                    } else {
                        msg = 'Sửa';
                        controller.LoadData();
                    }
                    bootbox.alert(msg + ' thành công!');
                } else if (res.status == -1) {
                    bootbox.alert('Hãng này đã tồn tại!');
                } else {
                    if ($('#id').val() == 0) {
                        msg = 'Thêm';
                    } else {
                        msg = 'Sửa';
                    }
                    bootbox.alert(msg + ' thất bại!');
                }
                setTimeout(() => {
                    bootbox.hideAll();
                }, 2000);
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    LoadData: function (changePageSize) {
        $.ajax({
            url: '/Brand/LoadData',
            type: 'GET',
            data: {
                brand: $('#txtSeach').val(),
                page: config.page,
                pageSize: config.pageSize
            },
            dataType: 'json',
            success: (res) => {
                var html = '';
                var template = $('#data-template').html();
                $.each(res.data, function (i, item) {
                    html += Mustache.render(template, {
                        id: item.id,
                        brand: item.brand,
                        pictureurl: item.pictureurl
                    });
                });
                $('#tblData').html(html);
                controller.registerEvent();
                controller.paging(res.totalRow, changePageSize);
            },
            error: (error) => {
                console.log(error);
            }
        });
    },
    paging: function (totalRow, changePageSize) {
        var totalPage = Math.ceil(totalRow / config.pageSize);
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
                config.page = page;
                setTimeout(function () {
                    controller.LoadData();
                }, 10);
            }
        });
    },
    ResetForm: function () {
        $('.form-control').removeClass('error');
        $('.error').hide();
        $('#id').val(0);
        $('#brand').val('');
        $('#pictureurl').val('');
    }
}
controller.init();