using System.Linq;
using System.Threading.Tasks;
using Common.Fixtures;
using HeadHunter.API.Services;
using Xunit;

namespace HeadHunter.API.Tests.Services
{
   public class SubjectServicesTests
    {
        public SubjectServicesTests()
        {
            _sut = new SubjectService(SubjectMockRepository.GetRepository().Object);
        }

        private ISubjectService _sut { get; }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public void  GetAll_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result =  _sut.GetAll();
            //Assert
            Assert.Equal(1, result.Count());

        }

        [Theory]
        [InlineData("Samuele", "Resca", "samuele.resca@gmail.com", 1)]
        public async Task GetById_Should_Return_Correct_Value(string name, string surname, string email, int id)
        {
            //Act
            var result = await _sut.GetById(1);
            //Assert
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
            Assert.Equal(1, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(surname, result.Surname);
            Assert.Equal(email, result.Email);
        }
    }
}
