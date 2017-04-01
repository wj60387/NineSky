//下拉栏目设置
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
        url: $("#ParentId").attr("data-ns-url")
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
};


$(document).ready(function () {
    dropdownCategoryTree = $.fn.zTree.init($("#ParentId-dropdown"), setting);
    dropdownCategoryTree.addNodes(null, { id: 0, name: "无" });
    $("#ParentId-text").click(function () {
        $("#ParentId-dropdown").show();
    });
    $("#ParentId-btn").click(function () {
        $("#ParentId-dropdown").show();
    });

    $('#ContentModuleId').on('changed.bs.select', function (e) {
        toggleContent();
    });
});