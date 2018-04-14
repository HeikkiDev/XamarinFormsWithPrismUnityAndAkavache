using System.Threading.Tasks;
using PCLStorage;

namespace XamarinForms_PrismExample.Services
{
    public interface IFileSystemService
    {
        IFolder RootFolder { get; }

        Task<bool> ExistsFileOrDirectoryAsync(IFolder folder, string name);
        Task<IFolder> CreateDirectoryAsync(IFolder folder, string folderName);
        Task<IFile> CreateFileAsync(IFolder folder, string filename);
        Task<bool> DeleteFileAsync(IFile file);
        Task<string> ReadFileAsync(IFile file);
        Task<bool> WriteFileAsync(IFile file, string fileContent);
    }
}