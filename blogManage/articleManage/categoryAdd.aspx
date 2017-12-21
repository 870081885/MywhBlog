<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="categoryAdd.aspx.cs" Inherits="blogManage.articleManage.categoryAdd" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增文章类型</title>
    <script src="/assets/plugins/validate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/validate/messages_zh.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
</head>
<!--防止出现阴影 -->
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>
                新增文章类型</h2>
            <ol class="breadcrumb">
                <li><a href="#">文章管理</a> </li>
                <li class="active"><strong>新增文章类型</strong> </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            新增文章类型</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <form id="categoryForm" class="form-horizontal">
                        <div class="box-body">
                            <div class="form-group">
                                <label for="categoryId" class="col-sm-2 control-label">
                                    类型Id</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="categoryId" name="categoryId" placeholder="类型Id" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="categoryName" class="col-sm-2 control-label">
                                    类型名称</label>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="categoryName" name="categoryName" placeholder="类型名称"
                                        required />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="parentCategory" class="col-sm-2 control-label">
                                    父类型</label>
                                <div class="col-sm-9">
                                    <select class="form-control" id="parentCategory" name="parentCategory" required>
                                        <option value=''>-请选择父类型-</option>
                                        <option value='0'>一级类型</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <button type="button" class="btn btn-default" onclick='$("#mainContent").load("/articleManage/categoryList.aspx");'>
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

<script type="text/javascript">
    // 全局变量
    var global = {
        categoryId: 0,
        validator: null
    };
    $(function () {
        getFirstCategory();
        bindCategoryInfo();
        validate();
    });

    //获取一级类型
    function getFirstCategory() {
        $.ajax({
            type: "post",
            url: "/ajax/Common.ashx",
            data: { action: 'selectList', tableName: 'tb_category', key: 'categoryName', value: 'categoryId', orderBy: 'categoryName', where: "parentId='0'" },
            async: false,
            success: function (data) {
                $("#parentCategory").append(data);
            }
        });
    }

    //绑定类型信息
    function bindCategoryInfo() {
        debugger;
        global.categoryId = '<%=Request.QueryString["categoryId"] %>';
        if (global.categoryId != "0" && global.categoryId != "undefined") {
            $.ajax({
                type: "post",
                url: "/ajax/Article.ashx",
                data: { action: 'getCategoryInfo', categoryId: global.categoryId },
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
                    $("#categoryId").val(my.obj[0].categoryId);
                    $("#categoryName").val(my.obj[0].categoryName);
                    $("#parentCategory").val(my.obj[0].parentId);
                }
            });
        }
    }

    //表单验证
    function validate() {
        global.validator = $("#categoryForm").validate({
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
        newCategoryId = $("#categoryId").val();
        var categoryName = $("#categoryName").val();
        var parentId = $("#parentCategory").val();
        $.ajax({
            type: "post",
            url: "/ajax/Article.ashx",
            data: { action: 'addOrEditCategory', 'newCategoryId': newCategoryId, 'oldCategoryId': global.categoryId, 'categoryName': escape(categoryName), 'parentId': parentId },
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
                    $("#mainContent").load("/articleManage/categoryList.aspx");
                }
            }
        });
    }
</script>
</html>
