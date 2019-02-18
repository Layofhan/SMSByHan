window.onload = function () {
    $.ajax({
        type: "post",
        url: "/Home/LoadingMenu",//http://localhost:21742
        dataType: "json",
        success: function (data) {
            if (data.Success) {
                //根据权限加载菜单--第一版无菜单监听事件
                //var str = "<ul class='layui-nav layui-nav-tree' lay-filter='Nav_Gaohan'>";
                //for (var i = 0; i < data.Data.length; i++) {
                //    if (data.Data[i].ChildNodes.length > 0) {
                //        str = str + "<li class='layui-nav-item layui-nav-itemed'><a href='javascript:;'>" + data.Data[i].Name + "</a>";//<span class='layui-nav-more'></span>
                //        str = str + "<dl class='layui-nav-child'>";
                //        for (var j = 0; j < data.Data[i].ChildNodes.length; j++) {
                //            str = str + "<dd><a href='" + data.Data[i].ChildNodes[j].Adress + "'>" + data.Data[i].ChildNodes[j].Name + "</a></dd>";
                //        }
                //        str = str + "</dl></li>";
                //    }
                //    else {
                //        str = str + "<li class='layui-nav-item'><a href='" + data.Data[i].Adress + "'>" + data.Data[i].Name + "</a></li>"
                //    }
                //}
                //str = str + "</ul>";
                //根据权限加载菜单--第二版添加菜单监听事件
                var str = "<ul class='layui-nav layui-nav-tree' lay-filter='Nav_Gaohan'>";
                for (var i = 0; i < data.Data.length; i++) {
                    if (data.Data[i].ChildNodes.length > 0) {
                        str = str + "<li class='layui-nav-item' lay-filter='gazi'><a href='javascript:;'><i class='layui-icon' style='font-size: 20px;margin-left:-5px'>" + data.Data[i].Icon + "</i><span style='margin-left:10px'>" + data.Data[i].Name + "</span></a>";//<span class='layui-nav-more'></span>
                        str = str + "<dl class='layui-nav-child'>";
                        for (var j = 0; j < data.Data[i].ChildNodes.length; j++) {
                            str = str + "<dd id='" + data.Data[i].ChildNodes[j].MenuId + "'><a href='javascript:;' onclick = Tab('" + data.Data[i].ChildNodes[j].Adress + "','" + data.Data[i].ChildNodes[j].Name + "','" + data.Data[i].ChildNodes[j].MenuId + "')><span style='margin-left:24px'>" + data.Data[i].ChildNodes[j].Name + "</span></a></dd>";
                        }
                        str = str + "</dl></li>";
                    }
                    else {
                        str = str + "<li class='layui-nav-item' id='" + data.Data[i].MenuId + "' lay-filter='gazi'><a href='javascript:;' onclick = Tab('" + data.Data[i].Adress + "','" + data.Data[i].Name + "','" + data.Data[i].MenuId + "')><i class='layui-icon' style='font-size: 20px;margin-left:-5px'>" + data.Data[i].Icon + "</i><span style='margin-left:10px'>" + data.Data[i].Name + "</span></a></li>"
                    }
                }
                str = str + "</ul>";
                $("#MenuLeft").prepend(str);
                //触发菜单栏的js效果
                layui.use('element', function () {
                    var element = layui.element;
                    //监听左边导航栏 菜单点击事件
                    element.on('nav(Nav_Gaohan)', function (elem) {
                        showmenu(); //得到当前点击的DOM对象
                    });
                    //监听选项卡切换事件
                    element.on('tab(Tab_Gaohan)', function (data) {
                        var changedtabid = $(".layui-tab-title .layui-this").attr("lay-id");
                        //改变菜单栏的当前选择项
                        MenuSelectedChange(changedtabid);
                        //刷新切换后页面
                        IframeFreshen();
                    });
                });
                //更新渲染操作
                //element.render('nav', 'test1');
            }
        }
    });
    //阻止隐藏菜单的右键事件
    $('#RightTab').bind('contextmenu', function () { return false; });
    $('.layui-tab-title li').bind('contextmenu', function () { return false; });
    //监听鼠标位置，当未在右键菜单上时，右键菜单消失
    $('body').mousemove(function (e) {
        var taby=$('#RightTab').offset().top;
        var tabx = $('#RightTab').offset().left;
        if (e.pageX < ( tabx - 5 )|| e.pageX > tabx + 100) {
            $('#RightTab').css('display', 'none');
        }
        if (e.pageY < ( taby - 5 )|| e.pageY > taby + 120) {
            $('#RightTab').css('display', 'none');
        }

    });
    //根据是否有未读消息 来对提示logo进行显示
    $.ajax({
        type: "post",
        url: "/Notification/IsExistUnread",
        success: function (data) {
            if (data === 'True') {
                $('#isnotice').show();
            }
            else {
                $('#isnotice').hide();
            }
        }
    })
}

