
//收入项,支出项,今日项===========================================================================
ItemsPanel=Ext.extend(Ext.grid.GridPanel,{
            
            
        
            ds:null,
            
            constructor:function(id){
            
                
                
                var _url= 'crud.aspx?app=items&item_type='+id
                
                this.ds=new Ext.data.JsonStore({
                                                        url:_url,
                                                        root: 'Result',
                                                        fields: ['item_id','item_AAS','item_type','item_name','item_amount','item_date'],
                                                        autoLoad:true ,
                                                        totalProperty: 'RowCount'
                                                    });                           
                                                    
                ItemsPanel.superclass.constructor.call(this,{
                                            
                                            renderTo:Ext.getBody(),                                            
                                            store:this.ds,
                                            autoScroll:false,
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
                                            
                                          
                                            width: Ext.get("main").getWidth(), 
                                            height:Ext.get("main").getHeight()-27,

                                            frame:true ,
                                            
                                            bbar: new Ext.PagingToolbar({
                                                pageSize: PAGESIZE,        //每页显示多少条数据
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
            }
                    
        })      
        
        
        
        
        
        
        
        
        
        
        