using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace LinqDemoWebDemo.Models
{

    //添加引用      System.Data.Linq.Mapping;

    [Table(Name = "Customer")]
    public class Customer
    {
        [Column(IsPrimaryKey = true)]
        public string Id { get; set; }
        [Column(Name = "ContactName")]
        public string Name { get; set; }
        [Column]
        public string City { get; set; }
    }
}