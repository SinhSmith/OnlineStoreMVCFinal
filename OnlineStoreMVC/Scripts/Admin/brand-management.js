var BrandManagement = {
    deleteBrand: function (id, brandName) {
        var title = "Xóa nhà sản xuất";
        var message = "Bạn có muốn xóa " + brandName + " ?";
        MessageBox.showMessageBox(title, message, function () {
            $.ajax({
                url: '/Admin/Brand/Delete',
                data: { id: id },
                type: 'POST',
                success: function () {
                    window.location.replace("/Admin/Brand/Index");
                },
                error: function () {
                    alert("Delete fail!");
                }
            });
        });
    }
}