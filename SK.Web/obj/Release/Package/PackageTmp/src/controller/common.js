/** layuiAdmin-v1.0.0-beta6 LPPL License By http://www.layui.com/admin/ */
; layui.define(function (e) {

    var i = (layui.$, layui.layer, layui.laytpl, layui.setter, layui.view, layui.admin);
    i.events.logout = function () {
        i.req({
            url: "/handler/admin/admin/logout", type: "post", data: {}, success: function (res) {
                //i.exit()
                if (res.code == 0) {
                    location.hash = "/admin/login";
                }
            }
        })
    }, e("common", {})
});