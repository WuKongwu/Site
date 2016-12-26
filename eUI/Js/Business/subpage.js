var editor1, editor2, editor3


$(function () {



    KindEditor.ready(function (K) {

        editor1 = K.create('textarea[name="payguide"]', {

            allowFileManager: false,
            id: "payguide",
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
        editor2 = K.create('textarea[name="creditguarantee"]', {

            allowFileManager: false,
            id: "creditguarantee",
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
        editor3 = K.create('textarea[name="aboutus"]', {

            allowFileManager: false,
            id: "aboutus",
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

});




