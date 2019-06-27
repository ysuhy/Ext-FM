
 //新项目=======================================================================================
 
NewPanel=Ext.extend(Ext.form.FormPanel,{
        
            constructor:function(){
            
                TJZCFLPanel.superclass.constructor.call(this,{                
                                                    plain:true,
                                                    width: Ext.get("main").getWidth(), 
                                                    height:Ext.get("main").getHeight()-27,
                                                    frame:true,                                             
                                                    layout:"form",
                                                    defaultType:"textfield",
                                                    items:[{                                                       
                                                        fieldLabel:'项目名称',
                                                        width:200 
                                                    },{
                                                       
                                                        fieldLabel:'收支分类',
                                                        xtype:'combo',                                                        
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
                                                            this.ownerCt.items.itemAt(2).setValue(_state)       
                                                            }                        
                                                        },
                                                        mode:"local",
                                                        emptyText:"请选择...",
                                                        triggerAction:"all"     
                                                    },{
                                                        xtype:'textfield',
                                                        fieldLabel:'收支状态',                                                         
                                                        readOnly:true                                                        
                                                    },{                                                       
                                                        fieldLabel:'收支金额',
                                                        xtype:'numberfield' 
                                                    },{                                                       
                                                        fieldLabel:'收支日期',
                                                        xtype:'datefield',
                                                        format:'Ymd' 
                                                    },{                                                       
                                                        fieldLabel:'收支备注',
                                                        xtype:'textarea' ,
                                                        width:600,
                                                        height:300
                                                    }],
                                                    buttons:[{text:'添加',handler:function(){
                                                    
                                                    var _name=this.ownerCt.items.itemAt(0).getValue();
                                                    var _type=this.ownerCt.items.itemAt(1).getValue();
                                                    var _amount=this.ownerCt.items.itemAt(3).getValue();
                                                    var _date=this.ownerCt.items.itemAt(4).value;
                                                    var _content=this.ownerCt.items.itemAt(5).getValue();
                                                   
                                                    var  ff=this;
                                               
                                                       Ext.Ajax.request({
                                                            method :'POST',
                                                            url : 'crud.aspx?app=additem', 
                                                             
                                                            params:{'_name':_name,'_type':_type,'_amount':_amount,'_date':_date,'_content':_content },  
                                                            success : function(form, action) {                                                                              
                                                                      Ext.MessageBox.alert("提示","添加成功")         
                                                                        ff.ownerCt.items.itemAt(0).setValue('')
                                                                        ff.ownerCt.items.itemAt(1).setValue('')
                                                                        ff.ownerCt.items.itemAt(2).setValue('')
                                                                        ff.ownerCt.items.itemAt(3).setValue('')
                                                                        ff.ownerCt.items.itemAt(4).setValue('')
                                                                        ff.ownerCt.items.itemAt(5).setValue('')                                                             
                                                                    },
                                                            failure : function(form, action) {
                                                                        Ext.MessageBox.alert("提示","添加失败")
                                                                    }           
                                                        })  

                                                    
                                                    }},{text:'清空',handler:function(){
                                                            this.ownerCt.items.itemAt(0).setValue('')
                                                            this.ownerCt.items.itemAt(1).setValue('')
                                                            this.ownerCt.items.itemAt(2).setValue('')
                                                            this.ownerCt.items.itemAt(3).setValue('')
                                                            this.ownerCt.items.itemAt(4).setValue('')
                                                            this.ownerCt.items.itemAt(5).setValue('')
                                                    }}]              
                })
            }        
        })    
        
        
        
        
        
        
        
        
        
 //高级查询=======================================================================================
 
 
 SearchPanel=Ext.extend(Ext.form.FormPanel,{
            
            _search:null,
            
            constructor:function(){
                
                
                this._search=new SearchPanelButtons();
                
                
                SearchPanel.superclass.constructor.call(this,{                
                                                    plain:true,
                                                    width: Ext.get("main").getWidth(), 
                                                    height:Ext.get("main").getHeight()-27,
                                                    frame:true,                                             
                                                    layout:"form",
                                                    border:false,
                                                    defaultType:"textfield",
                                                    items:[{                                                       
                                                        fieldLabel:'收支分类',
                                                        xtype:'combo',                                                        
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
                                                            this.ownerCt.items.itemAt(1).setValue(_state)       
                                                            }                        
                                                        },
                                                        mode:"local",
                                                        emptyText:"默认为全部类别",
                                                        triggerAction:"all"     
                                                    },{
                                                        xtype:'textfield',
                                                        fieldLabel:'收支状态',                                                         
                                                        readOnly:true                                       
                                                    },{                                                       
                                                        fieldLabel:'收支金额下限',
                                                        xtype:'numberfield' 
                                                    },{                                                       
                                                        fieldLabel:'收支金额上限',
                                                        xtype:'numberfield' 
                                                    },{                                                       
                                                        fieldLabel:'收支日期起始',
                                                        xtype:'datefield',
                                                        format:'Ymd' 
                                                    },{                                                       
                                                        fieldLabel:'收支日期结束',
                                                        xtype:'datefield',
                                                        format:'Ymd' 
                                                    },
                                                    
                                                    this._search
                                                    
                                                    ],
                                                    buttons:[{text:'搜索',handler:function(){                                                                  
                                                            var _grid=this.ownerCt.items.itemAt(6);                                                               
                                                            _grid.onChangeDataSource()               
                                                    
                                                    }},{text:'清空',handler:function(){
                                                            this.ownerCt.items.itemAt(0).setValue('')
                                                            this.ownerCt.items.itemAt(1).setValue('')
                                                            this.ownerCt.items.itemAt(2).setValue('')
                                                            this.ownerCt.items.itemAt(3).setValue('')
                                                            this.ownerCt.items.itemAt(4).setValue('')
                                                            this.ownerCt.items.itemAt(5).setValue('')
                                                    }}]              
                })
            }        
        })            
 

