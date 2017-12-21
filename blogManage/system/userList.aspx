<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userList.aspx.cs" Inherits="blogManage.system.userList" %>

<!DOCTYPE html>
<html>
<head>
    <title>用户列表</title>
    <link href="/assets/plugins/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <script src="/assets/plugins/DataTables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/DataTables/js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/inspinia/js/inspinia.js" type="text/javascript"></script>
</head>
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>用户列表</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">用户管理</a>
                </li>
                <li class="active">
                    <strong>用户列表</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>用户列表</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                            <%--<a class="close-link">
                                <i class="fa fa-times"></i>
                            </a>--%>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-inline">
                            <div class="form-group">
                    <select id="sStatus" class="form-control">
                        <option value="">请选择帐号状态</option>
                        <option value="1">启用</option>
                        <option value="0">禁用</option>
                    </select>
                </div>
                            <div class="form-group">
                    <select id="sUserType" class="form-control">
                        <option value=''>-请选择用户类型-</option>
                    </select>
                </div>
                            <div class="form-group">
                    <input id="sName" type="text" class="form-control" placeholder="用户名或姓名" />
                </div>
                            <button type="button" class="btn btn-success search btn-sm" onclick="search()">查 询</button>
                        </div>
                        <br />  
                        <table id="tbUser" class="table table-striped table-bordered" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    id
                                </th>
                                <th>
                                    用户名
                                </th>
                                <th>
                                    姓名
                                </th>
                                <th>
                                    昵称
                                </th>
                                <th>
                                    用户类型
                                </th>
                                <th>
                                    帐号状态
                                </th>
                                <th>
                                    注册时间
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                        </thead>
                    </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>       
<script type="text/javascript">
    // 全局变量
    var global = {
        tbUser: null
    };
    $(function () {
        getUserType();
        getUserList();
    });
    //获取用户类型列表
    function getUserList() {
        global.tbUser = $('#tbUser').DataTable({
            "searching": false,
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/ajax/User.ashx",
                "type": "POST",
                "data": function (d) {
                    d.action = "getUserList";
                    d.sStatus = $("#sStatus").val().trim();
                    d.sUserType = $("#sUserType").val().trim();
                    d.sName = escape($("#sName").val().trim());
                }
            },
            "lengthMenu": [15, 30, 50, 100],
            "language": {
                "url": "/assets/plugins/DataTables/language.txt"
            },
            "columns": [
                    { "data": "id" },
                    { "data": "userName" },
                    { "data": "trueName" },
                    { "data": "nikeName" },
                    { "data": "paramsName" },
                    { "data": "userStatus",
                        "render": function (data, type, full, meta) {
                            var ss = "<span class=\"label label-danger\">禁用</span>"
                            if (data == 1) {
                                ss = "<span class=\"label label-primary\">启用</span>";
                            }
                            return ss;
                        }
                    },
                    { "data": "createTime" },
                    {
                        "orderable": false, // 禁用排序
                        "DefaultContent": "",
                        "render": function (data, type, full, meta) {
                            var ss = "<a href=\"javascript:void(0)\" onclick=\"editInfo(" + full.id + ")\">" +
                                     "<span class=\"fa fa-pencil\" aria-hidden=\"true\" title=\"编辑\"></span></a>" +
                                     "<a href=\"javascript:void(0)\" onclick=\"deleteInfo(" + full.id + ")\">" +
                                     "<span class=\"fa fa-trash\" aria-hidden=\"true\" style=\"padding-left:10px\" title=\"删除\"></span></a>" +
                                     "<a href=\"javascript:void(0)\" onclick=\"setPower(" + full.id + ",'" + escape(full.userName) + "')\">" +
                                     "<span class=\"fa fa-lock\" aria-hidden=\"true\" style=\"padding-left:10px\" title=\"权限设置\"></span></a>" +
                                     "<a href=\"javascript:void(0)\" onclick=\"changePwd(" + full.id + ",'" + escape(full.userName) + "')\">" +
                                     "<span class=\"fa fa-edit\" aria-hidden=\"true\" style=\"padding-left:10px\" title=\"修改密码\"></span></a>";
                            if (full.userName == "admin") {
                                ss = "<a href=\"javascript:void(0)\" onclick=\"editInfo(" + full.id + ")\">" +
                                     "<span class=\"fa fa-pencil\" aria-hidden=\"true\" title=\"编辑\"></span></a>" +
                                     "<a href=\"javascript:void(0)\" onclick=\"changePwd(" + full.id + ",'" + escape(full.userName) + "')\">" +
                                     "<span class=\"fa fa-edit\" aria-hidden=\"true\" style=\"padding-left:10px\" title=\"修改密码\"></span></a>";
                            }
                            return ss;
                        }
                    }
                ],
            "dom": '<"#tablesTools">lrtip',
            "initComplete": function (settings, json) {
                $("#tablesTools").append(
                    "<button id=\"btnAdd\" type=\"button\" class=\"btn btn-info btn-sm\" onclick=\"addInfo()\">新增</button>"
                );
                $("#tablesTools").css("float", "right")
            }
        });
    }
    //获取用户类型
    function getUserType() {
        $.ajax({
            type: "Post",
            url: "/ajax/Common.ashx",
            data: { action: 'selectParams', parentId: 1 },
            async: false,
            success: function (data) {
                $("#sUserType").append(data);
            }
        });
    }
    //新增用户
    function addInfo() {
        $("#mainContent").load("/system/userAdd.aspx");
    }
    //编辑用户
    function editInfo(userId) {
        $("#mainContent").load("/system/userAdd.aspx?type=edit&userId=" + userId);
    }
    //删除用户
    function deleteInfo(userId) {
        window.layer.confirm('确定要删除此用户吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            $.ajax({
                type: "post",
                url: "/ajax/User.ashx",
                data: { action: 'deleteUser', userId: userId },
                async: false,
                success: function (data) {
                    var my = eval("(" + data + ")");
                    if (my.flag == -100) {
                        layer.msg(my.msg);
                        window.location.href = "/login.aspx";
                        return;
                    }
                    else if (my.flag == 1) {
                        window.layer.msg(my.msg);
                        global.tbUser.ajax.reload(null, false);
                    }
                    else {
                        window.layer.msg(my.msg);
                    }
                }
            });
        });
    }
    //权限设置
    function setPower(userId, userName) {
        $("#mainContent").load("/system/power.aspx?userId=" + userId + "&userName=" + userName);
    }
    //修改密码
    function changePwd(userId, userName) {
        $("#mainContent").load("/system/userAdd.aspx?type=changePwd&userId=" + userId + "&userName=" + userName);
    }
    //查询
    function search() {
        global.tbUser.ajax.reload(null, true);
    }  
</script>

</html>
