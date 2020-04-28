using System.IO;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Checkout.Tests.Files
{
    public class GetFileTests : IClassFixture<ApiTestFixture>
    {
        private readonly ICheckoutApi _api;

        public GetFileTests(ApiTestFixture fixture, ITestOutputHelper outputHelper)
        {
            fixture.CaptureLogsInTestOutput(outputHelper);
            _api = fixture.Api;
        }

        [Fact]
        public async Task CanGetFile()
        {
            // upload a file first
            var pathToFile = @"test_file.png";
            var fileInfo = new FileInfo(fileName: pathToFile);
            var uploadFileResponse = await _api.Files.UploadFileAsync(pathToFile: fileInfo.FullName, purpose: "dispute_evidence");

            uploadFileResponse.ShouldNotBeNull();

            // use returned fileID to try and get file details
            var getFileResponse = await _api.Files.GetFileAsync(id: uploadFileResponse.Id);

            getFileResponse.ShouldNotBeNull();
            getFileResponse.Id.ShouldBe(uploadFileResponse.Id);
            getFileResponse.Filename.ShouldBe(fileInfo.Name);
            getFileResponse.Size.ShouldBe((int)fileInfo.Length);
        }
    }
}
