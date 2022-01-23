using System.IO;
using System.Text;
using System.Threading.Tasks;
using ApiUnitest.ApiJob.Api.Repository.Repositories;

namespace ApiJobUnitests.ApiJob.Api.Services
{
    public class FileService : IFileService
    {
        private readonly IJobOpportunityRepository _jobOpportunityRepository;
        public FileService(IJobOpportunityRepository jobOpportunityRepository)
        {
            _jobOpportunityRepository = jobOpportunityRepository;
        }

        public async Task<Stream> GetExportFile()
        {
            string fileName = "Document01.txt";
            var byteArray = Encoding.ASCII.GetBytes(fileName);
            var file = new MemoryStream(byteArray);
            //file.Close();

            return file;
        }

        public async Task<StreamWriter> CreateFile(string fileName)
        {
            string rutaCompleta = @".\Files\" + fileName;
            string text = "test 01 writee....";

            StreamWriter file = new StreamWriter(rutaCompleta, true);
            file.WriteLine(text);

            return file;
        }

        public async Task<StreamWriter> CreateFileStreamWriter(int now, string fileName)
        {
            var users = _jobOpportunityRepository.GetUsersList();
            string archivo = Path.Combine(@"..\Files\" + fileName, now + ".txt");

            StreamWriter outfile = new StreamWriter(archivo);
            outfile.WriteLine(users);

            return outfile;
        }
    }
}