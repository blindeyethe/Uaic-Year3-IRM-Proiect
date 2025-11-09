using IRM.PhotoSystem;

namespace IRM.AlbumSystem
{
    public static class PhotoDataExtensions
    {
        public static string GetImageName(this PhotoData photoData) =>
            photoData.PhotoObjects + ".png";
    }
}