using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace LinqDemoWebDemo.Models
{
    public partial class NorthwindDataContext : DataContext
    {
        public Table<Customer> Customers;
        public NorthwindDataContext(IDbConnection connection) : base(connection)
        {

        }

        public NorthwindDataContext(string connection) : base(connection)
        {

        }
    }
}