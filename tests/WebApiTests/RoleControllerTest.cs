using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using MS.Common.Extensions;
using MS.Entities;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApiTests
{
    public class RoleControllerTest
    {
        const string _testUrl = "/role/";
        const string _mediaType = "application/json";
        readonly Encoding _encoding = Encoding.UTF8;

        [Theory]
        [InlineData(1222538617050763264)]
        public async Task Delete_Id_ReturnResult(long id)
        {
            //arrange
            string url = $"{_testUrl}?id={id.ToString()}";// url:  /role/?id=11111111
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            //act
            var response = await host.GetTestClient().DeleteAsync(url);//调用Delete接口
            var result = (await response.Content.ReadAsStringAsync()).GetDeserializeObject<ExecuteResult>();//获得返回结果并反序列化

            //assert
            Assert.Equal(result.IsSucceed, string.IsNullOrWhiteSpace(result.Message));
        }
        [Fact]
        public async Task Post_CreateRole_ReturnTrue()
        {
            //arrange
            RoleViewModel viewModel = new RoleViewModel
            {
                Name = "RoleForPostTest",
                DisplayName = "RoleForPostTest"
            };
            StringContent content = new StringContent(viewModel.ToJsonString(), _encoding, _mediaType);//定义post传递的参数、编码和类型
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            //act
            var response = await host.GetTestClient().PostAsync(_testUrl, content); //调用Post接口
            var result = (await response.Content.ReadAsStringAsync()).GetDeserializeObject<ExecuteResult<Role>>();//获得返回结果并反序列化

            //assert
            Assert.True(result.IsSucceed);

            //测完把添加的删除
            await Delete_Id_ReturnResult(result.Result.Id);
        }

        [Fact]
        public async Task Put_UpdateRole_ReturnTrue()
        {
            //arrange
            RoleViewModel viewModel = new RoleViewModel
            {
                Name = "RoleForPutTest",
                DisplayName = "RoleForPutTest"
            };
            StringContent content = new StringContent(viewModel.ToJsonString(), _encoding, _mediaType);//定义put传递的参数、编码和类型
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer
            var response = await host.GetTestClient().PostAsync(_testUrl, content);//先添加一个用于更新测试 

            viewModel.Id = (await response.Content.ReadAsStringAsync()).GetDeserializeObject<ExecuteResult<Role>>().Result.Id;
            content = new StringContent(viewModel.ToJsonString(), _encoding, _mediaType);

            //act
            response = await host.GetTestClient().PutAsync(_testUrl, content);
            var result = (await response.Content.ReadAsStringAsync()).GetDeserializeObject<ExecuteResult>();

            //assert
            Assert.True(result.IsSucceed);

            //测完把添加的删除
            await Delete_Id_ReturnResult(viewModel.Id);
        }
    }
}