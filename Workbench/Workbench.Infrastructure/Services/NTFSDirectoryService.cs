using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Workbench.Infrastructure.DI;

namespace Workbench.Infrastructure.Services
{
    public class NTFSDirectoryService : IDirectoryService
    {
        internal static bool VERBOSE { get; set; }

        private readonly ILoggingService _loggingService;

        public NTFSDirectoryService(IDependencyInjectionEngine di)
        {
            di.Inject(ref _loggingService);
        }

        public IEnumerable<FileInfo> GetAllFilesInDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return Enumerable.Empty<FileInfo>();
            return GetAllFilesInDirectory(new DirectoryInfo(directoryPath));
        }
        private IEnumerable<FileInfo> GetAllFilesInDirectory(DirectoryInfo info)
        {
            return info.EnumerateFiles();
        }

        public FileInfo GetFileAtPath(string path)
        {
            if (VERBOSE && !File.Exists(path))
                _loggingService.Warn(string.Format("Unable to find file at requested path: {0}", path));
            return new FileInfo(path);
        }
    }
}
