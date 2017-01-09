var timeFunction;
function pollingStates() {
   
    timeFunction = window.setInterval(CheckPayStates, 3000);

}
function CheckPayStates() {
    var url = $("body").data("website") + "Pay/GetPaperDownloadUrl";
    var orderNumber = $("#orderNumber").val();
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        cache: false,
        headers: { "Cache-Control": "no-cache" },
        data: { orderNumber: orderNumber },
        success: function (data) {
            if (data.success == true) {
                clearInterval(timeFunction);
                $(".modal-content").css("display", "none");
                $(".modal-backdrop").css("display", "none");
                $("#wx_pay_pop").css("display", "none");
                $(".paper-pay").css("display", "none");
                $("#paperdownload_div").css("display", "block");
               
            } else if (data.success == false) {
                alert(data.data)
            }

        },
        error: function (e) {
        }
    });

}
function DownloadPaperSource() {
    var orderNumber = $("#orderNumber").val();
    var newWin = window.open('', '_blank');
    alert(getRootPath() + "Pay/DownLoadPaper?orderNumber=" + orderNumber)
    newWin.location.href = getRootPath() + "/Pay/DownLoadPaper?orderNumber=" + orderNumber;
}