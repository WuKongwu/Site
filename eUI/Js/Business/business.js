$(function () {
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格
    $('#usergrid').datagrid({
        url: '/Business/SearchPay',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber: 1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
            { field: 'Name', title: '用户名', width: 200, align: 'left' },
            { field: 'OrderNumber', title: '订单号', width: 200, align: 'left' },
            {
                field: 'PayDate', title: '支付时间', width: 200, align: 'left', formatter: function (value, row, index) {
                    return DateFormat(row.PayDate);
                }
            },
            { field: 'Price', title: '价格', width: 250, align: 'left' },
            { field: 'Version', title: '论文编号', width: 250, align: 'left' },
            { field: 'Title', title: '论文标题', width: 250, align: 'left' },

            { field: 'Total', title: '总价', width: 250, align: 'left', display: 'none' },
            {
              field: 'action', title: '操作', width: '10%', align: 'center',
              formatter: function (value, row, index) {
                var d = '<a href="#" onclick="deletebusiness(' + row.Id + ')">删除</a>';
                return d;
              }
            }
        ]],
        onLoadSuccess: function (data) {
            $("#usergrid").datagrid("hideColumn", "Total"); // 设置隐藏列    
            var total = $("#datagrid-row-r1-2-0 td[field='Total'] div").html();
            $("#business_total").html(total);
        }
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

        //刷新grid
        if (checkDateTime() == false) {
            return false;
        }
        $('#usergrid').datagrid('load',
            {
                Name: $("#txtSearchUserName").val(),
                OrderNumber: $("#txtSearchOrderNumber").val(),
                Title: $("#txtSearchTitle").val(),
                PayDateStart: $("#txtSearchPayDateStart").val(),
                PayDateEnd: $("#txtSearchPayDateEnd").val(),
                Version: $("#txtSearchVersion").val()
            });
    });


    function checkDateTime() {
        var sDt = $("#txtSearchPayDateStart").val();
        var eDt = $("#txtSearchPayDateEnd").val();

        if ($("#txtSearchPayDateStart").val() != "" || $("#txtSearchPayDateEnd").val() != "") {
            if ($("#txtSearchPayDateStart").val() == "") {
                $.messager.alert("起始时间不能为空！");
                return false;
            } else if ($("#txtSearchPayDateEnd").val() == "") {
                $.messager.alert("截止时间不能为空！");
                return false;
            } else if ($("#txtSearchPayDateEnd").val() < $("#txtSearchPayDateStart").val()) {
                $.messager.alert("截止时间不能小于起始时间！");
                return false;
            }
        }
        else {
            return true;
        }
    }

});


function DateFormat(val) {
    var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
    //月份为0-11，所以+1，月份小于10时补个0
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}


function deletebusiness(id) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/Business/Detele',
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