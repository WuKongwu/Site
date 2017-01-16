$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格
    $('#usergrid').datagrid({
        url: '/AdminRecord/SearchAdmin',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber: 1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
            { field: 'Name', title: '用户名', width: 200, align: 'center' },
            { field: 'Email', title: '邮箱', width: 200, align: 'center' },
            { field: 'Password', title: '密码', width: 200, align: 'center' },
            {
                field: 'action', title: '操作', width: 200, align: 'center',
                formatter: function (value, row, index) {
                    var e = '<a href="#" onclick="editrow(' + row.Id + ')">修改</a> ';
                    var d = '<a href="#" onclick="deleterow(' + row.Id + ')">删除</a>';
                    return e + d;
                }
            }
        ]]
    });


    //设置分页控件 
    var p = $('#usergrid').datagrid('getPager');

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
        $('#usergrid').datagrid('load',
            {
                Name: $("#txtSearchUserName").val(),
                Email: $("#txtSearchEmail").val(),
            });
    });
    $('#submit').click(function (e) {
        debugger
        if (checkReg() == true) {
            var param = {
                Id: $("#Id").val(),
                Name: $("#admin_name").val(),
                Email: $("#admin_mail").val(),
                Password: $("#admin_paswd").val()
            };
            $.ajax({
                url: "../AdminRecord/Save",
                type: 'POST',
                data: param,
                success: function (data) {
                    if (data.success) {
                        $("#usergrid").datagrid('reload');
                        $('#dlg').window('close');
                    }
                }
            });
        }
    });
    $('#dlg').window('close');
});

function editrow(target) {
    $.ajax({
        url: "../AdminRecord/GetAdminRecordById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                $('#admin_name').val(data.models.Name),
                $('#admin_mail').val(data.models.Email),
                $("#Id").val(data.models.Id);
                $("#admin_paswd").val(data.models.Password);
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(id) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '../AdminRecord/Detele',
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
function checkReg() {
    var result = true;
    var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    var email = $("#admin_mail").val();
    var password = $("#admin_paswd").val();
   
    var name = $("#admin_name").val();
  
    if (email == "") {
        $.messager.alert("温馨提示","邮箱地址不能为空！");
        return false;
    } else {
        if (!reg.test(email)) {
            $.messager.alert("温馨提示", "邮箱地址格式不正确！");
            return false;
        }
    }
    if (password == "") {
        $.messager.alert("温馨提示", "密码不能为空！");
        return false;
    }
    if (password.length < 6 || password.length > 12) {
        $.messager.alert("温馨提示", "密码长度为6-12位字符！");
        return false;
    }
    if (name == "") {
        $.messager.alert("温馨提示", "昵称不能为空！");
        return false;
    }
    return result;
}