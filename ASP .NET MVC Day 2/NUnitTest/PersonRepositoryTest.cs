using ASP_.NET_MVC_Day_2.Enum;
using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.Data;
using ASP_.NET_MVC_Day_2.Repository;
using Moq;
using NUnit.Framework;

namespace ASP_.NET_MVC_Day_2.NUnitTest
{
    [TestFixture]
    public class PersonRepositoryTest
    {
        private Mock<IAppContext> _mockContext;
        private PersonRepository _repository;
        private List<Person> _mockPersons;

        [SetUp]
        public void SetUp()
        {
            _mockPersons = new List<Person>
                {
                    new Person(Guid.NewGuid(), "John", "Doe", GenderType.Male, new DateTime(1990, 1, 1), "1234567890", "New York", true),
                    new Person(Guid.NewGuid(), "Jane", "Smith", GenderType.Female, new DateTime(1995, 5, 5), "0987654321", "Los Angeles", false)
                };

            _mockContext = new Mock<IAppContext>();
            _mockContext.Setup(c => c.Persons).Returns(_mockPersons);

            _repository = new PersonRepository(_mockContext.Object);
        }

        [Test]
        public void GetAll_ShouldReturnAllPersons()
        {
            var result = _repository.GetAll();

            Assert.That(2,Is.EqualTo( result.Count()));
            Assert.That("John", Is.EqualTo(result.First().FirstName));
        }

        [Test]
        public void GetById_ShouldReturnCorrectPerson()
        {
            var personId = _mockPersons[0].Id;

            var result = _repository.GetById(personId);

            Assert.That(result,Is.Not.Null);
            Assert.That(personId, Is.EqualTo(result.Id));
        }

        [Test]
        public void GetById_ShouldThrowException_WhenPersonNotFound()
        {
            var invalidId = Guid.NewGuid();

            Assert.Throws<KeyNotFoundException>(() => _repository.GetById(invalidId));
        }

        [Test]
        public void Add_ShouldAddPersonToContext()
        {
            var newPerson = new Person(Guid.NewGuid(), "Alice", "Johnson", GenderType.Female,
                DateTime.SpecifyKind(new DateTime(2000, 10, 10), DateTimeKind.Utc), "1122334455", "Chicago", true);

            _mockContext.Setup(c => c.Persons).Returns(_mockPersons);

            _repository.Add(newPerson);

            Assert.That(_mockPersons.Contains(newPerson), Is.True);
        }

        [Test]
        public void Update_ShouldUpdateExistingPerson()
        {
            var personToUpdate = _mockPersons[0];
            personToUpdate.FirstName = "UpdatedName";

            _repository.Update(personToUpdate);

            Assert.That("UpdatedName", Is.EqualTo(_mockPersons[0].FirstName));
        }

        [Test]
        public void Update_ShouldThrowException_WhenPersonNotFound()
        {
            var nonExistentPerson = new Person(Guid.NewGuid(), "NonExistent", "Person", GenderType.Male, DateTime.Now, "0000000000", "Nowhere", false);

            Assert.Throws<KeyNotFoundException>(() => _repository.Update(nonExistentPerson));
        }

        [Test]
        public void Delete_ShouldRemovePersonFromContext()
        {
            var personId = _mockPersons[0].Id;

            _repository.Delete(personId);

            Assert.That(1, Is.EqualTo(_mockPersons.Count));
            Assert.That(_mockPersons.Any(p => p.Id == personId),Is.False);
        }

        [Test]
        public void Delete_ShouldDoNothing_WhenPersonNotFound()
        {
            var invalidId = Guid.NewGuid();

            _repository.Delete(invalidId);

            Assert.That(2, Is.EqualTo(_mockPersons.Count));
        }
    }
}
