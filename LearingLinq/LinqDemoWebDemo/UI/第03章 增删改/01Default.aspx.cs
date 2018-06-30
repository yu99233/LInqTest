using LinqDemoWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinqDemoWebDemo.UI.第3章_增删改
{
    public partial class _01Default : System.Web.UI.Page
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        GuestBookDataContext ctx = new GuestBookDataContext(ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SetBing();
            }
        }

        protected void Btn_SendMessage_Click(object sender, EventArgs e)
        {
            tbGuestBook gb = new tbGuestBook
            {
                ID = Guid.NewGuid(),
                UserName = tb_UserName.Text,
                Message = tb_Message.Text,
                IsReplied = false,
                PostTime = DateTime.Now
            };

            ctx.tbGuestBook.InsertOnSubmit(gb);
            ctx.SubmitChanges();

            SetBing();
        }

        private void SetBing()
        {
            rpt_Message.DataSource = from c in ctx.tbGuestBook orderby c.PostTime select c;
            rpt_Message.DataBind();
        }
    }
}