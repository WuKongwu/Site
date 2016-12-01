$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格 
    debugger
    $('#papergrid').datagrid({

        url: '/Admin/SearchPaper',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber:1,
        pageSize: 5,//每页显示的记录条数，默认为10 
        pageList: [5, 10, 15],//可以设置每页记录条数的列表 
        columns: [[
            { field: 'Title', title: '论文标题', width: 200, align: 'left' },
            { field: 'Info', title: '论文简介', width: 200, align: 'left' },
            { field: 'Price', title: '价格', width: 50, align: 'left' },
            {
                field: 'CreateDate', title: '上传时间', width: 200, align: 'left', formatter: function (value, row, index) {
                    return DateFormat(row.CreateDate);
                }
            },
            { field: 'Type', title: '论文类别', width: 200, align: 'left' },
            {
                field: 'action', title: '操作', width: 80, align: 'center',
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
            $('#papergrid').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#papergrid').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#papergrid').datagrid('refreshRow', index);
        }
    });

    //设置分页控件 
    var p = $('#papergrid').datagrid('getPager');

    $(p).pagination({
        //pageSize: 5,//每页显示的记录条数，默认为10 
        //pageList: [5, 10, 15],//可以设置每页记录条数的列表 
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
        $('#papergrid').datagrid('load',
            {
                name: $("#txtSearchUserName").textbox('getValue'),
                stTime: $("#txtstTime").datebox('getValue'),
                edTime: $("#txtedTime").datebox('getValue')
            });
    });

    //form提交
    $("#userInfoForm").form({
        url: '/api/User/AddEdit',
        onSubmit: function () {
            //验证表单验证是否成功
            var isValid = $("#userInfoForm").form('validate');
            if (!isValid) {
                removeload();
            }
            return isValid;
        },
        success: function (data) {
            removeload();
            if (data == 'true') {
                //清除Form表单数据
                $("#userInfoForm").form('clear');
                //关闭当前窗口
                $("#userdialog").window('close');
                //刷新grid
                $('#usergrid').datagrid('reload');
            }
            else {
                $.messager.alert("错误提示", '操作失败');
            }
        }

    });

    $("#importFileForm").form({
        url: '/api/Upload/UploadUserPic',
        onSubmit: function (param) {
            param.id = $('#papergrid').datagrid('getSelected').id;
        },
        success: function (data) {
            var data = JSON.parse(data);

            if (data.IsSucceed == true || data.IsSucceed == "true") {
                //清除Form表单数据
                //$("#importFileForm").form('clear');
                //关闭当前窗口
                $("#userUploadPic").window('close');
                //刷新页面
                $('#papergrid').datagrid('reload');
            }
            else {
                //alert(data.ErrorMsg);
            }
        }

    });


    $("body").keyup(function (event) {
        //"Esc"键关闭弹出窗口
        if (event.keyCode == 27) {
            $(".easyui-window").window("close");
        }
    });

    
});


function enableFilter() {
    $('#paper_input').datagrid('getRow');
}
function DateFormat(val) {
    var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
    //月份为0-11，所以+1，月份小于10时补个0
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}