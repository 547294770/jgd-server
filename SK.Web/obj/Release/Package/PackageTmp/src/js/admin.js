!(function () {
    //初始化日期控件
    $("[datetype=date]").each(function () {
        var ctrlid = $(this)[0].id;
        console.log("ctrlid:" + ctrlid);
        laydate.render({ elem: '#' + ctrlid });
    })
    $("[datetype=timesection]").each(function () {
        var ctrlid = $(this)[0].id;
        console.log("ctrlid:" + ctrlid);
        laydate.render({ elem: '#' + ctrlid, type: 'time', range: true });
    })
})();