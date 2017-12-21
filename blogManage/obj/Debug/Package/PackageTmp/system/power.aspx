<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="power.aspx.cs" Inherits="blogManage.system.power" %>

<!DOCTYPE html>
<html>
<head>
    <title>权限设置</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link href="/assets/plugins/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet"
        type="text/css" />
    <script src="/assets/plugins/zTree/js/jquery.ztree.core.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/zTree/js/jquery.ztree.excheck.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
    <script src="/assets/js/GetQueryString.js" type="text/javascript"></script>
    <script src="/assets/js/common.js" type="text/javascript"></script>
</head>
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>权限设置</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">用户管理</a>
                </li>
                <li class="active">
                    <strong>权限设置</strong>
                </li>
            </ol>
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>权限设置</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <label id="lblUser">
                            用户名:<%=userName %></label>
                        <button type="button" class="btn btn-primary btn-sm" style="float: right;" onclick="savePowers();">
                            保存设置</button>
                        <a class="btn btn-default btn-sm" href="javascript:void(0);" onclick='$("#mainContent").load("/system/userList.aspx");' style="float: right; margin-right: 5px;">
                            返回</a>
                        <br />
                        <br />
                        <div style="border: 1px solid #bbb;">
                         <ul id="menuTree" class="ztree" style="padding: 15px;">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
<script type="text/javascript">
    // 全局变量
    var global = {
        zMenuTree: null
    };

    // zChinaTree 的参数配置
    var menuTreeSetting = {
        check: {
            enable: true,
            chkStyle: "checkbox"
        }
    };
    $(function () {
        bindMenuTree();
    });
    //绑定树形菜单
    function bindMenuTree() {
        $.ajax({
            url: "/ajax/User.ashx",
            data: { action: 'getMenuTree' },
            async: false,
            type: "POST",
            datatype: "json",
            success: function (data) {
                var my = eval("(" + data + ")");
                if (my.flag == -100) {
                    layer.msg(my.msg);
                    window.location.href = "/login.aspx";
                    return;
                }
                else if (my.flag != 1) {
                    window.layer.msg(my.msg);
                }
                else {
                    global.zMenuTree = $.fn.zTree.init($("#menuTree"), menuTreeSetting, eval("(" + my.obj + ")"));
                    global.zMenuTree.expandAll(true);
                }
            }
        });
        $.ajax({
            url: "/ajax/User.ashx",
            data: { action: 'getUserMenu', userid: '<%=userid %>' },
            async: false,
            type: "POST",
            datatype: "json",
            success: function (data) {
                var my = eval("(" + data + ")");
                if (my.flag == -100) {
                    layer.msg(my.msg);
                    window.location.href = "/login.aspx";
                    return;
                }
                else if (my.flag != 1) {
                    window.layer.msg(my.msg);
                }
                else {
                    $.each(my.obj, function (i, item) {
                        node = global.zMenuTree.getNodesByParam("id", item["menuId"], null);
                        global.zMenuTree.checkNode(node[0], true, true, true);
                    });
                }
            }
        });
    }
    //保存权限
    function savePowers() {
        var node = global.zMenuTree.getCheckedNodes(true);
        var ids = "";
        $.each(node, function (i, item) {
            if (node.children == null) {
                ids = ids + item.id + ",";
            }
        });

        $.ajax({
            url: "/ajax/User.ashx",
            data: { action: 'savePowers', userid: '<%=userid %>', ids: ids },
            async: true,
            type: "POST",
            datatype: "json",
            success: function (data) {
                var my = eval("(" + data + ")");
                if (my.flag == -100) {
                    layer.msg(my.msg);
                    window.location.href = "/login.aspx";
                    return;
                }
                else if (my.flag != 1) {
                    window.layer.msg(my.msg);
                }
                else {
                    window.layer.msg(my.msg);
                }
            }
        });
    }
</script>
</html>
