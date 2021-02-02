using System.Threading.Tasks;

namespace CodeZero.Common
{
    public interface IFileSystem
    {
        Task<bool> SavePicture(string pictureName, string pictureBase64);
    }
}
