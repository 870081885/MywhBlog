<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blogMenu.aspx.cs" Inherits="blogManage.config.blogMenu" %>

<!DOCTYPE html>
<html>
<head>
    <title>博客导航设置</title>
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
            <h2>
                博客导航设置</h2>
            <ol class="breadcrumb">
                <li class="active"><strong>博客导航设置</strong> </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            博客导航设置</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div style="float: right;">
                            <button type="button" class="btn btn-primary btn-sm" onclick='$("#mainContent").load("/config/blogMenuAdd.aspx");'>
                                新增导航</button>
                            <button type="button" class="btn btn-warning btn-sm" onclick='editInfo();'>
                                编辑导航</button>
                            <button type="button" class="btn btn-danger btn-sm" onclick='deleteInfo();'>
                                删除导航</button>
                        </div>
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
            chkStyle: "radio",
            radioType: "all"
        }
    };
    $(function () {
        bindMenuTree();
    });
    //绑定博客树形菜单
    function bindMenuTree() {
        $.ajax({
            url: "/ajax/Config.ashx",
            data: { action: 'getBlogMenuTree' },
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
    }

    //编辑用户
    function editInfo() {
        var nodes = global.zMenuTree.getCheckedNodes(true);
        if (nodes.length == 0) {
            layer.msg("请先选中导航");
            return;
        }
        $("#mainContent").load("/config/blogMenuAdd.aspx?blogMenuId=" + nodes[0].id);
    }
    //删除用户
    function deleteInfo(userId) {
        var nodes = global.zMenuTree.getCheckedNodes(true);
        if (nodes.length == 0) {
            layer.msg("请先选中导航");
            return;
        }
        window.layer.confirm('确定要删除此导航吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            $.ajax({
                type: "post",
                url: "/ajax/Config.ashx",
                data: { action: 'deleteBlogMenu', bolgMenuId: nodes[0].id },
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
                        bindMenuTree();
                    }
                    else {
                        window.layer.msg(my.msg);
                    }
                }
            });
        });
    }


</script>
</html>
