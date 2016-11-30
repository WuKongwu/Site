var editor1, editor2

function editSub(target) {
    //
    $.ajax({
        url: "../SubPage/GetSubPageById",
        type: 'POST',
        data: { id: target },
        success: function (data) {
            if (data.success) {
                var DevelopmentToolPage = data.models.rows[0].DevelopmentToolPage;
                var TemplatePage = data.models.rows[0].TemplatePage;
                var Id = data.models.rows[0].Id;
                editor1.html(DevelopmentToolPage);
                editor2.html(TemplatePage);
                $("#Id").val(Id),
                $('#edit_sub').window('open');
            }
        },
    });
   
}

$(function () {

    $("#edit_sub").window('close');
    //css 
    $("#userInfoForm>div ").css("height", 35);
    //加载表格
    $('#usergrid').datagrid({
        url: '/SubPage/SearchTemplate',
        singleSelect: false,
        queryParams: { name: '' },
        pagination: false,
        ContentType: "application/json",
        pageSize: 5,//每页显示的记录条数，默认为10 
        pageList: [5, 10, 15],//可以设置每页记录条数的列表 
        columns: [[
            { field: 'DevelopmentToolPage', title: '开发工具模板', width: 400, align: 'left' },
            { field: 'TemplatePage', title: '毕业论文模板', width: 400, align: 'left' },
            {
                field: 'action', title: '操作', width: 80, align: 'center',
                formatter: function (value, row, index) {
                    var e = '<a href="#" onclick="editSub(' + row.Id + ')">修改</a> ';
                    return e;
                }
            }
        ]]
    });

    KindEditor.ready(function (K) {

         editor1 = K.create('textarea[name="DevelopmentToolPage"]', {

            allowFileManager: false,
            id: "DevelopmentToolPage",
            width: "100%", //编辑器的宽度为70%
            height: "200px", //编辑器的高度为100px
            filterMode: false, //不会过滤HTML代码
            resizeMode: 1, //编辑器只能调整高度
            items: [
        'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
        'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
        'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
        'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
        'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'link', 'unlink', '|', 'about'
            ]
        });
         editor2 = K.create('textarea[name="TemplatePage"]', {

            allowFileManager: false,
            id: "DevelopmentToolPage",
            width: "100%", //编辑器的宽度为70%
            height: "200px", //编辑器的高度为100px
            filterMode: false, //不会过滤HTML代码
            resizeMode: 1, //编辑器只能调整高度
            items: [
        'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
        'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
        'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
        'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
        'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'link', 'unlink', '|', 'about'
            ]
        });
    });
    function HTMLEncode(text) {
        text = text.replace(/&/g, "&");
        text = text.replace(/</g, "'<'");
        text = text.replace(/>/g, "'>'");
        return text;
    }

    $('#submit').click(function (e) {
        var param = {
            Id: $("#Id").val(),
            DevelopmentToolPage: HTMLEncode(editor1.html()),
            TemplatePage: HTMLEncode(editor2.html()),
        };
        $.ajax({
            url: "../SubPage/Save",
            type: 'POST',
            data: param,
            success: function (data) {
                if (data.success) {
                    $("#usergrid").datagrid('reload');
                    $('#edit_sub').window('close');
                }
            }
        });
    });
});




