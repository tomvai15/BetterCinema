using BetterCinema.Api.Constants;
using BetterCinema.Domain.Constants;
using BetterCinema.Domain.Entities;

namespace BetterCinema.UnitTests.Fakes
{
    public static class SampleData
    {
        public static List<UserEntity> GetUsers() => new List<UserEntity>
        {
            new UserEntity
            {
                Id = 1,
                Email="owner@gmail.com",
                Name="Petras",
                Surname="Petravičius",
                HashedPassword ="$2a$11$ktu80VJa3SexNZl76PQENOogwKBCcSWnFv.GC2/jZylN8ukZ/QZ0a",
                Role = Role.Owner,
            },
            new UserEntity
            {
                Id = 2,
                Email="admin@gmail.com",
                Name="Jonas",
                Surname="Jonavičius",
                HashedPassword ="$2a$11$ktu80VJa3SexNZl76PQENOogwKBCcSWnFv.GC2/jZylN8ukZ/QZ0a",
                Role = Role.Admin
            }
        };
    }
}
