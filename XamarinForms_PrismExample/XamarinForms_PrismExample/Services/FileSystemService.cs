using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms_PrismExample.Services
{
    /// <summary>
    /// Cross-platform File System Manager Service using PCLStorage Xamarin Plugin
    /// </summary>
    public class FileSystemService : IFileSystemService
    {
        /// <summary>
        /// Access the file system for the current platform
        /// </summary>
        IFileSystem _fileSystem = FileSystem.Current;

        /// <summary>
        /// Local Storage Root Folder reference
        /// </summary>
        private IFolder _rootFolder;
        public IFolder RootFolder
        {
            get { return _rootFolder; }
        }

        // Constructor
        public FileSystemService()
        {
            // Get the root directory of the file system for our application.
            _rootFolder = _fileSystem.LocalStorage;
        }

        /// <summary>
        /// Checks whether a folder or file exists at the given location
        /// </summary>
        /// <param name="folder">Folder reference</param>
        /// <param name="name">File or Directory name</param>
        /// <returns><c>true</c> si existe el fichero o directorio; <c>false</c> en caso contrario.</returns>
        public async Task<bool> ExistsFileOrDirectoryAsync(IFolder folder, string name)
        {
            try
            {
                await folder.CheckExistsAsync(name);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a folder into specific directory, if one doesn't already exist
        /// </summary>
        /// <param name="path">New folder path</param>
        /// <returns>Referencia al directorio creado</returns>
        public async Task<IFolder> CreateDirectoryAsync(IFolder folder, string folderName)
        {
            return await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Create a file into specific folder
        /// </summary>
        /// <param name="folder">Folder reference</param>
        /// <param name="filename">File name</param>
        /// <returns>Referencia al fichero creado</returns>
        public async Task<IFile> CreateFileAsync(IFolder folder, string filename)
        {
            return await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Read string content from file
        /// </summary>
        /// <param name="file">File reference</param>
        /// <returns>Contenido del fichero</returns>
        public async Task<string> ReadFileAsync(IFile file)
        {
            return await file.ReadAllTextAsync();
        }

        /// <summary>
        /// Write string content into specific file
        /// </summary>
        /// <param name="file">File reference</param>
        /// <param name="file_content">String content</param>
        /// <returns><c>true</c> si el contenido se ha escrito correctamente en el fichero; <c>false</c> en caso contrario.</returns>
        public async Task<bool> WriteFileAsync(IFile file, string fileContent)
        {
            try
            {
                await file.WriteAllTextAsync(fileContent);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete a file
        /// </summary>
        /// <param name="file">File reference</param>
        /// <returns><c>true</c> si el fichero se ha borrado correctamente; <c>false</c> en caso contrario.</returns>
        public async Task<bool> DeleteFileAsync(IFile file)
        {
            try
            {
                await file.DeleteAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
