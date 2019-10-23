layui.extend({
    admin1: "lib/admin",
}).define(["admin1", "form","laydate"], function (exports) {

    var admin = layui.admin1,
        form = layui.form,
        laydate = layui.laydate,
        $ = layui.$;

    admin.form = {};
    admin.form.render = function (f) {
        layui.use(["form"], function () {
            !(function () {
                $('select[data-enums]').each(function (e) {
                    var enumType = $(this).data("enums");
                    var data = GlobalSettings.enums[enumType];
                    if (data) {
                        var options = [];
                        for (var key in data) {
                            options.push('<option value=\"' + key + '\">' + data[key] + '</option>');
                        }
                        var optionHtml = options.join('');
                        $(this).html(optionHtml);
                    }
                });
            })();

            if (f) {
                layui.form.render(null, f);
            } else
                layui.form.render();

            !(function () {
                //初始化日期控件
                $("[datetype=date]").each(function () {
                    var ctrlid = $(this)[0].id;
                    laydate.render({ elem: '#' + ctrlid });
                })

                $("[datetype=datetime]").each(function () {
                    var ctrlid = $(this)[0].id;
                    laydate.render({ elem: '#' + ctrlid, type: 'datetime' });
                })
            })();
        })
    }
    exports("admin", admin);
})