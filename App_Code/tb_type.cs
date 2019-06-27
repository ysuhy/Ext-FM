using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
using System.Data.OleDb;

/// <summary>
/// Summary description for tb_type
/// </summary>
public class tb_type
{
    public tb_type()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    #region 添加
    public void Add(string type_name, int type_AAS)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into tb_type(");
        strSql.Append("type_name,type_AAS)");
        strSql.Append(" values (");
        strSql.Append("@type_name,@type_AAS)");
        OleDbParameter[] parameters = {
					new OleDbParameter("@type_name", OleDbType.VarChar,50),
					new OleDbParameter("@type_AAS", OleDbType.Integer,4)};
        parameters[0].Value = type_name;
        parameters[1].Value = type_AAS;

        DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
    }

    #endregion




    #region 更新

    public void Update(int type_id, string type_name, int type_AAS)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update tb_type set ");
        strSql.Append("type_name=@type_name,");
        strSql.Append("type_AAS=@type_AAS");
        strSql.Append(" where type_id=@type_id ");
        OleDbParameter[] parameters = {					
					new OleDbParameter("@type_name", OleDbType.VarChar,50),
					new OleDbParameter("@type_AAS", OleDbType.Integer,4),
                    new OleDbParameter("@type_id", OleDbType.Integer,4)};
        parameters[0].Value = type_name;
        parameters[1].Value = type_AAS;
        parameters[2].Value = type_id;

        DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
    }

    #endregion




    #region 获取列表
    public DataTable GetList(int AAS)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select type_id,type_name,type_AAS ");
        strSql.Append(" FROM tb_type where type_AAS=@AAS");
        OleDbParameter[] parameters = {				 
					new OleDbParameter("@AAS", OleDbType.Integer,4)};
        parameters[0].Value = AAS;
        return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
    }
    public DataTable GetList()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select type_id,type_name,type_AAS ");
        strSql.Append(" FROM tb_type");
        return DbHelperSQL.Query(strSql.ToString()).Tables[0];
    }
    #endregion




    #region 杂项
    public int GetAAS(int id)
    {
        string sql = "select type_AAS from tb_type where type_id=@id";
        OleDbParameter[] parameters = {				 
					new OleDbParameter("@id", OleDbType.Integer,4)};
        parameters[0].Value = id;
        return (int)DbHelperSQL.GetSingle(sql, parameters);

    }

    public string GetTypeName(int id)
    {
        string sql = "select type_name from tb_type where type_id=@id";
        OleDbParameter[] parameters = {				 
					new OleDbParameter("@id", OleDbType.Integer,4)};
        parameters[0].Value = id;
        return DbHelperSQL.GetSingle(sql, parameters).ToString();

    }

    #endregion

}
