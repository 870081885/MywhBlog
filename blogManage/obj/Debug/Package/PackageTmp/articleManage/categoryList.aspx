<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="categoryList.aspx.cs" Inherits="blogManage.articleManage.categoryList" %>

<!DOCTYPE html>
<html>
<head>
    <title>文章类型列表</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!-- ace styles -->
    <link href="/assets/plugins/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <script src="/assets/inspinia/js/inspinia.js" type="text/javascript"></script>
    <script src="/assets/plugins/DataTables/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="/assets/plugins/DataTables/js/dataTables.bootstrap.min.js" type="text/javascript"></script>
</head>
<!--防止列表下出现阴影 -->
<body>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-sm-4">
            <h2>文章类型列表</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">文章管理</a>
                </li>
                <li class="active">
                    <strong>文章类型列表</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>文章类型列表</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link" title="折叠">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-inline">
                            <div class="form-group">
                                <label class="sr-only" for="sCategory">
                                    标题</label>
                                <input type="text" class="form-control" id="sCategory" placeholder="类型名称或id" />
                            </div>
                            <button type="button" class="btn btn-success search btn-sm" onclick="search()">
                                查 询</button>
                        </div>
                        <br />
                        <table id="tbCategory" class="table table-striped table-bordered" cellspacing="0"
                            width="100%">
                            <thead>
                                <tr>
                                    <th>类型Id</th>
                                    <th>类型名称</th>
                                    <th>父类型Id</th>
                                    <th>操作</th>
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
        tbCategory: null
    };
    $(function () {
        getCategoryList();
    });
    //获取文章类型列表
    function getCategoryList() {
        global.tbCategory = $('#tbCategory').DataTable({
            "searching": false,
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": "/ajax/Article.ashx",
                "type": "POST",
                "data": function (d) {
                    d.action = "getCategoryList";
                    d.sCategory = escape($("#sCategory").val().trim());
                }
            },
            "lengthMenu": [15, 30, 50, 100],
            "language": {
                "url": "/assets/plugins/DataTables/language.txt"
            },
            "columns": [
                    { "data": "categoryId" },
                    { "data": "categoryName" },
                    { "data": "parentId" },
                    {
                        "orderable": false, // 禁用排序
                        "DefaultContent": "",
                        "render": function (data, type, full, meta) {
                            var ss = "<a href=\"javascript:void(0)\" onclick=\"addOrEditInfo('" + full.categoryId + "')\">" +
                                     "<span class=\"fa fa-pencil\" aria-hidden=\"true\" title=\"编辑\"></span></a>" +
                                     "<a href=\"javascript:void(0)\" onclick=\"deleteInfo('" + full.categoryId + "')\">" +
                                     "<span class=\"fa fa-trash\" aria-hidden=\"true\" title=\"删除\" style=\"padding-left:10px\"></span></a>";
                            return ss;
                        }
                    }
                ],
            "dom": '<"#tablesTools">lrtip',
            "initComplete": function (settings, json) {
                $("#tablesTools").append(
                    "<button id=\"btnAdd\" type=\"button\" class=\"btn btn-info btn-sm\" onclick=\"addOrEditInfo(0)\">新增</button>"
                    );
                $("#tablesTools").css("float", "right")
            }
        });
    }
    //新增或编辑
    function addOrEditInfo(categoryId) {
        $("#mainContent").load("/articleManage/categoryAdd.aspx?categoryId=" + categoryId);
    }

    //删除
    function deleteInfo(categoryId) {
        window.layer.confirm('确定要删除此文章类型吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            $.ajax({
                type: "post",
                url: "/ajax/Article.ashx",
                data: { action: 'deleteCategory', 'categoryId': categoryId },
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
                        global.tbCategory.ajax.reload(null, false);
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
        global.tbCategory.ajax.reload(null, true);
    }
</script>
</html>
