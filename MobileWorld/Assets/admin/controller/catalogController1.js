var homeConfig = {
    page: 1,
    pageIndex: 1,
    pageSize: 5
}
var homeController = {
    init: function () {
        homeController.loadData();
        homeController.loadCatalogTypes();
        homeController.loadCatalogBrands();
    },
    registerEvent: function () {
        $('#formData').validate({
            rules: {
                name: {
                    required: true,
                    minlength: 5
                },
                pictureuri: {
                    required: true
                },
                price: {
                    required: true
                },
                quantity: {
                    required: true
                },
                cpu: {
                    required: true
                },
                ram: {
                    required: true
                },
                screen: {
                    required: true
                },
                os: {
                    required: true
                }
            },
            messages: {
                name: {
                    required: "Hãy nhập tên sản phẩm!",
                    minlength: "Tên sản phẩm phải trên 5 kí tự!"
                },
                pictureuri: {
                    required: "Hãy nhập link ảnh!"
                },
                price: {
                    required: "Hãy nhập giá!"
                },
                quantity: {
                    required: "Hãy nhập số lượng!"
                },
                cpu: {
                    required: "Hãy nhập số hiệu cpu!"
                },
                ram: {
                    required: "Hãy nhập dung lượng!"
                },
                screen: {
                    required: "Hãy nhập loại màn hình!"
                },
                os: {
                    required: "Hãy nhập hệ điều hành!"
                }
            }
        });
        $('.btnEdit').off('click').on('click', function () {
            var id = $(this).data('id');
            $('.form-control').removeClass('error');
            $('.error').hide();
            $('.slType').prop('disabled', true);
            homeController.loadDetail(id);
            $('#exampleModal').modal('show');
        });
        $('#btnAdd').off('click').on('click', function () {
            homeController.resetForm();
            $('.mobileInput').show();
            $('.laptopInput').hide();
            $('.slType').prop('disabled', false);
            $('#exampleModal').modal('show');
        });
        $('#btnSave').off('click').on('click', function () {
            if ($('#formData').valid()) {
                homeController.saveCatalog();
                $('#exampleModal').modal('hide');
            }
        });
        $('#btnCancel').off('click').on('click', function () {
            $('#exampleModal').modal('hide');
        });
        $('#btnSeach').off('click').on('click', function () {
            homeController.loadData(true);
        });
        $('#btnReset').off('click').on('click', function () {
            homeController.resetForm();
            homeController.loadData(true);
        });
        $('.slType').off('change').on('change', () => {
            if ($('.slType').val() == 2) {
                $('.mobileInput').hide();
                $('.laptopInput').show();
            }
            else if ($('.slType').val() == 1) {
                $('.laptopInput').hide();
                $('.mobileInput').show();
            }
        });
        $('.btnDelete').off('click').on('click', function () {
            var id = $(this).data('id');
            bootbox.confirm({
                message: "<h5>Xóa sản phẩm này?</h5>",
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
                        homeController.deletaCatalog(id);
                    }
                }
            });
        });
    },
    deletaCatalog: function (id) {
        $.ajax({
            url: '/Catalog/DeleteCatalog',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: (res) => {
                if (res.status) {
                    bootbox.alert('Xóa thành công');
                    homeController.loadData(true);
                    setTimeout(() => {
                        bootbox.hideAll();
                    }, 2000);
                } else {
                    bootbox.alert('Xóa không thành công');
                    setTimeout(() => {
                        bootbox.hideAll();
                    }, 2000);
                }
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    saveCatalog: function () {
        var model = toObj(document.querySelector('#formData'))
        model.catalogtypeid = $('.slType').val();
        $.ajax({
            url: '/Catalog/SaveCatalog',
            type: 'POST',
            data: {
                id: parseInt($('#id').val()),
                type: $('.slType').val(),
                model: JSON.stringify(model)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    if (model.id == 0) {
                        homeController.loadData(true);
                        $('#exampleModal').modal('hide');
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
                    if (model.id == 0) {
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
            url: '/Catalog/LoadDetail',
            type: 'GET',
            data: { id },
            dataType: 'json',
            success: function (response) {
                if (response.data) {
                    var data = response.data;
                    $('#id').val(data.id)
                    $('#name').val(data.name);
                    $('#pictureuri').val(data.pictureuri);
                    $('#price').val(data.price);
                    $('#description').val(data.description);
                    $('#content').val(data.content);
                    $('#quantity').val(data.quantity);
                    $('.slBrand').val(data.catalogbrandid);
                    $('.slType').val(data.catalogtypeid);
                    $('#cpu').val(data.cpu);
                    $('#ram').val(data.ram);
                    $('#screen').val(data.screen);
                    $('#os').val(data.os);
                    if (data.catalogtypeid == 1) {
                        $('.mobileInput').show();
                        $('.laptopInput').hide();
                        $('#backcamera').val(data.backcamera);
                        $('#frontcamera').val(data.frontcamera);
                        $('#internalmemory').val(data.internalmemory);
                        $('#memorystick').val(data.memorystick);
                        $('#sim').val(data.sim);
                        $('#batery').val(data.batery);
                    } else if (data.catalogtypeid == 2) {
                        $('.mobileInput').hide();
                        $('.laptopInput').show();
                        $('#cardscreen').val(data.cardscreen);
                        $('#connector').val(data.connector);
                        $('#harddrive').val(data.harddrive);
                        $('#design').val(data.design);
                        $('#size').val(data.size);
                        $('#release').val(data.release);
                    }
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    },
    resetForm: function () {
        $('.form-control').removeClass('error');
        $('.error').hide();
        $('#id').val('0');
        $('#name').val('');
        $('#pictureuri').val('');
        $('#price').val('');
        $('#description').val('');
        $('#content').val('');
        $('#quantity').val('');
        $('#slBrand').val('0');
        $('#slType').val('0');
        $('.slBrand').val('1');
        $('.slType').val('1');
        $('#cpu').val('');
        $('#ram').val('');
        $('#screen').val('');
        $('#os').val('');
        $('#backcamera').val('');
        $('#frontcamera').val('');
        $('#internalmemory').val('');
        $('#memorystick').val('');
        $('#sim').val('');
        $('#batery').val('');
        $('#cardscreen').val('');
        $('#connector').val('');
        $('#harddrive').val('');
        $('#design').val('');
        $('#size').val('');
        $('#release').val('');
    },
    loadData: function (changePageSize) {
        $.ajax({
            url: '/Catalog/GetAll',
            type: 'GET',
            data: {
                name: $('#txtSeach').val(),
                idbrand: $('#slBrand').val(),
                idtype: $('#slType').val(),
                page: homeConfig.pageIndex,
                pageSize: homeConfig.pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.data != null) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            id: item.id,
                            name: item.name,
                            pictureuri: item.pictureuri,
                            description: item.description,
                            price: item.price,
                            screen: item.screen,
                            ram: item.ram,
                            cpu: item.cpu,
                            quantity: item.quantity
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
    loadCatalogTypes: function () {
        $.ajax({
            url: '/Catalog/GetAllType',
            type: 'GET',
            data: {},
            dataType: 'json',
            success: (res) => {
                if (res.types != null) {
                    var types = res.types;
                    $.each(types, function (i, item) {
                        $('#slType').append(`<option value="${item.id}">${item.type}</option>`);
                        $('.slType').append(`<option value="${item.id}">${item.type}</option>`);
                    });
                }
            },
            error: (err) => {
                console.log(err);
            }
        })
    },
    loadCatalogBrands: function () {
        $.ajax({
            url: '/Catalog/GetAllBrand',
            type: 'GET',
            data: {},
            dataType: 'json',
            success: (res) => {
                if (res.brands != null) {
                    $.each(res.brands, function (i, item) {
                        $('#slBrand').append(`<option value="${item.id}">${item.brand}</option>`);
                        $('.slBrand').append(`<option value="${item.id}">${item.brand}</option>`);
                    });
                }
            },
            error: (err) => {
                console.log(err);
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
homeController.init();

const toObj = (form) => {
    return Array.from(new FormData(form)).reduce(
        (acc, [key, value]) => ({
            ...acc,
            [key]: value
        }),
        {}
    );
}