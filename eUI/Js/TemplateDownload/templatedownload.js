$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格 
    debugger
    $('#Templategrid').datagrid({

        url: '/TemplateDownload/SearchTemplate',
        singleSelect: true,
        queryParams: { title: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber: 1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[

            { field: 'Title', title: '模板标题', width: '25%', align: 'center' },
            { field: 'Type', title: '模板类别', width: '20%', align: 'center' },
            {
                 field: 'CreateDate', title: '上传时间', width: '20%', align: 'center', formatter: function (value, row, index) {
                     return DateFormat(row.CreateDate);
                 }
            },
           { field: 'DownloadNum', title: '下载数量', width: '15%', align: 'center' },
           {
                field: 'action', title: '操作', width: '20%', align: 'center',
                formatter: function (value, row, index) {
                    var e = '<a href="#" onclick="editrow(' + row.Id + ')">修改</a> ';
                    var d = '<a href="#" onclick="deleterow(' + '\'' + row.Id + '\'' + ',' + '\'' + row.TemplateFile + '\'' + ')">删除</a>';
                    return e + d;
                }
            }
        ]],

        onBeforeRefresh: function () {
            return true;
        },
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
    var p = $('#Templategrid').datagrid('getPager');

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

    //查询
    $("#btnSearchType").click(function () {

        //刷新grid
        $('#Templategrid').datagrid('load',
            {
                TemplateType: $("#txtSearchType").val(),
            });
    });

    if ($("#selTmpType").find("option").length <= 1) {
        $.messager.alert("提示", '您还没有添加任何模板的类别，请您先去模板分类去添加模板类别');
    }

});

function editrow(target) {

    $.ajax({
        url: "/TemplateDownload/GetTemplateById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                initPop();
                $('#txtAddTitle').val(data.models.Title),
                $('#popType').val(data.models.TypeId);
                $('#txtAddNum').val(data.models.DownloadNum),
                $('#txtAddShow').val(data.models.Content),
                $("#Id").val(data.models.Id),
                $("#t_file1").html(data.models.TemplateFile),
                $("#t_file2").html(data.models.Picture),
                $("#txtDate").val(DateFormat(data.models.CreateDate))
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(id, fileName) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/TemplateDownload/DeteleTemplateDownload',
                data: { id: id ,fileName: fileName},
                type: 'Post',
                success: function (data) {
                    if (data.success == true || data == true) {

                        $('#Templategrid').datagrid('reload');
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
function DateFormat(val) {
    var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
    //月份为0-11，所以+1，月份小于10时补个0
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}
