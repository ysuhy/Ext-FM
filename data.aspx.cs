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

public partial class Ext_FM_data : System.Web.UI.Page
{
    BLLmanage manage = new BLLmanage();


    protected void Page_Load(object sender, EventArgs e)
    {
        string app = Request["app"].ToString();

        if (app == "first")
        {
            GetTreeMenu();
        }

        if (app == "tab")
        {
            GetTab();
        }

        if (app == "gettypeJSONstring")
        {
            GettypeJSONstring();
        }

    }



    #region 分类项
    private void GettypeJSONstring()
    {
        DataTable dt = manage.GetTypeList();
        string result = "[";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i != 0) { result += ","; }
            result += "{";
            result += "'type_id':'" + dt.Rows[i]["type_id"].ToString() + "',";
            result += "'type_name':'" + dt.Rows[i]["type_name"].ToString() + "',";
            result += "'type_AAS':'" + dt.Rows[i]["type_AAS"].ToString() + "'";
            result += "}";
        }
        result += "]";
        Response.Write(result);
    }


    #endregion



    #region 应用入口

    private void GetTab()
    {
        string id = Request["id"].ToString();
        string result = "";
        switch (id)
        {
            case "tjsrfl":
            case "tjzcfl":
            case "newcount":
            case "today_0":
            case "today_1":
            case "search":
            case "managetype":
                result = ManageString(id);
                break;


            case "Statistics_0_day":
            case "Statistics_0_month":
            case "Statistics_0_year":
            case "Statistics_1_day":
            case "Statistics_1_month":
            case "Statistics_1_year":
            case "Statistics_type":
                result = GetStatisticsString(id);
                break;


            default:
                result = "{title:'" + manage.GetTypeNameByID(Int32.Parse(id)) + "',id:'" + id + "',items:new ItemsPanel(" + id + ")}";
                break;
        }
        Response.Write(result);
    }

    #endregion




    #region 统计项
    private string  GetStatisticsString(string id) {
        string tempstring = "";
        switch (id) {
            case "Statistics_0_day":
                tempstring = "{title:'按日统计(支出)',id:'"+id+"',items:new StatisticsPanel('day',0)}";
                break;
            case "Statistics_0_month":
                tempstring = "{title:'按月统计(支出)',id:'"+id+"',items:new StatisticsPanel('month',0)}";
                break;
            case "Statistics_0_year":
                tempstring = "{title:'按年统计(支出)',id:'"+id+"',items:new StatisticsPanel('year',0)}";
                break;
            case "Statistics_1_day":
                tempstring = "{title:'按日统计(收入)',id:'" + id + "',items:new StatisticsPanel('day',1)}";
                break;
            case "Statistics_1_month":
                tempstring = "{title:'按月统计(收入)',id:'" + id + "',items:new StatisticsPanel('month',1)}";
                break;
            case "Statistics_1_year":
                tempstring = "{title:'按年统计(收入)',id:'" + id + "',items:new StatisticsPanel('year',1)}";
                break;
            case "Statistics_type":
                tempstring = "{title:'按类别统计',id:'" + id + "',items:new StatisticsTypePanel()}";
                break;
        }

        return tempstring;
    }
    #endregion




    #region 列表项
    private string ManageString(string id)
    {
        string tempstring = "";
        if (id.ToString() == "tjsrfl")
        {
            tempstring = "{title:'添加收入分类',id:'tjsrfl',items:new TJSRFLPanel()}";
        }
        if (id.ToString() == "tjzcfl")
        {
            tempstring = "{title:'添加支出分类',id:'tjzcfl',items:new TJZCFLPanel()}";
        }
        if (id.ToString() == "newcount")
        {
            tempstring = "{title:'新项目',id:'newcount',items:new NewPanel()}";
        }
        if (id.ToString() == "today_1")
        {
            tempstring = "{title:'今日收入',id:'today_1',items:new ItemsPanel('today_1')}";
        }
        if (id.ToString() == "today_0")
        {
            tempstring = "{title:'今日支出',id:'today_0',items:new ItemsPanel('today_0')}";
        } if (id.ToString() == "search")
        {
            tempstring = "{title:'高级查询',id:'search',items:new SearchPanel()}";
        }

        if (id.ToString() == "managetype")
        {
            tempstring = "{title:'管理收支分类',id:'managetype',items:new ManageTypePanel()}";
        }
        return tempstring;

       
    }
    #endregion




    #region 导航树



    private string GetSRORZCString(int AAS)
    {
        DataTable dt = manage.GetTypeList(AAS);
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i != 0) { result += ","; }
            result += "{";
            result += "text:'" + dt.Rows[i]["type_name"].ToString() + "'";
            result += ",leaf:true,";
            result += "id:'" + dt.Rows[i]["type_id"].ToString() + "'";
            result += "}";
        }
        return result;
    }







    private void GetTreeMenu()
    {

        string xx = @"{tree:[


{
title:'时间项',
items:
new Ext.tree.TreePanel({
                      
                    animate:true, 
                    collapsible:true,
                    rootVisible:false,
                    autoScroll:true,
                    autoHeight:true,
                    enableDrag:true,         
                    enableDD:true,           
                    trackMouseOver:true,   
                    border:false,   
                    lines:true,
                    loader:new Ext.tree.TreeLoader(),
                    root:new Ext.tree.AsyncTreeNode({
                            id:'root',
                            text:'根节点',
                            leaf:false,
                            children:[{
                                id:'newcount',
                                leaf:true,
                                text:'新项目'
                            },
                            {
                                id:'today_1',
                                leaf:true,
                                text:'查看今日收入'
                            } ,{
                                id:'today_0',
                                leaf:true,
                                text:'查看今日支出'    
                            },{
                                id:'search',
                                leaf:true,
                                text:'高级查询'    
                            }
                            
                            ]
                        }),                    
                    listeners:{ 
                            'click':function(node,e){
                            if(node.leaf!=true) {return}; 

                             AddTab(node.id)
 
                            }
                    } 


                   })
},



 
{
title:'支出项',
items:
new Ext.tree.TreePanel({
                      
                    animate:true, 
                    collapsible:true,
                    rootVisible:false,
                    autoScroll:true,
                    autoHeight:true,
                    enableDrag:true,         
                    enableDD:true,           
                    trackMouseOver:true,   
                    border:false,   
                    lines:true,
                    loader:new Ext.tree.TreeLoader(),
                    root:new Ext.tree.AsyncTreeNode({
                            id:'root',
                            text:'根节点',
                            leaf:false,
                            children:[" + GetSRORZCString(0) + @"]
                        }),
                  listeners:{ 
                            'click':function(node,e){
                            if(node.leaf!=true) {return};

                            AddTab(node.id)

                            }
                    } 

                })
},




{
title:'收入项',
items:
new Ext.tree.TreePanel({
                      
                    animate:true, 
                    collapsible:true,
                    rootVisible:false,
                    autoScroll:true,
                    autoHeight:true,
                    enableDrag:true,         
                    enableDD:true,           
                    trackMouseOver:true,   
                    border:false,   
                    lines:true,
                    loader:new Ext.tree.TreeLoader(),
                    root:new Ext.tree.AsyncTreeNode({
                            id:'root',
                            text:'根节点',
                            leaf:false,
                            children:[" + GetSRORZCString(1) + @"]
                        }),
                        listeners:{ 
                                'click':function(node,e){
                                if(node.leaf!=true) {return};

                                AddTab(node.id)

                                }
                        } 
                })
}

,



{
title:'管理项',
items:
new Ext.tree.TreePanel({
                      
                    animate:true, 
                    collapsible:true,
                    rootVisible:false,
                    autoScroll:true,
                    autoHeight:true,
                    enableDrag:true,         
                    enableDD:true,           
                    trackMouseOver:true,   
                    border:false,   
                    lines:true,
                    loader:new Ext.tree.TreeLoader(),
                    root:new Ext.tree.AsyncTreeNode({
                            id:'root',
                            text:'根节点',
                            leaf:false,
                            children:[{
                                text:'添加收入分类',
                                leaf:true,
                                id:'tjsrfl'
                            },{
                                text:'添加支出分类',
                                leaf:true,
                                id:'tjzcfl'                            
                            },{
                                text:'管理收支分类',
                                leaf:true,
                                id:'managetype'
                            }]
                        }),                    
                    listeners:{ 
                            'click':function(node,e){
                            if(node.leaf!=true) {return}; 

                             AddTab(node.id)
 
                            }
                    } 


                   })
},


{
title:'数据统计',
items:
new Ext.tree.TreePanel({
                      
                    animate:true, 
                    collapsible:true,
                    rootVisible:false,
                    autoScroll:true,
                    autoHeight:true,
                    enableDrag:true,         
                    enableDD:true,           
                    trackMouseOver:true,   
                    border:false,   
                    lines:true,
                    loader:new Ext.tree.TreeLoader(),
                    root:new Ext.tree.AsyncTreeNode({
                            id:'root',
                            text:'根节点',
                            leaf:false,
                            children:[{
                                text:'按日统计(支出)',
                                leaf:true,
                                id:'Statistics_0_day'
                            },{
                                text:'按月统计(支出)',
                                leaf:true,
                                id:'Statistics_0_month'                            
                            },{
                                text:'按年统计(支出)',
                                leaf:true,
                                id:'Statistics_0_year'                            
                            },{
                                text:'按日统计(收入)',
                                leaf:true,
                                id:'Statistics_1_day'
                            },{
                                text:'按月统计(收入)',
                                leaf:true,
                                id:'Statistics_1_month'                            
                            },{
                                text:'按年统计(收入)',
                                leaf:true,
                                id:'Statistics_1_year'                            
                            },{
                                text:'按类别统计',
                                leaf:true,
                                id:'Statistics_type'                            
                            }]
                        }),                    
                    listeners:{ 
                            'click':function(node,e){
                            if(node.leaf!=true) {return}; 

                             AddTab(node.id)
 
                            }
                    } 


                   })
} 

]






,PAGESIZE:"+DbHelperSQL.PAGESIZE+@"
,SEARCH_PAGESIZE:"+DbHelperSQL.SEARCH_PAGESIZE+@"



}
";



        Response.Write(xx);

    }


    #endregion



}





