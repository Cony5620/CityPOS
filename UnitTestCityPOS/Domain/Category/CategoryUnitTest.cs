using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Domain;
using CityPOS.Services;
using CityPOS.UnitOfWorks;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestCityPOS.Domain.Category
{
    public class CategoryUnitTest
    {
        public Mock<ICategoryService> categoryServiceMock = new Mock<ICategoryService>();
        public Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        public Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
        [Fact]
        public void CreateUnitTest()
        {//1.Arrange
            string id = "12345";
            var expectedCategoryViewModel = new CategoryViewModel()
            {
                id = id,
                Code = "001",
                Name = "Shower",
                Description = "-"
            };
            var dbCategoryEntity = new CategoryEntity()
            {
                id = id,
                Code = expectedCategoryViewModel.Code,
                Name = expectedCategoryViewModel.Name,
                Description = expectedCategoryViewModel.Description,
            };
            categoryRepositoryMock.Setup(c => c.Create(dbCategoryEntity));
            unitOfWorkMock.Setup(u=>u.CategoryRepository).Returns(categoryRepositoryMock.Object);

            //2.Act
            var categoryService=new CategoryService(unitOfWorkMock.Object);
            categoryService.Create(expectedCategoryViewModel);

            //3.Assert

            try
            {
                categoryService.Create(expectedCategoryViewModel);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
                
            }
        }

        [Fact]
        public void GetAll()
        {
            IEnumerable<CategoryViewModel> expectedResult = new List<CategoryViewModel>()
            {
                new CategoryViewModel{id="1",Code="001",Name="Cover",Description="d"}
            };
            IEnumerable<CategoryEntity> categoryEntities = new List<CategoryEntity>()
            { 
                new CategoryEntity{id="1",Code="001",Name="Cover",Description="d"}
            };
            categoryRepositoryMock.Setup(c => c.GetAll()).Returns(categoryEntities);
            unitOfWorkMock.Setup(u => u.CategoryRepository).Returns(categoryRepositoryMock.Object);

            var categoryService = new CategoryService(unitOfWorkMock.Object); 
            var actualResult= categoryService.GetAll();

            var input=JsonConvert.SerializeObject(expectedResult);
            var output=JsonConvert.SerializeObject(actualResult);
            Assert.Equal(input, output);
        }
    }
}
    

