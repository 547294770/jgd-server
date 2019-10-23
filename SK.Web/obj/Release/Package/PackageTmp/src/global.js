var GlobalSettings = {};
!(function (e) {
    var $ = layui.jquery;
    $.post({
        "url": "/handler/member/global/init",
        "async": true,
        "success": function (data) {
            GlobalSettings.enums = data;
        }
    })
})();
