//图片删除鼠标悬浮效果
function ThumbLi() {
    $('.Thumb_li').hover(function () {
        $(this).children('.bg').fadeIn();
    }, function () {
        $(this).children('.bg').fadeOut();
    });
}

//上传图片
function mUploadImg(domId,src,srtFunc) { 
    var str="";
    str += "<div class=\"Thumblist\">";
    str +=     "<div class=\"Thumb_li\">";
    str +=         "<div class=\"bg\" style=\"display: none;\">";
    str += "<a href=\"javascript:\" class=\"Thumb_delete\" title=\"删除\" data-index=\"0\" onclick=\"mDelImg('" + domId + "','" + srtFunc + "')\">删除</a>";
    str +=         "</div>";
    str +=         "<img id=\"uploadImage_0\" src=\"" + src + "\" class=\"upload_image\" />";
    str +=     "</div>";
    str += "</div>";
    $(domId).html(str);
    ThumbLi();
}

//删除图片
function mDelImg(domId, srtFunc) { 
    var str="";
    str += "<div class=\"Thumblistbg upload-img\">";
    str +=     "<input id=\"fileImg\" type=\"file\"  name=\"file\" style=\"display:none;\"/>";
    str +=     "<a href=\"javascript:void(0);\" class=\"Thumbbtn\" id=\"btnUploadImg\"><i class=\"fa fa-upload\"></i>上传图片</a>";
    str += "</div>";
    $(domId).html(str);
    eval(srtFunc);
}