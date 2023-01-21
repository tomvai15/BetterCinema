using BetterCinema.Api.Constants;
using BetterCinema.Api.Models;
using static BetterCinema.UnitTests.Fakes.Dummy;

namespace BetterCinema.UnitTests.Fakes
{
    public static class SampleData
    {
        public static List<User> GetUsers() => new List<User>
        {
            new User
            {
                UserId = 1,
                Email="owner@gmail.com",
                Name="Petras",
                Surname="Petravičius",
                HashedPassword ="$2a$11$ktu80VJa3SexNZl76PQENOogwKBCcSWnFv.GC2/jZylN8ukZ/QZ0a",
                Role = Role.Owner
            },
            new User
            {
                UserId = 2,
                Email="admin@gmail.com",
                Name="Jonas",
                Surname="Jonavičius",
                HashedPassword ="$2a$11$ktu80VJa3SexNZl76PQENOogwKBCcSWnFv.GC2/jZylN8ukZ/QZ0a",
                Role = Role.Admin
            }
        };
    }
}
