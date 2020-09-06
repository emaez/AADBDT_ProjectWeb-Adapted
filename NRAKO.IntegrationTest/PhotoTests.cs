using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NRAKOProjektWeb;
using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NRAKO.IntegrationTest
{
    public class PhotoTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        public PhotoTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
        [Fact]
        public async Task TestGetStockItemsAsync()
        {
            // Arrange
            var request = "/Photos";
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task CreateNewPhoto()
        {
            // Arrange
            var request = "/Photos/CreateNewPhoto";
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task CreateNewPhoto_Post()
        {
            // Arrange
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var rootPath = pathToDllDirectory.Split("\\bin\\")[0];
            Image image = Image.FromFile(rootPath + @"\test.jpg");
            var Url = "/Photos/CreateNewPhoto";
            var formContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            formContent.Add(new StreamContent(new MemoryStream(ImageToByteArray(image))), "PhotoFile", "image.png");
            formContent.Add(new StringContent("Integration"), "HashtagsString");
            formContent.Add(new StringContent("This is from integration test"), "Description");
            // Act
            var response = await Client.PostAsync(Url, formContent);
            //var value = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        }
        [Fact]
        public async Task UserPhotos()
        {
            // Arrange
            var request = "/Photos/UserPhotos?id=" + AdminData.Id;
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
