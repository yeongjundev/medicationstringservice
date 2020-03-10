using System;
using MedicationStringService.API.Models;
using MedicationStringService.API.Persistences;
using MedicationStringService.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MedicationStringService.Tests
{
    public class StatisticsControllerTests
    {
        // StatisticsController does not modify data.
        // Thus, all test cases share same DbContext.
        private readonly DbContextOptions<AppDbContext> options;

        public StatisticsControllerTests()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var dbContext = new AppDbContext(options))
            {
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid1", BottleSize = BottleSizeEnum.S, DosageCount = 1 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid2", BottleSize = BottleSizeEnum.M, DosageCount = 2 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid2", BottleSize = BottleSizeEnum.L, DosageCount = 3 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid3", BottleSize = BottleSizeEnum.XL, DosageCount = 4 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid3", BottleSize = BottleSizeEnum.XXL, DosageCount = 5 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid3", BottleSize = BottleSizeEnum.XL, DosageCount = 6 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid4", BottleSize = BottleSizeEnum.L, DosageCount = 7 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid4", BottleSize = BottleSizeEnum.M, DosageCount = 8 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid5", BottleSize = BottleSizeEnum.S, DosageCount = 9 });
                dbContext.MedicationStrings.Add(new MedicationString { MedicationId = "mid6", BottleSize = BottleSizeEnum.NA, DosageCount = 10 });

                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async void MedicationStringRepository_TotalCount_Return10()
        {
            using (var dbContext = new AppDbContext(options))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                int result = await repository.TotalCount();
                Assert.True(result == 10);
            }
        }

        [Fact]
        public async void MedicationStringRepository_TotalDosageCount_Return55()
        {
            using (var dbContext = new AppDbContext(options))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                int result = await repository.TotalDosageCount();
                Assert.True(result == 55);
            }
        }

        [Fact]
        public async void MedicationStringRepository_TotalNumberByBottleSize_ReturnS2M2L2XL2XXL1NA1()
        {
            using (var dbContext = new AppDbContext(options))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                var results = await repository.TotalNumberByBottleSize();
                foreach (var result in results)
                {
                    switch (result.BottleSize)
                    {
                        case BottleSizeEnum.S:
                            Assert.True(result.Count == 2);
                            break;
                        case BottleSizeEnum.M:
                            Assert.True(result.Count == 2);
                            break;
                        case BottleSizeEnum.L:
                            Assert.True(result.Count == 2);
                            break;
                        case BottleSizeEnum.XL:
                            Assert.True(result.Count == 2);
                            break;
                        case BottleSizeEnum.XXL:
                            Assert.True(result.Count == 1);
                            break;
                        case BottleSizeEnum.NA:
                            Assert.True(result.Count == 1);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
        }

        [Fact]
        public async void MedicationStringRepository_DistinctMedicationIdsAndCount_Return_1_2_3_2_1_1()
        {
            using (var dbContext = new AppDbContext(options))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                var results = await repository.DistinctMedicationIds();
                foreach (var result in results)
                {
                    switch (result.MedicationId)
                    {
                        case "mid1":
                            Assert.True(result.Count == 1);
                            break;
                        case "mid2":
                            Assert.True(result.Count == 2);
                            break;
                        case "mid3":
                            Assert.True(result.Count == 3);
                            break;
                        case "mid4":
                            Assert.True(result.Count == 2);
                            break;
                        case "mid5":
                            Assert.True(result.Count == 1);
                            break;
                        case "mid6":
                            Assert.True(result.Count == 1);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
        }
    }
}