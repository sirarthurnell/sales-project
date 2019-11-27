using System.Collections.Generic;
using Sales.Data.Entities;

namespace Sales.Api.Models.Responses
{
    public class LoginResponse
    {
        public LoginResponse(string token, string userName, string email)
        {
            Token = token;
            UserName = userName;
            Email = email;
        }

        public string Token { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }
    }

    public class SignUpResponse
    {
        public SignUpResponse(string token, string userName, string email)
        {
            Token = token;
            UserName = userName;
            Email = email;
        }

        public string Token { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }
    }

    public class UserDataResponse
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class FiltersAggregateResponse
    {
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<string> ItemTypes { get; set; }
        public IEnumerable<string> OrderPriorities { get; set; }
        public IEnumerable<string> Regions { get; set; }
        public IEnumerable<string> RegionsCountries { get; set; }
        public IEnumerable<string> SalesChannels { get; set; }
    }

    public class PaginatedSalesResponse
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<Sale> PageContent { get; set; }
    }
}