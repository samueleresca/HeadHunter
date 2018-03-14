using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunter.API.Infrastructure;
using HeadHunter.API.Models;
using HeadHunter.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Common.Fixtures
{
    public static class SubjectMockRepository
    {
        public static Mock<ISubjectRepository> GetRepository()
        {
            var subjects = new List<Subject>
            {
              new Subject
              {
                  Id= 1,
                  Name= "Samuele",
                  Surname = "Resca",
                  Phone = "+393470683445",
                  Email = "samuele.resca@gmail.com",
                  Feedbacks =  new List<Feedback>
                  {
                      new Feedback
                      {
                          Id= 1,
                          Title = "mmm..",
                          Description = "Not so bad",
                          DateAdded = DateTime.Today
                      },
                      new Feedback
                      {
                          Id= 2,
                          Title = "mmm..",
                          Description = "Good",
                          DateAdded = DateTime.Today
                      },
                      new Feedback
                      {
                          Id= 3,
                          Title = "mmm..",
                          Description = "Not so bad",
                          DateAdded = DateTime.Today
                      }
                  }
              }
            };

            var repository = new Mock<ISubjectRepository>();
            repository.Setup(x => x.GetAll())
                .Returns(subjects.AsQueryable());

            repository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns((string email) => Task.Run(() => subjects.First(t => t.Email == email)));

            repository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((string name) => Task.Run(() => subjects.First(t => t.Name == name)));

            repository.Setup(x => x.Create(It.IsAny<Subject>()))
                .Callback((Subject s) => subjects.Add(s))
                .Returns((Subject s) => Task.Run(() => s));

            repository.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Subject>()))
                .Callback(
                    (int id, Subject entity) =>
                    {
                        entity.Id = id;
                        subjects[subjects.FindIndex(x => x.Id == id)] = entity;
                    })
                .Returns((int id, Subject label) => Task.Run(() => subjects.Find(t => t.Id == id)));

            repository.Setup(x => x.Delete(It.IsAny<int>()))
                .Callback((int id) => subjects.Where(x => x.Id == id).ToList().ForEach(_ => { _.IsDeleted = true; }))
                .Returns((int id) => Task.Run(() => subjects.Find(t => t.Id == id)));

            return repository;
        }


        public static ISubjectRepository GetInMemoryRepository(string name)
        {
            //Arrange 
            var options = new DbContextOptionsBuilder<FeedbackContext>()
                .UseInMemoryDatabase(name)
                .Options;

            // Run the test against one instance of the context
            var context = new FeedbackContext(options);
            return new SubjectRepository(context);
        }
    }
}