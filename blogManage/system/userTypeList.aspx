<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userTypeList.aspx.cs" Inherits="blogManage.system.userTypeList" %>

<!DOCTYPE html>
<html>
<head>
    <title>用户类型列表</title>
    <link href="/assets/plugins/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <script src="/assets/plugins/DataTables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/DataTables/js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
</head>
<!--防止列表下出现阴影 -->
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>用户类型列表</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">用户管理</a>
                </li>
                <li class="active">
                    <strong>用户类型列表</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>用户类型列表</h5>
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
                                <label class="sr-only" for="userType">
                                    用户类型</label>
                                <input type="text" class="form-control" id="userType" placeholder="用户类型" />
                            </div>
                            <button type="button" class="btn btn-success search btn-sm" onclick="search()">
                                查 询</button>
                        </div>
                        <br />  
                        <table id="tbUserType" class="table table-striped table-bordered" cellspacing="0"
                            width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        id
                                    </th>
                                    <th>
                                        用户类型
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
        tbUserType: null
    };
    $(function () {
        getTypeList();
    });
    //获取用户类型列表
    function getTypeList() {
        global.tbUserType = $('#tbUserType').DataTable({
            "searching": false,
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/ajax/User.ashx",
                "type": "POST",
                "data": function (d) {
                    d.action = "getTypeList";
                    d.userType = escape($("#userType").val().trim());
                }
            },
            "lengthMenu": [15, 30, 50, 100],
            "language": {
                "url": "/assets/plugins/DataTables/language.txt"
            },
            "columns": [
                    { "data": "id" },
                    { "data": "paramsName" },
                    {
                        "orderable": false, // 禁用排序
                        "DefaultContent": "",
                        "render": function (data, type, full, meta) {
                            var ss = "<a href=\"javascript:void(0)\" onclick=\"addOrEditInfo(" + full.id + ",'" + escape(full.paramsName) + "')\">" +
                                     "<span class=\"fa fa-pencil\" aria-hidden=\"true\" title=\"编辑\"></span></a>";
                            return ss;
                        }
                    }
                ],
            "dom": '<"#tablesTools">lrtip',
            "initComplete": function (settings, json) {
                $("#tablesTools").append(
                    "<button id=\"btnAdd\" type=\"button\" class=\"btn btn-info btn-sm\" onclick=\"addOrEditInfo(0,'')\">新增</button>"
                    );
                $("#tablesTools").css("float", "right")
            }
        });
    }
    //新增或编辑
    function addOrEditInfo(userTypeId, userType) {
        $("#mainContent").load("/system/userTypeAdd.aspx?userTypeId=" + userTypeId + "&userType=" + userType);
    }
    //删除
    function deleteInfo(userTypeId) {
        window.layer.confirm('确定要删除此用户类型吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            $.ajax({
                type: "post",
                url: "/ajax/User.ashx",
                data: { action: 'deleteUserType', 'id': userTypeId },
                async: false,
                success: function (data) {
                    var my = eval("(" + data + ")");
                    if (my.flag == -100) {
                        window.layer.msg(my.msg);
                        window.location.href = "/login.aspx";
                        return;
                    }
                    else if (my.flag == 1) {
                        window.layer.msg(my.msg);
                        global.tbUserType.ajax.reload(null, false);
                    }
                    else {
                        window.layer.msg(my.msg);
                    }
                }
            });
        });
    }
    //查询
    function search() {
        global.tbUserType.ajax.reload(null, true);
    }
</script>
</html>
