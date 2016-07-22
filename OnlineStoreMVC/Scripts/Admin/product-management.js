var Mode = {
    Display: 0,
    Edit: 1
};
var ProductManagement = ProductManagement || {};

ProductManagement = {
    init: function () {
        // support ajax to upload images
        window.addEventListener("submit", function (e) {
            var form = e.target;
            if (form.getAttribute("enctype") === "multipart/form-data") {
                if (form.dataset.ajax) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    var xhr = new XMLHttpRequest();
                    xhr.open(form.method, form.action);
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            if (form.dataset.ajaxUpdate) {
                                var updateTarget = document.querySelector(form.dataset.ajaxUpdate);
                                if (updateTarget) {
                                    updateTarget.innerHTML = xhr.responseText;
                                }
                            }
                        }
                    };
                    xhr.send(new FormData(form));
                }
            }
        }, true);

        // install CKEditor
        CKEDITOR.replace('Description2');
        // install chosen control
        $(".chzn-select").chosen();
    },
    requestDeleteProductImage: function (productId, imageId) {
        /// <summary>
        /// Delete a image from list images of product
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $.ajax({
            url: "/Admin/Product/DeleteImage",
            dataType: "html",
            data: { productId: productId, imageId: imageId },
            success: function (result) {
                alert("delete image successful!!!");
                $("#EditProduct_ListProductImages").empty();
                $("#EditProduct_ListProductImages").html(result);
            },
            error: function (result) {
                alert("delete image error");
            }
        });
    },
    deleteProduct: function (id, productName) {
        var title = "Xóa sản phẩm";
        var message = "Bạn có muốn xóa " + productName + " ?";
        MessageBox.showMessageBox(title, message, function () {
            $.ajax({
                url: '/Admin/Product/Delete',
                data: { id: id },
                type: 'POST',
                success: function () {
                    window.location.replace("/Admin/Product/Index");
                },
                error: function () {
                    alert("Delete fail!");
                }
            });
        });
    },
    editProductImage: function (e, imageId) {
        this.changeMode(imageId, Mode.Edit);
    },
    updateProductImage: function (e, productId, imageId) {
        // Call function request to server to update data
        var name = $("#EditProduct_ListProductImages tr.edit-mode[data-id='" + imageId + "'] .txt-imagename").val();
        var isActive = $("#EditProduct_ListProductImages tr.edit-mode[data-id='" + imageId + "'] .ckb-isactive").is(":checked");
        var isCoverImage = $("#EditProduct_ListProductImages tr.edit-mode[data-id='" + imageId + "'] .ckb-iscoverimage").is(":checked");
        var request = {
            ProductId: productId,
            ImageId : imageId,
            Name: name,
            IsActive: isActive,
            IsCoverImage: isCoverImage
        }
        $.ajax({
            url: '/Admin/Product/UpdateProductImage',
            data: { request: request },
            type: 'POST',
            success: function (result) {
                $("#EditProduct_ListProductImages").empty();
                $("#EditProduct_ListProductImages").append(result);
                //window.location.replace("/Admin/Product/Index");
            },
            error: function () {
                alert("Update image product fail!");
            }
        })
        // Change to Display mode
        this.changeMode(imageId, Mode.Display);
    },
    cancelUpdateProductImage: function (e, imageId) {

        // Change to Display mode
        this.changeMode(imageId, Mode.Display);
    },
    changeMode: function (imageId,mode) {
        switch (mode) {
            case Mode.Edit: {
                $("#EditProduct_ListProductImages tr.display-mode[data-id='" + imageId + "']").removeClass("hidden").addClass("hidden");
                $("#EditProduct_ListProductImages tr.edit-mode[data-id='" + imageId + "']").removeClass("hidden");
                break;
            }
            case Mode.Display: {
                $("#EditProduct_ListProductImages tr.display-mode[data-id='" + imageId + "']").removeClass("hidden");
                $("#EditProduct_ListProductImages tr.edit-mode[data-id='" + imageId + "']").removeClass("hidden").addClass("hidden");
                break;
            }
        }
    }
};