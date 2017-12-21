<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="articleAdd.aspx.cs" Inherits="blogManage.articleManage.articleAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增文章</title>
    <link href="/assets/css/mywh.css" rel="stylesheet" />

    <script src="/assets/js/ajaxfileupload.js"></script>
    <script src="/assets/js/mywh.js"></script>
    <script src="/assets/plugins/validate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/validate/messages_zh.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
    <link href="/assets/plugins/tagEditor/jquery.tag-editor.css" rel="stylesheet" type="text/css" />
    <script src="/assets/plugins/tagEditor/jquery.caret.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/tagEditor/jquery.tag-editor.min.js" type="text/javascript"></script>
</head>
<!--防止出现阴影 -->
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>
                新增文章</h2>
            <ol class="breadcrumb">
                <li><a href="#">文章管理</a> </li>
                <li class="active"><strong>新增文章</strong> </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            新增文章</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <form id="articleForm" class="form-horizontal">
                        <div class="box-body">
                            <div class="form-group">
                                <label for="title" class="col-sm-2 control-label">
                                    标题</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="title" name="title" placeholder="标题"
                                        required />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="category" class="col-sm-2 control-label">
                                    文章类型</label>
                                <div class="col-sm-9">
                                    <select class="form-control" id="category" name="category" required>
                                        <option value=''>-请选择文章类型-</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="tags" class="col-sm-2 control-label">
                                    文章标签</label>
                                <div class="col-sm-9">
                                   <textarea class="form-control" id="tags" rows="3"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="tags" class="col-sm-2 control-label">
                                    列表图片</label>
                                <div class="col-sm-9" id="divImg">
                                   <div class="Thumblistbg upload-img">
                                        <input id="fileImg" type="file"  name="file" style="display:none;"/>
                                        <a href="javascript:void(0);" class="Thumbbtn" id="btnUploadImg"><i class="fa fa-upload"></i>上传图片</a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    是否置顶
                                </label>
                                <div class="col-sm-10">
                                    <label class="checkbox-inline i-checks"><input type="radio" id="isTop1" name="isTop" value="1" required>是 </label>
                                    <label class="checkbox-inline i-checks"><input type="radio" id="isTop0" name="isTop" value="0" required> 否 </label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="content" class="col-sm-2 control-label">
                                    文章内容</label>
                                <div class="col-sm-9">
                                    <!-- 加载编辑器的容器 -->
                                    <script id="content" type="text/plain" style="width: 100%;"></script>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <button type="button" class="btn btn-default" onclick='$("#mainContent").load("/articleManage/articleList.aspx");'>
                                返回</button>
                            <button type="button" class="btn btn-info pull-right" onclick="save();">
                                保存</button>
                        </div>
                        <!-- /.box-footer -->
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
<script src="/assets/plugins/ueditor/ueditor.config.js" type="text/javascript"></script>
<script src="/assets/plugins/ueditor/ueditor.all.min.js" type="text/javascript"></script>
<script src="/assets/plugins/ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
<script type="text/javascript">
    // 全局变量
    var global = {
        articleId: 0,
        UEditor:null,
        validator: null
    };
    $(function () {
        //        $("input").iCheck({
        //            checkboxClass: 'icheckbox_minimal-green',
        //            radioClass: 'iradio_minimal-green',
        //            increaseArea: '20%'
        //        });
        getCategoryList();
        global.UEditor = UE.getEditor('content');
        $("#tags").tagEditor({ placeholder: "文章标签",maxLength:20,maxTags:5 });
        validate();
        //编辑器准备就绪后触发该事件
        global.UEditor.addListener('ready', function (editor) {
            //绑定文章信息
            bindArticleInfo();
        });
        //上传图片
        uploadImg();      
    });

    //上传推广图片
    function uploadImg() {
        var layerIndex = null;
        $("#btnUploadImg").click(function () {
            $("#fileImg").unbind();
            $("#fileImg").change(function () {
                $.ajaxFileUpload({
                    url: '/ajax/Upload.ashx?action=uploadImg',
                    type: 'post',
                    fileElementId: 'fileImg',
                    dataType: 'json',
                    beforeSend: function (XMLHttpRequest) {
                        layerIndex = layer.load(0, { shade: false });
                    },
                    success: function (data, textStatus) {
                        if (data.flag == -100) {
                            window.layer.msg(data.msg);
                            window.location.href = "/login.aspx";
                            return;
                        }
                        else if (data.flag != 1) {
                            window.layer.msg(data.msg);
                        }
                        mUploadImg("#divImg", data.obj, "uploadImg()");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var msg = "服务器出错，错误内容：" + XMLHttpRequest.responseText;
                        layer.msg(msg);
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        layer.close(layerIndex);
                    }
                });
            });
            $("#fileImg").click();
        });
    }

    //获取文章类型
    function getCategoryList() {
        $.ajax({
            type: "post",
            url: "/ajax/Common.ashx",
            data: { action: 'selectList', tableName: 'tb_category', key: 'categoryName', value: 'categoryId', orderBy: 'categoryName'},
            async: false,
            success: function (data) {
                $("#category").append(data);
            }
        });
    }    

    //表单验证
    function validate() {
        global.validator = $("#articleForm").validate({
            submitHandler: function (form) {
                //alert("执行此方法");
            },
            errorElement: 'span',
            errorClass: 'help-block',
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            errorPlacement: function (error, element) {
                element.closest('div').append(error);
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            }
        });
    }

    //绑定文章信息
    function bindArticleInfo() {
        global.articleId = '<%=Request.QueryString["articleId"] %>';
        if (global.articleId != "0" && global.articleId != "undefined") {
            $.ajax({
                type: "post",
                url: "/ajax/Article.ashx",
                data: { action: 'getArticleInfo', articleId: global.articleId },
                async: false,
                success: function (data) {
                    var my = eval("(" + data + ")");
                    if (my.flag == -100) {
                        window.layer.msg(my.msg);
                        window.location.href = "/login.aspx";
                        return;
                    }
                    else if (my.flag != 1) {
                        window.layer.msg(my.msg);
                        return;
                    }
                    $("#title").val(my.obj[0].title);
                    $("#category").val(my.obj[0].categoryId);
                    if (my.obj[0].isTop == 0) {
                        $("#isTop0").prop("checked", true);
                    } else {
                        $("#isTop1").prop("checked", true);
                    }
                    mUploadImg("#divImg", my.obj[0].img, "uploadImg()");
                    global.UEditor.setContent(my.obj[0].content);
                    $.each(my.obj2, function (idx, item) {
                        $("#tags").tagEditor("addTag", item.tagName);
                    });
                }
            });
        }
    }

    //保存
    function save() {
        if (!global.validator.form()) {
            return;
        }
        var title = $("#title").val();
        var category = $("#category").val();
        var tags = $("#tags").val();
        var isTop = $("input[name=isTop]:checked").val();
        var img = $("#divImg").find("img")[0].src;
        var content = global.UEditor.getContent();
        $.ajax({
            type: "post",
            url: "/ajax/Article.ashx",
            data: { action: 'addOrEditArticle', 'articleId': global.articleId, 'title': escape(title), 'category': category, 'tags': escape(tags), 'isTop': isTop, 'content': escape(content), img: img },
            async: false,
            success: function (data) {
                var my = eval("(" + data + ")");
                if (my.flag == -100) {
                    window.parent.layer.msg(my.msg);
                    window.location.href = "/login.aspx";
                    return;
                }
                else if (my.flag != 1) {
                    window.parent.layer.msg(my.msg);
                }
                else {
                    $("#mainContent").load("/articleManage/articleList.aspx");
                }
            }
        });
    }
</script>
</html>
