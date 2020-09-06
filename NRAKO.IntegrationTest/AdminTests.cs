using NRAKOProjektWeb;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NRAKO.IntegrationTest
{
    public class AdminTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;
        public AdminTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
        [Fact]
        public async Task Index()
        {
            // Arrange
            var request = "/Admin";
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task ManageUsers()
        {
            // Arrange
            var request = "/Admin/ManageUsers";
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task EditUser()
        {
            // Arrange
            var request = "/Admin/EditUser?id=" + AdminData.Id;
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task EditUser_Post()
        {
            // Arrange
            var Url = "/Admin/EditUser";
            var formContent = new MultipartFormDataContent("Data----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            formContent.Add(new StringContent(AdminData.Id), "Id");
            formContent.Add(new StringContent(AdminData.Name), "Username");
            formContent.Add(new StringContent(AdminData.Email), "Email");
            formContent.Add(new StringContent("true"), "Enabled");
            formContent.Add(new StringContent("1"), "SubscriptionModelId");
            // Act
            var response = await Client.PostAsync(Url, formContent);

            // Assert
            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        }
        [Fact]
        public async Task UserStatiscticsAndActions()
        {
            // Arrange
            var request = "/Admin/UserStatiscticsAndActions?id=" + AdminData.Id;
            // Act
            var response = await Client.GetAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
