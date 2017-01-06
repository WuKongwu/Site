$(function () {
    $("#userInfoForm>div ").css("height", 35);
    //加载表格 
    $('#papergrid').datagrid({

        url: '/Menu/SearchMenu',
        singleSelect: true,
        queryParams: { name: '' },
        pagination: true,
        ContentType: "application/json",
        pageNumber: 1,
        pageSize: 20,//每页显示的记录条数，默认为10 
        pageList: [20, 50, 100],//可以设置每页记录条数的列表 
        columns: [[
             { field: 'MenuName', title: '菜单名称', width: '10%', align: 'center' },
             {
                 field: 'Type', title: '菜单类型', width: '10%', align: 'center', formatter: function (value, row, index) {
                     return Display(row.Type);
                 }
             },
            { field: 'ChildImageURL', title: '图片连接地址', width: '30%', align: 'center' },
              { field: 'OrderIndex', title: '菜单顺序', width: '10%', align: 'center' },
            {
                field: 'action', title: '操作', width: '20%', align: 'center',
                formatter: function (value, row, index) {
                    var e = '<a href="#" onclick="editrow(' + row.Id + ')">修改</a> ';
                    var d = '<a href="#" onclick="deleterow(' + '\'' + row.Id + '\'' + ',' + '\'' + row.ImageURL + '\'' + ')">删除</a>';
                    return e + d;
                }
            }

        ]],

        onBeforeRefresh: function () {
            return true;
        },
        onClickCell: function (rowIndex, field, value) {
            //如果点击的是商品列.弹出窗口.
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
    $("#menu-Type").change(function () {
        var div = $("#upload-div");
        if (this.value == 1) {
            $("#upload-div").show();
        } else {
            $("#upload-div").hide();
        }
    });
    $('#dlg').window('close');
    $('#submit').click(function (e) {
        var param = {
            Id: $("#Id").val(),
            MenuName: $("#menu-Name").val(),
            Type: $("#menu-Type").val(),
            ChildImageUrl: $("#t_Cfile").text(),
            OrderIndex: $("#menu-Index").val()
        };
        $.ajax({
            url: "../Menu/Save",
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
        var name = $(file).val();
        var point = name.lastIndexOf(".");

        var postfix = name.substr(point);
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
    var oldFileName = $("#t_Cfile").html();
    $.ajaxFileUpload
    (
        {
            url: '/Menu/UploadImg', //用于文件上传的服务器端请求地址
            type: 'post',
            data: { oldFileName: oldFileName }, //此参数非常严谨，写错一个引号都不行
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
        url: "../Menu/GetMenuById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                $('#menu-Name').val(data.models.MenuName);
                $('#menu-Type').val(data.models.Type);
                $("#Id").val(data.models.Id);
                $("#t_Cfile").text(data.models.ChildImageURL);
                $("#menu-Index").val(data.models.OrderIndex);
                if (data.models.Type == 1) {
                    $("#upload-div").show();
                } else {
                    $("#upload-div").hide();
                }
            }
        },
    });
    $('#dlg').window('open');
}
function deleterow(id, fileName) {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            $.ajax({
                url: '/Menu/Delete',
                data: { id: id, fileName: fileName },
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
function Display(num) {
    var showText = "";
    switch (num) {
        case 0:
            showText = "首页菜单";
            break;
        case 1:
            showText = "子节点菜单";
            break;
        default:
            showText = "";
            break;
    }
    return showText;
}