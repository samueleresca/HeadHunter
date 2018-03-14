//using System.Collections.Generic;
//using System.Net;
//using System.Threading.Tasks;
//using AutoMapper;
//using Common.Fixtures;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using ToggleSys.API.Controllers;
//using ToggleSys.API.Models.Requests.Service;
//using ToggleSys.API.Models.Responses;
//using ToggleSys.Data.Models;
//using ToggleSys.Data.Repositories;
//using ToggleSys.Services.Providers;
//using Xunit;

//namespace ToggleSys.API.Tests.Controllers
//{
//    public class ServiceControllerTests : IClassFixture<TestFixture<Startup>>
//    {
//        public ServiceControllerTests(TestFixture<Startup> fixture)
//        {
//            ToggleRepository = ToggleMockRepository.GetRepository();
//            ServiceRepository = ServiceMockRepository.GetRepository();

//            var imapper = (IMapper)fixture.Server.Host.Services.GetService(typeof(IMapper));
//            var ilogger =
//                (ILogger<ServiceController>)fixture.Server.Host.Services.GetService(typeof(ILogger<ServiceController>));
//            var srv = new ServiceProvider(ServiceRepository.Object);

//            //SERVICES CONFIGURATIONS
//            _sut = new ServiceController(imapper, ilogger, srv)
//            {
//                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
//            };
//            _sut.ControllerContext.HttpContext.Request.Scheme = "http";
//            _sut.ControllerContext.HttpContext.Request.Host = new HostString("fakehost", 4000);
//        }

//        private Mock<IToggleRepository> ToggleRepository { get; }
//        private Mock<IServiceRepository> ServiceRepository { get; }

//        private ServiceController _sut { get; }

//        [Theory]
//        [InlineData("Service_Test", "v1", "http://serivce1//subscribeendpoint")]
//        public async Task GetById_Should_Return_Correct_Value(string name, string version, string subscribeEndpoint)
//        {
//            //Act
//            var result = await _sut.Get(name, version) as OkObjectResult;
//            // Assert
//            ServiceRepository.Verify(x => x.Find(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
//            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
//            Assert.Equal(((ServiceDetailResponseModel)result.Value).Name, name);
//            Assert.Equal(((ServiceDetailResponseModel)result.Value).Version, version);
//            Assert.Equal(((ServiceDetailResponseModel)result.Value).SubscriptionEndpoint, subscribeEndpoint);
//        }


//        [Theory]
//        [InlineData("Test", "v3", "https://service1/subscribeendpoint")]
//        public async Task Insert_Should_Return_Added_Value(string name, string version, string subscribeEndpoint)
//        {
//            //Assert
//            var entity = new CreateServiceRequest { Name = name, Version = version, SubscriptionEndpoint = subscribeEndpoint };
//            //Act
//            var result = await _sut.Create(entity) as CreatedResult;
//            // Assert
//            ServiceRepository.Verify(x => x.Add(It.IsAny<Service>()), Times.Once);

//            var model = (ServiceResponseModel)result.Value;
//            Assert.Equal(model.Name, entity.Name);
//            Assert.Equal(model.Version, entity.Version);
//            Assert.Equal(model.SubscriptionEndpoint, entity.SubscriptionEndpoint);
//        }

//        [Fact]
//        public async Task Get_Should_Return_Values()
//        {
//            //Act
//            var result = await _sut.Get() as OkObjectResult;

//            //Assert
//            ServiceRepository.Verify(x => x.Get(), Times.Once);
//            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
//            Assert.Single((IEnumerable<ServiceResponseModel>)result.Value);
//        }
//    }
//}