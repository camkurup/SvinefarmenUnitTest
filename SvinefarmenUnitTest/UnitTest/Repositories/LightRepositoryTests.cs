using Microsoft.EntityFrameworkCore;
using SvinefarmenUnitTest.Model;
using SvinefarmenUnitTest.Repository;
using Xunit;

namespace SvinefarmenUnitTest.UnitTests.Repositories
{
	public class LightRepositoryTests
	{
		private readonly DbContextOptions<ThePigFarmContext> _options;
		private readonly ThePigFarmContext _context;
		private readonly LightRepository _repository;

		public LightRepositoryTests()
		{
			//Setting op af mocked DB
			_options = new DbContextOptionsBuilder<ThePigFarmContext>()
				.UseInMemoryDatabase(databaseName: "ThePigFarm")
				.Options;

			_context = new(_options);

			_repository = new(_context);
		}

		[Fact]
		public async void CreateLightLog_ShouldReturnLightLog()
		{
			//Arrange
			await _context.Database.EnsureDeletedAsync();
			await _context.SaveChangesAsync();

			int expectedId = 1;
			LightLogRequest newLightLog = new()
			{
				Leveloflight = 75,
				Timeoflog = DateTime.UtcNow,
				Lightlevelinstable = 1
			};

			//Act
			var result = await _repository.CreateLightLog(newLightLog);
				
			//Assert
			Assert.NotNull(result);
			Assert.IsType<Lightlog>(result);
			Assert.Equal(expectedId, result.Id);
		}

		[Fact]
		public async void GetLevelOfLight_ShouldReturnLightLog()
		{
			//Arrange
			await _context.Database.EnsureDeletedAsync();

			DateTime time1 = DateTime.UtcNow.AddMinutes(-10);
			DateTime time2 = DateTime.UtcNow.AddMinutes(-20);
			Lightlog lightLog1 = new()
			{
				Id= 1,
				Leveloflight = 90,
				Timeoflog = time1,
				Lightlevelinstable = 1

			};
			Lightlog lightLog2 = new()
			{
				Id = 2,
				Leveloflight = 100,
				Timeoflog = time2,
				Lightlevelinstable = 2
			};
			_context.Lightlogs.Add(lightLog1);
			_context.Lightlogs.Add(lightLog2);

			await _context.SaveChangesAsync();
			//Act
			var result = await _repository.GetLevelOfLight();
			//Assert
			Assert.NotNull(result);
			Assert.Equal(result.Timeoflog, time1);
		}
	}
}
