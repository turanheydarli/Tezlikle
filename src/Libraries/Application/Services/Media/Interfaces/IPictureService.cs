using Domain.Catalog;
using Domain.Media;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Media.Interfaces;

public interface IPictureService 
{
    /// <summary>
    /// Deletes a picture
    /// </summary>
    /// <param name="picture">Picture</param>
    void DeletePicture(Picture picture);


    /// <summary>
    /// Gets pictures by product identifier
    /// </summary>
    /// <param name="productId">Product identifier</param>
    /// <param name="recordsToReturn">Number of records to return. 0 if you want to get all items</param>
    /// <returns>Pictures</returns>
    IList<Picture> GetPicturesByProductId(int productId, int recordsToReturn = 0);

    /// <summary>
    /// Inserts a picture
    /// </summary>
    /// <returns>Picture</returns>
    Picture InsertPicture(IFormFile file,  string path, PictureType pictureType);

    /// <summary>
    /// Updates the picture
    /// </summary>
    /// <param name="pictureId">The picture identifier</param>
    /// <param name="pictureBinary">The picture binary</param>
    /// <param name="mimeType">The picture MIME type</param>
    /// <param name="seoFilename">The SEO filename</param>
    /// <param name="altAttribute">"alt" attribute for "img" HTML element</param>
    /// <param name="titleAttribute">"title" attribute for "img" HTML element</param>
    /// <param name="isNew">A value indicating whether the picture is new</param>
    /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
    /// <returns>Picture</returns>
    Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType,
        string seoFilename, string altAttribute = null, string titleAttribute = null,
        bool isNew = true, bool validateBinary = true);

    /// <summary>
    /// Get product picture (for shopping cart and order details pages)
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>Picture</returns>
    Picture GetProductPictures(Product product);

    Picture SetDefaultPicture(string imageName);
}