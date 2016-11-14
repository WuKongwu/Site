﻿

function backToTop() {
	var speed = 500;//滑动的速度
	$('body,html').animate({ scrollTop: 0 }, speed);
	return false;
}

function scrollFixed() {
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


$(function () {
	scrollFixed();
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
		$(".modal-content").css("display", "none");
		$("#login_content.modal-content").css("display", "block");
		$(".modal h1 span").removeClass("on");
		$(".modal h1 span.login").addClass("on");
		var wid = ($(window).width() - 400) / 2;
		$(".modal").css("display", "block");
		$(".modal-dialog").css({
			display: "block",
			right: wid,
			top: "0"
		}).animate({
			top: "40%",

		}, function () {
			$(".modal-dialog").animate({ top: "20%" });
		});
	});

	$(".btn-open-register").off("click").on("click", function () {
		$(".modal-content").css("display", "none");
		$("#reg_content.modal-content").css("display", "block");
		$(".modal h1 span").removeClass("on");
		$(".modal h1 span.register").addClass("on");
		var wid = ($(window).width() - 400) / 2;
		$(".modal").css("display", "block");
		$(".modal-dialog").css({
			display: "block",
			right: wid,
			top: "60%"
		}).animate({
			top: "10%",
		}, function () {
			$(".modal-dialog").animate({ top: "20%" });
		});
	});


	$(".btn-close").off("click").on("click", function () {
		$(".modal").fadeOut(500);

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
		if (password != regPassword) {
			$reg_password_error.html("两次输入的密码不一致！");
			result = false;
		} if (name == "") {
			$name_error.html("昵称不能为空！");
			result = false;
		}
		return result;


	}

	var flagLog = "true";
	$("#reg_btn").off("click").on("click", function () {
		if (checkReg() == true) {
			var url = $("body").data("website") + "Login/UserRegist";
			var data = $("#register_fom").serialize();

			if (flagLog == "true") {
				flagLog = "false";

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

						if (dataSet["Result"] == "false") {
							$(".back-error").html(dataSet["data"]);
						}
					},
					error: function (e) {
						flagLog = "true";
					}
				});
			}
		}
	});
})