//添加选项卡
function Tab(Url, Name, Id) {
    layui.use('element', function () {
        var element = layui.element;
        if (!FindTab(Id)) {
            element.tabAdd("Tab_Gaohan", {
                title: Name
                , content: "<iframe src=" + Url + " class='layui-iframe' frameborder='0' scrolling='auto' style='z-index:1000;background-color:#F2F2F2;'></iframe>"//支持传入html
                , id: Id
            });
        }
        element.tabChange("Tab_Gaohan", Id);
    });
    //阻止选项卡的右键事件
    $('.layui-tab-title li').bind('contextmenu', function () { return false; });
    $('.layui-tab-title li').mousedown(function (e) {
        if (e.which == 3) {
            //添加选项卡右键菜单到鼠标的位置
            $('#RightTab').css('display', 'block');
            $('#RightTab').offset({ top:e.pageY , left: e.pageX });
        }
    });
}
//功能--查找选项卡是否已经添加了，防止重复添加
function FindTab(Id) {
    var f = false;
    return $(".layui-tab-title li[lay-id='" + Id + "']").length > 0;
}
//删除当前显示选项卡
function RemoveTab() {
    layui.use('element', function () {
        var element = layui.element;
        var tabid = $('.layui-tab-title .layui-this').attr('lay-id');
        console.log(tabid);
        if (tabid != 'mainindex') {
            element.tabDelete('Tab_Gaohan', tabid);
        }
        if (tabid == 'mainindex') {
            han.message("高涵说你关不掉&nbsp<i class='layui-icon'>&#xe664;</i>");
        }
        $('#RightTab').css('display', 'none');
    });
}
//删除右边的选项卡
function RemoveRightAllTab() {
    layui.use('element', function () {
        var element = layui.element;
        var ff = false;
        var currentid = $('.layui-tab-title .layui-this').attr('lay-id');
        $('.layui-tab-title li[lay-id]').each(function (index, elemen) { 
            if (ff && $(this).attr('lay-id') != 'mainindex') {
                element.tabDelete('Tab_Gaohan', $(this).attr('lay-id'));
            }
            if ($(this).attr('lay-id') === currentid) {
                ff = true;
            }
        });
        $('#RightTab').css('display', 'none');
    });
}
//删除除已显示和首页外的全部选项卡
function RemoveOtherTab() {
    layui.use('element', function () {
        var element = layui.element;
        var ff = false;
        var currentid = $('.layui-tab-title .layui-this').attr('lay-id');
        $('.layui-tab-title li[lay-id]').each(function (index, elemen) {
            if (currentid != $(this).attr('lay-id') && $(this).attr('lay-id') != 'mainindex') {
                element.tabDelete('Tab_Gaohan', $(this).attr('lay-id'));
            }
        });
        $('#RightTab').css('display', 'none');
    });
}
//删除除首页外的全部选项卡
function RemoveAllTab() {
    layui.use('element', function () {
        var element = layui.element;
        $('.layui-tab-title li[lay-id]').each(function (index, elemen) {
            if ($(this).attr('lay-id') != 'mainindex') {
                element.tabDelete('Tab_Gaohan', $(this).attr('lay-id'));
            }
        });
        $('#RightTab').css('display', 'none');
    });
}

