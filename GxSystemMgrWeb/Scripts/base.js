$(function () {

});

function getModel(url, data, key) {
    var str = '';
    $.ajax({
        url: url,
        type: 'post',
        data: data,
        async: false,
        cache: false,
        success: function (r) {
            str = r[key];
        }
    });
    return str;
}

var $bool = function (v, d, r) { return v == false ? '' : '√'; };

//图片预览
function picview(file, $img) {
    if (file["files"] && file["files"][0]) {
        var reader = new FileReader();
        reader.onload = function (evt) {
            $img.attr('src', evt.target.result).show();
        };
        reader.readAsDataURL(file.files[0]);
    }
    else {
        file.select();
        var path = document.selection.createRange().text;
        $img.attr('src', path).show();
    }
}

//上传图片预览 针对当前单个图片上传指定显示预览的位置
function imgview(file, imgUrl) {
    if (file["files"] && file["files"][0]) {
        var reader = new FileReader();
        reader.onload = function (evt) {
            $("#" + imgUrl).attr('src', evt.target.result).show();
        };
        reader.readAsDataURL(file.files[0]);
    }
    else {
        file.select();
        var path = document.selection.createRange().text;
        $("#" + imgUrl).attr('src', path).show();
    }
}

//弹出警告提示对话框
function alertWarning(text, fn) {
    /// <summary>弹出警告提示对话框</summary>
    /// <param name="text" type="Object">提示文本</param>
    /// <param name="fn" type="Object">确定回调函数,可选参数</param>
    $.messager.alert('操作提示', text, 'warning', fn);
}
//弹出错误提示对话框
function alertError(text, fn) {
    /// <summary>弹出错误提示对话框</summary>
    /// <param name="text" type="Object">提示文本</param>
    /// <param name="fn" type="Object">确定回调函数,可选参数</param>
    $.messager.alert('操作提示', text, 'error', fn);
}
//弹出一般提示对话框
function alertInfo(text, fn) {
    /// <summary>弹出一般提示对话框</summary>
    /// <param name="text" type="Object">提示文本</param>
    /// <param name="fn" type="Object">确定回调函数,可选参数</param>
    $.messager.alert('操作提示', text, 'info', fn);
}
//弹出询问提示对话框
function alertQuestion(text, fn) {
    /// <summary>弹出询问提示对话框</summary>
    /// <param name="text" type="Object">提示文本</param>
    /// <param name="fn" type="Object">确定回调函数,可选参数</param>
    $.messager.alert('操作提示', text, 'question', fn);
}
//在屏幕右下角弹出消息窗体
function showwindow(msg, title) {
    /// <summary>在屏幕右下角弹出消息窗体</summary>
    /// <param name="msg" type="Object">消息内容,支持HTML</param>
    $.messager.show({
        title: title == undefined ? '新消息' : title,
        msg: msg,//'【顾问：Emliy】把【客户：张先生】的状态改为【正在看房】。<a href="#">查看详情</a>',
        timeout: 0,
        showType: 'slide'
    });
}
//格式化时间
var $date = function (val, data, rownumber) {
    if (val == "" || val == undefined)
        return "";
    var re = /-?\d+/;
    var m = re.exec(val);
    var d = new Date(parseInt(m[0]));
    return d.Format("yyyy-MM-dd");
};
//删除首尾空格
String.prototype.Trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
};

/*
* 对Date的扩展，将 Date 转化为指定格式的String       
* 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符       
* 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)       
* eg:       
* (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423       
* (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04       
* (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04       
* (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04       
* (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18       
*/
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份        
        "d+": this.getDate(), //日        
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时        
        "H+": this.getHours(), //小时        
        "m+": this.getMinutes(), //分        
        "s+": this.getSeconds(), //秒        
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度        
        "S": this.getMilliseconds() //毫秒        
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};

Array.prototype.Sum = function() {
    var r = 0;
    for (var i = 0; i < this.length; i++) {
        r += parseInt(this[i]);
    }
    return r;
};

/*
 清空FORM表单内容  id：表单ID
*/
function ClearForm(id) {
    var objId = document.getElementById(id);
    if (objId == undefined) {
        return;
    }
    for (var i = 0; i < objId.elements.length; i++) {
        if (objId.elements[i].type == "text") {
            objId.elements[i].value = "";
        }
        else if (objId.elements[i].type == "password") {
            objId.elements[i].value = "";
        }
        else if (objId.elements[i].type == "radio") {
            objId.elements[i].checked = false;
        }
        else if (objId.elements[i].type == "checkbox") {
            objId.elements[i].checked = false;
        }
        else if (objId.elements[i].type == "select-one") {
            objId.elements[i].options[0].selected = true;
        }
        else if (objId.elements[i].type == "select-multiple") {
            for (var j = 0; j < objId.elements[i].options.length; j++) {
                objId.elements[i].options[j].selected = false;
            }
        }
        else if (objId.elements[i].type == "textarea") {
            objId.elements[i].value = "";
        } 
    }
}