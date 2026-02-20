using FlagX0.Web.Business.UseCases.Flags;
using FlagX0.Web.Business.UserInfo;
using FlagX0.Web.Data;
using FlagX0.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlagX0.UnitTests.Web.Business.UseCases.Flags
{
    public class AddFlagUseCaseTest
    {
        [Fact]
        public async Task WhenFlagNameAlreadyExist_ThenError()
        {
            //Arrange
            IFlagUserDetails flagUserDetails = new FlagUserDetailsStub();
            ApplicationDbContext inMemoryDb = GetInMemoryDbContext(flagUserDetails);

            FlagEntity currentFlag = new FlagEntity()
            {
                UserId = flagUserDetails.UserId,
                Name = "name",
                Value = true
            };

            inMemoryDb.Flags.Add(currentFlag);
            await inMemoryDb.SaveChangesAsync();


            //Act
            AddFlagUseCase addFlagUseCase = new AddFlagUseCase(inMemoryDb, flagUserDetails);
            var result = await addFlagUseCase.Execute(currentFlag.Name, true);


            //Assert
            Assert.False(result.Success);
            Assert.Equal("Flag name already exists.", result.Errors.First().Message);

        }

        private ApplicationDbContext GetInMemoryDbContext(IFlagUserDetails flagUserDetails)
        {
            DbContextOptions<ApplicationDbContext> databaseOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("flagx0Db").Options;

            return new ApplicationDbContext(databaseOptions, flagUserDetails);
        }
    }

    public class  FlagUserDetailsStub : IFlagUserDetails
    {
        public string UserId => "1";
    }
}
