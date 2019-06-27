 
 
 
 
 
    MenuPanel = function(obj) { 
                MenuPanel.superclass.constructor.call(this, {
                                            id : 'menu', 
                                            region : 'west',
                                            title : "系统菜单",
                                            split : true,          
                                            width : 200,
                                            minSize : 175,
                                            maxSize : 500,
                                          //  autoScroll:true,
                                            collapsible : true,
                                            containerScroll: true,
                                            border : true,
                                            margins : '0 0 0 0',
                                            cmargins : '0 0 0 0',		
                                            layout : "accordion",
                                            items : obj ,  //动态调用  
                                            layoutConfig : {
                                                        titleCollapse: true, //
                                                        activeOnTop:false
//                                                                                                            
                                                     }  
                                            });
             };  
    Ext.extend(MenuPanel, Ext.Panel);     
    
    
    
    
    
    
    
    
    
    
    
    //============================================
    
    
    
    
    
    
    
    
    
    
    
    MainPanel = function() { 
        MainPanel.superclass.constructor.call(this, {
            id: 'main',
            region: 'center',
            margins: '0 0 0 0',
            resizeTabs: true,
             minTabWidth: 135,
             tabWidth: 135,
            border: false,     
            height:500,   
            enableTabScroll: true,
            activeTab: 0,
            items: [{
                border: false, 
              
                title: '首页' 
            }]
            });
        };  
    Ext.extend(MainPanel, Ext.TabPanel);  //主操作区面板
    
    
    
    
    
    
    
    //===================================
    
    
    
    
//    
 
        
    
    
    //点击树节点触发函数,子菜单前台入口
    function AddTab(id){
                    
                    
        var tabs=Ext.getCmp('main')
        
   
        
        for(_i=0;_i<tabs.items.length;_i++)
        {
            if(tabs.items.itemAt(_i).id==('tab_'+id))    //如果当前应用已经打开则返回
            { 
                tabs.setActiveTab('tab_'+id);
                tabs.doLayout()
                return;                 
            }
        } 
     
       var _url='data.aspx?app=tab&id='+id; 
       Ext.Ajax.request({
                url : _url,
                success:AddTab_Result
       })     
    }
    
    
    
    
    
    
    
    function AddTab_Result(rsp){
    
        var obj=Ext.decode(rsp.responseText);
        var tabs=Ext.getCmp('main')
        
            var tabstring={       
                            title:obj.title,
                            id:'tab_'+obj.id,
                            height:200,
                            items:obj.items,
                            autoScroll:true,                 
                            closable:true,
                            border:false
                         }; 
            tabs.add(tabstring) 
     
        
        
        tabs.setActiveTab('tab_'+obj.id);  
        tabs.doLayout()
       
    }
    
    
    
    
    
    
    
    
 