using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinqDemoWebDemo.UI.第二章_DataContext
{
    public partial class _02 : System.Web.UI.Page
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connectionStringCreateDB"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            //CreateDatabase();

            UseDbDataReader();
        }

        private void UseDbDataReader()
        {
            var con = new SqlConnection(ConnectionString);

            var ctx = new DataContext(con);

            var cmd = new SqlCommand("SELECT * FROM dbo.test WHERE Name LIKE '张%'", con);

            con.Open();

            var reader = cmd.ExecuteReader();

            //Translate   将现有 System.Data.Common.DbDataReader 转换为对象
            GridView1.DataSource = ctx.Translate<test>(reader);

            GridView1.DataBind();

            con.Close();
        }


        /// <summary>
        /// 创建数据库
        /// </summary>
        private void CreateDatabase()
        {
            textdbContext ctx = new textdbContext(ConnectionString);
            ctx.CreateDatabase();
            //ctx.DeleteDatabase();
        }
    }




    [Table(Name = "test")]
    public class test
    {
        //IsDbGenerated  该值指示列是否包含数据库自动生成的值
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(DbType = "varchar(50)")]
        public string Name { get; set; }
    }

    public partial class textdbContext : DataContext
    {
        public Table<test> test;
        public textdbContext(string connection) : base(connection)
        {

        }
    }
}