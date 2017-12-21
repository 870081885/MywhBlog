<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blogMenuAdd.aspx.cs" Inherits="blogManage.config.blogMenuAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增导航</title>
    <script src="/assets/plugins/validate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/validate/messages_zh.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
</head>
<!--防止出现阴影 -->
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>
                新增导航</h2>
            <ol class="breadcrumb">
                <li><a href="#">博客导航设置</a> </li>
                <li class="active"><strong>新增导航</strong> </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            新增导航</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <form id="articleForm" class="form-horizontal">
                        <div class="box-body">
                            <div class="form-group">
                                <label for="menuName" class="col-sm-2 control-label">
                                    导航名称</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="menuName" name="menuName" placeholder="标题"
                                        required />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="menuUrl" class="col-sm-2 control-label">
                                    导航Url</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="menuUrl" name="menuUrl" placeholder="导航Url"
                                        required />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="menuKey" class="col-sm-2 control-label">
                                    导航Key</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="menuKey" name="menuKey" placeholder="导航Key"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="category" class="col-sm-2 control-label">
                                    文章类型</label>
                                <div class="col-sm-9">
                                    <select class="form-control" id="category" name="category">
                                        <option value=''>-请选择文章类型-</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="parentMenu" class="col-sm-2 control-label">
                                    父菜单</label>
                                <div class="col-sm-9">
                                    <select class="form-control" id="parentMenu" name="parentMenu" required>
                                        <option value=''>-请选择文章类型-</option>
                                        <option value='0'>一级菜单</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    显示方式
                                </label>
                                <div class="col-sm-10">
                                    <label class="checkbox-inline i-checks"><input type="radio" name="showType" id="showType0" value="0" required>不带图列表</label>
                                    <label class="checkbox-inline i-checks"><input type="radio" name="showType" id="showType1" value="1" required>带图列表</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="sort" class="col-sm-2 control-label">
                                    排序</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="sort" name="sort" placeholder="排序"
                                        required />
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <button type="button" class="btn btn-default" onclick='$("#mainContent").load("/config/blogMenu.aspx");'>
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
<!-- 实例化编辑器 -->
<script type="text/javascript">
    // 全局变量
    var global = {
        blogMenuId: 0,
        validator: null
    };
    $(function () {
        getFirstMenu();
        getCategoryList();
        bindBolgMenuInfo();
        validate();
    });
   
    //获取一级菜单
    function getFirstMenu() {
        $.ajax({
            type: "post",
            url: "/ajax/Common.ashx",
            data: { action: 'selectList', tableName: 'tb_blogmenu', key: 'menuName', value: 'id', orderBy: 'sort', where: 'parentId=0' },
            async: false,
            success: function (data) {
                $("#parentMenu").append(data);
            }
        });
    }

    //获取文章类型
    function getCategoryList() {
        $.ajax({
            type: "post",
            url: "/ajax/Common.ashx",
            data: { action: 'selectList', tableName: 'tb_category', key: 'categoryName', value: 'categoryId', orderBy: 'categoryName' },
            async: false,
            success: function (data) {
                $("#category").append(data);
            }
        });
    }

    //绑定博客菜单信息
    function bindBolgMenuInfo() {
        global.blogMenuId = '<%=Request.QueryString["blogMenuId"] %>';
        if (global.blogMenuId != "" && global.blogMenuId != "undefined") {
            $.ajax({
                type: "post",
                url: "/ajax/Config.ashx",
                data: { action: 'getBlogMenuInfo', blogMenuId: global.blogMenuId },
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
                    $("#menuName").val(my.obj[0].menuName);
                    $("#menuUrl").val(my.obj[0].menuUrl);
                    $("#parentMenu").val(my.obj[0].parentId);
                    $("#category").val(my.obj[0].categoryId);              
                    $("#menuKey").val(my.obj[0].menuKey);                 
                    if (my.obj[0].showType == 0) {
                        $("#showType0").prop("checked", true);
                    } else {
                        $("#showType1").prop("checked", true);
                    }
                    $("#sort").val(my.obj[0].sort);
                }
            });
        }
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
    //保存
    function save() {
        if (!global.validator.form()) {
            return;
        }
        var menuName = $("#menuName").val();
        var menuUrl = $("#menuUrl").val();
        var menuKey = $("#menuKey").val();
        var category = $("#category").val();       
        var parentId = $("#parentMenu").val();   
        var showType = $("input[name=showType]:checked").val();
        var sort = $("#sort").val();
        $.ajax({
            type: "post",
            url: "/ajax/Config.ashx",
            data: { action: 'addOrEditBlogMenu', 'blogMenuId': global.blogMenuId, 'menuName': escape(menuName), 'menuUrl': escape(menuUrl), 'menuKey': escape(menuKey),category:category, 'parentId': parentId, 'showType': showType, 'sort': sort },
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
                    $("#mainContent").load("/config/blogMenu.aspx");
                }
            }
        });
    }
</script>
</html>
