﻿/// <reference path="~/lib/bootstrap3-dialog/dist/js/bootstrap-dialog.min.js" />
$(document).ready(
    function () {
        //删除
        $("#btn_del").click(function () {
            BootstrapDialog.confirm({
                title: '提示',
                message: '确认删除栏目',
                type: BootstrapDialog.TYPE_DANGER, 
                closable: true,
                draggable: true,
                btnOKLabel: '确定',
                btnCancelLabel: '取消',
                btnOKClass: 'btn-danger',
                callback: function (result) {
                    if (result) {
                        $.post("/System/Category/Delete/", { id: $("#btn_del").attr("data-value") }, function (data) {
                            if (data != undefined)
                            { }
                            else BootstrapDialog.alert("请求失败！");
                        }, 'json');
                    }
                }
            });
            
        });
    });