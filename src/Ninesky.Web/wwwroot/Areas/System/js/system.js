
function InitTree(tree,data,url,setting)
{
$.post(url, data, function (result) {
        categoryTree = $.fn.zTree.init($(tree),setting , result);
    }, "json");
}