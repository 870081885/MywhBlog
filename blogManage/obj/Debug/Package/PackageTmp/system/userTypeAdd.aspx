<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userTypeAdd.aspx.cs" Inherits="blogManage.system.userTypeAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增用户类型</title>
    <script src="/assets/plugins/validate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/validate/messages_zh.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
</head>
<!--防止出现阴影 -->
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>新增用户类型</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">用户管理</a>
                </li>
                <li class="active">
                    <strong>新增用户类型</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>新增用户类型</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <form id="userTypeForm" class="form-horizontal">
                        <div class="box-body">
                            <div class="form-group">
                                <label for="paramsName" class="col-sm-2 control-label">
                                    用户类型</label>
                                <div class="col-sm-10">
                                    <input type="text" class="form-control" id="paramsName" name="paramsName" placeholder="用户类型"
                                        required />
                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <button type="button" class="btn btn-default" onclick='$("#mainContent").load("/system/userTypeList.aspx");'>
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
        userTypeId: 0,
        validator: null
    };

    $(function () {
        showInfo();
        validate();
    });

    //表单验证
    function validate() {
        global.validator = $("#userTypeForm").validate({
            submitHandler: function (form) {
                //alert("执行此方法");
            },
            errorElement: 'span',
            errorClass: 'help-block',
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            errorPlacement: function (error, element) {
                element.parent('div').append(error);
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            }
        });
    }

    //展示信息
    function showInfo() {
        global.userTypeId = '<%=Request.QueryString["userTypeId"] %>';
        var userType = unescape('<%=Request.QueryString["userType"] %>');
        if (global.userTypeId == "" || global.userTypeId == "undefined") {
            window.parent.layer.msg("页面地址参数不正确！");
            return;
        }
        if (global.userTypeId > 0) {
            $(".sTitle").text("编辑用户类型");
            $("#paramsName").val(userType);
        }
        else {
            $(".sTitle").text('新增用户类型');
        }
    }

    //保存
    function save() {
        if (!global.validator.form()) {
            return;
        }
        var paramsName = $("#paramsName").val();
        $.ajax({
            type: "post",
            url: "/ajax/User.ashx",
            data: { action: 'addOrEditUserType', 'id': global.userTypeId, 'paramsName': escape(paramsName) },
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
                    $("#mainContent").load("/system/userTypeList.aspx");
                }
            }
        });
    }

</script>
</html>

