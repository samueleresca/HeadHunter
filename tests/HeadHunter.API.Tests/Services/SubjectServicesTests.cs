using System.Linq;
using System.Threading.Tasks;
using Common.Fixtures;
using HeadHunter.API.Models;
using HeadHunter.API.Repositories;
using HeadHunter.API.Services;
using Moq;
using Xunit;

namespace HeadHunter.API.Tests.Services
{
    public class SubjectServicesTests
    {
        private ISubjectService _sut { get; }
        private Mock<ISubjectRepository> SubjectRepository { get; }

        public SubjectServicesTests()
        {
            SubjectRepository = SubjectMockRepository.GetRepository();
            _sut = new SubjectService(SubjectRepository.Object);
        }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public void GetAll_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = _sut.GetAll();
            //Assert
            SubjectRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(1, result.Count());

        }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public async Task GetById_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.GetById(1);
            //Assert
            SubjectRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.Equal(1, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(email, result.Email);
        }


        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public async Task GetByEmail_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.GetByEmail(email);
            //Assert
            SubjectRepository.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once);
            Assert.Equal(1, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(email, result.Email);
        }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public async Task Create_Should_Add_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.Create(new Subject { Name = name, Surname = surname, Email = email });
            //Assert
            SubjectRepository.Verify(x => x.Create(It.IsAny<Subject>()), Times.Once);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(email, result.Email);
        }

        [Theory]
        [InlineData("Samuele", "Resca", "test@gmail.com", 1)]
        public async Task Update_Should_Modify_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.Update(id, new Subject { Name = name, Surname = surname, Email = email });
            //Assert
            SubjectRepository.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<Subject>()), Times.Once);
            Assert.Equal(1, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(email, result.Email);
        }

        [Theory]
        [InlineData("Samuele", "Resca", "test@gmail.com", 1)]
        public async Task Delete_Should_Modify_IsDeleted_Field(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.Delete(id);
            //Assert
            SubjectRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
            Assert.True(result.IsDeleted);
        }
    }
}
