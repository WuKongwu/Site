$(function () {

    $("#admin_login").off("click").on("click", function () {
        alert("0000");
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
});