//common.js 116为f5
$(function () {
    $("body").bind("keydown", function (event) {
        if (event.keyCode == 116) {
            if (typeof ("iframeContent") != 'undefined') {
                window.parent.frames["iframeContent"].window.location.reload();
                return false;
            }
        }
    });
});
//补齐两位数
function padleft0(obj) {
    return obj.toString().replace(/^[0-9]{1}$/, "0" + obj);
}
//保留两位小数
function toDecimal(x, y) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return 0;
    }

    //除数
    var a = "1";
    while (a.length <= y) {
        a += '0';
    }
    var b = +a;

    var f = Math.round(x * b) / b;
    var s = f.toString();
    if (+s != 0) {
        var rs = s.indexOf('.');
        if (rs < 0) {
            rs = s.length;
            s += '.';
        }
        while (s.length <= rs + y) {
            s += '0';
        }
    }
    return s;
}
//计算日期差
function dateDiff(DateOne, DateTwo) {
    var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('-'));
    var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('-') + 1);
    var OneYear = DateOne.substring(0, DateOne.indexOf('-'));

    var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('-'));
    var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('-') + 1);
    var TwoYear = DateTwo.substring(0, DateTwo.indexOf('-'));

    var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 1000 / 60 / 60 / 24);
    return Math.abs(cha);
}
//获取日期
function getDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount); //获取AddDayCount天后的日期
    var y = dd.getFullYear();
    var m = padleft0(dd.getMonth() + 1); //获取当前月份的日期
    var d = padleft0(dd.getDate());
    return y + "-" + m + "-" + d;
}
//function checkRefresh(event) {
//    var iframe = 'iframeContent';
//    if (event.keyCode == 116) {
//        frames[iframe].window.location.reload();
//        event.keyCode = 0;
//        $("#iframeContent").contents().find("body").attr("onkeydown", "parent.checkRefresh(event); return false;");
//    }
//}
