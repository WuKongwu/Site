/////用户增删改查

function add() {
    $("#txtHideID").val(-1);
    openDialog('新增用户');
}

function edit() {
    //获取选中行记录
    if ($('#usergrid').datagrid('getSelected') == null) {
        $.messager.alert("提示", '请先选中一条数据');
        return;
    }
    var userId = $('#usergrid').datagrid('getSelected').id;

    openDialog('修改用户');

    //ajax给form表单赋值
    $.ajax({
        url: '/api/User/GetUserInfo',
        data: { id: userId },
        type: 'POST',
        success: function (data) {
            $("#txtHideID").val(userId);
            $("#txtName").textbox('setValue', data.name);
            $("#txtPwd").passwordbox('setValue', data.pwd);
            $("#txtPwdAgain").passwordbox('setValue', data.pwd);
            $("#txtTel").textbox('setValue', data.tel);
            $("#txtEmail").textbox('setValue', data.email);
            $("#txtAddress").textbox('setValue', data.address);
            $(":radio").val([data.gender]);
        }
    });

}

function del() {
    //获取选中行记录
    if ($('#usergrid').datagrid('getSelected') == null) {
        $.messager.alert("提示", '请先选中一条数据');
        return;
    }
    //提示是否删除该条数据
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/api/User/Del',
                data: { id: $('#usergrid').datagrid('getSelected').id },
                type: 'Post',
                success: function (data) {
                    if (data == 'true' || data == true) {
                        //刷新grid
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

function openDialog(title) {
    $('#userdialog').dialog({
        title: title,
        modal: true,
        width: 400,
        shadow: true,
        height: 350,
        closed: false,
        buttons: [{
            text: '保存',
            handler: function () {
                onloading('正在保存...');
                $("#userInfoForm").submit();
            }
        }, {
            text: '取消',
            handler: function () {
                $('#userdialog').window('close');
            }
        }],
        onClose: function () {
            //清除Form表单数据
            $("#userInfoForm").form('clear');
            //$(this).dialog('destroy');
        }
    });
}