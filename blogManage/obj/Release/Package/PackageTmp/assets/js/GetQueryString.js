//获取QueryString的数组

function getQueryString() {

    var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+", "g"));

    if (result == null) {

        return "";

    }

    for (var i = 0; i < result.length; i++) {

        result[i] = result[i].substring(1);

    }

    return decodeURI(result);

}

//根据QueryString参数名称获取值

function getQueryStringByName(name) {

    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

    if (result == null || result.length < 1) {

        return "";

    }

    return decodeURI(result[1]);

}

//根据QueryString参数索引获取值

function getQueryStringByIndex(index) {

    if (index == null) {

        return "";

    }

    var queryStringList = getQueryString();

    if (index >= queryStringList.length) {

        return "";

    }

    var result = queryStringList[index];

    var startIndex = result.indexOf("=") + 1;

    result = result.substring(startIndex);

    return decodeURI(result);

}


//重新计算父页面高度
function SetParentFrameHeight(obj) {
    //    var bHeight = document.body.scrollHeight; 
    //    if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
    //        bHeight = document.documentElement.scrollHeight;
    //    }
    //    else if (document.documentElement && document.documentElement.scrollHeight) {
    //        bHeight = document.documentElement.scrollHeight;
    //    }
    //    else if (document && document.body.scrollHeight) {
    //        bHeight = document.body.scrollHeight;
    //    }
    //    if (bHeight < 600) {
    //        bHeight = 600;
    //    }
    //    //默认重新计算父页面高度
    //    setTimeout(function () {
    //        var sHeight = $("body").height(); 
    //        var height = Math.max(sHeight, bHeight); 
    //        $("#mainPage", parent.document).height(height);
    //    }, 1000);

    //    //选择分页后重新加载父页面高度
    //    $("#" + obj + "_length select").live("change", function () {
    //        setTimeout(function () {
    //            var sHeight = $("body").height();
    //            var height = Math.max(sHeight, bHeight);
    //            $("#mainPage", parent.document).height(height);
    //        }, 1000); 
    //    });
}

function layerAlertMsg(msgcontent, msgType, showtime) {
    showtime = (typeof (showtime) == "undefined") ? 3000 : showtime;
    switch (msgType) {
        case 1:
            layer.msg(msgcontent, { time: 3000, icon: 5 });//未选中行
            break;
        case 2:
            layer.msg(msgcontent, { time: 3000, icon: 6 }); //成功
            break;
        case 3:
            layer.msg(msgcontent, { time: 3000, icon: 2 }); //错误
            break;
        case 3:
            var html = "<ul class=\"layer_notice layui-layer-wrap\">";
            html += " <li>" + msgcontent + "</li>";
            html += "</ul>";
            layer.open({
                type: 1,
                shade: false,
                title: false, //不显示标题 
                content: html, //捕获的元素
                cancel: function (index) {
                    layer.close(index);
                }
            });
            break;
        default:
            break;
    }
}
$(function () { pageLoading(); });
function pageLoading() { 
    var html = " <div class=\"datagrid-mask-msg\" style=\"position: absolute; border: 1px solid rgb(255, 183, 82); color: rgb(255, 102, 0); width: 250px; line-height: 28px; padding-left: 4px; display: none; left: 482px; top: 252.5px; background-color: rgb(252, 248, 227);text-align: center;z-index:99999999;\">";
    html += " <img src=\"../assets/plugins/layer/skin/default/loading-0.gif\" />";
    html += " <br />";
    html += " 正在处理，请耐心等待...";
    html += " </div>";
    $(".panel-default").append(html); 
}