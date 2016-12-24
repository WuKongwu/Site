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
        pageNumber: 1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [ [
            {field: 'Id', title: '用户编号', width: 200, align: 'left' },
            { field: 'Name', title: '用户名', width: 200, align: 'left' },
            { field: 'Email', title: '邮箱', width: 350, align: 'left' },
            {
                field: 'action', title: '操作', width: '10%', align: 'center',
                formatter: function (value, row, index) {
                    var d = '<a href="#" onclick="deleteuser(' + row.Id + ')">删除</a>';
                    return d;
                }
            }
        ]]
    });

   
    //设置分页控件 
     p = $('#usergrid').datagrid('getPager');

    $(p).pagination({
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        onBeforeRefresh: function () {
            $(this).pagination('loading');
            $(this).pagination('loaded');
        }
    });
    ////查询
    $("#btnSearch").click(function () {
        //刷新grid
        $('#usergrid').datagrid('load',
            {
                Name: $("#txtSearchUserName").textbox('getValue'),
                Email: $("#txtSearchUserMail").textbox('getValue')
            });
    });

});

function enableFilter() {
    $('#usergrid').datagrid('getRow');
}

function deleteuser(id) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/UserRecord/Detele',
                data: { id: id },
                type: 'Post',
                success: function (data) {
                    if (data.success == true || data == true) {

                        $('#usergrid').datagrid('reload');
                    }
                    else {
                        $.messager.alert("错误提示", '删除失败');
                    }
                },
                error: function () {
                    $.messager.alert("错误", '删除失败');
                }
            })
        }
    });
}