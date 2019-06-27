
//按照年,月,日统计收入,支出===================================================================================
StatisticsPanel=Ext.extend(Ext.grid.GridPanel,{
            ds:null,
            constructor:function(id,aas){
                var _url= 'crud.aspx?app=statistics&id='+id+'&aas='+aas
                this.ds=new Ext.data.JsonStore({
                                                        url:_url,
                                                        root: 'Result',
                                                        fields: ['item_AAS','item_date','item_amount'],
                                                        autoLoad:true ,
                                                        totalProperty: 'RowCount'
                                                    });  
                StatisticsPanel.superclass.constructor.call(this,{
                                            store:this.ds,
                                            columns: [                                                  
                                                {header: "收支状态",  sortable: true, dataIndex: 'item_AAS'},  
                                                {header: "收支日期", sortable: true, dataIndex: 'item_date'},
                                                {header: "收支金额",  sortable: true, dataIndex: 'item_amount'}
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
            }
        })      
        

 
 
//按照分类统计收入支出===================================================================================
StatisticsTypePanel=Ext.extend(Ext.grid.GridPanel,{
            ds:null,
            constructor:function(){
                
                var _url= 'crud.aspx?app=statisticstype'
                this.ds=new Ext.data.JsonStore({
                                                        url:_url,
                                                        root: 'Result',
                                                        fields: ['item_AAS','item_type','item_amount'],
                                                        autoLoad:true ,
                                                        totalProperty: 'RowCount'
                                                    });    
                StatisticsTypePanel.superclass.constructor.call(this,{
                                            store:this.ds,
                                            columns: [                                                  
                                                {header: "收支状态",  sortable: true, dataIndex: 'item_AAS'},  
                                                {header: "收支类型", sortable: true, dataIndex: 'item_type'},
                                                {header: "收支金额",  sortable: true, dataIndex: 'item_amount'}
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
            }  
        })      
        
        