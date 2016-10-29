$(function(){
	
    $(".inbox").toggle(function () {
		var me = $(this);
		me.flip({
			direction:'lr',
			speed: 300,
			color:'#505050',
			onEnd: function(){
			    me.find("#reg_content").toggle();
			    me.find("#login_content").toggle();
			}
		});
	},function(){
		var me = $(this);
		me.flip({
			direction:'rl',
			speed: 300,
			color:'#303030',
			onEnd: function(){
			    me.find("#reg_content").toggle();
			    me.find("#login_content").toggle();
			}
		});
	});
	
});