using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for BLLmanage
/// </summary>
public class BLLmanage
{
	public BLLmanage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   private tb_type dal_type = new tb_type();
    private tb_items dal_item = new tb_items();



    #region type项

    public void AddType(string typename, int AAS) {
        dal_type.Add(typename, AAS);
    }


    public DataTable GetTypeList(int AAS) {
        return dal_type.GetList(AAS);
    }
    public DataTable GetTypeList()
    {
        return dal_type.GetList();
    }



    public int GetAASByID(int id) {
        return dal_type.GetAAS(id);
    }

    public string GetTypeNameByID(int id) {
        return dal_type.GetTypeName(id);
    }



    public void Update(int type_id, string type_name, int type_AAS) {
        dal_type.Update(type_id, type_name, type_AAS);
    }

    #endregion



    #region items项

    public void AddItem(string item_name, int item_AAS, decimal item_amount, int item_date, string item_content, int item_type) {

        dal_item.Add(item_name, item_AAS, item_amount, item_date, item_content, item_type);
    }




    //根据分类id得到全部
    public DataTable GetItemsList(int type_id) {
        return dal_item.GetList(type_id);
    }


    //得到今天的收入或者支出明细
    public DataTable GetItemsList(string type_id)
    {
        int tm = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
        if (type_id == "today_0")
        {
            return dal_item.GetList(0, tm);
        }
        if (type_id == "today_1")
        {
            return dal_item.GetList(1, tm);
        }
        return null;
    }




    //高级查询
    public DataTable GetItemsList(int _typeid, int _AAS, string _start, string _end, decimal _amount_min, decimal _amount_max)
    {
        return dal_item.GetList(_typeid, _AAS, _start, _end, _amount_min, _amount_max);
    
    }

 





    //统计信息
    public DataTable GetStatistics(string st,int aas)
    {
        return dal_item.GetStatistics(st,aas);
    }
    public DataTable GetStatistics()
    {
        return dal_item.GetStatistics();
    }











    public string GetItemContentByID(int item_id)
    {
        return dal_item.GetItemContent(item_id);
    }

    public string GetTypeNameByItemID(int item_id) {
        int type_id = dal_item.GetItemTypeIDByID(item_id);
        string typename = dal_type.GetTypeName(type_id);
        return typename;
    }

    #endregion
}
