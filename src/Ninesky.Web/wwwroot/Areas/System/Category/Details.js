/// <reference path="~/lib/bootstrap3-dialog/dist/js/bootstrap-dialog.min.js" />

var dropdownCategoryTree;
var setting = {
    data: {
        simpleData: {
            enable: true,
            idKey: "id",
            pIdKey: "pId",
            rootPId: 0
        }
    },
    async: {
        enable: true,
        url: $("#ParentId").attr("data-url")
    },
    callback: {
        onClick: function (event, treeId, treeNode) {
            $("#ParentId").val(treeNode.id);
            $("#ParentId-text").val(treeNode.name);
            $("#ParentId-dropdown").hide();
        },
        onAsyncSuccess: function (event, treeId, treeNode, msg) {
            var node = dropdownCategoryTree.getNodeByParam("id", $('#ParentId').val(), null);
            dropdownCategoryTree.selectNode(node);
            $('#ParentId-text').val(node.name);
        }
    }
}
function toggleContent() {
    if ($("#General_ModuleId").selectpicker('val') == "") {
        $("#General_ContentOrder").empty();
    }
    else {
        $.post($("#General.ContentOrder").attr("data-url"), { id: $('#General_ModuleId').val() }, function (data) {
            if (data != undefined) {
                $.each(data, function (ndex, element) {
                    $("#General_ContentOrder").append("<option value='" + element.order + "'>" + element.name + "</option>");
                })
            }
        }, 'json');

    }
}

$(document).ready(
    function () {
        //栏目树形下拉菜单
        dropdownCategoryTree = $.fn.zTree.init($("#ParentId-dropdown"), setting);
        dropdownCategoryTree.addNodes(null, { id: 0, name: "无" });
        $("#ParentId-text").click(function () {
            $("#ParentId-dropdown").show();
        });
        $("#ParentId-btn").click(function () {
            $("#ParentId-dropdown").show();
        });
        toggleContent();
        $('#General_ModuleId').on('changed.bs.select', function (e) {
            toggleContent();
        });

        //文本编辑器
        var ue = UE.getEditor('Page_Content');

        //删除事件
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
                        $.post($("#btn_del").attr("data-action"), { id: $("#btn_del").attr("data-value") }, function (data) {
                            if (data != undefined) {
                                if (data.succeed == true) {
                                    BootstrapDialog.alert({ title: "成功", message: data.message, callback: function () { location.href = "/System/Category"; } });
                                }
                                else BootstrapDialog.alert({ title: "失败", message: data.message });
                            }
                            else BootstrapDialog.alert({ title: "消息", message: "请求失败" });
                        }, 'json');
                    }
                }
            });
            
        });
    });