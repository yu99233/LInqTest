using LinqDemoWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinqDemoWebDemo.UI
{
    public partial class _01 : System.Web.UI.Page
    {
        private static readonly string constr = "server=.;database=TEST;uid=sa;pwd=912167";
        protected void Page_Load(object sender, EventArgs e)
        {
            OneMethod();

            TwoMethod();

            ThreeMethod();

            //日志功能
            LogMethod();

        }

        private void LogMethod()
        {
            NorthwindDataContext ctx = new NorthwindDataContext(constr);
            StreamWriter sw = new StreamWriter(Server.MapPath("../Log/log.txt"), true);
            ctx.Log = sw;
            GridView4.DataSource= from c in ctx.Customers
                                  select new
                                  {
                                      顾客编号 = c.Id,
                                      顾客姓名 = c.Name,
                                      所在地 = c.City
                                  };
            GridView4.DataBind();
            sw.Close();
        }

        private void OneMethod()
        {
            DataContext ctx = new DataContext(constr);

            Table<Customer> customers = ctx.GetTable<Customer>();

            GridView1.DataSource = from c in customers
                                   where c.Id.StartsWith("A")
                                   select new
                                   {
                                       顾客ID = c.Id,
                                       顾客名 = c.Name,
                                       城市 = c.City
                                   };
            GridView1.DataBind();
        }

        private void TwoMethod()
        {
            IDbConnection conn = new SqlConnection(constr);

            DataContext ctx = new DataContext(conn);

            Table<Customer> customer = ctx.GetTable<Customer>();

            GridView2.DataSource = from c in customer
                                   select new
                                   {
                                       顾客编号 = c.Id,
                                       顾客姓名 = c.Name,
                                       所在地 = c.City
                                   };
            GridView2.DataBind();
        }

        private void ThreeMethod()
        {
            NorthwindDataContext ctx = new NorthwindDataContext(constr);

            GridView3.DataSource = from c in ctx.Customers
                                   select new
                                   {
                                       顾客编号 = c.Id,
                                       顾客姓名 = c.Name,
                                       所在地 = c.City
                                   };
            GridView3.DataBind();
        }
    }


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