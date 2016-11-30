



function editrow(target) {
   
    $.ajax({
        url: "../Admin/GetPaperById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                initPop();
                $('#txtAddTitle').val(data.models.Title),
                //$('[name="txtAddTitle"]').prev().val(data.models.Title);
                $('#txtAddShow').val(data.models.Info),
                //$('[name="txtAddShow"]').prev().val(data.models.Info);
                $('#txtAddPrice').val(data.models.Price),
                //$('[name="txtAddPrice"]').prev().val(data.models.Price);
                $("#t_file3").val(data.models.ImgC);
                $("#t_file1").val(data.models.ImgA);
                $("#t_file2").val(data.models.ImgB);
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
