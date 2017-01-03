var asidecategorysetting = {
    data: {
        simpleData: {
            enable: true,
            idKey: "id",
            pIdKey: "pId",
            rootPId: 0
        }
    }
};

$(document).ready(function ()
{
    InitTree("#categoryTree", null, $("#categoryTree").attr("data-url"), asidecategorysetting);
});

    function InitTree(tree,data,url,setting)
    {
        $.post(url, data, function (result) {
            categoryTree = $.fn.zTree.init($(tree),setting , result);
        }, "json");
    }