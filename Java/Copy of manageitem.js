 
//        



//管理收支分类===========================================================================


 

 
                                                        
                                                        
                                                        

  ManageTypePanel=Ext.extend(Ext.grid.EditorGridPanel,{
            
            
            
            constructor:function(){
            
                    ManageTypePanel.superclass.constructor.call(this,{
                                                        
                                                      store : new Ext.data.JsonStore({
                                                                url:'data.aspx?app=gettypeJSONstring',                                                              
                                                                fields: ['type_id','type_name','type_AAS'],
                                                                autoLoad:true                                                                 
                                                      }),       
                                                      viewConfig: {
                                                          forceFit: true
                                                      },                                               
                                                      
                                                      title:'Mys GridPanel',
                                                      height:360,
                                                
                                                      collapsible : true,
                                                      animCollapse : true,
                                                      allowDomMove : true,
                                                      
                                                      colModel : new Ext.grid.ColumnModel([
                                                                       {header : "ID号"  ,sortable: true, dataIndex : 'type_id'},
                                                                       
                                                                       
                                                                   //---------------------------------------------------    
                                                                       {header : "分类名称"   , 
                                                                       editor : new Ext.form.ComboBox({
                                                                                            valueField:'type_id', 
                                                                                            displayField:"type_name",
                                                                                            triggerAction:"all",
                                                                                            typeAhead: true,            
                                                                                            store:new Ext.data.JsonStore({  
                                                                                                   autoLoad:true,
                                                                                                   url:"data.aspx?app=gettypeJSONstring",     
                                                                                                   fields: ["type_id", "type_name","type_AAS"]
                                                                                            }),  
                                                                                            listeners: {                    
                                                                                                select: function(combo, record, index){      
                                                                                                    var _state="收入";
                                                                                                if(record.data.type_AAS==0){
                                                                                                     _state="支出";
                                                                                                } 
                                                                                               // this.ownerCt.items.itemAt(2).setValue(_state)       
                                                                                               
                                                                                                alert(this.value)
                                                                                                }                        
                                                                                            }
                                                                                        })
                                                                        ,sortable: true, dataIndex : 'type_name'},
                                                                   //-------------------------------------------------------     
                                                                       
                                                                       
                                                                       
                                                                       {header : "收支状态" ,sortable: true, dataIndex : 'type_AAS'} 
                                                      ]), 
             
                                                      clicksToEdit : 2 ,
                                                      renderTo:Ext.getBody()          
                    })
            }        
        })      