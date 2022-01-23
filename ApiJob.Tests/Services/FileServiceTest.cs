using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Services;
using Moq;
using NUnit.Framework;
using ApiUnitest.ApiJob.Api.Repository.Repositories;
using FluentAssertions;
using System.IO;
using System;

namespace ApiJobUnitests.ApiJob.Tests.Services
{
    public class FileServiceTest
    {
        private readonly IFileService _fileService;
        private readonly Mock<IJobOpportunityRepository> _jobOpportunityRepository;
        public FileServiceTest()
        {
            _jobOpportunityRepository = new Mock<IJobOpportunityRepository>(MockBehavior.Strict);
            _fileService = new FileService(_jobOpportunityRepository.Object);
        }

        [Test, Order(1)]
        public async Task GetFile_ShouldReturnStreamWriter()
        {
            var result = _fileService.GetExportFile();
            var reader = new StreamReader(result.Result);
            var content = reader.ReadToEnd();

            result.Result.Length.Should().NotBe(0);
            content.Should().Be("Document01.txt");
        }

        [Test, Order(2)]
        public async Task CreateFile_ShouldReturnStreamWriter()
        {
            string fileName = "Document01.txt";

            var result = _fileService.CreateFile(fileName);
            result.Should().NotBeNull();
        }

        [Test, Order(3)]
        public async Task CreateFileStreamWriter_ShouldReturnStreamWriter()
        {
            string fileName = "Document01.txt";
            var now = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            var result = _fileService.CreateFileStreamWriter(now, fileName);
            result.Should().NotBeNull();
        }
    }
}