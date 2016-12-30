$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格 
    debugger
    $('#templatetypegrid').datagrid({

        url: '/TemplateDownload/SearchTmpType',
        singleSelect: true,
        queryParams: { title: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber:1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
           
            { field: 'TemplateType', title: '模板类别', width: '30%', align: 'center' },
            {
                field: 'action', title: '操作', width: '30%', align: 'center',
                formatter: function (value, row, index) {
                    var e = '<a href="#" onclick="Typeeditrow(' + row.Id + ')">修改</a> ';
                    var d = '<a href="#" onclick="Typedeleterow(' + row.Id + ')">删除</a>';
                    return e + d;
                }
            }
        ]],

        onBeforeRefresh: function () {  
            return true;                 },
        onClickCell: function (rowIndex, field, value) {
            //如果点击的是商品列.弹出窗口.
            debugger
            if (field == "GoodsName") {
                detailIndex = rowIndex;
                $("#goodsWindows").window("open");
            }
        },
        onBeforeEdit: function (index, row) {
            row.editing = true;
            $('#toolgrid').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#toolgrid').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#toolgrid').datagrid('refreshRow', index);
        }
    });
  
    //设置分页控件 
    var p = $('#templatetypegrid').datagrid('getPager');

    $(p).pagination({
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        beforePageText: '第',//页数文本框前显示的汉字 
        afterPageText: '页    共 {pages} 页',
        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        onBeforeRefresh:function(){
            $(this).pagination('loading');
            $(this).pagination('loaded');
        }
    });

    //查询
    $("#btnSearchType").click(function () {
    
        //刷新grid
        $('#templatetypegrid').datagrid('load',
            {
                TemplateType: $("#txtSearchType").val(),
            });
    });    
});

function Typeeditrow(target) {

    $.ajax({
        url: "/TemplateDownload/GetTemplateTypeById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                initPop();
                $('#txtAddTypeTitle').val(data.models.TemplateType),
                $("#Id").val(data.models.Id)
            }
        },
    });
    $('#dlg').window('open');
}
function Typedeleterow(id) {
    $.messager.confirm('确认', '删除此模板类别，会使之前设置此类别的模板失去类别信息，请谨慎删除？', function (r) {
        if (r) {
            $.ajax({
                url: '/TemplateDownload/DeteleTemplateType',
                data: { id: id },
                type: 'Post',
                success: function (data) {
                    if (data.success == true || data == true) {

                        $('#templatetypegrid').datagrid('reload');
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
