namespace FlightLogNet.Tests.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FlightLogNet.Models;
    using FlightLogNet.Repositories;
    using FlightLogNet.Repositories.Interfaces;

    using Xunit;

    using Microsoft.Extensions.Configuration;

    public class PersonRepositoryTests
    {
        private readonly IConfiguration configuration;

        public PersonRepositoryTests(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        private IPersonRepository CreatePersonRepository()
        {
            return new PersonRepository(this.configuration);
        }
        
        private void RenewDatabase()
        {
            TestDatabaseGenerator.RenewDatabase(this.configuration);
        }

        [Fact]
        public void AddGuestPerson_ShouldAddNewPerson()
        {
            // Arrange
            const int expectedId = 113;
            RenewDatabase();
            var personRepository = this.CreatePersonRepository();
            
            PersonModel personModel = new PersonModel
            {
                Address = new AddressModel { City = "NY", PostalCode = "456", Street = "2nd Ev", Country = "USA" },
                FirstName = "John",
                LastName = "Smith",
                MemberId = 1
            };

            // Act
            var result = personRepository.AddGuestPerson(personModel);

            // Assert
            Assert.Equal(expectedId, result);
        }
        
        [Fact]
        public void CreateClubMember_ShouldCreateNewClubMember()
        {
            // Arrange
            const int expectedId = 113;
            RenewDatabase();
            var personRepository = this.CreatePersonRepository();
            
            PersonModel personModel = new PersonModel
            {
                Address = new AddressModel { City = "NY", PostalCode = "456", Street = "2nd Ev", Country = "USA" },
                FirstName = "John",
                LastName = "Smith",
                MemberId = 1
            };

            // Act
            var result = personRepository.CreateClubMember(personModel);

            // Assert
            Assert.Equal(expectedId, result);
        }

    }
}
