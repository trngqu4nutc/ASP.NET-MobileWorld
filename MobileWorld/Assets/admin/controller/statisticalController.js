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
    },
    loadData: (changePageSize) => {
        $.ajax({
            url: '/Statistical/GetAllCatalog',
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
                $('#totalMoney').html(`Tổng doanh thu: ${formatCurrency(res.totalMoney)} VNĐ`)
                let template = $('#data-template').html();
                $.each(res.data, (i, item) => {
                    html += Mustache.render(template, {
                        id: item.catalogid,
                        name: item.name,
                        brand: item.brand,
                        unit: item.unit,
                        quantity: item.quantity,
                        cost: formatCurrency(item.cost)
                    });
                });
                $('#tblData').html(html);
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