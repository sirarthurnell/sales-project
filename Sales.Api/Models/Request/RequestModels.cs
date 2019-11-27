using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Sales.Api.Extensions;
using Sales.Data.Entities;

namespace Sales.Api.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class SearchRequest
    {
        public string RegionCountry { get; set; }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }

        public Expression<Func<Sale, bool>> ComposeSearch()
        {
            Expression<Func<Sale, bool>> baseExpression = s => true;

            if (!string.IsNullOrWhiteSpace(RegionCountry))
            {
                var region = RegionCountry.Split(':')[0].Trim();
                var country = RegionCountry.Split(':')[1].Trim();

                Expression<Func<Sale, bool>> regionExpression = s => s.Region == region;
                Expression<Func<Sale, bool>> countryExpression = s => s.Country == country;
                baseExpression = baseExpression.And(regionExpression);
                baseExpression = baseExpression.And(countryExpression);
            }

            if (!string.IsNullOrWhiteSpace(ItemType))
            {
                Expression<Func<Sale, bool>> itemTypeExpression = s => s.ItemType == ItemType;
                baseExpression = baseExpression.And(itemTypeExpression);
            }

            if (!string.IsNullOrWhiteSpace(OrderPriority))
            {
                Expression<Func<Sale, bool>> orderPriorityExpression = s => s.OrderPriority == OrderPriority;
                baseExpression = baseExpression.And(orderPriorityExpression);
            }

            return baseExpression;
        }
    }

    public enum OrderDirection
    {
        None = 0,
        Asc = 1,
        Desc = -1
    }

    public class OrderRequest
    {
        public string Field { get; set; } = "region";
        public string Direction { get; set; } = "none";

        public Func<IQueryable<Sale>, IOrderedQueryable<Sale>> ComposeOrdering()
        {
            Func<IQueryable<Sale>, IOrderedQueryable<Sale>> orderFunction = null;

            if (Direction != "none")
            {
                if (Direction == "asc")
                {
                    switch (Field)
                    {
                        case "region":
                            orderFunction = (sale) => sale.OrderBy(s => s.Region);
                            break;
                        case "country":
                            orderFunction = (sale) => sale.OrderBy(s => s.Country);
                            break;
                        case "itemType":
                            orderFunction = (sale) => sale.OrderBy(s => s.ItemType);
                            break;
                        case "salesChannel":
                            orderFunction = (sale) => sale.OrderBy(s => s.SalesChannel);
                            break;
                        case "orderPriority":
                            orderFunction = (sale) => sale.OrderBy(s => s.OrderPriority);
                            break;
                    }
                }
                else
                {
                    switch (Field)
                    {
                        case "region":
                            orderFunction = (sale) => sale.OrderByDescending(s => s.Region);
                            break;
                        case "country":
                            orderFunction = (sale) => sale.OrderByDescending(s => s.Country);
                            break;
                        case "itemType":
                            orderFunction = (sale) => sale.OrderByDescending(s => s.ItemType);
                            break;
                        case "salesChannel":
                            orderFunction = (sale) => sale.OrderByDescending(s => s.SalesChannel);
                            break;
                        case "orderPriority":
                            orderFunction = (sale) => sale.OrderByDescending(s => s.OrderPriority);
                            break;
                    }
                }
            }

            return orderFunction;
        }
    }
}