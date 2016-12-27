$(function () {
    $("#userInfoForm>div ").css("height", 35);
    //加载表格 
    debugger
    $('#papergrid').datagrid({

        url: '/ImageManage/SearchImage',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber: 1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
             { field: 'ImageName', title: '图片名称', width: '20%', align: 'left' },
             { field: 'ImagePosition', title: '图片地址', width: '20%', align: 'left' },
            { field: 'ImageURL', title: '图片连接地址', width: '20%', align: 'left' },
            { field: 'CreateDate', title: '创建时间', width: '20%', align: 'left' },
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
        onBeforeRefresh: function () {
            $(this).pagination('loading');
            $(this).pagination('loaded');
        }
    });
    $("#btnSearch").click(function () {
        //刷新grid
        $('#papergrid').datagrid('load',
            {
                Name: $("#txtSearchName").val(),
            });
    });
    $('#dlg').window('close');
    $('#submit').click(function (e) {
        debugger
        var param = {
            Id: $("#Id").val(),
            Name: $("#image-Name").val(),
            SiteURL: $("#site-url").val(),
            ImageURL: $("#t_Cfile").text()
        };
        $.ajax({
            url: "../ImageManage/Save",
            type: 'POST',
            data: param,
            success: function (data) {
                if (data.success) {
                    $("#papergrid").datagrid('reload');
                    $('#dlg').window('close');
                }
            }
        });
    });
    $('#uploadCode').click(function (e) {
        if ($("#Cfile").val().length > 0) {
            var size = $("#Cfile")[0].files[0].size;
            if (CheckFileType($("#Cfile"), "img")) {
                if (size > 5242880) { alert("图片大小不能超过5M！"); } else { var img = ajaxFileUpload("Cfile"); }
            } else {
                alert("图片格式支持jpeg、jpg、png、bmp、gif");
            }
        }
        else {
            alert("请选择图片");
        }
    });
    function CheckFileType(file, type) {
        var reg = /[^\\\/]*[\\\/]+/g; //匹配文件的名称和后缀的正则表达式
        var name = $(file).val().replace(reg, '');
        var postfix = /\.[^\.]+/.exec(name);//获取文件的后缀
        postfix = postfix + "";
        postfix = postfix.toLowerCase();
        if (type == "img") {
            if (postfix == ".jpeg") {
                return true;
            } else if (postfix == ".png") {
                return true;
            }
            else if (postfix == ".jpg") {
                return true;
            }
            else if (postfix == ".bmp") {
                return true;
            } else if (postfix == ".gif") {
                return true;
            } else {
                return false;
            }
        } else if (type == "video") {
            if (postfix == ".mp4") {
                return true;
            } else if (postfix == ".ogg") {
                return true;
            } else if (postfix == ".webm") {
                return true;
            } else {
                return false;
            }
        } else {
            if (postfix == ".zip") {
                return true;
            } else if (postfix == ".rar") {
                return true;
            } else {
                return false;
            }
        }
        return false;
    }

});
function ajaxFileUpload(val) {
    //var guid = $("#t_guid").text();
    $.ajaxFileUpload
    (
        {
            url: '/ImageManage/UploadImg', //用于文件上传的服务器端请求地址
            type: 'post',
            data: {}, //此参数非常严谨，写错一个引号都不行
            secureuri: false, //一般设置为false
            fileElementId: [val], //文件上传空间的id属性  <input type="file" id="file" name="file" />
            dataType: 'HTML', //返回值类型 一般设置为json
            success: function (data, status)  //服务器成功响应处理函数
            {
                var ext = JSON.parse(data)

                $("#t_" + val).text(ext.imgPath1);
                $("#t_guid").text(ext.guid);
            },
            error: function (data, status, e)//服务器响应失败处理函数
            {
                alert(e);
            }
        }
    )

}
function editrow(target) {
    $.ajax({
        url: "../ImageManage/GetById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                $('#image-Name').val(data.models.Name),
                $('#site-url').val(data.models.SiteURL),
                $("#Id").val(data.models.Id);
                $("#t_Cfile").text(data.models.ImageURL);
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(target) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/ImageManage/Delete',
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
