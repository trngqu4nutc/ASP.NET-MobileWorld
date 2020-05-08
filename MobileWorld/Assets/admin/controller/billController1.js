var state = {
    page: 1,
    pageSize: 5,
    seach: '',
    status: 0,
    month: 0
}
controller = {
    init: () => {
        controller.loadData(true);
    },
    registerEvent: () => {
        $('#slStatus').off('change').on('change', function () {
            state.seach = $('#txtSeach').val();
            state.status = $(this).val();
            controller.loadData(true);
        });
        $('#slMonth').off('change').on('change', function () {
            state.seach = $('#txtSeach').val();
            state.month = $(this).val();
            controller.loadData(true);
        });
        $('#btnSeach').off('click').on('click', function () {
            state.seach = $('#txtSeach').val();
            controller.loadData(true);
        });
        $('#btnReset').off('click').on('click', function () {
            controller.resetState();
            controller.loadData(true);
        });
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
                        controller.deleteBill(id);
                    }
                }
            });
        });
        $('.btnAccept').off('click').on('click', function () {
            var id = $(this).data('id');
            var status = $(this).data('status');
            if (status == 0) {
                bootbox.confirm({
                    message: "<h5>Duyệt đơn hàng này?</h5>",
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
                            controller.updateBill(id, 1);
                        }
                    }
                });
            }
        });
    },
    updateBill: (id, status) => {
        var model = {
            id: id,
            status: status
        }
        $.ajax({
            url: '/Bill/UpdateBill',
            type: 'POST',
            data: { model: JSON.stringify(model) },
            dataType: 'json',
            success: (res) => {
                var msg = '';
                if (res.status) {
                    msg = 'Duyệt đơn hàng thành công!';
                    controller.loadData(true);
                } else {
                    msg = 'Không thể duyệt đơn hàng này!'
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
    },
    deleteBill: (id) => {
        $.ajax({
            url: '/Bill/DeleteBill',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: (res) => {
                var msg = '';
                if (res.status) {
                    msg = 'Xóa thành công!';
                    controller.loadData(true);
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
    loadData: (changePageSize) => {
        $.ajax({
            url: '/Bill/LoadData',
            type: 'GET',
            data: {
                seach: state.seach,
                status: state.status,
                month: state.month,
                page: state.page,
                pageSize: state.pageSize
            },
            dataType: 'json',
            success: (res) => {
                var html = '';
                var template = $('#data-template').html();
                $.each(res.data, function (i, item) {
                    html += Mustache.render(template, {
                        id: item.id,
                        username: item.username,
                        name: item.name,
                        unit: item.unit,
                        cost: formatCurrency(item.unit * item.price),
                        status: item.status,
                        style: setStatus(item.status).colorStyle,
                        textStatus: setStatus(item.status).textStatus,
                        shiptoaddress: item.shiptoaddress,
                        createdAt: item.createdAt
                    });
                });
                $('#tblData').html(html);
                controller.registerEvent();
                controller.paging(res.totalRow, changePageSize);
            },
            error: (err) => {
                console.log(err);
            }
        });
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
    resetState: function () {
        $('#txtSeach').val('');
        $('#slStatus').val('0');
        $('#slMonth').val('0');
        state.status = 0;
        state.seach = '';
        state.month = 0;
    }
}
controller.init();
const setStatus = (status) => {
    if (status == -1) {
        return {
            colorStyle: 'danger',
            textStatus: 'Đã hủy'
        }
    } else if (status == 0) {
        return {
            colorStyle: 'primary',
            textStatus: 'Duyệt'
        }
    } else if (status == 1) {
        return {
            colorStyle: 'warning',
            textStatus: 'Đã duyệt'
        }
    } else if (status == 2) {
        return {
            colorStyle: 'success',
            textStatus: 'Đã bán'
        }
    }
}
function formatCurrency(n, separate = ".") {
    var s = n.toString();
    var regex = /\B(?=(\d{3})+(?!\d))/g;
    var ret = s.replace(regex, separate);
    return ret;
}