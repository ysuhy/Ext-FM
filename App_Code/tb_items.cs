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
/// Summary description for tb_items
/// </summary>
public class tb_items
{
	public tb_items()
	{
		//
		// TODO: Add constructor logic here
		//
    }


    #region 添加
    public void Add(string item_name,int item_AAS,decimal item_amount,int item_date,string item_content,int item_type)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into tb_item(");
        strSql.Append("item_name,item_AAS,item_amount,item_date,item_content,item_type)");
        strSql.Append(" values (");
        strSql.Append("@item_name,@item_AAS,@item_amount,@item_date,@item_content,@item_type)");
        OleDbParameter[] parameters = {
					new OleDbParameter("@item_name", OleDbType.VarChar,50),
					new OleDbParameter("@item_AAS", OleDbType.Integer,4),
					new OleDbParameter("@item_amount", OleDbType.Decimal),
					new OleDbParameter("@item_date", OleDbType.Integer,4),
					new OleDbParameter("@item_content", OleDbType.VarChar,0),
					new OleDbParameter("@item_type", OleDbType.Integer,4)};
        parameters[0].Value = item_name;
        parameters[1].Value = item_AAS;
        parameters[2].Value = item_amount;
        parameters[3].Value = item_date;
        parameters[4].Value = item_content;
        parameters[5].Value = item_type;

        DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
    }

    #endregion



    #region 列表项

    //某一分类的收入或者支出
    public DataTable GetList(int typeid){
        string sql = "select * from tb_item where item_type=@id order by  item_date desc";
        OleDbParameter[] parameters = {					 
					new OleDbParameter("@item_type", OleDbType.Integer,4)};      
        parameters[0].Value = typeid;
        return DbHelperSQL.Query(sql, parameters).Tables[0];
    }
    //某一天的收入或者支出
    public DataTable GetList(int item_AAS, int item_date)
    {
        string sql = "select * from tb_item where item_date=@item_date and item_AAS=@item_AAS order by  item_date desc";
        OleDbParameter[] parameters = {					 
					new OleDbParameter("@item_date", OleDbType.Integer,4),
                    new OleDbParameter("@item_AAS", OleDbType.Integer,4)};
        parameters[0].Value = item_date;
        parameters[1].Value = item_AAS;

        return DbHelperSQL.Query(sql, parameters).Tables[0];
    }

    public DataTable GetList(int _typeid, int _AAS, string _start, string _end, decimal _amount_min, decimal _amount_max)
    {

        string sql = " select * from tb_item where 1=1 ";
        if (_typeid != -1) { sql += " and item_type=" + _typeid; }
        if (_AAS != -1) { sql += " and item_AAS=" + _AAS; }
        if (_start != "-1") { sql += " and item_date>=" + _start; }
        if (_end != "-1") { sql += " and item_date<=" + _end; }
        if (_amount_min != -1) { sql += " and item_amount>=" + _amount_min; }
        if (_amount_max != -1) { sql += " and item_amount<=" + _amount_max; }
        sql += " order by item_date desc";
        return DbHelperSQL.Query(sql).Tables[0];

    }


    #endregion



    #region 杂项
    public string GetItemContent(int id)
    {
        string sql = "select item_content from tb_item where item_id=@id";
        OleDbParameter[] parameters = {				 
					new OleDbParameter("@id", OleDbType.Integer,4)};
        parameters[0].Value = id;
        return DbHelperSQL.GetSingle(sql, parameters).ToString();

    }
    public int GetItemTypeIDByID(int id)
    {
        string sql = "select item_type from tb_item where item_id=@id";
        OleDbParameter[] parameters = {				 
					new OleDbParameter("@id", OleDbType.Integer,4)};
        parameters[0].Value = id;
        return (int)DbHelperSQL.GetSingle(sql, parameters);

    }

    #endregion



    #region 统计项
    public DataTable GetStatistics(string st,int aas) {

        string sql = "";
        switch (st) { 
        
            case "day":
                sql = "select sum(item_amount) as s_amount,item_date as s_date,'"+ (aas==0?"支出":"收入") +"' as s_AAS  from tb_item where item_AAS=" + aas + " group by item_date";
                break;
            case "month":
                sql = "select sum(item_amount) as s_amount,LEFT(item_date,6) as s_date,'" + (aas == 0 ? "支出" : "收入") + "' as s_AAS  from tb_item where item_AAS=" + aas + " group by LEFT(item_date,6)";
                break;
            case "year":
                sql = "select sum(item_amount) as s_amount,LEFT(item_date,4) as s_date,'" + (aas == 0 ? "支出" : "收入") + "' as s_AAS  from tb_item where item_AAS=" + aas + " group by LEFT(item_date,4)";
                break;
            default :
                sql = "select sum(item_amount) as s_amount,item_date as s_date,'" + (aas == 0 ? "支出" : "收入") + "' as s_AAS  from tb_item where item_AAS=" + aas + " group by item_date";
                break;     
        } 

        return DbHelperSQL.Query(sql).Tables[0]; 
    }
    public DataTable GetStatistics()
    {

        string sql = "select sum(item_amount) as s_amount, item_AAS ,item_type from tb_item   group by item_type,item_AAS order by item_AAS";
        return DbHelperSQL.Query(sql).Tables[0];
    }


    #endregion

}
