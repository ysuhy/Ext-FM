 
 
 
 
 
 
 //定义全局变量
 
  var PAGESIZE=0;    //其它的Grid页分页数量
  var SEARCH_PAGESIZE=0; //搜索页的GRID分页数量
 
 
 
 
 
 
 
 
 //整个系统的入口
    Ext.Ajax.request({
            url : 'data.aspx?app=first',
            success: successFC  //调用请求成功的函数 
    })     
     
 
 
 
  function successFC( response, request )   
     {    
        var obj= Ext.decode(response.responseText) ;
        
        
        
        PAGESIZE=obj.PAGESIZE; 
        SEARCH_PAGESIZE=obj.SEARCH_PAGESIZE;
        
        
        
     
        Ext.onReady(function() { 
            var menu = new MenuPanel(obj.tree);
            var main = new MainPanel();
     
            var viewport = new Ext.Viewport({    //系统整体布局
                layout: 'border',
                border: false,
                items: [ menu, main]   
            });    
             
        })   
    } 
    
    
    
    
    
    
    
    
    
    
    
////最大化整个系统    
//window.moveTo(0,0) ;
//window.resizeTo(screen.width,screen.height) ;

 