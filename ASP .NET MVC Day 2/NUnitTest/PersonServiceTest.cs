using ASP_.NET_MVC_Day_2.Enum;
using ASP_.NET_MVC_Day_2.Helpers;
using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.DTO;
using ASP_.NET_MVC_Day_2.Repository;
using ASP_.NET_MVC_Day_2.Services;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace ASP_.NET_MVC_Day_2.NUnitTest;

[TestFixture]
public class PersonServiceTest
{
    private Mock<IPersonRepository> _personRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private PersonService _personService;
    private List<Person> _mockPersons;

    [SetUp]
    public void Setup()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _mapperMock = new Mock<IMapper>();
        _personService = new PersonService(_personRepositoryMock.Object, _mapperMock.Object);
        _mockPersons = new List<Person>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2003,12,23),
                BirthPlace = "Ha Noi",
                IsGraduated = false,
                Gender = GenderType.Male,
                PhoneNumber = "0123456789",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                FirstName = "Luu",
                LastName = "Duoc phi",
                DateOfBirth = new DateTime(2000,10,12),
                BirthPlace = "Ha Noi",
                IsGraduated = false,
                Gender = GenderType.Female,
                PhoneNumber = "0123456789",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                FirstName = "Trinh",
                LastName = "Sang",
                DateOfBirth = new DateTime(2003,01,01),
                BirthPlace = "Ha Nam",
                IsGraduated = false,
                Gender = GenderType.Male,
                PhoneNumber = "0123456789",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                FirstName = "He Mong",
                LastName = "Dao",
                DateOfBirth = new DateTime(1999,02,23),
                BirthPlace = "Tu Xuyen",
                IsGraduated = false,
                Gender = GenderType.Male,
                PhoneNumber = "0123456789",
            },
            new ()
            {
                Id = Guid.NewGuid(),
                FirstName = "Trinh Tran",
                LastName = "Phuong Tuan",
                DateOfBirth = new DateTime(1997,04,12),
                BirthPlace = "Ha Noi",
                IsGraduated = false,
                Gender = GenderType.Female,
                PhoneNumber = "0123456789",
            }


        };
    }
    [Test]
    public void AddPerson_WithNullPerson_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(null));
    }

    [Test]
    public void AddPerson_ShouldAddPerson_WhenValidPersonIsProvided()
    {
        // Arrange
        var personCreateDto = new PersonCreateDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Gender = GenderType.Male,
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "1234567890",
            BirthPlace = "City"
        };
        var person = new Person();

        _mapperMock.Setup(m => m.Map<Person>(personCreateDto)).Returns(person);

        // Act
        _personService.AddPerson(personCreateDto);

        // Assert
        _personRepositoryMock.Verify(r => r.Add(person), Times.Once);
        Assert.That(personCreateDto.CreatedAt,Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
    }

  
    [Test]
    public void DeletePerson_ShouldThrowException_WhenPersonNotFound()
    {
        // Arrange
        var personId = Guid.NewGuid();
        _personRepositoryMock.Setup(r => r.GetById(personId)).Returns((Person)null);

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _personService.DeletePerson(personId));
    }

    [Test]
    public void DeletePerson_WithValidId_CallsRepositoryDelete()
    {
        var validId = _mockPersons[0].Id;
        _personRepositoryMock.Setup(r => r.GetById(validId)).Returns(_mockPersons[0]);

        _personService.DeletePerson(validId);

        _personRepositoryMock.Verify(r => r.Delete(validId), Times.Once);
    }
    [Test]
    public void GetOldestPerson_ShouldReturnOldestPerson()
    {
        // Arrange  
        var persons = _mockPersons;
        var oldestPerson = persons.OrderBy(p => p.DateOfBirth).First();
        var personDto = new PersonDTO
        {
            Id = oldestPerson.Id,
            FirstName = oldestPerson.FirstName,
            LastName = oldestPerson.LastName,
            Gender = oldestPerson.Gender,
            DateOfBirth = oldestPerson.DateOfBirth,
            PhoneNumber = oldestPerson.PhoneNumber,
            BirthPlace = oldestPerson.BirthPlace,
            IsGraduated = oldestPerson.IsGraduated
        };

        _personRepositoryMock.Setup(r => r.GetAll()).Returns(persons);
        _mapperMock.Setup(m => m.Map<PersonDTO>(oldestPerson)).Returns(personDto);

        // Act  
        var result = _personService.GetOldestPerson();

        // Assert  
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(personDto.Id));
        Assert.That(result.FirstName, Is.EqualTo(personDto.FirstName));
        Assert.That(result.LastName, Is.EqualTo(personDto.LastName));
        Assert.That(result.DateOfBirth, Is.EqualTo(personDto.DateOfBirth));
    }
    [Test]
    public void GetOldestPerson_WithEmptyList_ThrowsException()
    {
        _personRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Person>());

        Assert.Throws<Exception>(() => _personService.GetOldestPerson());
    }

    [Test]
    public void GetPersonById_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var person = new Person { Id = personId };
        var personDto = new PersonDTO { Id = personId };

        _personRepositoryMock.Setup(r => r.GetById(personId)).Returns(person);
        _mapperMock.Setup(m => m.Map<PersonDTO>(person)).Returns(personDto);

        // Act
        var result = _personService.GetPersonById(personId);

        // Assert
        Assert.That(personDto, Is.EqualTo(result));
    }

    [Test]
    public void FilterPerson_WithBornIn2000_ReturnsPeopleBornIn2000()
    {
        var year = 2000;
        var expectedPerson = _mockPersons[1];
        _mapperMock.Setup(m => m.Map<List<PersonDTO>>(It.IsAny<List<Person>>()))
            .Returns(new List<PersonDTO> { new PersonDTO { FirstName = expectedPerson.FirstName } });

        var result = _personService.FilterPerson("BornIn", year);

        Assert.That(result.Count, Is.EqualTo(1));
    }

    [Test]
    public void FilterPerson_WithBornGreater2000_ReturnsPeopleGreater2000()
    {
        // Arrange  
        var year = 2000;
        var expectedPersons = _mockPersons.Where(x => x.DateOfBirth.Year > year).ToList();
        var expectedPersonDtos = expectedPersons.Select(p => new PersonDTO { FirstName = p.FirstName }).ToList();

        _mapperMock.Setup(m => m.Map<List<PersonDTO>>(expectedPersons)).Returns(expectedPersonDtos);

        // Act  
        var result = _personService.FilterPerson("BornGreater", year);

        // Assert  
        Assert.That(result, Is.Not.Null); // Ensure result is not null  
        Assert.That(result.Count, Is.EqualTo(expectedPersonDtos.Count));
    }

    [Test]
    public void FilterPerson_WithBornLess2000_ReturnsPeopleBornLess2000()
    {
        // Arrange  
        var year = 2000;
        var expectedPersons = _mockPersons.Where(x => x.DateOfBirth.Year < year).ToList();
        var expectedPersonDtos = expectedPersons.Select(p => new PersonDTO { FirstName = p.FirstName }).ToList();

        _mapperMock.Setup(m => m.Map<List<PersonDTO>>(expectedPersons)).Returns(expectedPersonDtos);

        // Act  
        var result = _personService.FilterPerson("BornLess", year);

        // Assert  
        Assert.That(result.Count, Is.EqualTo(expectedPersonDtos.Count));
    }

    [Test]
    public void FilterPerson_WithInvalidFilter_ReturnsNull()
    {
        var result = _personService.FilterPerson("InvalidFilter", 2000);
        Assert.That(result,Is.Null);
    }
    [Test]
    public void GetAllPerson_ReturnsPaginatedList()
    {
        var page = 1;
        _mapperMock.Setup(m => m.Map<IEnumerable<PersonDTO>>(It.IsAny<IEnumerable<Person>>()))
            .Returns(_mockPersons.Select(p => new PersonDTO { FirstName = p.FirstName }));

        var result = _personService.GetAllPerson(page);

        Assert.That(result, Is.InstanceOf<PaginatedList<PersonDTO>>());
        Assert.That(result.Count, Is.EqualTo(5));
    }
    [Test]
    public void GetFullNameList_ReturnsFullNameOfPerson()
    {
        var expectedNames = _mockPersons.Select(p => $"{p.FirstName} {p.LastName}").ToList();
        var expectedpersonDto = new List<PersonDTO>();
        _mapperMock.Setup(m => m.Map<List<PersonDTO>>(_mockPersons))
            .Returns(expectedpersonDto);

        var result = _personService.GetFullNameList(expectedpersonDto);

        Assert.That(result, Is.EquivalentTo(expectedNames));
    }

    [Test]
    public void GetPersonById_WithInvalidId_ReturnsNull()
    {
        var invalidId = Guid.NewGuid();
        _personRepositoryMock.Setup(r => r.GetById(invalidId)).Returns((Person)null);

        var result = _personService.GetPersonById(invalidId);

        Assert.That(result,Is.Null);
    }

    [Test]
    public void GetPersonById_WithValidId_ReturnsPerson()
    {
        var expectedPerson = _mockPersons[0];
        _personRepositoryMock.Setup(r => r.GetById(expectedPerson.Id)).Returns(expectedPerson);
        _mapperMock.Setup(m => m.Map<PersonDTO>(expectedPerson))
            .Returns(new PersonDTO { FirstName = expectedPerson.FirstName });

        var result = _personService.GetPersonById(expectedPerson.Id);

        Assert.That(result.FirstName, Is.EqualTo(expectedPerson.FirstName));
    }

    [Test]
    public void GetPersonIsMale_ReturnsOnlyMales()
    {
        var expectedPeople = _mockPersons.Where(p => p.Gender == GenderType.Male).ToList();
        _mapperMock.Setup(m => m.Map<List<PersonDTO>>(It.IsAny<List<Person>>()))
            .Returns(expectedPeople.Select(p => new PersonDTO { FirstName = p.FirstName }).ToList());

        var result = _personService.GetPersonIsMale();

        Assert.That(result.Count, Is.EqualTo(3));
    }
    [Test]
    public void UpdatePerson_WithInvalidId_ThrowsKeyNotFoundException()
    {
        var invalidId = Guid.NewGuid();
        var updateDto = new PersonUpdatedDTO();
        _personRepositoryMock.Setup(r => r.GetById(invalidId)).Returns((Person)null);

        Assert.Throws<KeyNotFoundException>(() => _personService.UpdatePerson(invalidId, updateDto));
    }

    [Test]
    public void UpdatePerson_WithValidId_CallsRepositoryUpdate()
    {
        var validId = _mockPersons[0].Id;
        var updateDto = new PersonUpdatedDTO();
        var person = _mockPersons[0];
        _personRepositoryMock.Setup(r => r.GetById(validId)).Returns(person);
        _mapperMock.Setup(m => m.Map<Person>(updateDto)).Returns(person);

        _personService.UpdatePerson(validId, updateDto);

        _personRepositoryMock.Verify(r => r.Update(It.IsAny<Person>()), Times.Once);
        Assert.That(updateDto.UpdatedAt, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void GenerateExcelFile_ReturnsCorrectByteArray()
    {
        var persons = new List<PersonDTO>
            {
                new PersonDTO
                {
                    FirstName = "Test",
                    LastName = "User",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = GenderType.Male,
                    PhoneNumber = "1234567890",
                    BirthPlace = "Test City"
                }
            };

        var result = _personService.GenerateExcelFile(persons);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.GreaterThan(0));
    }
}
