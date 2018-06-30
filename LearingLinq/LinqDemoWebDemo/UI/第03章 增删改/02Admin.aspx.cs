using LinqDemoWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinqDemoWebDemo.UI.第03章_增删改
{
    public partial class _02Admin : System.Web.UI.Page
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        GuestBookDataContext ctx = new GuestBookDataContext(ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetBind();
            }

        }


        protected void rpt_Message_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            StreamWriter sw = new StreamWriter(Server.MapPath("../../Log/log.txt"), true);
            ctx.Log = sw;
            tbGuestBook gb = ctx.tbGuestBook.Single(a => a.ID == new Guid(e.CommandArgument.ToString()));

            if (e.CommandName.Equals("DeleteMessage"))
            {
                ctx.tbGuestBook.DeleteOnSubmit(gb);
            }
            if (e.CommandName.Equals("SendReply"))
            {
                gb.Reply = ((TextBox)e.Item.FindControl("tb_Reply")).Text;
                gb.IsReplied = true;
            }
            ctx.SubmitChanges();
            SetBind();
            sw.Close();
        }

        private void SetBind()
        {
            rpt_Message.DataSource = from c in ctx.tbGuestBook orderby c.PostTime select c;
            rpt_Message.DataBind();
        }
    }
}