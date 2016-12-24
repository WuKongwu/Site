$(function () {

    function clear() {
        var name = $("#ReadminName").val("");
        var Oldpassword = $("#ReadminOPaw").val("");
        var Newpassword = $("#ReadminNPaw").val("");
        var ReNadminName = $("#ReNadminName").val("");
    }

    $("#admin_login").off("click").on("click", function () {
       
        var name = $("#adminName").val();
        var password = $("#adminPaw").val();
        $.ajax({
            url: "../AdminLogin/User",
            type: 'POST',
            data: { name: name, password: password },
            success: function (data) {
                if (data.success) {
                    window.location.href = "/Home/Index";
                } else {
                    $(".login-bg-message").html("账号或者密码不正确！");
                }
            }
        });

    });

    $(".close-pop").off("click").on("click", function () {
        $('.admin-popup').css('display', 'none');
        clear();
    });

    $("#resetPw").off("click").on("click", function () {
    
        $(".admin-popup").css("display", "block");
        $("#admin_reset").off("click").on("click", function () {
            var name = $("#ReadminName").val();
            var Oldpassword = $("#ReadminOPaw").val();
            var Newpassword = $("#ReadminNPaw").val();
            var ReNadminName = $("#ReNadminName").val();
            $.ajax({
                url: "../AdminLogin/ResetPws",
                type: 'POST',
                data: { name: name, Opassword: Oldpassword,Npassword: Newpassword,ReNadminName:ReNadminName},
                success: function (data) {
                    if (data.success) {
                        $(".admin-popup").css("display", "none");
                        alert("密码修改成功！");
                        clear();
                       
                    } else {
                        alert("你的账号名或原始密码不正确，密码修改失败！");
                    }
                }
            });
        });

    });
});