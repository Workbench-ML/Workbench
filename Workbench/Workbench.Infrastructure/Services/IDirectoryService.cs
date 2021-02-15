using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Workbench.Infrastructure.Services
{
    public interface IDirectoryService
    {
        IEnumerable<FileInfo> GetAllFilesInDirectory(string directoryPath);

        FileInfo GetFileAtPath(string FilePath);
    }
}
