using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;

namespace ApiJobUnitests.ApiJob.Api.Services
{
    public interface IFileService
    {
        Task<Stream> GetExportFile();
        Task<StreamWriter> CreateFile(string fileName);
        Task<StreamWriter> CreateFileStreamWriter(int now, string fileName);
    }
}