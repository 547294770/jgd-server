var EXEHASHCHANGE = function (e) {
    var app_header = document.querySelector("#app_header");
    var app_body = document.querySelector("#app_body");

    console.log(app_body);

    var hash = window.location.hash;
    var url = hash.substr(1) + '.html';
    console.log(url);

    mui.showLoading();

    mui.ajax(url, {
        data: {},
        dataType: 'html',//服务器返回json格式数据
        type: 'get',//HTTP请求类型
        timeout: 10000,//超时时间设置为10秒；
        //headers: { 'Content-Type': 'application/json' },
        success: function (data) {
            //服务器返回响应，根据响应结果，分析是否登录成功；
            //...
            console.log("ajax ok!");
            //mui(app_body).html("<div>hello1</div><script>alert('5555');</script>");
            //document.querySelector("").appendChild(
            app_body.outerHTML = "<div>hello1</div><script>alert('5555');</script>";
            
            
            mui.hideLoading();
        },
        error: function (xhr, type, errorThrown) {
            //异常处理；
            console.log(errorThrown);
            mui.alert(errorThrown);
            //mui.hideLoading();
        }
    });
}

window.addEventListener('hashchange', function (e) {
    console.log('The hash has changed!');
    EXEHASHCHANGE(e);
}, false);