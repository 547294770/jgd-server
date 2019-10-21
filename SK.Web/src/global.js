var GlobalSettings = {};

layui.define(['admin'], function (exports) {

    var $ = layui.$,
        admin = layui.admin;

    !(function (e) {
        $.post({
            "url": "/handler/member/global/init",
            "success": function (data) {
                GlobalSettings.enums = data;
            }
        })
    })();

    //导出
    exports('global', {});
})