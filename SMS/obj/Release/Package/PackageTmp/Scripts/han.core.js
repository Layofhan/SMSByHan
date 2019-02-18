
var han = {
    //右下角弹框
	open: function (cont, co) {
		layui.use('layer', function () {
			if (co) { cont = "<font style='color:green'>" + cont + "</font>" }
			else { cont = "<font style='color:red'>" + cont + "</font>"; }
			var layer = layui.layer;
			layer.open({ type: 1, title: "这是通知栏", content: cont, skin: '.demo-class', area: ['200px', '100px'], offset: 'rb', closeBtn: 0, shade: 0, time: 3000, anim: 2, resize: false, move: false });
		});
	},
    //数据表格
	table:function (param) {
		var defaultOptions = {
			url: null,
			elem: '#Han_Table',
			cols: null,
			page: true,
			limit: 10,
			request: null
		};
		var options = $.extend({}, defaultOptions, param);
		
		layui.use('table', function () {
			var table = layui.table;
                
			table.render({
			    elem: options.elem
			  , url: options.url
              , page: options.page
              , limit: options.limit
              , toolbar: options.toolbar
              , data:options.data
			  , parseData: function (res) { //res 即为原始返回的数据
		  		return {
		  			"code": 0, //解析接口状态
		  			"msg": res.Message, //解析提示文本
		  		    //"count": res.Data.length,
		  			"count": param.counts,
		  			"data": res.Data //解析数据列表
		  		};
			  }
              , request: options.request
			  , cellMinWidth: 100 //全局定义常规单元格的最小宽度，layui 2.2.1 新增
			  , cols: options.cols
              , done: function () {
                  layui.use('layer', function () {
                      layer.closeAll('loading');
                  });
              }
			});
		});

	},
    //数据表格重载
	reloadtable:function(param){
	    layui.use('table', function () {
	        var table = layui.table;
	        table.reload(param.id);
	    });
	},
    //加载层
    waiting:function (){
        layui.use('layer', function () {
            layer.load(2, { shade: 0, shadeClose: false });
        });
    },
    //关闭所有弹层
    closewaiting: function (index) {
        if (index == 1) {
            layui.use('layer', function () {
                layer.closeAll('page');
            });
        }
        else {
            layui.use('layer', function () {
                layer.closeAll('loading');
            });
        }  
    },
    //消息弹层
    message: function (content) {
        layui.use('layer', function () {
            var layer = layui.layer;
            if (content == null) {
                content = "高涵，你好像忘了点什么<i class='layui-icon'>&#xe664;</i> ";
            }
            layer.msg(content);
        });   
    },
    //重新渲染表单元素
    formrender: function (type, filter) {
        layui.use('form', function () {
            var form = layui.form;
            form.render(type,filter);
        });
    },
    //日期组件
    date: function (param) {
        var defaultOptions = {
            type: 'datetime',
            range: true,
            showBottom: true,
            lang: 'cn',
            theme: 'default',
        };
        var options = $.extend({}, defaultOptions, param);
        layui.use('laydate', function () {
            var laydate = layui.laydate;

            //执行一个laydate实例
            laydate.render({
                elem: options.elem,
                type: options.type,
                range: options.range,
                showBottom: options.showBottom,
                lang: options.lang,
                theme:options.theme,
            });
        });
    },
    //页弹层
    page: function (param) {
        layui.use('layer', function () {
            layer.open({
                id: 'hanpage',
                title:param.title,
                type: 1,
                area: ['400px', '350px'],
                fixed: false, //不固定
                maxmin: param.maxmin,
                content: "<script>$.ajax({url:'" + param.url + "',beforeSend:function(){han.waiting();},success:function(data){han.closewaiting(); if(data!=null){$('#hanpage').append(data);}else han.closewaiting(1); }   })</script>",//$('#hanpage').append(data);
                btn: ['确定', '取消'],
                yes: function (index, layero) {
                    confirm();
                },
                btn2: function (index, layero) {
                    han.closewaiting();
                    layer.close(index);
                },
                end: function () {
                    End();
                }
            });
        });
    },
    //模板引擎
    tpl: function (param)
    {
        layui.use('laytpl', function () {
            var laytpl = layui.laytpl;
            //模板渲染
            laytpl(param.view).render(param.data, function (html) {
                $(param.id).prepend(html);
            });
        });
    }
}