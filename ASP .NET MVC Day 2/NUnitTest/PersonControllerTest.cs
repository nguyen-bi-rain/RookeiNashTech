using ASP_.NET_MVC_Day_2.Controllers;
using ASP_.NET_MVC_Day_2.Enum;
using ASP_.NET_MVC_Day_2.Helpers;
using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.DTO;
using ASP_.NET_MVC_Day_2.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ASP_.NET_MVC_Day_2.NUnitTest
{
    [TestFixture]
    public class PersonControllerTest
    {
        private Mock<IPersonService> _mockPersonService;
        private PersonController _controller;

        [SetUp]
        public void Setup()
        {
            _mockPersonService = new Mock<IPersonService>();
            _controller = new PersonController(_mockPersonService.Object);
        }

        private List<PersonDTO> GetMockPersons()
        {
            return new List<PersonDTO>
            {
                new PersonDTO
                {
                    Id = Guid.NewGuid(), FirstName = "Tran", LastName = "Tuyen",Gender = GenderType.Female,IsGraduated = false,DateOfBirth = new DateTime(2000,10,02),PhoneNumber = "0123456789",BirthPlace = "Hanoi"
                },
                new PersonDTO
                {
                    Id = Guid.NewGuid(), FirstName = "Vu", LastName = "My",Gender = GenderType.Male,IsGraduated = true,DateOfBirth = new DateTime(2003,12,23),PhoneNumber = "0123456789",BirthPlace = "QuangNinh"
                },
                new PersonDTO
                {
                Id = Guid.NewGuid(), FirstName = "Le", LastName = "Nguyen",Gender = GenderType.Male,IsGraduated = true,DateOfBirth = new DateTime(2003,05,16),PhoneNumber = "0123456789",BirthPlace = "QuangNinh"
                },
                new PersonDTO
                {
                    Id = Guid.NewGuid(), FirstName = "Le", LastName = "Truyen",Gender = GenderType.Female,IsGraduated = true,DateOfBirth = new DateTime(2003,04,12),PhoneNumber = "0123456789",BirthPlace = "Quang Ninh"
                },
                new PersonDTO
                {
                    Id = Guid.NewGuid(), FirstName = "Le", LastName = "Nguyen",Gender = GenderType.Female,IsGraduated = true,DateOfBirth = new DateTime(2003,04,14),PhoneNumber = "0123456789",BirthPlace = "QuangNinh"
                }

            };
        }

        [Test]
        public void Index_ReturnsViewResult_WithPaginatedList()
        {
            // Arrange
            var persons = GetMockPersons();
            _mockPersonService.Setup(s => s.GetAllPerson(It.IsAny<int>())).Returns(persons);

            // Act
            var result = _controller.Index(1);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.InstanceOf<PaginatedList<PersonDTO>>());
            var model = viewResult?.Model as PaginatedList<PersonDTO>;
            Assert.That(5, Is.EqualTo(model.Count));
        }
        [Test]
        public void Index_WithPageParameter_ReturnsCorrectPage()
        {
            // Arrange
            var testPersons = GetMockPersons();
            _mockPersonService.Setup(s => s.GetAllPerson(It.IsAny<int>()))
                .Returns(testPersons);

            // Act
            var result = _controller.Index(1);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as PaginatedList<PersonDTO>;
            Assert.That(1, Is.EqualTo(model.PageIndex));
        }

        [Test]
        public void Create_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var person = new PersonCreateDTO()
            {
                FirstName = "Le",
                LastName = "Nguyen",
                Gender = GenderType.Female,
                IsGraduated = true,
                DateOfBirth = new DateTime(2003, 04, 14),
                PhoneNumber = "0123456789",
                BirthPlace = "QuangNinh"
            };

            // Act
            var result = _controller.Create(person);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult?.ActionName, Is.EqualTo("Index"));
            _mockPersonService.Verify(s => s.AddPerson(It.IsAny<PersonCreateDTO>()), Times.Once);
        }

        [Test]
        public void Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("FirstName", "Required");
            var person = new PersonCreateDTO();

            // Act
            var result = _controller.Create(person);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(person, Is.EqualTo(viewResult.Model));
        }

        [Test]

        public void Detail_ReturnsViewResult_WithPerson()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var person = new PersonDTO { Id = personId, FirstName = "John", LastName = "Doe" };
            _mockPersonService.Setup(s => s.GetPersonById(personId)).Returns(person);

            // Act
            var result = _controller.Detail(personId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Detail_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var testId = Guid.NewGuid();
            _mockPersonService.Setup(s => s.GetPersonById(testId))
                .Returns((PersonDTO)null);

            // Act
            var result = _controller.Detail(testId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Edit_Get_ValidId_ReturnsViewWithPerson()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var testPerson = new PersonDTO { Id = testId, FirstName = "Test" };
            _mockPersonService.Setup(s => s.GetPersonById(testId))
                .Returns(testPerson);

            // Act
            var result = _controller.Edit(testId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as PersonDTO;
            Assert.That(model.Id, Is.EqualTo(testId));
        }

        [Test]
        public void Edit_Get_ReturnsViewResult_WithPerson()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var testPerson = new PersonDTO { Id = testId, FirstName = "Test" };
            _mockPersonService.Setup(s => s.GetPersonById(testId))
                .Returns(testPerson);

            // Act
            var result = _controller.Edit(testId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as PersonDTO;
            Assert.That(model.Id, Is.EqualTo(testId));
        }
        [Test]
        public void Edit_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var testId = Guid.NewGuid();
            _controller.ModelState.AddModelError("FirstName", "Required");
            var person = new PersonUpdatedDTO();

            // Act
            var result = _controller.Edit(testId, person);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            Assert.That(viewResult.Model, Is.EqualTo(person));
        }

        [Test]
        public void Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var person = new PersonUpdatedDTO
            {
                FirstName = "Adele",
                LastName = "Name",
                Gender = GenderType.Male,
                DateOfBirth = new DateTime(1995, 1, 1),
                PhoneNumber = "1234567890",
                BirthPlace = "City",
                IsGraduated = true,
            };

            // Act
            var result = _controller.Edit(testId, person);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"));
            _mockPersonService.Verify(s => s.UpdatePerson(testId, person), Times.Once);
        }
        [Test]
        public void Delete_Get_ValidId_ReturnsViewWithPerson()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var testPerson = new PersonDTO { Id = testId, FirstName = "Test" };
            _mockPersonService.Setup(s => s.GetPersonById(testId))
                .Returns(testPerson);

            // Act
            var result = _controller.Delete(testId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as PersonDTO;
            Assert.That(model.Id, Is.EqualTo(testId));
        }
        [Test]
        public void Delete_Get_ReturnsViewResult_WithPerson()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var person = new PersonDTO { Id = personId, FirstName = "John", LastName = "Doe" };
            _mockPersonService.Setup(s => s.GetPersonById(personId)).Returns(person);

            // Act
            var result = _controller.Delete(personId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void DeleteConfirmed_Post_ValidId_RedirectsToConfirmation()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var testPerson = new PersonDTO { Id = testId, FirstName = "Test", LastName = "User" };
            _mockPersonService.Setup(s => s.GetPersonById(testId))
                .Returns(testPerson);

            // Act
            var result = _controller.DeleteConfirmed(testId);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Confirmation"));
            Assert.That(redirectResult.RouteValues["name"], Is.EqualTo("Test User"));
            _mockPersonService.Verify(s => s.DeletePerson(testId), Times.Once);
        }


        [Test]
        public void GetListMalePerson_ReturnsViewResult_WithMalePersons()
        {
            // Arrange
            var persons = GetMockPersons();
            _mockPersonService.Setup(s => s.GetPersonIsMale()).Returns(persons.Where(x => x.Gender == GenderType.Male));

            // Act
            var result = _controller.GetListMalePerson();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<PersonDTO>;
            Assert.That(model.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetOldest_ReturnsViewResult_WithOldestPerson()
        {
            var testPersons = GetMockPersons();
            var oldest = testPersons.OrderBy(p => p.DateOfBirth).First();
            _mockPersonService.Setup(s => s.GetOldestPerson())
                .Returns(oldest);

            // Act
            var result = _controller.GetOldest();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as PersonDTO;
            Assert.That(model.Id, Is.EqualTo(oldest.Id));
        }

        [Test]
        public void FilterPerson_ReturnsViewResult_WithFilteredPersonsBornIn2000()
        {
            var testPersons = GetMockPersons();
            _mockPersonService.Setup(s => s.FilterPerson("BornIn", 2000))
                .Returns(testPersons.Where(p => p.DateOfBirth.Year == 2000).ToList());

            // Act
            var result = _controller.FilterPerson("BornIn", 2000);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<PersonDTO>;
            Assert.That(model.Count(), Is.EqualTo(1));
        }
        [Test]
        public void FilterPerson_ReturnsViewResult_WithFilteredPersonsBornGreater2000()
        {
            var testPersons = GetMockPersons();
            _mockPersonService.Setup(s => s.FilterPerson("BornGreater", 2000))
                .Returns(testPersons.Where(p => p.DateOfBirth.Year > 2000).ToList());

            // Acts
            var result = _controller.FilterPerson("BornGreater", 2000);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<PersonDTO>;
            Assert.That(model.Count(), Is.EqualTo(4));
        }
        [Test]
        public void FilterPerson_ReturnsViewResult_WithFilteredPersonsBornLess2000()
        {
            var testPersons = GetMockPersons();
            _mockPersonService.Setup(s => s.FilterPerson("BornLess", 2000))
                .Returns(testPersons.Where(p => p.DateOfBirth.Year < 2000).ToList());

            // Acts
            var result = _controller.FilterPerson("BornLess", 2000);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<PersonDTO>;
            Assert.That(model.Count(), Is.EqualTo(0));
        }
        [Test]
        public void FilterPerson_InvalidQuery_ReturnsContent()
        {
            // Arrange
            _mockPersonService.Setup(s => s.FilterPerson("Invalid", 0))
                .Returns((List<PersonDTO>)null);

            // Act
            var result = _controller.FilterPerson("Invalid", 0);

            // Assert
            Assert.That(result, Is.InstanceOf<ContentResult>());
            var contentResult = result as ContentResult;
            Assert.That(contentResult.Content, Is.EqualTo("Invalid query"));
        }
        [Test]
        public void GetFullNames_ReturnsViewResult_WithFullNames()
        {
            // Arrange
            var testPersons = GetMockPersons();
            _mockPersonService.Setup(s => s.GetAllPerson(1))
                .Returns(testPersons);
            _mockPersonService.Setup(s => s.GetFullNameList(testPersons))
                .Returns(testPersons.Select(p => $"{p.FirstName} {p.LastName}").ToList());

            // Act
            var result = _controller.GetFullNames();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<string>;
            Assert.That(model.Count(), Is.EqualTo(5));
        }

        [Test]
        public void ExportExcelFile_ReturnsFileResult_WhenDataExists()
        {
            // Arrange
            var testPersons = GetMockPersons();
            _mockPersonService.Setup(s => s.GetAllPerson(1))
                .Returns(testPersons);
            _mockPersonService.Setup(s => s.GenerateExcelFile(testPersons))
                .Returns(new byte[100]);

            // Act
            var result = _controller.ExportExcelFile();

            // Assert
            Assert.That(result, Is.InstanceOf<FileContentResult>());
            var fileResult = result as FileContentResult;
            Assert.That(fileResult.FileDownloadName, Is.EqualTo("Persons.xlsx"));
            Assert.That(fileResult.ContentType, Is.EqualTo("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
        }
        [Test]
        public void ExportExcelFile_NoData_ReturnsContent()
        {
            // Arrange
            _mockPersonService.Setup(s => s.GetAllPerson(1))
                .Returns(new List<PersonDTO>());

            // Act
            var result = _controller.ExportExcelFile();

            // Assert
            Assert.That(result, Is.InstanceOf<ContentResult>());
            var contentResult = result as ContentResult;
            Assert.That(contentResult.Content, Is.EqualTo("No data to export"));
        }
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}
