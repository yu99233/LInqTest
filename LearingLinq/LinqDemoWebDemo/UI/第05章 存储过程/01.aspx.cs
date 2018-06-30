using LinqDemoWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinqDemoWebDemo.UI.第05章_存储过程
{
    public partial class _01 : System.Web.UI.Page
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        GuestBookDataContext ctx = new GuestBookDataContext(ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            单结果集存储过程();

            带参数的存储过程();

            带返回值的存储过程();

            多结果集的存储过程();

            //使用存储过程新增数据();

            //使用存储过程删除数据();

            //使用存储过程更改数据();
        }

        private void 使用存储过程更改数据()
        {
            throw new NotImplementedException();
        }

        private void 使用存储过程删除数据()
        {
            throw new NotImplementedException();
        }

        private void 使用存储过程新增数据()
        {
            throw new NotImplementedException();
        }

        private void 多结果集的存储过程()
        {
            var moreResult = ctx.sp_multiresultset();
            var customers = moreResult.Where(a=>true);
            GridView2.DataSource = customers;
            GridView2.DataBind();

        }

        private void 单结果集存储过程()
        {
            GridView1.DataSource = from c in ctx.sp_singleresultset()
                                   select c;
            GridView1.DataBind();
        }

        private void 带参数的存储过程()
        {
            int? rowCount = -1;
            ctx.sp_withparameter("", ref rowCount);
            Response.Write(rowCount + "<br/>");
            ctx.sp_withparameter("ABC", ref rowCount);
            Response.Write(rowCount + "<br/>");
        }

        private void 带返回值的存储过程()
        {
            Response.Write(ctx.sp_withreturnvalue("")+"<br>");
            Response.Write(ctx.sp_withreturnvalue("ABC")+"<br>");
        }
    }
}