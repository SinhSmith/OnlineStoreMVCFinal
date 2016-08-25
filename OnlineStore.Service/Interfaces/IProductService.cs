using OnlineStore.Model.Context;
using OnlineStore.Model.MessageModel;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IProductService:IDisposable
    {
        void RefreshAll();
        IEnumerable<ProductSummaryViewModel> GetListProducts();
        IEnumerable<ProductSummaryViewModel> GetProducts(int pageNumber, int pageSize, out int totalItems);

        /// <summary>
        /// Get list brand using for create brand dropdownlist to let user choose brand for product 
        /// </summary>
        /// <returns></returns>
         IEnumerable<ecom_Brands> GetListBrand();
        /// <summary>
        /// Get list product group for create dropdownlist
        /// </summary>
        /// <returns></returns>
        IEnumerable<ecom_ProductGroups> GetListProductGroup();
        /// <summary>
        /// Add new image to database
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
         Nullable<int> AddImage(share_Images image);
        /// <summary>
        /// Add new product to database
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
         bool AddProduct(CreateProductPostRequest newProduct);
        /// <summary>
        /// Add image for exist product 
        /// </summary>
        /// <param name="IdProduct">product id</param>
        /// <param name="photo">new image</param>
         /// <param name="listImages"> return list image after adding finish</param>
         /// <returns>return true if action is success or false action is fail</returns>
         bool AddImageForProduct(int IdProduct, share_Images photo);
        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool UpdateProduct(ProductFullView product);
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ecom_Products GetProductById(int id);
        /// <summary>
        /// Delete image in product
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="imageId">id of image need to delete</param>
        /// <param name="listImages">list images of product after do action</param>
        /// <param name="imagePath">path of deteled image(using for delete image in folder)</param>
        /// <returns>return true if action is success or false if action is fail</returns>
        bool DeleteImage(int productId, int imageId, out share_Images deleteImages);
        /// <summary>
        /// Delete product (set status is Delete)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool DeleteProduct(int id);
        /// <summary>
        /// Set as cover image of product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="imageId"></param>
        /// <param name="listImages"></param>
        /// <returns></returns>
        bool SetAsCoverImage(int productId, int imageId);
        /// <summary>
        /// update image information of product
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="image">image id</param>
        /// <param name="isCoverImage">is cover image of product or not</param>
        /// <param name="listImages">list images of product returned</param>
        /// <returns></returns>
        bool UpdateProductImage(int productId, OnlineStore.Service.Messaging.UpdateProductImage image, bool isCoverImage);
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<ecom_Categories> GetListCategory();
    }
}
