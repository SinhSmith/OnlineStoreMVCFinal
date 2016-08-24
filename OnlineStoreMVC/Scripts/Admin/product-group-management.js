var ProductGroupManagement = {
    deleteGroup: function (id, groupName) {
        var title = "Xóa nhóm sản phẩm";
        var message = "Bạn có muốn xóa " + groupName + " ?";
        MessageBox.showMessageBox(title, message, function () {
            $.ajax({
                url: '/Admin/ProductGroup/Delete',
                data: { id: id },
                type: 'POST',
                success: function () {
                    window.location.replace("/Admin/ProductGroup/Index");
                },
                error: function () {
                    alert("Delete fail!");
                }
            });
        });
    }
}