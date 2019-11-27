using System;
using AspNetCore.Identity.Mongo.Model;

namespace Sales.Authentication.Mongo.IdentityModels
{
    public class ApplicationUser : MongoUser
    {
        public override string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Email { get; set; }
        public override string Id { get; set; }
    }
}