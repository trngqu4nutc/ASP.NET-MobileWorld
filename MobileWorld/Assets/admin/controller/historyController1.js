var state = {
    page: 1,
    pageSize: 5,
    seach: '',
    brandid: 0,
    month: 0
}
controller = {
    init: () => {
        controller.loadData(true);
        controller.loadCatalogBrands();
        controller.registerEvent();
    },
    registerEvent: () => {
        $('#slBrand').off('onchange').on('change', function () {
            state.brandid = parseInt($(this).val());
            controller.loadData(true);
        });
        $('#slMonth').off('onchange').on('change', function () {
            state.month = parseInt($(this).val());
            controller.loadData(true);
        });
        $('#btnSeach').off('click').on('click', function () {
            state.seach = $('#txtSeach').val();
            controller.loadData(true);
        });
        $('#btnReset').off('click').on('click', function () {
            controller.reset();
            controller.loadData(true);
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            let ids = []
            var data = $('#tblData > tr > td > button');
            $.each(data, (i, item) => {
                ids.push(parseInt(item.attributes[0].value));
            });
            bootbox.confirm({
                message: "<h5>Xóa các đơn hàng này?</h5>",
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
                        controller.deleteAllHistory(ids);
                    }
                }
            });
        });
    },
    deleteAllHistory: (ids) => {
        $.ajax({
            url: '/History/DeleteAllHistory',
            type: 'GET',
            data: { ids: JSON.stringify(ids) },
            dataType: 'json',
            success: (res) => {
                let msg = '';
                if (res.status) {
                    msg = 'Xóa thành công!';
                    controller.loadData(true);
                } else {
                    msg = 'Xóa không thành công!';
                }
                bootbox.alert(msg);
                setTimeout(() => {
                    bootbox.hideAll();
                }, 1500);
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    deleteHistory: (id) => {
        $.ajax({
            url: '/History/DeleteHistory',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: (res) => {
                let msg = '';
                if (res.status) {
                    msg = 'Xóa thành công!';
                    controller.loadData(true);
                } else {
                    msg = 'Xóa không thành công!';
                }
                bootbox.alert(msg);
                setTimeout(() => {
                    bootbox.hideAll();
                }, 1500);
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    loadData: (changePageSize) => {
        $.ajax({
            url: '/History/GetAll',
            type: 'GET',
            data: {
                seach: state.seach,
                month: state.month,
                page: state.page,
                brandid: state.brandid,
                pageSize: state.pageSize
            },
            dataType: 'json',
            success: (res) => {
                let html = '';
                $('#total').html(`Tổng đơn hàng: ${res.totalRow}.`)
                let template = $('#data-template').html();
                $.each(res.data, (i, item) => {
                    html += Mustache.render(template, {
                        id: item.id,
                        name: item.name,
                        brand: item.brand,
                        inputprice: formatCurrency(item.inputprice),
                        unit: item.unit,
                        cost: formatCurrency(item.inputprice * item.unit),
                        createdAt: item.createdAt
                    });
                });
                $('#tblData').html(html);
                $('.btnDelete').off('click').on('click', function () {
                    var id = $(this).data('id');
                    bootbox.confirm({
                        message: "<h5>Xóa đơn hàng này?</h5>",
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
                                controller.deleteHistory(id);
                            }
                        }
                    });
                });
                controller.paging(res.totalRow, changePageSize);
            },
            error: (err) => {
                console.log(err);
            }
        });
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
                    });
                }
            },
            error: (err) => {
                console.log(err);
            }
        })
    },
    paging: function (totalRow, changePageSize) {
        var totalPage = Math.ceil(totalRow / state.pageSize);
        //if (totalPage == 0) {
        //    totalPage = 1;
        //}
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
                state.page = page;
                setTimeout(function () {
                    controller.loadData();
                }, 10);
            }
        });
    },
    reset: () => {
        $('#txtSeach').val('');
        $('#slBrand').val('0');
        $('#slMonth').val('0');
        state.page = 1;
        state.brandid = 0;
        state.seach = '';
        state.month = 0;
    }
}
controller.init();
const formatCurrency = (n, separate = ".") => {
    var s = n.toString();
    var regex = /\B(?=(\d{3})+(?!\d))/g;
    var ret = s.replace(regex, separate);
    return ret;
}