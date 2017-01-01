

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
                $('#txtAddVersion').val(data.models.Version),
                $('#txtAddReadNum').val(data.models.ReadNum),
                $('#txtAddPayNum').val(data.models.PayNum),
                $('#t_VfileZip').val(data.models.VideoZip),
                $("#t_file1").html(data.models.ImgA);
                $("#t_file2").html(data.models.ImgB);
                $("#t_file3").html(data.models.ImgC);
                $("#t_file4").html(data.models.ImgD);
                $("#t_file5").html(data.models.ImgE);
                $("#txtAddType").val(data.models.Type),
                $("#Id").val(data.models.Id);
                $("#textarea_id").val(data.models.DetailInfo),
                $("#t_Cfile").text(data.models.FileUrl),
                $("#t_guid").val(data.models.Code),
                $("#txtAddDate").val(DateFormat(data.models.CreateDate))
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(target, fileName) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/Admin/Detele',
                data: { id: target, guid: fileName },
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
