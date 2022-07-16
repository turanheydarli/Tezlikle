using Microsoft.AspNetCore.Http;

namespace Shared.Extensions;

public static class MediaExtension
{
    public static bool IsSupportedImage(this IFormFile formFile)
    {
        return formFile.ContentType.Contains("image/");
    }

    public static bool IsSupportedTrack(this IFormFile formFile)
    {
        return formFile.ContentType.Contains("audio/") 
               && formFile.Length < 8000000 
               && formFile.FileName.ToLower().Contains("mp3");
    }

    public enum SizeUnits
    {
        Byte, KB, MB, GB, TB, PB, EB, ZB, YB
    }

    public static string ToSize(this Int64 value, SizeUnits unit)
    {
        return (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00");
    }

}