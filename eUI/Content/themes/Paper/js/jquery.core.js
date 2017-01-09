

function backToTop() {
    var speed = 500;//滑动的速度
    $('body,html').animate({ scrollTop: 0 }, speed);
    return false;
}

function scrollFixed() {
    if ($(".cont-box").length > 0) {
        var obMenu = $(".cont-box").offset().top + 90;

        var win = $(window); //得到窗口对象
        var sc = $(document);//得到document文档对象。
        win.scroll(function () {
            if (sc.scrollTop() >= obMenu) {
                $(".menu-bg").css("display", "block");
                $(".menu-bg").addClass("menufixed");
            } else {
                $(".menu-bg").removeClass("menufixed");
            }
        })
    }
}
function SearchTemplateByType() {
    var flag = $("#selTmpType").val();
    var url = $("body").data("website") + "Paper/TemplateDownload?flag=" + flag;
    window.location.href = url;
}


function InitMenu() {
    var type = $("body").data("type");
    $(".menu-bar-fixd li a").removeClass("active");
    $(".menu-bar-fixd li").each(function (index, element) {

        if (type == (index + 1)) {
            $(element).find("a").addClass("active");
        }

    });
}
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);

    return prePath;
}
function AddDownloadNum(downloadUrl, id) {
    var url = $("body").data("website") + "ToolDownload/AddDownloadNum";
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        cache: false,
        headers: { "Cache-Control": "no-cache" },
        data: { id: id },
        success: function (data) {
            if (data.success == true) {
                $(".badge").html(data.num);
                var newWin = window.open('', '_blank');
                newWin.location.href = getRootPath() + downloadUrl;
            }
        },
        error: function (e) {
        }
    });
}

function AddTmpDownloadNum(downloadUrl, id) {
    var url = $("body").data("website") + "TemplateDownload/AddDownloadNum";
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        cache: false,
        headers: { "Cache-Control": "no-cache" },
        data: { id: id },
        success: function (data) {
            if (data.success == true) {
                $(".read-div span").html(data.num);
                $(".download-num span").html(data.num);
                var newWin = window.open('', '_blank');
                newWin.location.href = getRootPath() + downloadUrl;
            }
        },
        error: function (e) {
        }
    });
}


function InitTopMenu() {

    var url = window.location.pathname;
    $item = $(".header .nav-list li");
    $item.removeClass("active");
    if (url.indexOf("PayGuide") >= 0) {
        $item.eq(1).addClass("active");
    } else if (url.indexOf("CreditGuarantee") >= 0) {
        $item.eq(2).addClass("active");
    } else if (url.indexOf("ToolDownload") >= 0) {
        $item.eq(3).addClass("active");
    } else if (url.indexOf("TemplateDownload") >= 0) {
        $item.eq(4).addClass("active");
    } else if (url.indexOf("TmpDetail") >= 0) {
        $item.eq(4).addClass("active");
    } else if (url.indexOf("About") >= 0) {
        $item.eq(5).addClass("active");
    } else {
        $item.eq(0).addClass("active");
    }
}


function loginState() {
    var state = $("body").data("loguser");
    if (state == "") {
        $(".nav-list.top-user").css("display", "block");
        $(".user-log-info").css("display", "none");
    } else {
        $(".nav-list.top-user").css("display", "none");
        $(".user-log-info span").html(state);
        $(".user-log-info").css("display", "flex")
    }
}




