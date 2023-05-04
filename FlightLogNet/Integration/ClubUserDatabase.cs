namespace FlightLogNet.Integration
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using FlightLogNet.Models;

    using Microsoft.Extensions.Configuration;

    using RestSharp;

    public class ClubUserDatabase : IClubUserDatabase
    {
        // 8.1: Přidejte si přes dependency injection configuraci 
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public ClubUserDatabase(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this._mapper = mapper;
        }

        public bool TryGetClubUser(long memberId, out PersonModel personModel)
        {
            personModel = this.GetClubUsers().FirstOrDefault(person => person.MemberId == memberId);

            return personModel != null;
        }

        public IList<PersonModel> GetClubUsers()
        {
            IList<ClubUser> x = this.ReceiveClubUsers();
            return this.TransformToPersonModel(x);
        }

        private IList<ClubUser> ReceiveClubUsers()
        {
            // 8.2: Naimplementujte volání endpointu ClubDB pomocí RestSharp
            var client = new RestClient(configuration["ClubUsersApi"]);
            var request = new RestRequest("club/user");
            var response = client.Get<List<ClubUser>>(request);
            return response;
        }

        private IList<PersonModel> TransformToPersonModel(IList<ClubUser> users)
        {
            return this._mapper.Map<IList<PersonModel>>(users);
        }
    }
}
