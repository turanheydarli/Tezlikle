using Application.Services.Media.Interfaces;
using Domain.Catalog;
using Domain.Media;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.Hosting;

namespace Application.Services.Media;

public class PictureService:IPictureService
{
    private readonly IRepository<Picture> _pictureRepository;
    private readonly IHostEnvironment _environment;

    public PictureService(IRepository<Picture> pictureRepository, IHostEnvironment environment)
    {
        _pictureRepository = pictureRepository;
        _environment = environment;
    }

    public void DeletePicture(Picture picture)
    {
        throw new NotImplementedException();
    }

    public IList<Picture> GetPicturesByProductId(int productId, int recordsToReturn = 0)
    {
        throw new NotImplementedException();
    }

    public Picture InsertPicture(IFormFile file, string path, PictureType pictureType)
    {
        var extension = "jpeg";// Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(_environment.ContentRootPath, "wwwroot", path);
        
        var smallFilePath = $"img_500x_{Guid.NewGuid().ToString()}.{Path.GetExtension(file.Name)}.jpeg";
        var bigFilePath = $"img_1920x_{Guid.NewGuid().ToString()}.{Path.GetExtension(file.Name)}.jpeg";
        
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }
        
        using (var fullSizeStream = File.OpenWrite(Path.Combine(fullPath, bigFilePath)))
        using (var smallStream = File.OpenWrite(Path.Combine(fullPath, smallFilePath)))
        using (var image = Image.Load(file.OpenReadStream()))
        {
            // Save original constrained
            var clone = image.Clone(context => context
                .Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(1920, 1920)
                }));
            clone.SaveAsJpeg(fullSizeStream, new JpegEncoder { Quality = 80 });

            //Save three sizes Cropped:
            var jpegEncoder = new JpegEncoder { Quality = 75 };
            clone = image.Clone(context => context
                .Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Crop,
                    Size = new Size(500, 500)
                }));
            clone.SaveAsJpeg(smallStream, jpegEncoder);

        }

        
        var picture = new Picture()
        {
            ImageDefault = bigFilePath,
            ImageBig = bigFilePath,
            ImageSmall = smallFilePath,
            MimeType = extension,
            CreatedTime = DateTime.UtcNow
        };

        picture = _pictureRepository.Insert(picture);

        return picture;
    }

    public Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType, string seoFilename,
        string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true)
    {
        throw new NotImplementedException();
    }

    public Picture GetProductPictures(Product product)
    {
        throw new NotImplementedException();
    }

    public Picture SetDefaultPicture(string imageName)
    {
        return _pictureRepository.Get(p => p.ImageDefault == imageName);
    }
}