using System.Linq;
using System.Threading.Tasks;
using Common.Fixtures;
using HeadHunter.API.Repositories;
using HeadHunter.API.Services;
using Moq;
using Xunit;

namespace HeadHunter.API.Tests.Services
{
   public class SubjectServicesTests
    {
        private Mock<ISubjectRepository> SubjectRepository { get; }
        public SubjectServicesTests()
        {
            SubjectRepository = SubjectMockRepository.GetRepository();
            _sut = new SubjectService(SubjectRepository.Object);
        }

        private ISubjectService _sut { get; }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public void  GetAll_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result =  _sut.GetAll();
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
    }
}
