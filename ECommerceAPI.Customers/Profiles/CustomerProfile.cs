using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
       public CustomerProfile()
        {
            CreateMap<Db.Customer, Models.CustomerModel>();
        }
    }
}
