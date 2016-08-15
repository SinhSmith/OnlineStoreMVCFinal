var Mode = {
    Display: 0,
    Edit: 1
};
var ProductManagement = ProductManagement || {};

ProductManagement = {
    init: function () {
        // support ajax to upload images
        window.addEventListener("submit", function (e) {
            ProductManagement.showSpin();
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
                                    
                                    ProductManagement.hideSpin();
                                }
                            }
                        }
                    };
                    xhr.send(new FormData(form));
                }
            }
        }, true);

        // Init spin
        this.controls.spin = new Spinner({
            lines: 13 // The number of lines to draw
            , length: 28 // The length of each line
            , width: 14 // The line thickness
            , radius: 42 // The radius of the inner circle
            , scale: 1 // Scales overall size of the spinner
            , corners: 1 // Corner roundness (0..1)
            , color: '#000' // #rgb or #rrggbb or array of colors
            , opacity: 0.25 // Opacity of the lines
            , rotate: 0 // The rotation offset
            , direction: 1 // 1: clockwise, -1: counterclockwise
            , speed: 1 // Rounds per second
            , trail: 60 // Afterglow percentage
            , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
            , zIndex: 2e9 // The z-index (defaults to 2000000000)
            , className: 'spinner' // The CSS class to assign to the spinner
            , top: '50%' // Top position relative to parent
            , left: '50%' // Left position relative to parent
            , shadow: true // Whether to render a shadow
            , hwaccel: false // Whether to use hardware acceleration
            , position: 'fixed' // Element positioning
        }).spin();

        // install CKEditor
        CKEDITOR.replace('Description2');
        // install chosen control
        $(".chzn-select").chosen();

        // bind events
        //$("#Btn_UploadImage").unbind("click").bind("click", ProductManagement.upLoadImage)
    },
    controls: {
        spin: null
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
                alert("Xóa ảnh thành công!!!");
                $("#EditProduct_ListProductImages").empty();
                $("#EditProduct_ListProductImages").html(result);
            },
            error: function (result) {
                alert("Xóa ảnh thất bại");
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
                    alert("Xóa ảnh thất bại!");
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
                alert("Cập nhật ảnh thất bại!");
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
    },
    showSpin: function (target) {
        /// <summary>
        /// Create spin control
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>s

        $("#images").append(ProductManagement.controls.spin.spin().el);
    },
    hideSpin: function () {
        /// <summary>
        /// Hide spin control
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        ProductManagement.controls.spin.stop();
    }
};