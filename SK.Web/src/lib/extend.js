layui.extend({
    admin1: "lib/admin",
}).define(["admin1", "form","laydate","table"], function (exports) {

    var admin = layui.admin1,
        form = layui.form,
        laydate = layui.laydate,
        table = layui.table,
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
    };
    exports("admin", admin);
})

layui.define(["table","admin","view"], function (exports) {

    var table = layui.table,
        admin = layui.admin,
        view = layui.view,
        $ = layui.$;

    table.tool = function (tableId) {
        table.on("tool(" + tableId + ")", function (ele) {
            var obj = ele.tr.find("[lay-event=" + ele.event + "]");
            var action = obj.attr("lay-action");
            var title = obj.attr("lay-title");
            var attrarea = obj.attr("lay-area");
            var area = attrarea ? JSON.parse(obj.attr("lay-area")) : ["600px", "400px"];
            switch (ele.event) {
                case "detail":
                case "edit":
                    {
                        admin.popup({
                            id: "LAY_info_" + Math.round(Math.random() * 10000),
                            title: title? title : "信息",
                            area: area,
                            success: function (a, b) {
                                view(this.id).render(action, ele.data);
                            }
                        });
                    }
                    break;
                default:
                    break;

            }
        });
    };
});