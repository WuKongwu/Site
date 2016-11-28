///datagrid行内编辑

//function getRowIndex(target) {

//    var tr = $(target).parents("tr");

//    return parseInt(tr.attr('datagrid-row-index'));
//}

function editrow(target) {
    //var row = $("#papergrid").datagrid("getSelected");
    //if (row) {
    //    $("#dlg").dialog("open").dialog('setTitle', 'Edit User');
    //    $("#dlg").form("load", row);
    //    url = '../Admin/GetPaperById?id=' + target;
    //}

    //$('#dlg').dialog({
    //    title: 'My Dialog',
    //    width: 400,
    //    height: 200,
    //    closed: false,
    //    cache: false,
    //    href: '../Admin/GetPaperById?id=' + target,
    //    modal: true
    //});
    //$('#dlg').dialog('refresh', '../Admin/GetPaperById?id=' + target);
    //console.log(target);
    //
    $.ajax({
        url: "../Admin/GetPaperById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                console.log(data.models.DetailInfo);
                debugger
                $('[name="txtAddTitle"]').val(data.models.Title),
                $('[name="txtAddTitle"]').prev().val(data.models.Title);
                $('[name="txtAddShow"]').val(data.models.Info),
                $('[name="txtAddShow"]').prev().val(data.models.Info);
                $('[name="txtAddPrice"]').val(data.models.Price),
                $('[name="txtAddPrice"]').prev().val(data.models.Price);
                $("#txtAddType").val(data.models.Type),
                $("#Id").val(data.models.Id);
                $("#textarea_id").val(data.models.DetailInfo)
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(target) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/Admin/Detele',
                data: { id: target },
                type: 'Post',
                success: function (data) {
                    if (data.success == true || data == true) {
                       
                        $('#papergrid').datagrid('reload');
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
//function saverow(target) {
//    $('#usergrid').datagrid('endEdit', getRowIndex(target));
//}
//function cancelrow(target) {
//    $('#usergrid').datagrid('cancelEdit', getRowIndex(target));
//}