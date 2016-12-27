$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格 
    debugger
    $('#toolgrid').datagrid({

        url: '/ToolDownload/SearchTool',
        singleSelect: true,
        queryParams: { title: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber:1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
           
            { field: 'title', title: '工具标题', width: '20%', align: 'left' },
           
            { field: 'content', title: '工具简介', width: '10%', align: 'left' },
           
             { field: 'url', title: '下载链接', width: '10%', align: 'left' },

              { field: 'downloadNum', title: '下载数量', width: '5%', align: 'left' },
            {
                field: 'action', title: '操作', width: '10%', align: 'center',
                formatter: function (value, row, index) {
                    var e = '<a href="#" onclick="editrow(' + row.Id + ')">修改</a> ';
                    var d = '<a href="#" onclick="deleterow(' + row.Id + ')">删除</a>';
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
    var p = $('#toolgrid').datagrid('getPager');

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
    $("#btnSearch").click(function () {
    
        //刷新grid
        $('#toolgrid').datagrid('load',
            {
                title: $("#txtSearchTitle").val(),
            });
    });    
});

function editrow(target) {

    $.ajax({
        url: "/ToolDownload/GetToolById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                initPop();
                $('#txtAddTitle').val(data.models.title),
                $('#txtAddUrl').val(data.models.url),
                $('#txtAddNum').val(data.models.downloadNum),
                $("#t_file1").html(data.models.picture),
                $("#Id").val(data.models.Id),
                $("#txtAddShow").val(data.models.content)
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(id, fileName) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/ToolDownload/Detele',
                data: { id: id, fileName: fileName },
                type: 'Post',
                success: function (data) {
                    if (data.success == true || data == true) {

                        $('#toolgrid').datagrid('reload');
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
