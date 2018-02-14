$(function () {
    getMenuData();
});

function getMenuData() {
    $.ajax({
        url: "/home/menu",
        async: false,
        cache: false,
        contenttype: 'application/json',
        success: function (data) {
            createMenu(data);
        }
    });
}

function createMenu(data) {
    var json = data;
    $("#nav").accordion({ animate: true });

    $.each(json.menus, function (i, n) {
        var tempHtml = '<ul>';
        if (n.MenuList) {
            $.each(n.MenuList, function (j, o) {
                tempHtml += '<li><div><a mid="' + o.Id + '" ico="' + o.Icon + '" url="' + o.Url + '" href="javascript:void(0)"><span class="icon2 ' + o.Icon + '" >&nbsp;</span><span class="nav">' + o.Name + '</span></a></div></li> ';
            });
            tempHtml += '</ul>';
            $('#nav').accordion('add', {
                title: n.Name,
                content: tempHtml,
                iconCls: 'icon2 ' + n.Icon
            });
        }
    });

    $('#nav').find('a').live('click', function () {
        var tabTitle = $(this).children('.nav').text();
        var url = $(this).attr("url");
        var menuid = $(this).attr("mid");
        var icon = "icon2 " + $(this).attr('ico');
        addTab(tabTitle, url, icon);
        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).mouseover(function () {
        $(this).parent().addClass("hover");
    }).mouseout(function () {
        $(this).parent().removeClass("hover");
    });

    //选中第一个
    var panels = $('#nav').accordion('panels');
    if (panels.length > 0) {
        var t = panels[0].panel('options').title;
        $('#nav').accordion('select', t);
    }

    tabClose();
    tabCloseEven();
}

function addTab(subtitle, url, icon) {
    icon = "icon2 " + icon;
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#mm-tabupdate').click();
    }

    $('.easyui_iframe').each(function () {
        $(this).parent().css({ 'overflow': 'hidden' });
    });
}

function createFrame(url) {
    var s = '<iframe scrolling="auto" class="easyui_iframe" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").live("dblclick", function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    }); /*为选项卡绑定右键*/
    $(".tabs-inner").live('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}

function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').live("click", function () {
        var currTab = $('#tabs').tabs('getSelected');

        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        });
    }); //关闭当前
    $('#mm-tabclose').live("click", function () {
        var currtabTitle = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtabTitle);
    }); //全部关闭
    $('#mm-tabcloseall').live("click", function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').live("click", function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').live("click", function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //$.messager.alert("系统提示", "已经到最后！", "info");
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').live("click", function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            //$.messager.alert("系统提示", "已经到最前！", "info");
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //退出
    $("#mm-exit").live("click", function () {
        $('#mm').menu('hide');
    });
}