SearchPanelButtons=Ext.extend(Ext.grid.GridPanel,{
        
            ds:null,
            
            constructor:function(id){            
                                
                this.ds=new Ext.data.JsonStore({
                                                        url:'crud.aspx?app=search',
                                                        root: 'Result',
                                                        fields: ['item_id','item_AAS','item_type','item_name','item_amount','item_date'],
                                                        autoLoad:false ,
                                                        totalProperty: 'RowCount'
                                                    });                                                  
                                                                                                      
                                                    
                SearchPanelButtons.superclass.constructor.call(this,{
                                            border:false,                                        
                                            store:this.ds,
                                            listeners:{   
                                                       celldblclick:this.onRowDbclick  
				                            },        
                                            columns: [                                                  
                                                {header: "收支状态",  sortable: true, dataIndex: 'item_AAS'},                                                                                               
                                                {header: "收支类别",  sortable: true, dataIndex: 'item_type'},                                            
                                                {header: "收支名称", sortable: true, dataIndex: 'item_name'},
                                                {header: "收支金额", sortable: true, dataIndex: 'item_amount'},
                                                {header: "收支日期",  sortable: true, dataIndex: 'item_date'}
                                            ],
                                            viewConfig: {
                                                forceFit: true
                                            },
                                           // height:400,
                                            width: Ext.get("main").getWidth(), 
                                            height:Ext.get("main").getHeight()-247,
                                            frame:true ,                                            
                                            bbar: new Ext.PagingToolbar({
                                                pageSize:  SEARCH_PAGESIZE ,        //每页显示多少条数据
                                                store: this.ds,       
                                                displayInfo: true,
                                                displayMsg: '{0}-{1}/{2}',
                                                emptyMsg: "暂无数据"
                                            })
         
                })
            },
            
            onRowDbclick:function(_gird, _rowIndex, _columnIndex, _e){
                        
                        
                       var ss=  _gird.store.data.items[_rowIndex].data.item_id
                       var _url='crud.aspx?app=content&id='+ss;
                       
                   
                       Ext.Ajax.request({
                           url: _url,
                           success: this.onWindow                         
                      
                        });
                     
                        },
                        onWindow:function(rsp){
                        
                   
                        
                       var obj=Ext.decode(rsp.responseText);
                        
                        new Ext.Window({
                            title:'详细信息',
                            html:obj.msg+'<br><br><br><br><br><br><br><br><br><br>',
                            width:400,
                            heigjht:300,
                            modal:true 
                        }).show()
                         
                        },
             onChangeDataSource:function(){ 
             
             this.store.removeAll(); 
             
                    var _type=this.ownerCt.items.itemAt(0).getValue();
                    var _AAS=this.ownerCt.items.itemAt(1).getValue();
                    var _amount_min=this.ownerCt.items.itemAt(2).getValue();
                    var _amount_max=this.ownerCt.items.itemAt(3).getValue();
                    var _start=this.ownerCt.items.itemAt(4).value;
                    var _end=this.ownerCt.items.itemAt(5).value;
                    
            var _url='crud.aspx?app=search&_typeid='+_type+'&_AAS='+_AAS+'&_amount_min='+_amount_min+'&_amount_max='+_amount_max+'&_start='+_start+'&_end='+_end 
            this.store.proxy = new Ext.data.HttpProxy({url :_url });
     //       this.store.load({'params':{'limit':20,'start':0}}); 
     
            this.store.load({'params':{'limit':SEARCH_PAGESIZE,'start':0}}); 
            
            
       
               //       this.getView().refresh();  
                       
                       
            this.getBottomToolbar().bind(this.store)        
 
                                                  
                                                    
             }           
            
                    
        })      
        


 