$(function () {
    InitTopMenu();
    scrollFixed();
    loginState();
    InitMenu();
    $(".flexslider").flexslider();
    var nav = $(".menu-bar"); //得到导航对象
    var win = $(window); //得到窗口对象
    var sc = $(document);//得到document文档对象。
    win.scroll(function () {
        if (sc.scrollTop() >= 700) {

            $(".menu-bar").fadeIn();
        } else {

            $(".menu-bar").fadeOut();
        }
    })
    $("#search_ipt").focus(function () {
        $("#search_ipt").attr("placeholder", "请输入搜索内容");
        $("#search_ipt").css("width", "150px");
        $("#search_ipt").css("color", "#000");
    });
    $("#search_ipt").blur(function () {
        $("#search_ipt").attr("placeholder", "");
        $("#search_ipt").css("width", "65px");
        $("#search_ipt").css("color", "#fff");
    });

    $(".btn-open-login").off("click").on("click", function () {
        $("#log-popup .modal-content").css("display", "none");
        $("#login_content.modal-content").css("display", "block");
        $("#log-popup.modal h1 span").removeClass("on");
        $("#log-popup.modal h1 span.login").addClass("on");
        var wid = ($(window).width() - 400) / 2;
        $("#log-popup.modal").css("display", "block");
        $("#log-popup .modal-dialog").css({
            display: "block",
            right: wid,
            top: "0"
        }).animate({
            top: "40%",

        }, function () {
            $("#log-popup .modal-dialog").animate({ top: "20%" });
        });
    });

    $(".btn-open-register").off("click").on("click", function () {
        $("#log-popup .modal-content").css("display", "none");
        $("#reg_content.modal-content").css("display", "block");
        $("#log-popup.modal h1 span").removeClass("on");
        $("#log-popup.modal h1 span.register").addClass("on");
        var wid = ($(window).width() - 400) / 2;
        $("#log-popup.modal").css("display", "block");
        $("#log-popup .modal-dialog").css({
            display: "block",
            right: wid,
            top: "60%"
        }).animate({
            top: "10%",
        }, function () {
            $("#log-popup .modal-dialog").animate({ top: "20%" });
        });
    });


    $(".btn-close").off("click").on("click", function () {
        $("#log-popup.modal").fadeOut(500, function () {
            $("#log-popup .modal-dialog .form-box dd label").html("");
            $(".form-box form input").val("");
        });


    });

    $(".socialwrapper").flip({
        direction: 'lr',
        speed: 300,
        color: '#f9f9f9',
        onEnd: function () {
            $(".inbox").find("#reg_content").toggle();
            $(".inbox").find("#login_content").toggle();
        }
    });


    $(".modal-title .login").off("click").on("click", function () {

        if ($("#login_content.modal-content").css("display") == "block") {
            return;
        }
        $(".modal h1 span").removeClass("on");
        $(".modal h1 span.login").addClass("on");
        $(".modal-content").css("display", "none");
        $(".modal-dialog .form-box dd label").html("");
        $("#reg_content.modal-content").css("display", "block");

        $(".socialwrapper").flip({
            direction: 'lr',
            speed: 300,
            color: '#f9f9f9',
            onEnd: function () {
                $(".inbox").find("#reg_content").toggle();
                $(".inbox").find("#login_content").toggle();
            }
        });
    });

    $(".modal-title .register").off("click").on("click", function () {
        if ($("#reg_content.modal-content").css("display") == "block") {
            return;
        }
        $(".modal h1 span").removeClass("on");
        $(".modal h1 span.register").addClass("on");
        $(".modal-content").css("display", "none");
        $(".modal-dialog .form-box dd label").html("");
        $("#login_content.modal-content").css("display", "block");

        $(".socialwrapper").flip({
            direction: 'rl',
            speed: 300,
            color: '#f9f9f9',
            onEnd: function () {
                $(".inbox").find("#reg_content").toggle();
                $(".inbox").find("#login_content").toggle();
            }
        });
    });


    $("#WxPay").off("click").on("click", function () {
        var stats = $(".user-log-info").css("display");

        if (stats != "none") {
            var url = $("body").data("website") + "Paper/WXPayUrl";
            var data = $("#paperData").serialize();
            $.ajax({
                url: url,
                type: "POST",
                dataType: "json",
                cache: false,
                headers: { "Cache-Control": "no-cache" },
                data: data,
                success: function (data) {
                    if (data.success == true) {
                        var codeUrl = data.data;

                        $("#wxPayCode").qrcode({
                            width: 200,
                            height: 200,
                            text: codeUrl
                        });
                        $(".modal-content").css("display", "block");
                        $(".modal-backdrop").css("display", "block");
                        $("#wx_pay_pop").css("display", "block");
                      
                        $("#orderNumber").val(data.orderNumber);
                        pollingStates();

                    } else {
                        alert("微信生成订单时出现错误，请您重新支付！");
                    }
                },
                error: function (e) {

                }
            });
        } else {
            alert("请您先登录，才能购买商品哦！");
        }

    });

    $("button.close").off("click").on("click", function () {
        $(".modal-backdrop").css("display", "none");
        $(".modal-content").css("display", "none");
        $("#wx_pay_pop").css("display", "none");
        $("canvas").remove();

    });

    function checkReg() {
        var result = true;
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        var email = $("#reg_mail").val();
        var password = $("#password").val();
        var regPassword = $("#reg_password").val();
        var name = $("#reg_name").val();
        $emailLeb = $(".mail-error");
        $password_error = $(".password-error");
        $reg_password_error = $(".reg-password-error");
        $name_error = $(".name-error");


        if (email == "") {
            $emailLeb.html("邮箱地址不能为空！");
            result = false;
        } else {
            if (!reg.test(email)) {
                $emailLeb.html("邮箱地址格式不正确！");
                result = false;
            }
        }
        if (password == "") {
            $password_error.html("密码不能为空！");
            result = false;
        }
        if (password.length > 6 && password.length < 12) {
            $password_error.html("密码长度为6-12位字符！");
            result = false;
        }
        if (password != regPassword) {
            $reg_password_error.html("两次输入的密码不一致！");
            result = false;
        } if (name == "") {
            $name_error.html("昵称不能为空！");
            result = false;
        }
        return result;
    }

    function checkLogin() {
        var result = true;
        var email = $("#login_mail").val();
        var password = $("#login_pwd").val();
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        $emailLeb = $(".mail-login-error");
        $password_error = $(".pwd-login-error");

        if (email == "") {
            $emailLeb.html("邮箱地址不能为空！");
            result = false;
        } else {
            if (!reg.test(email)) {
                $emailLeb.html("邮箱地址格式不正确！");
                result = false;
            }
        }
        if (password == "") {
            $password_error.html("密码不能为空！");
            result = false;
        }
        if (password.length > 6 && password.length < 12) {
            $password_error.html("密码长度为6-12位字符！");
            result = false;
        }
        return result;
    }




    var flagLog = "true";
    $("#reg_btn").off("click").on("click", function () {
        $(".modal-dialog .form-box dd label").html("");
        if (checkReg() == true) {
            var url = $("body").data("website") + "Login/UserRegist";
            var data = $("#register_fom").serialize();

            if (flagLog == "true") {
                flagLog = "false";
                $(".loading-pop").css("display", "block");
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    headers: { "Cache-Control": "no-cache" },
                    data: data,
                    success: function (data) {
                        flagLog = "true";
                        var dataSet = $.parseJSON(data);
                        $(".loading-pop").css("display", "none");
                        if (dataSet["Result"] == "false") {
                            $(".back-error").html(dataSet["data"]);

                        } else if (dataSet["Result"] == "true") {

                            $(".modal").css("display", "none");
                            $(".modal-dialog .form-box dd label").html("");
                            alert(dataSet["data"]);

                        }
                    },
                    error: function (e) {
                        flagLog = "true";
                        $(".loading-pop").css("display", "none");
                    }
                });
            }
        }
    });
    var flags = "true";
    $("#login_btn").off("click").on("click", function () {

        $(".modal-dialog .form-box dd label").html("");
        if (checkLogin() == true) {
            var url = $("body").data("website") + "Login/UserLogin";
            var data = $("#login_fom").serialize();

            if (flags == "true") {
                flags = "false";
                $(".loading-pop").css("display", "block");
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    headers: { "Cache-Control": "no-cache" },
                    data: data,
                    success: function (data) {
                        flags = "true";
                        var dataSet = $.parseJSON(data);
                        if (dataSet["Result"] == "false") {
                            $(".back-error").html(dataSet["data"]);
                            $(".loading-pop").css("display", "none");
                        } else if (dataSet["Result"] == "true") {
                            $(".modal").fadeOut(500, function () {
                                $(".modal-dialog .form-box dd label").html("");
                                //var emailText = $.parseJSON(dataSet["data"])[0]["Email"];
                                var nameText = $.parseJSON(dataSet["data"])[0]["Name"];
                                $(".nav-list.top-user").css("display", "none");
                                $(".user-log-info span").html(nameText);
                                $(".user-log-info").css("display", "flex")


                            });

                        }
                    },
                    error: function (e) {
                        flags = "true";
                        $(".loading-pop").css("display", "none");
                    }
                });
            }
        }
    });

    clearInterval(timeFunction);
})
