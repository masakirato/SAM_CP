#region Using
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class Views_RolePermission : System.Web.UI.Page
{
    #region Private Variable
    private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Control Events

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (pnlForm.Visible == true)
            //{

           // TreeView2.Attributes.Add("onclick", "postBackByObject()");

            InitialTreeView();
            // }

            ShowGridPanel();
        }
    }

    protected void ButtonCreateNew_Click(object sender, EventArgs e)
    {
        //
        GridViewRole.EditIndex = -1;
        //DataTable dt = dbo_RoleDataClass.SelectAll();
        //GridViewRole.DataSource = dt.DefaultView;
        //GridViewRole.DataBind();
        GridViewRole.ShowFooter = true;
        SearchSubmit();

        try
        {

            TextBox _Row_ID = (TextBox)GridViewRole.FooterRow.FindControl("txtNewRole_ID");
            _Row_ID.Enabled = false;
            _Row_ID.Text = GenerateID.Role_ID();

            TextBox _Row_Name = (TextBox)GridViewRole.FooterRow.FindControl("txtNewRole_Name");
            DropDownList _Row_Type = (DropDownList)GridViewRole.FooterRow.FindControl("ddlNewRole_Type");

            _Row_Name.Focus();
        }
        catch (Exception)
        {

        }

        //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", "  window.scrollTo(0, document.body.clientHeight);", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
        //   "selectNode", @"var node = document.getElementById('ContentPlaceHolder1_TreeView2t22');node.scrollIntoView(true);elem.scrollLeft=0;", true);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        dbo_RolePermissionClass role = new dbo_RolePermissionClass();
        role.Role_ID = txtRole_ID.Text;
        dbo_RolePermissionDataClass.Delete(role);

        try
        {
            TreeNode oMainNode = (TreeNode)TreeView2.Nodes[0];


            PrintNodesRecursive(oMainNode);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        // Show("บันทึกสำเร็จ");
        //pnlGrid.Visible = true;
        //pnlForm.Visible = false;

        //Response.Redirect(Request.UrlReferrer.ToString());
        //Response.Redirect("../Views/RolePermission.aspx");
        //string script = @"swal(""บันทึกสำเร็จ!"", """", ""success"")";
        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);

        string script = string.Format("alert('{0}');window.location ='RolePermission.aspx';", "บันทึกสำเร็จ");
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        pnlGrid.Visible = true;
        pnlForm.Visible = false;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //InitialTreeView();

        ShowGridPanel();

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", @"__doPostBack("""", """");", true);
    }

    protected void btnShowGrid_Click(object sender, EventArgs e)
    {
        ShowGridPanel();

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", @"__doPostBack("""", """");", true);
    }

    protected void ddlRoleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchSubmit();
    }

    protected void OnTreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        //if (e.Node.Checked)
        //{
        //    // e.Node.Parent.Checked = true;
        //    if (e.Node.Parent != null)
        //    {
        //        e.Node.Parent.Checked = e.Node.Parent.ChildNodes.Cast<TreeNode>().All(n => n.Checked);
        //        TreeView2.ShowLines = false;
        //    }
        //}
        //else
        //{
        //    if (e.Node.Parent != null)
        //    {
        //        e.Node.Parent.Checked = e.Node.Parent.ChildNodes.Cast<TreeNode>().Any(n => n.Checked);
        //    }
        //}


        //// Determine if checked Node is a root node.
        //if (e.Node.ChildNodes.Count > 0)
        //{
        //    // Check or uncheck all of the child nodes based on status of parent node.
        //    if (e.Node.Checked)
        //    {

        //        ChangeChecked(e.Node, true);

        //    }
        //    else
        //    {
        //        ChangeChecked(e.Node, false);
        //    }

        //}


        if (TreeView2.CheckedNodes.Count > 0)
        {
            // the selected nodes.
            foreach (TreeNode node in TreeView2.CheckedNodes)
            {

                if (node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode childNode in node.ChildNodes)
                    {
                        childNode.Checked = true;
                    }
                }

            }

        }


        // TreeView2.ClientID
        // ScrollToMyFavouriteControl(e.Node.Parent);

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ScrollToSelectedNode", "ScrollToSelectedNode();", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
        //    "selectNode", @" var o = window.event.srcElement;  if (o.tagName == ""INPUT"" && o.type == ""checkbox"") { alert(o.id);  };var node = document.getElementById('ContentPlaceHolder1_TreeView2t22');node.scrollIntoView(true);elem.scrollLeft=0;", true);



    }

    protected void OnSelectedNodeChanged(object sender, EventArgs e)
    {
        if (((TreeView)sender).SelectedNode.ChildNodes.Count > 0)
        {
            if ((bool)((TreeView)sender).SelectedNode.Expanded)
            {
                ((TreeView)sender).SelectedNode.Collapse();  
            }
            else
            {
                ((TreeView)sender).SelectedNode.Expand();
            }
        }

        // Deselects the SelectedNode so it can be toggled without clicking on another node first.
        ((TreeView)sender).SelectedNode.Selected = false;
       
    }
    #endregion

    #region Methods
    private void InitialTreeView()
    {
      
        TreeView2.Nodes.Clear();
        List<MenuNode> RootNode = dbo_RolePermissionDataClass.GetMenuNode(-1);
        BindTree(RootNode, null);
        TreeView2.ExpandAll();
        TreeView2.ShowLines = false;
        

    }  

    private void ChangeChecked(TreeNode node, bool check)
    {
        // "Queue" up child nodes to be checked or unchecked.
        if (node.ChildNodes.Count > 0)
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
                ChangeChecked(node.ChildNodes[i], check);
        }


        node.Checked = check;
    }

    private void BindTree(IEnumerable<MenuNode> list, TreeNode parentNode)
    {
        var nodes = list.Where(x => parentNode == null ? x.Parent_MenuID == -1 : x.Parent_MenuID == int.Parse(parentNode.Value));

        foreach (var node in nodes)
        {

           
            TreeNode newNode = new TreeNode(node.Function_Name, node.MenuID.ToString());

          
            // newNode
            newNode.ToolTip = node.Function_ID;
            if (node.Function_ID == "0000")
            {
                newNode.ShowCheckBox = false;
            }
            else
            {
                newNode.ShowCheckBox = true;
            }
           

            dbo_RolePermissionClass item = dbo_RolePermissionDataClass.GetRolePermissionByRole_IDAndFunctionName(txtRole_ID.Text, node.Function_ID);

            newNode.Checked = (item == null ? false : true);

            if (parentNode == null)
            {
                TreeView2.ShowLines = false;
                TreeView2.Nodes.Add(newNode);

            }
            else
            {
                parentNode.ChildNodes.Add(newNode);


            }
            BindTree(list, newNode);
        }
    }

    private List<MenuNode> GetMenuNode(MenuNode RootNode)
    {
        try
        {
            List<MenuNode> node = dbo_RolePermissionDataClass.GetMenuNode(RootNode.MenuID);
            for (int i = 0; i < node.Count; i++)
            {
                node[i].ListOfMenuNode = GetMenuNode(node[i]);
            }
            return node;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            return null;
        }


    }   

    public void PrintNodesRecursive(TreeNode oParentNode)
    {
        if (oParentNode.Checked)
        {
            dbo_RolePermissionClass role = new dbo_RolePermissionClass();
            role.Function_Name = oParentNode.ToolTip;
            role.Role_ID = txtRole_ID.Text;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_RolePermissionDataClass.Add(role, User_ID);
        }

        foreach (TreeNode oSubNode in oParentNode.ChildNodes)
        {
            PrintNodesRecursive(oSubNode);
        }
    }

    private void CheckAllChildNodes1(TreeNode treeNodeAdv)
    {
        foreach (TreeNode nd in treeNodeAdv.ChildNodes)
        {
            nd.Checked = treeNodeAdv.Checked;
        }
    }

    private void UncheckChildNodes1(TreeNode treeNodeAdv)
    {
        foreach (TreeNode nd in treeNodeAdv.ChildNodes)
        {
            nd.Checked = treeNodeAdv.Checked;
        }
    }    

    private void ShowGridPanel()
    {
        pnlGrid.Visible = true;
        pnlForm.Visible = false;

        //DataTable dt = dbo_RoleDataClass.SelectAll();
        //GridViewRole.DataSource = dt.DefaultView;
        //GridViewRole.DataBind();

        // List<dbo_RoleClass> item = dbo_RoleDataClass.Search(string.Empty, string.Empty, ddlRoleType.SelectedValue);
        //List<dbo_RoleClass> item = new List<dbo_RoleClass>();
        //GridViewRole.DataSource = item;
        //GridViewRole.DataBind();
        SearchSubmit();
    }

    private void ShowFormPanel(string Role_ID)
    {
        pnlGrid.Visible = false;
        pnlForm.Visible = true;


        if (!string.IsNullOrEmpty(Role_ID))
        {
            dbo_RoleClass role = dbo_RoleDataClass.Select_Record(Role_ID);

            txtRole_ID.Text = role.Role_ID;
            txtRole_Name.Text = role.Role_Name;           


            TreeView2.Nodes.Clear();
            TreeView2.ShowLines = false;

            List<MenuNode> RootNode = dbo_RolePermissionDataClass.GetMenuNode(int.Parse(txtRole_ID.Text));
            BindTree(RootNode, null);


            TreeView2.ExpandAll();
            TreeView2.ShowLines = false;
        }


    }    
    
    public void Show(string message)
    {
        try
        {
            string cleanMessage = message.Replace("'", "\'");
            // Page page = HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            //if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            //{
            //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);


            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
        //}
    }

    private void Alert(string type, string key)
    {
        string script = string.Empty;
        switch (type)
        {
            case "Success":
                script = Messages.SuccessAlert();
                break;
            case "Duplicate":

                script = Messages.DuplicateKeyAlert("Role ID");

                break;
            case "Error":
                script = Messages.ExceptionAlert();
                break;

            case "UsedKey":
                script = Messages.UsedKeyAlert("Role ID");
                break;

            case "SuccessDelete":
                script = Messages.SuccessDeleteAlert();
                break;
        }


        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAM", script, true);

    }

    private void SearchSubmit()
    {
        try
        {
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
            dbo_UserClass user_class = dbo_UserDataClass.Select_Record(User_ID);
            List<dbo_RoleClass> item = new List<dbo_RoleClass>();
            if (user_class.User_Group_ID == "Agent")
            {
                item = dbo_RoleDataClass.Search(string.Empty, string.Empty, "Agent");
            }
            else
            {
                item = dbo_RoleDataClass.Search(string.Empty, string.Empty, string.Empty);
            }

            //List<dbo_RoleClass> item = new List<dbo_RoleClass>();
            //if (ddlRoleType.SelectedIndex == 0)
            //{
            //    item = dbo_RoleDataClass.Search(string.Empty, string.Empty, string.Empty);

            //}
            //else
            //{
            //    item = dbo_RoleDataClass.Search(string.Empty, string.Empty, ddlRoleType.Text);
            //}

            //GridViewRole.ShowFooter = false;

            if (item.Count == 0)
            {
                item.Add(new dbo_RoleClass());
                GridViewRole.DataSource = item;
                GridViewRole.DataBind();
                GridViewRole.Rows[0].Visible = false;
            }
            else
            {
                GridViewRole.DataSource = item;
                GridViewRole.DataBind();
            }


        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region GridView Events
    protected void GridViewRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int index = e.RowIndex;
        Label _lblRole_ID = (Label)GridViewRole.Rows[e.RowIndex].FindControl("lblRole_ID");

        List<dbo_UserClass> user = dbo_UserDataClass.Search(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, _lblRole_ID.Text, string.Empty, null, string.Empty, string.Empty);

        dbo_RoleClass role = new dbo_RoleClass();

        role.Role_ID = _lblRole_ID.Text;

        dbo_RoleDataClass.Delete(role);

        dbo_RolePermissionClass role_per = new dbo_RolePermissionClass();
        role_per.Role_ID = _lblRole_ID.Text;
        dbo_RolePermissionDataClass.Delete(role_per);

        System.Threading.Thread.Sleep(500);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
        Show("ลบข้อมูลสำเร็จ");

        GridViewRole.ShowFooter = false;

        SearchSubmit();

    }
    
    protected void GridViewRole_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRole.EditIndex = e.NewEditIndex;


        //DataTable dt = dbo_RoleDataClass.SelectAll();
        GridViewRole.ShowFooter = false;
        //GridViewRole.DataSource = dt.DefaultView;
        //GridViewRole.DataBind();

        SearchSubmit();


        LinkButton btnEdit_ = (LinkButton)GridViewRole.Rows[e.NewEditIndex].FindControl("lnkB_SetPermission");
        btnEdit_.Visible = false;
    }
    
    protected void GridViewRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int RowIndex = Convert.ToInt32((e.RowIndex).ToString());

        Label _lblRole_ID = (Label)GridViewRole.Rows[RowIndex].FindControl("lblRole_ID");
        TextBox _Row_Name = (TextBox)GridViewRole.Rows[RowIndex].FindControl("txtEditRole_Name");
        DropDownList _Role_Type = (DropDownList)GridViewRole.Rows[RowIndex].FindControl("ddlEditRole_Type");

        if (string.IsNullOrEmpty(_Row_Name.Text) || _Role_Type.SelectedIndex == 0)
        {

            System.Threading.Thread.Sleep(500);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
            Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
        }
        else
        {
            dbo_RoleClass role = new dbo_RoleClass() { Role_ID = _lblRole_ID.Text, Role_Name = _Row_Name.Text, Role_Type = _Role_Type.SelectedValue };

            bool success = false;
            string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;

            success = dbo_RoleDataClass.Update(role, User_ID);


            if (!success)
            {

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกไม่สำเร็จ กรุณาติดต่อผู้ดูแลระบบ");
                //Alert("Error", string.Empty);
            }
            else
            {
                //Alert("Success", string.Empty);

                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("บันทึกสำเร็จ");
                GridViewRole.EditIndex = -1;
                GridViewRole.ShowFooter = false;

                SearchSubmit();

                //DataTable dt = dbo_RoleDataClass.SelectAll();
                //GridViewRole.DataSource = dt.DefaultView;
                //GridViewRole.DataBind();

                LinkButton btnEdit_ = (LinkButton)GridViewRole.Rows[RowIndex].FindControl("lnkB_SetPermission");
                btnEdit_.Visible = true;
            }
        }
    }

    protected void GridViewRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridViewRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewRole.EditIndex = -1;
        GridViewRole.ShowFooter = false;

        //DataTable dt = dbo_RoleDataClass.SelectAll();
        //GridViewRole.DataSource = dt.DefaultView;
        //GridViewRole.DataBind();
        SearchSubmit();

        if (GridViewRole.ShowFooter)
        {
            LinkButton btnEdit_ = (LinkButton)GridViewRole.Rows[e.RowIndex].FindControl("lnkB_SetPermission");
            btnEdit_.Visible = true;
        }
    }

    protected void GridViewRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditPermission")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            string Role_ID = lnkView.CommandArgument;

            ShowFormPanel(Role_ID);

        }
        else if (e.CommandName == "AddNew")
        {
            TextBox _Row_ID = (TextBox)GridViewRole.FooterRow.FindControl("txtNewRole_ID");
            TextBox _Row_Name = (TextBox)GridViewRole.FooterRow.FindControl("txtNewRole_Name");
            DropDownList _Role_Type = (DropDownList)GridViewRole.FooterRow.FindControl("ddlNewRole_Type");

            if (string.IsNullOrEmpty(_Row_Name.Text) || _Role_Type.SelectedIndex == 0)
            {
                System.Threading.Thread.Sleep(500);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                Show("กรุณากรอกข้อมูลที่จำเป็นให้ครบถ้วน");
            }
            else
            {
                dbo_RoleClass role = new dbo_RoleClass() { Role_ID = int.Parse(_Row_ID.Text).ToString("00").ToString(), Role_Name = _Row_Name.Text, Role_Type = _Role_Type.SelectedValue };
                string User_ID = HttpContext.Current.Request.Cookies["User_ID"].Value;
                bool success = dbo_RoleDataClass.Add(role, User_ID);

                if (success)
                {

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกสำเร็จ");
                }
                else
                {

                    System.Threading.Thread.Sleep(500);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "SAMWAIT", "myApp.hidePleaseWait();", true);
                    Show("บันทึกไม่สำเร็จ กรุณาติดต่อผู้ดูแลระบบ");
                }

                GridViewRole.ShowFooter = false;
                SearchSubmit();
            }
        }
    }
    #endregion     

}