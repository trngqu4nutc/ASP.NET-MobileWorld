var config = {
    size: 1,
    costPrice: 0,
    brand: []
}
controller = {
    init: function () {
        controller.LoadBrand();
        controller.LoadData();
        controller.registerEvent();
    },
    registerEvent: function () {
        $('.viewmore').off('click').on('click', function () {
            controller.LoadData();
        });
        $('.resetPage').off('click').on('click', function () {
            config.size = 1;
            config.costPrice = 0;
            config.brand = [];
            controller.LoadData();
            controller.resetCost();
            $('.logo').removeClass('check');
        });
    },
    FilterCost: function (cost) {
        controller.resetCost(cost);
        config.size = 1;
        config.costPrice = cost.data('id');
        controller.LoadData();
    },
    FilterBrand: function (idlogo) {
        if (idlogo.hasClass('check')) {
            idlogo.removeClass('check');
            config.brand.splice(config.brand.indexOf(idlogo.data('id')), 1);
            console.log(config.brand);
        } else {
            config.brand.push(idlogo.data('id'));
            idlogo.addClass('check');
            console.log(config.brand);
        }
        config.size = 1;
        controller.LoadData();
    },
    LoadData: function () {
        $.ajax({
            url: '/HomeCatalog/LoadData',
            type: 'GET',
            data: {
                type: 1,
                cost: config.costPrice,
                brand: JSON.stringify(config.brand),
                index: config.size
            },
            datType: 'json',
            success: (res) => {
                var html = '';
                var template = $('#data-template').html();
                $.each(res.data, function (i, item) {
                    html += Mustache.render(template, {
                        id: item.id,
                        name: item.name,
                        pictureuri: item.pictureuri,
                        price: formatCurrency(item.price)
                    });
                });
                if (config.size == 1) {
                    $('#data-catalogs').html(html);
                    $('#menuHome').removeClass('active');
                    config.size = res.total;
                    controller.LoadViewsMore(res.total);
                } else {
                    $('#data-catalogs').append(html);
                    config.size += res.total;
                    controller.LoadViewsMore(res.total);
                }
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    LoadBrand: function () {
        $.ajax({
            url: '/HomeCatalog/LoadBrand',
            type: 'GET',
            data: { type: 1 },
            dataType: 'json',
            success: (res) => {
                var html = '';
                var template = $('#data-brands').html();
                $.each(res.brands, function (i, item) {
                    html += Mustache.render(template, {
                        id: item.id,
                        pictureurl: item.pictureurl
                    });
                });
                $('#brands').html(html);
            },
            error: (err) => {
                console.log(err);
            }
        });
    },
    resetCost: function (cost = 0) {
        $('.cost').removeClass('check');
        if (cost != 0) {
            cost.addClass('check');
        }
    },
    LoadViewsMore: function (total) {
        if (total == 0) {
            $('.viewmore').hide();
        } else {
            $('.viewmore').show();
        }
    }
}
controller.init();
function formatCurrency(n, separate = ".") {
    var s = n.toString();
    var regex = /\B(?=(\d{3})+(?!\d))/g;
    var ret = s.replace(regex, separate);
    return ret;
}