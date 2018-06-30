using LinqDemoWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinqDemoWebDemo.UI.第二章_DataContext
{
    public partial class _01 : System.Web.UI.Page
    {
        private static readonly string constr = "server=.;database=TEST;uid=sa;pwd=912167";

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            OneMethod();

            TwoMethod();

            ThreeMethod();

            //日志功能
            LogMethod();

            //探究查询
            InquiryQuery();

            //执行查询
            ExecuteQuery();

        }

        /// <summary>
        /// 执行查询
        /// </summary>
        private void ExecuteQuery()
        {
            NorthwindDataContext ctx = new NorthwindDataContext(ConnectionString);

            string newName = "李四";

            ctx.ExecuteCommand("update Customer set City={0} where Id like 'A%'", newName);

            IEnumerable<Customer> customers = ctx.ExecuteQuery<Customer>("SELECT * FROM dbo.Customer WHERE Id LIKE 'A%';");

            GridView5.DataSource = customers;

            GridView5.DataBind();
        }

        private void OneMethod()
        {
            DataContext ctx = new DataContext(ConnectionString);

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

        /// <summary>
        /// 日志功能
        /// </summary>
        private void LogMethod()
        {
            NorthwindDataContext ctx = new NorthwindDataContext(constr);
            StreamWriter sw = new StreamWriter(Server.MapPath("../../Log/log.txt"), true);
            ctx.Log = sw;
            GridView4.DataSource = from c in ctx.Customers
                                   select new
                                   {
                                       顾客编号 = c.Id,
                                       顾客姓名 = c.Name,
                                       所在地 = c.City
                                   };
            GridView4.DataBind();
            sw.Close();
        }

        /// <summary>
        /// 探究查询
        /// </summary>
        private void InquiryQuery()
        {
            NorthwindDataContext ctx = new NorthwindDataContext(ConnectionString);

            var select = from c in ctx.Customers
                         where c.Id.StartsWith("A")
                         select new
                         {
                             顾客ID = c.Id,
                             顾客名 = c.Name,
                             城市 = c.City
                         };
            //GetCommand  获取提供有关由 LINQ to SQL 生成的 SQL 命令的信息
            DbCommand cmd = ctx.GetCommand(select);

            Response.Write(cmd.CommandText + "<br/>");

            foreach (DbParameter item in cmd.Parameters)
            {
                Response.Write(string.Format("参数名{0},参数值{1}<br/>", item.ParameterName, item.Value));
            }

            Customer customer = ctx.Customers.First();

            customer.Name = "喻大大";

            //GetChangeSet  获取由 System.Data.Linq.DataContext跟踪的被修改对象。
            //该对象集返回为三个只读的集合
            IList<object> queryText = ctx.GetChangeSet().Updates;

            Response.Write(((Customer)queryText[0]).Name);

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