//功能--选项卡改变，菜单的选择也改变
function MenuSelectedChange(id) {
    var selectedid = $("#MenuLeft .layui-this").attr("id");
    var i = "#" + selectedid;
    $(i).removeClass("layui-this");

    var a = "#" + id;
    $(a).addClass("layui-this");
}
//功能--选项卡的刷新
function IframeFreshen(id) {
    $('.layui-show iframe').attr('src', $('.layui-show iframe').attr('src'));
}
//隐藏左边菜单栏
var ishide = true;
function HindLeftMenu() {
    if (ishide) {
        hindmenu();
    }
    else {
        showmenu();
    }
    window.setTimeout(function () { $('#checke').removeClass('layui-this'); }, 10);
}
//隐藏左边菜单
function hindmenu() {
    $('.layui-side-scroll .layui-nav-item').removeClass('layui-nav-itemed');
    $('.layui-side-scroll .layui-nav-item a span').hide();
    //$('.layui-side').css('width', '50px');
    //$('.layui-logo').css('width', '50px');
    //$('.layui-body').css('left', '50px');
    //$('.layui-footer').css('left', '50px');
    //$('.layui-header .layui-layout-left').css('left', '50px');
    $('.layui-side').animate({ width: '50px' });
    $('.layui-logo').animate({ width: '50px' });
    $('.layui-body').animate({ left: '50px' });
    $('.layui-footer').animate({ left: '50px' });
    $('.layui-header .layui-layout-left').animate({ left: '50px' });
    $('#check').removeClass('layui-icon-shrink-right');
    $('#check').addClass('layui-icon-spread-left');
    $('.layui-logo').text("Han");
    ishide = false;
}
//隐藏右边菜单
function showmenu() {
    var tabid = $('.layui-tab-title .layui-this').attr('lay-id');
    var a = '#' + tabid;
    //$(a).closest('li').addClass('layui-nav-itemed');
    $('.layui-side-scroll .layui-nav-item a span').show();
    //$('.layui-side').css('width', '200px');
    //$('.layui-logo').css('width', '200px');
    //$('.layui-body').css('left', '200px');
    //$('.layui-footer').css('left', '200px');
    //$('.layui-header .layui-layout-left').css('left', '200px');
    $('.layui-side').animate({ width: '200px' });
    $('.layui-logo').animate({ width: '200px' });
    $('.layui-body').animate({ left: '200px' });
    $('.layui-footer').animate({ left: '200px' });
    $('.layui-header .layui-layout-left').animate({ left: '200px' });
    $('#check').removeClass('layui-icon-spread-left');
    $('#check').addClass('layui-icon-shrink-right');
    $('.layui-logo').text("PigOnline");

    ishide = true;
}

//消息通知logo点击后，未读消息标志隐藏
function noticeclick() {
    $('#isnotice').hide();
}
function RemoveLaythisForRight() {
    window.setTimeout(function () { $('#MessageCenterli').removeClass('layui-this'); }, 5);
}

function TabForNoticce(Url, Name, Id) {
    layui.use('element', function () {
        var element = layui.element;
        if (!FindTab(Id)) {
            element.tabAdd("Tab_Gaohan", {
                title: Name
                , content: "<iframe src=" + Url + " class='layui-iframe' frameborder='no' border='0' scrolling='auto' style='z-index:1000;background-color:#F2F2F2;'></iframe>"//支持传入html
                , id: Id
            });
        }
        element.tabChange("Tab_Gaohan", Id);
    });
    //阻止选项卡的右键事件
    $('.layui-tab-title li').bind('contextmenu', function () { return false; });
    $('.layui-tab-title li').mousedown(function (e) {
        if (e.which == 3) {
            //添加选项卡右键菜单到鼠标的位置
            $('#RightTab').css('display', 'block');
            $('#RightTab').offset({ top: e.pageY, left: e.pageX });
        }
    });
    noticeclick();
    RemoveLaythisForRight();
}