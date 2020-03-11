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
        private AppDbContext _BuildDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new AppDbContext(options);

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
            return dbContext;
        }

        [Fact]
        public async void MedicationStringRepository_TotalCount_Return10()
        {
            using (var dbContext = _BuildDbContext("totalCountTestDb"))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                int result = await repository.TotalCount();
                Assert.Equal<int>(10, result);
            }
        }

        [Fact]
        public async void MedicationStringRepository_TotalDosageCount_Return55()
        {
            using (var dbContext = _BuildDbContext("totalDosageCountTestDb"))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                int result = await repository.TotalDosageCount();
                Assert.Equal<int>(55, result);
            }

        }

        [Fact]
        public async void MedicationStringRepository_TotalNumberByBottleSize_ReturnS2M2L2XL2XXL1NA1()
        {
            using (var dbContext = _BuildDbContext("TotalNumberByBottleSizeTestDb"))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                var results = await repository.TotalNumberByBottleSize();
                foreach (var result in results)
                {
                    switch (result.BottleSize)
                    {
                        case BottleSizeEnum.S:
                            Assert.Equal<int>(2, result.Count);
                            break;
                        case BottleSizeEnum.M:
                            Assert.Equal<int>(2, result.Count);
                            break;
                        case BottleSizeEnum.L:
                            Assert.Equal<int>(2, result.Count);
                            break;
                        case BottleSizeEnum.XL:
                            Assert.Equal<int>(2, result.Count);
                            break;
                        case BottleSizeEnum.XXL:
                            Assert.Equal<int>(1, result.Count);
                            break;
                        case BottleSizeEnum.NA:
                            Assert.Equal<int>(1, result.Count);
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
            using (var dbContext = _BuildDbContext("DistinctMedicationIdsAndCountTestDb"))
            {
                var repository = new MedicationStringRepository(dbContext.Set<MedicationString>());
                var results = await repository.DistinctMedicationIds();
                foreach (var result in results)
                {
                    switch (result.MedicationId)
                    {
                        case "mid1":
                            Assert.Equal<int>(1, result.Count);
                            break;
                        case "mid2":
                            Assert.Equal<int>(2, result.Count);
                            break;
                        case "mid3":
                            Assert.Equal<int>(3, result.Count);
                            break;
                        case "mid4":
                            Assert.Equal<int>(2, result.Count);
                            break;
                        case "mid5":
                            Assert.Equal<int>(1, result.Count);
                            break;
                        case "mid6":
                            Assert.Equal<int>(1, result.Count);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
        }
    }
}