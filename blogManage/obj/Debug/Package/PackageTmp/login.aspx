<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="blogManage.login1" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��Ӱ�����ͺ�̨</title>
    <link href="assets/css/jq22.css" rel="stylesheet" type="text/css" />
    <link href="assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <style>
        #login .loginform .pad
        {
            padding-bottom:10px;    
        }
    </style>

    <script src="assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#login #password').focus(function () {
                $('#owl-login').addClass('password');
            }).blur(function () {
                $('#owl-login').removeClass('password');
            });
        });
    </script>
    <script src="assets/js/jquery.cookie.js" type="text/javascript"></script>
    <script src="assets/plugins/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            onKeypress();
            readUserPwd();
            $("#userName").focus();
        });
        //��ȡ�û�����
        function readUserPwd() {
            if ($.cookie('user_name') && $.cookie('user_pwd')) {
                $("#userName").val($.cookie('user_name'));
                $("#password").val($.cookie('user_pwd'));
                $("#remember").attr("checked", true);
            }
        }
        //�����¼�
        function onKeypress() {
            document.onkeypress = function (e) {
                var currKey = 0, e = e || event;
                if (e.keyCode == 13) login();
            }
        }
        //��¼
        function login() {
            var uname = $("#userName").val();
            var pwd = $("#password").val();
            var isRemember = 0;
            if (uname.length < 1) {
                $("#userName").focus();
                layer.msg("�û�������Ϊ��");
                return;
            }
            if (pwd.length < 1) {
                $("#password").focus();
                layer.msg("���벻��Ϊ��");
                return;
            }
            if ($("#remember").attr("checked")) {
                isRemember = 1;
            }
            $.ajax({
                url: "./ajax/User.ashx",
                data: { action: 'userLogin', userName: uname, pwd: pwd, isRemember: isRemember },
                async: true,
                type: "POST",
                datatype: "json",
                success: function (json) {
                    var my = eval("(" + json + ")");
                    if (my.flag == 1) {
                        //if ($("#remember").attr("checked") == "checked") {
                        if ($('#remember').is(':checked')) {
                            $.cookie('user_name', uname);
                            $.cookie('user_pwd', pwd);
                        }
                        else {
                            $.cookie('user_name', null);
                            $.cookie('user_pwd', null);
                        }
                        location.href = "main.aspx";
                    }
                    else {
                        layer.msg(my.msg);
                    }
                }
            });
        }       
    </script>
</head>
<body>
    <!-- begin -->
    <div style="margin-top:100px;text-align:center;">
        <span style="color:#472D20;font-size:50px;margin-left:-100px;">��Ӱ��</span><br />
        <span style="color:#472D20;font-size:50px;margin-left:50px;">-���˲���</span>
    </div>
    <div id="login">
        <div class="wrapper">
            <div class="login">
                <form action="#" method="post" class="container offset1 loginform">
                <div id="owl-login">
                    <div class="hand">
                    </div>
                    <div class="hand hand-r">
                    </div>
                    <div class="arms">
                        <div class="arm">
                        </div>
                        <div class="arm arm-r">
                        </div>
                    </div>
                </div>
                <div class="pad">
                    <div class="control-group">
                        <div class="controls">
                            <label for="userName" class="control-label fa fa-envelope">
                            </label>
                            <input id="userName" type="text" name="userName" placeholder="userName" tabindex="1"
                                autofocus="autofocus" class="form-control input-medium">
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                            <label for="password" class="control-label fa fa-asterisk">
                            </label>
                            <input id="password" type="password" name="password" placeholder="password" tabindex="2"
                                class="form-control input-medium">
                        </div>
                    </div>
                    <div class="control-group">
                        <div class="controls">
                                <input name="remember" id="remember" type="checkbox"/>
                                <span class="lbl">��ס����</span>
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <%-- <a href="#" tabindex="5" class="btn pull-left btn-link text-muted">Forgot password?</a><a
                        href="#" tabindex="6" class="btn btn-link text-muted">Sign Up</a>--%>
                    <button type="button" tabindex="4" class="btn btn-primary" onclick=login();>
                        Login</button>
                </div>
                </form>
            </div>
        </div>
    </div>
    <!-- end -->
 <div style="text-align:center;margin-left:-110px;"><a href="http://www.miitbeian.gov.cn/state/outPortal/loginPortal.action" target="_blank" style="position:fixed;bottom:0;">ԥICP��17002472��-1</a></div>
   
</body>
</html>
