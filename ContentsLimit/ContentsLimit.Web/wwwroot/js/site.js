// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const DELETE_CATEGORY_FORMATTED_STRING = "Are you sure you want to delete all items in the {0} category?";
const DELETE_ITEM_FORMATTED_STRING = "Are you sure you want to delete {0} from the {1} category?";

String.Format = function (b) {
    var args = arguments;
    return b.replace(/(\{\{\d\}\}|\{\d\})/g, function (b) {
        if (b.substring(0, 2) == "{{") return b;
        var c = parseInt(b.match(/\d/)[0]);
        return args[c + 1]
    })
};

    $(document).ready(function () {
        $(".delete-cat-item").on('click', function (e) {
            var categoryName = $(this).data("category-name");
            var categoryId = $(this).data("category-id");
            if (window.confirm(String.Format(DELETE_CATEGORY_FORMATTED_STRING, categoryName))) {
                var deleteCategoryData = { id: categoryId };
                $.ajax({
                    type: 'POST',
                    url: 'api/v1/category/delete',
                    data: JSON.stringify(deleteCategoryData),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) { location.reload(); }
                });
            }
            else {
                alert("Delete cancelled.");
            }
        });
        $(".delete-item").on('click', function (e) {
            var itemId = $(this).data("item-id");
            var itemName = $(this).data("item-name");
            var categoryName = $(this).data("category-name");
            if (confirm(String.Format(DELETE_ITEM_FORMATTED_STRING, itemName, categoryName))) {
                var deleteItemData = { id: itemId };
                $.ajax({
                    type: 'POST',
                    url: 'api/v1/item/delete',
                    data: JSON.stringify(deleteItemData),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) { location.reload(); }
                });
            }
        });
    });