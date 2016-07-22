var CategoryManagement = {
    deleteCategory: function (id, categoryName) {
        var title = "Xóa danh mục sản phẩm";
        var message = "Bạn có muốn xóa " + categoryName + " ?";
        MessageBox.showMessageBox(title, message, function () {
            $.ajax({
                url: '/Admin/Category/Delete',
                data: { id: id },
                type: 'POST',
                success: function () {
                    window.location.replace("/Admin/Category/Index");
                },
                error: function () {
                    alert("Delete fail!");
                }
            });
        });
    }
}