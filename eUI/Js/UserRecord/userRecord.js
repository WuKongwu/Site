$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格
    $('#usergrid').datagrid({
        url: '/UserRecord/SearchUsers',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        ContentType: "application/json",
        pageSize: 5,//每页显示的记录条数，默认为10 
        pageList: [5, 10, 15],//可以设置每页记录条数的列表 
        columns: [ [
            {field: 'Id', title: '用户编号', width: 200, align: 'left' },
            { field: 'Name', title: '用户名', width: 200, align: 'left' },
            { field: 'Email', title: '邮箱', width: 350, align: 'left' }
        ]]
    });

    //////行渲染
    ////$('#usergrid').datagrid("options").view.onBeforeRender = function (target, rows) {
    ////    $.each(rows, function (index, row) {
    ////        row.gender = formatSex(row.gender);
    ////        row.headpic = showheadpic(row.picurl);
    ////        row.createTime = formatDate(row.createTime);
    ////    });
    ////};
    ////设置分页控件 
    // p = $('#usergrid').datagrid('getPager');

    //$(p).pagination({
    //    //pageSize: 5,//每页显示的记录条数，默认为10 
    //    //pageList: [5, 10, 15],//可以设置每页记录条数的列表 
    //    beforePageText: '第',//页数文本框前显示的汉字 
    //    afterPageText: '页    共 {pages} 页',
    //    displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
    //    /*onBeforeRefresh:function(){
    //        $(this).pagination('loading');
    //        alert('before refresh');
    //        $(this).pagination('loaded');
    //    }*/
    //});
    ////查询
    $("#btnSearch").click(function () {
        //刷新grid
        $('#usergrid').datagrid('load',
            {
                Email: $("#txtSearchUserName").textbox('getValue')
               
            });
    });

    ////form提交
    //$("#userInfoForm").form({
    //    url: '/api/User/AddEdit',
    //    onSubmit: function () {
    //        //验证表单验证是否成功
    //        var isValid = $("#userInfoForm").form('validate');
    //        if (!isValid) {
    //            removeload();
    //        }
    //        return isValid;
    //    },
    //    success: function (data) {
    //        removeload();
    //        if (data == 'true') {
    //            //清除Form表单数据
    //            $("#userInfoForm").form('clear');
    //            //关闭当前窗口
    //            $("#userdialog").window('close');
    //            //刷新grid
    //            $('#usergrid').datagrid('reload');
    //        }
    //        else {
    //            $.messager.alert("错误提示", '操作失败');
    //        }
    //    }

    //});

  
    //$("#importFileForm").form({
    //    url: '/api/Upload/UploadUserPic',
    //    onSubmit: function (param) {
    //        param.id = $('#usergrid').datagrid('getSelected').id;
    //    },
    //    success: function (data) {
    //        var data = JSON.parse(data);

    //        if (data.IsSucceed == true || data.IsSucceed == "true") {
    //            //清除Form表单数据
    //            //$("#importFileForm").form('clear');
    //            //关闭当前窗口
    //            $("#userUploadPic").window('close');
    //            //刷新页面
    //            $('#usergrid').datagrid('reload');
    //        }
    //        else {
    //            alert(data.ErrorMsg);
    //        }
    //    }

    //});
    ////加载权限树
    ////LoadTree();
    ////格式化日期输入框
    ////InitDateBox();

    //$("body").keyup(function (event) {
    //    //"Esc"键关闭弹出窗口
    //    if (event.keyCode == 27) {
    //        $(".easyui-window").window("close");
    //    }
    //});


});

function enableFilter() {
    $('#usergrid').datagrid('getRow');
}