var DetailProductManagement = {
    init: function () {
        this.bindEvents();
        this.initZoomImageControl();
    },
    bindEvents: function () {
        $("#thumbs_list_frame li").unbind("click").bind("click", function () {
            var imagePath = $(this).data("imagepath") || "/Content/Images/no-image.png";
            
            //var fileName = imagePath.replace(/\.[^.$]+$/, '');
            //var extensions = imagePath.match(/\.[^.$]+$/);
            //var fileExt = (extensions === null) ? "" : extensions[0];
            //var smallImagePath = fileName + "_small"+fileExt;
            $("#CoverImage").attr("src", imagePath);
            $("#CoverImage").attr("data-zoom-image",  imagePath);
            //$(".zoom-image-area").css("background", "url('" + imagePath + "') no-repeat;")
        });

        // search control
        $("#SearchProduct_Btn").unbind("click").bind("click", function (e) {
            searchProduct();
        });
        $("#tmsearch #tm_search_query").unbind("keypress").bind("keypress", function (e) {
            if (e.keyCode == 13) {
                searchProduct();
            }
        });

        function searchProduct() {
            var searchString = $("#tmsearch #tm_search_query").val();
            window.location.replace("/Product/SearchProduct?searchString=" + searchString);
        }
    },
    initZoomImageControl: function () {
        $('#CoverImage').elevateZoom({
            zoomType: "inner",
            cursor: "crosshair",
            zoomWindowFadeIn: 500,
            zoomWindowFadeOut: 750
        });
    }
}
