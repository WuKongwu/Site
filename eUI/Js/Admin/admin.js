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
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
             { field: 'Version', title: '论文编号', width: '10%', align: 'left' },
            { field: 'Title', title: '论文标题', width: '20%', align: 'left' },
           
            { field: 'Price', title: '价格', width: '10%', align: 'left' },
            {
                field: 'CreateDate', title: '上传时间', width: '20%', align: 'left', formatter: function (value, row, index) {
                    return DateFormat(row.CreateDate);
                }
            },
            {
                field: 'Type', title: '论文类别', width: '10%', align: 'left', formatter: function (value, row, index) {

                return ShowType(row.Type);
                }
            },
              { field: 'ReadNum', title: '阅读数', width: '5%', align: 'left' },
                { field: 'PayNum', title: '购买数', width: '5%', align: 'left' },
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
        if (checkDateTime() == false) {
            return false;
        }
        //刷新grid
        $('#papergrid').datagrid('load',
            {
                Number: $("#txtSearchOrderNumber").val(),
                Title: $("#txtSearchTitle").val(),
                StTime: $("#txtSearchPayDateStart").val(),
                EdTime: $("#txtSearchPayDateEnd").val(),
                Type: $("#txtType").val()
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

function checkDateTime() {
    var sDt = $("#txtSearchPayDateStart").val();
    var eDt = $("#txtSearchPayDateEnd").val();

    if ($("#txtSearchPayDateStart").val() != "" || $("#txtSearchPayDateEnd").val() != "") {
        if ($("#txtSearchPayDateStart").val() == "") {
            alert("起始时间不能为空！");
            return false;
        } else if ($("#txtSearchPayDateEnd").val() == "") {
            alert("截止时间不能为空！");
            return false;
        } else if ($("#txtSearchPayDateEnd").val() < $("#txtSearchPayDateStart").val()) {
            alert("截止时间不能小于起始时间！");
            return false;
        }
    }
    else {
        return true;
    }
}


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

function ShowType(val) {
    var showStr = "";
    
    switch(val.toString()){
        case "1":
            showStr = "asp作品";
            break;
        case "2":
            showStr = "jsp/java作品";
            break;
        case "3":
            showStr = "asp.net/c#作品";
            break;
        case "4":
            showStr = "php作品";
            break;
        case "5":
            showStr = "vc++作品";
            break;
        case "6":
            showStr = "安卓作品";
            break;
        case "7":
            showStr = "ios作品";
            break;
        case "8":
            showStr = "游戏作品";
            break;
        default:
            showStr = "未知类型";
            break;
           
    }
    return showStr
}