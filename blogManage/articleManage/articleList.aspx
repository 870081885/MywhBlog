<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="articleList.aspx.cs" Inherits="blogManage.articleManage.articleList" %>

<!DOCTYPE html>
<html>
<head>
    <title>文章列表</title>
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
            <h2>文章列表</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="#">文章管理</a>
                </li>
                <li class="active">
                    <strong>文章列表</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>文章列表</h5>
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
                                    文章类型</label>
                                 <select class="form-control" id="sCategory">
                                    <option value="">-请选择-</option>
                                 </select>
                            </div>
                            <div class="form-group">
                                <label class="sr-only" for="sTitle">
                                    标题</label>
                                <input type="text" class="form-control" id="sTitle" placeholder="标题" />
                            </div>
                            <button type="button" class="btn btn-success search btn-sm" onclick="search()">
                                查 询</button>
                        </div>
                        <br />
                        <table id="tbArticle" class="table table-striped table-bordered" cellspacing="0"
                            width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        id
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th>
                                        文章类型
                                    </th>
                                    <th>
                                        是否置顶
                                    </th>
                                    <th>
                                        更新时间
                                    </th>
                                    <th>
                                        状态
                                    </th>
                                    <th>
                                        开关
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
        getCategoryList();
        getArticleList();
    });

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

    //获取文章列表
    function getArticleList() {
        global.tbArticle = $('#tbArticle').DataTable({
            "searching": false,
            "processing": true,
            "serverSide": true,
            "order": [[4, 'desc']],
            "ajax": {
                "url": "/ajax/Article.ashx",
                "type": "POST",
                "data": function (d) {
                    d.action = "getArticleList";
                    d.sTitle = escape($("#sTitle").val().trim());
                    d.sCategory = $("#sCategory").val();
                }
            },
            "lengthMenu": [15, 30, 50, 100],
            "language": {
                "url": "/assets/plugins/DataTables/language.txt"
            },
            "columns": [
                    { "data": "id" },
                    { "data": "title" },
                    { "data": "categoryName" },
                    { 
                      "data": "isTop",
                      "render": function (data, type, full, meta) {
                          var ss = "否";
                          if (data == 1) {
                              ss = "是";
                          }
                          return ss;
                      } 
                    },
                    { "data": "updateTime" },
                    {
                        "data": "status",
                        "render": function (data, type, full, meta) {
                            var ss = "<span class=\"label label-danger\">暂停</span>";
                            if (data == 1) {
                                ss = "<span class=\"label label-primary\">启用</span>";
                            }
                            return ss;
                        }
                    },
                    {
                        "data": "status",
                        "render": function (data, type, full, meta) {
                            var ss = "<span class=\"label label-danger\">暂停</span>";
                            if (data == 1) {
                                ss = "<span class=\"label label-primary\">启用</span>";
                            }
                            return ss;
                        }
                    },
                    {
                        "orderable": false, // 禁用排序
                        "DefaultContent": "",
                        "render": function (data, type, full, meta) {
                            var ss = "<a href=\"javascript:void(0)\" onclick=\"addOrEditInfo(" + full.id + ")\">" +
                                     "<span class=\"fa fa-pencil\" aria-hidden=\"true\" title=\"编辑\"></span></a>"+
                                     "<a href=\"javascript:void(0)\" onclick=\"deleteInfo(" + full.id + ")\">" +
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
    function addOrEditInfo(articleId) {
        $("#mainContent").load("/articleManage/articleAdd.aspx?articleId=" + articleId);
    }

    //删除
    function deleteInfo(articleId) {
        window.layer.confirm('确定要删除此文章吗？', {
            btn: ['确定', '取消'] //按钮
        }, function () {
            $.ajax({
                type: "post",
                url: "/ajax/Article.ashx",
                data: { action: 'deleteArticle', 'articleId': articleId },
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
                        global.tbArticle.ajax.reload(null, false);
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
        global.tbArticle.ajax.reload(null, true);
    }
</script>
</html>
