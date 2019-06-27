using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Ext_FM_crud : System.Web.UI.Page
{


    BLLmanage manage = new BLLmanage();






    protected void Page_Load(object sender, EventArgs e)
    {




        string app = Request["app"];

        if (app == "addtype")
        {
            AddType();
        }
        if (app == "items")
        {
            GetItems();
        }
        if (app == "additem")
        {
            AddItem();
        }
        if (app == "content")
        {
            GetContent();
        }
        if (app == "search")
        {
            SearchItemsString();
        }
        if (app == "statistics")
        {
            StatisticsString();
        }
        if (app == "statisticstype")
        {
            StatisticsTypeString();
        }
        if (app == "updatetype")
        {
            UpdateType();
        }




    }


    #region 更新
    private void UpdateType()
    {

        string result = "{success:true}";

        int id = 0;
        string typename = "";
        int AAS = 0;

        try
        {
            id = Int32.Parse(Request["_id"].ToString());
            typename = Request["_name"].ToString();
            AAS = Int32.Parse(Request["_aas"].ToString());
            manage.Update(id, typename, AAS);
        }
        catch
        {
            result = "{success:false}";
        }
        finally
        {
            Response.Write(result);
        }

    }

    #endregion



    

    #region 添加
    private void AddType()
    {
        string typename = Request["typename"].ToString();
        int AAS = Int32.Parse(Request["AAS"].ToString());
        manage.AddType(typename, AAS);
        Response.Write("{success:true}");

    }





    public void AddItem()
    {
        string _name = Request["_name"].ToString();
        int _type = Int32.Parse(Request["_type"].ToString());
        decimal _amount = decimal.Parse(Request["_amount"].ToString());
        Int32 _date = Int32.Parse(Request["_date"].ToString());
        string _content = Request["_content"].ToString();
        int _AAS = manage.GetAASByID(_type);
        manage.AddItem(_name, _AAS, _amount, _date, _content, _type);
        Response.Write("{success:true}");
    }
    #endregion





    #region 统计

    #region 按照类别统计
    private void StatisticsTypeString()
    {
        int PageSize = DbHelperSQL.PAGESIZE;

        int start = 0;
        if (!string.IsNullOrEmpty(Request["start"]))
        {
            start = Int32.Parse(Request["start"]);
        }

        DataTable dt = manage.GetStatistics();
        string jsonstring = "{   RowCount:" + dt.Rows.Count + ", Result:[";
        for (int i = start; (i < dt.Rows.Count && i < start + PageSize); i++)
        {
            if (i != start) jsonstring += ",";
            jsonstring += "{";
            jsonstring += "'item_AAS':'" + GetAASString(dt.Rows[i]["item_AAS"].ToString()) + "',";
            jsonstring += "'item_type':'" + manage.GetTypeNameByID(Int32.Parse(dt.Rows[i]["item_type"].ToString())) + "',";
            jsonstring += "'item_amount':'" + dt.Rows[i]["s_amount"].ToString() + "'";
            jsonstring += "}";
        }

        jsonstring += "]}";
        Response.Write(jsonstring);
    }
    #endregion

    


    #region   按照年月日统计收入支出
    private void StatisticsString()
    {
        int PageSize = DbHelperSQL.PAGESIZE;



        int aas = Int32.Parse(Request["aas"]);



        string id = Request["id"].ToString();            //统计的是年,月,日 


        int start = 0;
        if (!string.IsNullOrEmpty(Request["start"]))
        {
            start = Int32.Parse(Request["start"]);
        }

        DataTable dt = manage.GetStatistics(id, aas);
        string jsonstring = "{   RowCount:" + dt.Rows.Count + ", Result:[";




        for (int i = start; (i < dt.Rows.Count && i < start + PageSize); i++)
        {
            if (i != start) jsonstring += ",";
            jsonstring += "{";
            jsonstring += "'item_AAS':'" + dt.Rows[i]["s_AAS"].ToString() + "',";
            jsonstring += "'item_amount':'" + dt.Rows[i]["s_amount"].ToString() + "',";
            jsonstring += "'item_date':'" + dt.Rows[i]["s_date"].ToString() + "'";
            jsonstring += "}";
        }

        jsonstring += "]}";
        Response.Write(jsonstring);
    }

    #endregion


    #endregion





    #region 高级查询

    private void SearchItemsString()
    {

        int PageSize = DbHelperSQL.SEARCH_PAGESIZE;
        int start = 0;
        try
        {
            start = Int32.Parse(Request["start"].ToString());
        }
        catch
        {
            start = 0;
        }

        int _AAS = -1;
        if (!string.IsNullOrEmpty(Request["_AAS"]))
        {
            if (Request["_AAS"].ToString() == "收入") _AAS = 1;
            if (Request["_AAS"].ToString() == "支出") _AAS = 0;
        }
        int _typeid = -1;
        if (!string.IsNullOrEmpty(Request["_typeid"]))
        {
            _typeid = Int32.Parse(Request["_typeid"]);
        }
        string _start = "-1";
        if (!string.IsNullOrEmpty(Request["_start"]))
        {
            _start = Request["_start"].ToString();
            if (_start == "undefined") _start = "-1";
        }
        string _end = "-1";
        if (!string.IsNullOrEmpty(Request["_end"]))
        {
            _end = Request["_end"].ToString();
            if (_end == "undefined") _end = "-1";
        }
        decimal _amount_min = -1;
        if (!string.IsNullOrEmpty(Request["_amount_min"]))
        {
            _amount_min = Int32.Parse(Request["_amount_min"]);
        }
        decimal _amount_max = -1;
        if (!string.IsNullOrEmpty(Request["_amount_max"]))
        {
            _amount_max = Int32.Parse(Request["_amount_max"]);
        }
        DataTable dt = manage.GetItemsList(_typeid, _AAS, _start, _end, _amount_min, _amount_max);

        string jsonstring = "{   RowCount:" + dt.Rows.Count + ", Result:[";
        for (int i = start; (i < dt.Rows.Count && i < start + PageSize); i++)
        {
            if (i != start) jsonstring += ",";
            jsonstring += "{";
            jsonstring += "'item_id':'" + dt.Rows[i]["item_id"].ToString() + "',";
            jsonstring += "'item_AAS':'" + GetAASString(dt.Rows[i]["item_AAS"].ToString()) + "',";
            jsonstring += "'item_type':'" + manage.GetTypeNameByID(Int32.Parse(dt.Rows[i]["item_type"].ToString())) + "',";
            jsonstring += "'item_name':'" + dt.Rows[i]["item_name"].ToString() + "',";
            jsonstring += "'item_amount':'" + dt.Rows[i]["item_amount"].ToString() + "',";
            jsonstring += "'item_date':'" + dt.Rows[i]["item_date"].ToString() + "'";
            jsonstring += "}";
        }
        jsonstring += "]}";
        Response.Write(jsonstring);

    }
    #endregion




    #region 获取某条记录的备注
    private void GetContent()
    {
        int id = Int32.Parse(Request["id"].ToString());
        string result = "{success:true,msg:'";
        result += manage.GetItemContentByID(id);
        result += "'}";
        Response.Write(result);

    }
    #endregion

  

    #region 获取分类列表,今天项的详细数据
    private void GetItems()
    {

        int PageSize = DbHelperSQL.PAGESIZE;
        int start = 0;
        try
        {

            start = Int32.Parse(Request["start"].ToString());
        }
        catch
        {
            start = 0;

        }
        DataTable dt = new DataTable();
        try
        {
            int item_type = Int32.Parse(Request["item_type"].ToString());
            dt = manage.GetItemsList(item_type);
        }
        catch
        {
            string item_type = Request["item_type"].ToString();
            if (item_type == "today_0" || item_type == "today_1")
            {
                dt = manage.GetItemsList(item_type);
            }
        }
        string jsonstring = "{   RowCount:" + dt.Rows.Count + ", Result:[";
        for (int i = start; (i < dt.Rows.Count && i < start + PageSize); i++)
        {
            if (i != start) jsonstring += ",";
            jsonstring += "{";
            jsonstring += "'item_id':'" + dt.Rows[i]["item_id"].ToString() + "',";
            jsonstring += "'item_AAS':'" + GetAASString(dt.Rows[i]["item_AAS"].ToString()) + "',";
            jsonstring += "'item_type':'" + manage.GetTypeNameByID(Int32.Parse(dt.Rows[i]["item_type"].ToString())) + "',";
            jsonstring += "'item_name':'" + dt.Rows[i]["item_name"].ToString() + "',";
            jsonstring += "'item_amount':'" + dt.Rows[i]["item_amount"].ToString() + "',";
            jsonstring += "'item_date':'" + dt.Rows[i]["item_date"].ToString() + "'";
            jsonstring += "}";
        }
        jsonstring += "]}";
        Response.Write(jsonstring);

    }

    #endregion




    #region 将AAS转化成"收入","支出"
    private string GetAASString(string i)
    {
        return i == "1" ? "收入" : "支出";
    }
    #endregion

}