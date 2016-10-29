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
        pageSize: 5,//每页显示的记录条数，默认为10 
        pageList: [5, 10, 15],//可以设置每页记录条数的列表 
        columns: [[
            { field: 'Name', title: '姓名', width: 200, align: 'left' },
            { field: 'OrderNumber', title: '訂單號', width: 200, align: 'left' },
            {
                field: 'PayDate', title: '支付時間', width: 200, align: 'left', formatter: function (value, row, index) {
                    return DateFormat(row.PayDate);
                }
            },
            { field: 'Price', title: '價格', width: 250, align: 'left' },
              { field: 'Title', title: '論文標題', width: 250, align: 'left' }
        ]]
    });
    ////查询
    $("#btnSearch").click(function () {
        //刷新grid
        $('#usergrid').datagrid('load',
            {
                Email: $("#txtSearchUserName").textbox('getValue')

            });
    });
});


function DateFormat(val) {
    var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
    //月份为0-11，所以+1，月份小于10时补个0
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}