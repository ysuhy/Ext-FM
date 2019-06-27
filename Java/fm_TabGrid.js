

TabGridPanel=Ext.extend(Ext.form.FormPanel,{
            
            
            store:null,
            
            constructor:function(obj){
                
                
                this.store=new Ext.data.JsonStore({
                                                    url: 'crud.aspx',
                                                    root: 'Result',
                                                    fields: ['id', 'name'],
                                                    autoLoad:true ,
                                                    totalProperty: 'RowCount'
                                                });
                                                             
                
                TabGridPanel.superclass.constructor.call(this,{
                                                      
                                                    plain:true,
                                                    
                                                    autoHeight:true, 
                                                    frame:true, 
                                                    layout:"form", 
                                                    items:new Ext.grid.GridPanel({
                                                                
                                                            store:this.store,   
                                                            autoHeight:true,                                                          
                                                            columns: [
                                                                {header: "id", width: 200, sortable: true, dataIndex: 'id'},                                                
                                                                {header: "name", width: 120, sortable: true, dataIndex: 'name'}
                                                            ],
                                                            viewConfig: {
                                                                forceFit: true
                                                            },
                                                            listeners:{   
                                                                 celldblclick:this.onRowdblclick  
				                                            },
                                                            frame:true,
                                                            title:'ddd' ,
                                                            bbar: new Ext.PagingToolbar({
                                                            pageSize: 2,
                                                            store:this.store,
                                                            displayInfo: true,
                                                            displayMsg: '当前显示 {0} - {1}条记录 /共 {2}条记录',
                                                            emptyMsg: "无显示数据"
                                                            })

                                                        })
                                                        
                })
                
            },
            onRowdblclick:function( _gird, _rowIndex, _columnIndex, _e){
                            
                         
                            
                            this.ownerCt.onWindow()
                            debugger 
                            var record=this.getSelectionModel().getSelected();
                            
                        //    alert(record.data.id)
                            
//                           new Ext.Window({
//                            height:500,
//                            width:300,
//                            title:'详细数据',
//                            html:'ssss'
//                            }).show();
                
//                         var _rightgird=this.ownerCt.rightgrid   ;                        
//                            if ( this.onGetSelRec() ) 
//                            {                                   
//                                      _rightgird.getStore().add(this.onGetSelRec() )   ;  
//                                      this.getStore().remove(this.onGetSelRec());                                  
//                            }
//                            _rightgird.getSelectionModel().selectLastRow(true);   
                            
                            
            },
            onWindow:function(){ 
                new Ext.Window({
                            height:500,
                            width:300,
                            title:'详细数据',
                            html:'ssss'
                }).show();
                
               
            }
                      
        })      
        
 