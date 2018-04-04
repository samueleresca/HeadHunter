using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Fixtures;
using HeadHunter.API.Controllers;
using HeadHunter.API.Infrastructure.Requests.Service;
using HeadHunter.API.Infrastructure.Responses;
using HeadHunter.API.Models;
using HeadHunter.API.Repositories;
using HeadHunter.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HeadHunter.API.Tests.Controllers
{
    public class ServiceControllerTests : IClassFixture<TestFixture<Startup>>
    {
        public ServiceControllerTests(TestFixture<Startup> fixture)
        {
            SubjectRepository = SubjectMockRepository.GetRepository();

            var imapper = (IMapper)fixture.Server.Host.Services.GetService(typeof(IMapper));
            var ilogger =
                (ILogger<SubjectController>)fixture.Server.Host.Services.GetService(typeof(ILogger<SubjectController>));
            var srv = new SubjectService(SubjectRepository.Object);

            //SERVICES CONFIGURATIONS
            _sut = new SubjectController(imapper, ilogger, srv)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            _sut.ControllerContext.HttpContext.Request.Scheme = "http";
            _sut.ControllerContext.HttpContext.Request.Host = new HostString("fakehost", 4000);
        }

        private Mock<ISubjectRepository> SubjectRepository { get; }

        private SubjectController _sut { get; }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com")]
        public void GetAll_Should_Return_Correct_Value(string name, string surname, string email)
        {
            //Act
            var result = _sut.Get() as OkObjectResult;
            // Assert
            SubjectRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(((List<SubjectResponseModel>)result.Value).First().Name, name);
            Assert.Equal(((List<SubjectResponseModel>)result.Value).First().Surname, surname);
            Assert.Equal(((List<SubjectResponseModel>)result.Value).First().Email, email);
        }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public async Task GetById_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.GetById(id) as OkObjectResult;
            // Assert
            SubjectRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(((SubjectResponseModel)result.Value).Name, name);
            Assert.Equal(((SubjectResponseModel)result.Value).Surname, surname);
            Assert.Equal(((SubjectResponseModel)result.Value).Id, id);
        }


        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public async Task GetByEmail_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.GetByEmail(email) as OkObjectResult;
            // Assert
            SubjectRepository.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once);
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(((SubjectResponseModel)result.Value).Name, name);
            Assert.Equal(((SubjectResponseModel)result.Value).Surname, surname);
            Assert.Equal(((SubjectResponseModel)result.Value).Id, id);
        }


        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com")]
        public async Task Insert_Should_Return_Added_Value(string name, string surname, string email)
        {
            //Assert
            var entity = new CreateSubjectRequest { Name = name, Surname = surname, Email = email };
            //Act
            var result = await _sut.Create(entity) as CreatedResult;
            // Assert
            SubjectRepository.Verify(x => x.Create(It.IsAny<Subject>()), Times.Once);

            var model = (SubjectResponseModel)result.Value;
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.Surname, entity.Surname);
            Assert.Equal(model.Email, entity.Email);
        }

        [Theory]
        [InlineData("Sem", "Test", "email", 1)]
        public async Task Update_Should_Return_Updated_Value(string name, string surname, string email, int id)
        {
            //Assert
            var entity = new UpdateSubjectRequest { Name = name , Surname = surname, Email = email };
            //Act
            var result = await _sut.Update(id, entity) as OkObjectResult;
            // Assert
            SubjectRepository.Verify(x => x.Update(It.IsAny<int>(),It.IsAny<Subject>()), Times.Once);

            var model = (SubjectResponseModel)result.Value;
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.Surname, entity.Surname);
            Assert.Equal(model.Email, entity.Email);
        }

        [Theory]
        [InlineData("Sem", "Test", "email", 1)]
        public async Task Delete_Should_Return_IsDeleted_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.Delete(id) as OkObjectResult;
            // Assert
            SubjectRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);

            var model = (SubjectResponseModel)result.Value;
            Assert.True(model.IsDeleted);
        }

    }
}