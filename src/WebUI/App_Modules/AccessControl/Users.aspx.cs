using System;
using System.Web.UI.WebControls;

using InfoControl;
using InfoControl.Web.Security;
using InfoControl;
using Vivina.Erp.DataClasses;
using Vivina.Erp.BusinessRules;
using System.Web.Services;

[PermissionRequired("Users")]
public partial class ListUsers : Vivina.Erp.SystemFramework.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErr.Visible = false;
    }

    /// <summary>
    ///  Esse m�todo, verifica o n�mero de usu�rios cadastrados no sistema atrav�s do CompanyReferenceId
    ///ou seja, todas as Empresas interligadas. Sendo maior ou igual do que o n�mero m�ximo permitido pelo
    ///plano, o bot�o de inser��o, some da tela, impossibilitando um novo cadastro de usu�rio.
    /// </summary>
    /// <param name="e"></param>

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        CompanyManager cManager = new CompanyManager(this);

    }

    protected void odsUsers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["companyId"] = Company.CompanyId;
    }

    protected void grdListUsers_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortExpression == "Insert")
        {
            Server.Transfer("User.aspx");
        }
    }
    protected void grdListUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(grdListUsers.DataKeys[e.Row.RowIndex]["UserId"]) == User.Identity.UserId)
                e.Row.Cells[e.Row.Cells.Count - 1].Attributes["style"] = "display:none";

            e.Row.Cells[e.Row.Cells.Count - 1].Attributes.Add("onclick", "event.cancelBubble=true;javascript:if(confirm('O registro ser� excluido e n�o poder� mais ser recuperado, deseja realmente efetuar a opera��o?') == false) return false;");
            e.Row.Attributes["onclick"] = "location='User.aspx?UserId=" + e.Row.DataItem.GetPropertyValue("UserId").EncryptToHex() + "';";
        }
    }


    [WebMethod]
    public static bool DisassociateUser(int userId, int companyId)
    {
        bool result = true;
        using (CompanyManager companyManager = new CompanyManager(null))
        {
            try
            {
                companyManager.DisassociateUser(userId, companyId);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                result = false;
            }
        }
        return result;
    }
}
