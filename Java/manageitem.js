//添加收入分类=======================================================================================================================

  TJSRFLPanel=Ext.extend(Ext.form.FormPanel,{
        
            constructor:function(){
            
                TJSRFLPanel.superclass.constructor.call(this,{
                
                                                    plain:true,
                                                    width: Ext.get("main").getWidth(), 
                                                    height:Ext.get("main").getHeight()-27,                                                 
                                                    frame:true,
                                                    
                                                    layout:"form",
                                                    defaultType:"textfield",
                                                    items:[{
                                                         
                                                        fieldLabel:'收入分类' 
                                                    }],
                                                    buttons:[{text:'添加',handler:function(){
                                                    
                                                    var _typename=this.ownerCt.items.itemAt(0).getValue(); 
                                             
                                                                           Ext.Ajax.request({
                                                                                 url : 'crud.aspx?app=addtype', 
                                                                                 params:{'typename':_typename,'AAS':1 },  
                                                                                 success : function(form, action) { 
                                                                                             Ext.MessageBox.alert('提示信息','添加成功')
	                                                                                    },
	                                                                             failure : function(form, action) {
		                                                                                     Ext.MessageBox.alert('提示信息',"添加失败")
                                                                                        }           
                                                                            })  

                                                    
                                                    }}]              
                })
            }        
        })      
        
        
        

 



//添加支出分类=======================================================================================================================
  TJZCFLPanel=Ext.extend(Ext.form.FormPanel,{
        
            constructor:function(){
            
                TJZCFLPanel.superclass.constructor.call(this,{
                
                                                    plain:true,
                                                    
                                                    frame:true,
                                                    width: Ext.get("main").getWidth(), 
                                                    height:Ext.get("main").getHeight()-27,
                                                    layout:"form",
                                                    defaultType:"textfield",
                                                    items:[{
                                                       
                                                        fieldLabel:'支出分类' 
                                                    }],
                                                    buttons:[{text:'添加',handler:function(){
                                                    
                                                    var _typename=this.ownerCt.items.itemAt(0).getValue();
                                                    
                                                   
                                                                           Ext.Ajax.request({
                                                                                 url : 'crud.aspx?app=addtype', 
                                                                                 params:{'typename':_typename,'AAS':0 },  
                                                                                 success : function(form, action) { 
                                                                                   
                                                                                           Ext.MessageBox.alert('提示信息',"添加成功")                                                                                           
                                                                                      
	                                                                                    },
	                                                                             failure : function(form, action) {
		                                                                                       Ext.MessageBox.alert('提示信息',"添加失败")
                                                                                        
	                                                                                    }           
                                                                            })                                                      
                                                    }}]              
                })
            }        
        })          
        
        
        
        
        
        
        
        
        
        


//        







//管理收支分类=======================================================================================================================

                                               

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
                                                      autoHeight:true , 
                                                      colModel : new Ext.grid.ColumnModel([
                                                                       {header : "ID号"  ,sortable: true, dataIndex : 'type_id'},
                                                                       {header : "分类名称"   , editor : new Ext.form.TextField({}) ,sortable: true, dataIndex : 'type_name'},
                                                                       {header : "收支状态" ,renderer:function(e){return  e=="1"?"收入":"支出"},sortable: true,
                                                                                editor :  new Ext.form.ComboBox({
                                                                                        valueField:'aas_id', 
                                                                                        displayField:"aas_name",
                                                                                        triggerAction:"all",                                                                                                       
                                                                                        mode:"local",     
                                                                                        store:new Ext.data.SimpleStore({
                                                                                            fields:["aas_id","aas_name"],
                                                                                            data:[["1","收入"],["0","支出"]]
                                                                                        }),
                                                                                        listeners: {                    
                                                                                            select: function(combo, record, index){      
                                                                                                var _state="收入";
                                                                                            if(record.data.type_AAS==0){
                                                                                                 _state="支出";
                                                                                            } 
                                                                                            }                        
                                                                                        }
                                                                                    })
                                                                        ,dataIndex : 'type_AAS'} 
                                                      ]),              
                                                      clicksToEdit : 2  
                    }) 
                    this.onInit()
                     
            },
   
         onInit:function(){
                    this.on("afteredit",function(e){
                            var _id=e.record.data.type_id;
                            var _name=e.record.data.type_name;
                            var _aas=e.record.data.type_AAS 
                      
                            var _url="crud.aspx?app=updatetype&_id="+_id+"&_name="+_name+"&_aas="+_aas ; 
                           
                               Ext.Ajax.request({
                                   url: _url,
                                   success: function(form,action){ 
                                            Ext.MessageBox.alert('提示信息','修改成功')
                                   },
                                   failure:function(form,action){
                                            Ext.MessageBox.alert('提示信息','修改失败')
                                   }   
                                });
                             
                       })   
            
         } 
                    
})      