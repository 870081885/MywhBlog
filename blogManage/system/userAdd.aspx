<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userAdd.aspx.cs" Inherits="blogManage.system.userAdd" %>

<!DOCTYPE html>
<html>
<head>
    <title>新增用户</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <script src="/assets/plugins/validate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/validate/messages_zh.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
    <script src="/assets/js/GetQueryString.js" type="text/javascript"></script>

</head>
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>新增用户</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">用户管理</a>
                </li>
                <li class="active">
                    <strong>新增用户</strong>
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
                        <form id="userForm" class="form-horizontal">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="userName" class="col-sm-2 control-label">
                                        用户名</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="userName" name="userName" placeholder="用户名"
                                            required />
                                        <label id="lblUserName" class="control-label" style="display:none;"></label>
                                    </div>
                                </div>
                                <div id="divInfo">  
                                <div class="form-group">
                                    <label for="trueName" class="col-sm-2 control-label">
                                        真实姓名</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="trueName" name="trueName" placeholder="真实姓名"
                                            required />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="nikeName" class="col-sm-2 control-label">
                                        昵称</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="nikeName" name="nikeName" placeholder="昵称"
                                            required />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="userType" class="col-sm-2 control-label">
                                        用户类型</label>
                                    <div class="col-sm-10">
                                        <select class="form-control" id="userType" name="userType" required>
                                            <option value=''>-请选择用户类型-</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="userStatus" class="col-sm-2 control-label">
                                        帐号状态</label>
                                    <div class="col-sm-10">
                                        <select class="form-control" id="userStatus" name="userStatus" required>
                                            <option value="">-请选择-</option>
                                            <option value="0">禁用</option>
                                            <option value="1" selected="selected">启用</option>
                                        </select>
                                    </div>
                                </div>
                                </div>
                                <div id="divPwd">
                                <div class="form-group">
                                    <label for="password" class="col-sm-2 control-label">
                                        密码</label>
                                    <div class="col-sm-10">
                                        <input type="password" class="form-control" id="password" name="password" placeholder="密码"
                                            required />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="password2" class="col-sm-2 control-label">
                                        确认密码</label>
                                    <div class="col-sm-10">
                                        <input type="password" class="form-control" id="password2" name="password2" placeholder="确认密码"
                                            required equalto="#password" />
                                    </div>
                                </div>
                                </div>
                            </div>
                            <!-- /.box-body -->
                            <div class="box-footer">
                                <button type="button" class="btn btn-default" id="btnBack" onclick='$("#mainContent").load("/system/userList.aspx");'>
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
        type: "",
        validator: null
    };

    $(function () {
        showInfo();
        validate();
    });

    //表单验证
    function validate() {
        global.validator = $("#userForm").validate({
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
        global.type = '<%=Request.QueryString["type"] %>';
        //是否为新增
        if (global.type == "" || global.type == "undefined") {
            type = "";
            getUserType();
            return;
        }

        var userId = '<%=Request.QueryString["userId"] %>';
        if (userId == "" || userId == "undefined") {
            window.parent.layer.msg("页面地址参数不正确！");
            return;
        }
        if (global.type == "edit") {
            $("#divPwd").hide();
            $("#userName").hide();
            $("#lblUserName").show();
            $(".sTitle").text("编辑用户");
            getUserType();
            bindUserInfo(userId);
        }
        else if (global.type == "changePwd" || global.type == "changePwd2") {
            $("#userName").hide();
            $("#lblUserName").show();
            $("#lblUserName").text('<%=Request.QueryString["userName"] %>');
            $("#divInfo").hide();
            $(".sTitle").text("修改密码");
            if (global.type == "changePwd2") {
                $("#btnBack").hide();
            }
        }
    }

    //获取用户类型
    function getUserType() {
        $.ajax({
            type: "post",
            url: "/ajax/Common.ashx",
            data: { action: 'selectParams', parentId: 1 },
            async: false,
            success: function (data) {
                $("#userType").append(data);
            }
        });
    }

    //绑定用户信息
    function bindUserInfo(userId) {
        $.ajax({
            type: "post",
            url: "/ajax/User.ashx",
            data: { action: 'getUserInfo', userId: userId },
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
                $("#lblUserName").text(my.obj[0].userName);
                $("#trueName").val(my.obj[0].trueName);
                $("#nikeName").val(my.obj[0].nikeName);
                $("#userType").val(my.obj[0].userType);
                $("#userStatus").val(my.obj[0].userStatus);
            }
        });
    }

    //保存
    function save() {
        if (!global.validator.form()) {
            return;
        }
        if (global.type == "changePwd" || global.type == "changePwd2") {
            $.ajax({
                type: "post",
                url: "/ajax/User.ashx",
                data: {
                    action: 'changePwd',
                    userId: '<%=Request.QueryString["userId"] %>',
                    password: escape($("#password").val()) //密码
                },
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
                        return;
                    }
                    else {
                        if (global.type == "changePwd2") {
                            window.parent.layer.msg(my.msg);
                        }
                        else {
                            $("#mainContent").load("/system/userList.aspx");
                        }
                    }
                }
            });
        }
        else {
            $.ajax({
                type: "post",
                url: "/ajax/User.ashx",
                data: {
                    action: 'addOrEditUser',
                    userId: '<%=Request.QueryString["userId"] %>',
                    userName: escape($("#userName").val()), //用户名
                    nikeName: escape($("#nikeName").val()), //昵称
                    password: escape($("#password").val()), //密码
                    truename: escape($("#trueName").val()), //真实姓名
                    userType: $("#userType").val(), //用户类型
                    userStatus: $("#userStatus").val()//用户状态
                },
                success: function (data) {
                    var my = eval("(" + data + ")");
                    if (my.flag == -100) {
                        window.parent.layer.msg(my.msg);
                        window.location = "/login.aspx";
                        return;
                    }
                    else if (my.flag != 1) {
                        window.parent.layer.msg(my.msg);
                        return;
                    }
                    else {
                        $("#mainContent").load("/system/userList.aspx");
                    }
                }
            });
        }
    }

</script>
</html>
