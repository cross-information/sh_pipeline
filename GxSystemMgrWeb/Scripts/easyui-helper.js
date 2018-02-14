(function ($) {
    // the code of this function is from  
    // http://lucassmith.name/pub/typeof.html 
    $.type = function (o) {
        var _toS = Object.prototype.toString;
        var _types = {
            'undefined': 'undefined',
            'number': 'number',
            'boolean': 'boolean',
            'string': 'string',
            '[object Function]': 'function',
            '[object RegExp]': 'regexp',
            '[object Array]': 'array',
            '[object Date]': 'date',
            '[object Error]': 'error'
        };
        return _types[typeof o] || _types[_toS.call(o)] || (o ? 'object' : 'null');
    };
    // the code of these two functions is from mootools 
    // http://mootools.net 
    var $specialChars = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' };
    var $replaceChars = function (chr) {
        return $specialChars[chr] || '\\u00' + Math.floor(chr.charCodeAt() / 16).toString(16) + (chr.charCodeAt() % 16).toString(16);
    };
    $.toJSON = function (o) {
        var s = [];
        switch ($.type(o)) {
            case 'undefined':
                return 'undefined';
                break;
            case 'null':
                return 'null';
                break;
            case 'number':
            case 'boolean':
            case 'date':
            case 'function':
                return o.toString();
                break;
            case 'string':
                return '"' + o.replace(/[\x00-\x1f\\"]/g, $replaceChars) + '"';
                break;
            case 'array':
                for (var i = 0, l = o.length; i < l; i++) {
                    s.push($.toJSON(o[i]));
                }
                return '[' + s.join(',') + ']';
                break;
            case 'error':
            case 'object':
                for (var p in o) {
                    s.push(p + ':' + $.toJSON(o[p]));
                }
                return '{' + s.join(',') + '}';
                break;
            default:
                return '';
                break;
        }
    };
    $.ajaxRequest = function (url, data, callback) {
        $('#tt').datagrid('loading');
        $.ajax({
            type: 'post',
            url: url,
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            success: function (json) {
                $('#tt').datagrid('loaded');
                if (typeof callback == 'function') {
                    callback(json);
                }
            }
        });
    };
    $.evalJSON = function (s) {
        if ($.type(s) != 'string' || !s.length) return null;
        return eval('(' + s + ')');
    };
    $.cookie = function (name, value, options) {
        if (typeof value != 'undefined') { // name and value given, set cookie
            options = options || {};
            if (value === null) {
                value = '';
                options.expires = -1;
            }
            var expires = '';
            if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                var date;
                if (typeof options.expires == 'number') {
                    date = new Date();
                    date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                } else {
                    date = options.expires;
                }
                expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
            }
            var path = options.path ? '; path=' + options.path : '';
            var domain = options.domain ? '; domain=' + options.domain : '';
            var secure = options.secure ? '; secure' : '';
            document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
        } else { // only name given, get cookie
            var cookieValue = null;
            if (document.cookie && document.cookie != '') {
                var cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = jQuery.trim(cookies[i]);
                    // Does this cookie string begin with the name we want?
                    if (cookie.substring(0, name.length + 1) == (name + '=')) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                        break;
                    }
                }
            }
            return cookieValue;
        }
    };

    $.rs = {
        failed: 0,//失败
        success: 1,//成功
        unAuth: 2,//未登录或没有权限
        exists: 3,//已存在
        deleted: 4//已删除
    };

    $.redirectLogin = function () {
        window.location.href = '/account/loginout';
    };

})(jQuery);

var page = {
    go: function (url) {
        if (url) {
            this.clear();
            location.href = url;
        } else {
            history.go(1);
        }
    },
    back: function () {
        history.go(-1);
    },
    init: function (cookieName) {
        var p = this.getBackup(cookieName);
        if (p && p.url) {
            $('#tt').datagrid({ url: p.url, queryParams: p.params, pageNumber: 1 });
            if (p.params && p.params['nmark']) {
                $('#toolbar-c select').val(p.params['nmark']);
                $('#tb select').val(p.params['nmark']);
            }
        }
    },
    getBackup: function (cookieName) {
        cookieName = cookieName || cookieNames.p0;
        var p = $.cookie(cookieName);
        if (p) {
            return eval('(' + p + ')');
        }
        return null;
    },
    backup: function (cookieName, url, params) {
        var p = {};
        p['url'] = url;
        p['params'] = params;
        $.cookie(cookieName, $.toJSON(p), { path: '/' });
    },
    clear: function () {
        $.cookie(cookieNames.businessDistrict, null, { path: '/' });

    }
};

var cookieNames =
{
    businessDistrict: 'businessDistrict',
    pageSize: 'pageSize'
};


function onChangeTheme(themeName) {
    /// <summary>更换主题</summary>
    /// <param name="themeName" type="Object">主题名称</param>
    if (themeName != '0') {
        var headEle = document.getElementsByTagName('HEAD').item(0);
        var linkDom = document.createElement('link');
        linkDom.href = '/Scripts/jquery-easyui-1.3.5/themes/' + themeName + '/easyui.css';
        linkDom.rel = 'stylesheet';
        linkDom.type = 'text/css';
        headEle.appendChild(linkDom);
    }
}

$.extend($.fn.validatebox.defaults.rules, {
    minLength: { // 判断最小长度
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '最少输入 {0} 个字符。'
    },
    length: {
        validator: function (value, param) {
            var len = $.trim(value).length;
            return len >= param[0] && len <= param[1];
        },
        message: "内容长度介于{0}和{1}之间."
    },
    phone: {// 验证电话号码
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '格式不正确,请使用下面格式:020-88888888'
    },
    mobile: {// 验证手机号码
        validator: function (value) {
            return /^(13|15|18)\d{9}$/i.test(value);
        },
        message: '手机号码格式不正确(正确格式如：13450774432)'
    },
    phoneOrMobile: {//验证手机或电话
        validator: function (value) {
            return /^(13|15|18)\d{9}$/i.test(value) || /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '请填入手机或电话号码,如13688888888或020-8888888'
    },
    idcard: {// 验证身份证
        validator: function (value) {
            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value);
        },
        message: '身份证号码格式不正确'
    },
    floatOrInt: {// 验证是否为小数或整数
        validator: function (value) {
            return /^-?[1-9]*(\.\d*)?$|^-?0(\.\d*)?$/i.test(value);
        },
        message: '请输入数字，并保证格式正确'
    },
    currency: {// 验证货币
        validator: function (value) {
            return /^d{0,}(\.\d+)?$/i.test(value);
        },
        message: '货币格式不正确'
    },
    qq: {// 验证QQ,从10000开始
        validator: function (value) {
            return /^[1-9]\d{4,9}$/i.test(value);
        },
        message: 'QQ号码格式不正确(正确如：453384319)'
    },
    integer: {// 验证整数
        validator: function (value) {
            return /^[+]?[1-9]+\d*$/i.test(value);
        },
        message: '请输入整数'
    },
    chinese: {// 验证中文
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value);
        },
        message: '请输入中文'
    },
    english: {// 验证英语
        validator: function (value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: '请输入英文'
    },
    unnormal: {// 验证是否包含空格和非法字符
        validator: function (value) {
            return /.+/i.test(value);
        },
        message: '输入值不能为空和包含其他非法字符'
    },
    username: {// 验证用户名
        validator: function (value) {
            return /^[a-zA-Z][a-zA-Z0-9_]{5,15}$/i.test(value);
        },
        message: '用户名不合法（字母开头，允许6-16字节，允许字母数字下划线）'
    },
    faxno: {// 验证传真
        validator: function (value) {
            return /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/i.test(value);
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '传真号码不正确'
    },
    zip: {// 验证邮政编码
        validator: function (value) {
            return /^[1-9]\d{5}$/i.test(value);
        },
        message: '邮政编码格式不正确'
    },
    ip: {// 验证IP地址
        validator: function (value) {
            return /d+.d+.d+.d+/i.test(value);
        },
        message: 'IP地址格式不正确'
    },
    name: {// 验证姓名，可以是中文或英文
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value) | /^\w+[\w\s]+\w+$/i.test(value);
        },
        message: '请输入姓名'
    },
    carNo: {
        validator: function (value) {
            return /^[\u4E00-\u9FA5][\da-zA-Z]{6}$/.test(value);
        },
        message: '车牌号码无效（例：粤J12350）'
    },
    carenergin: {
        validator: function (value) {
            return /^[a-zA-Z0-9]{16}$/.test(value);
        },
        message: '发动机型号无效(例：FG6H012345654584)'
    },
    email: {
        validator: function (value) {
            return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value);
        },
        message: '请输入有效的电子邮件账号(例：abc@126.com)'
    },
    msn: {
        validator: function (value) {
            return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value);
        },
        message: '请输入有效的msn账号(例：abc@hotnail(msn/live).com)'
    },
    department: {
        validator: function (value) {
            return /^[0-9]*$/.test(value);
        },
        message: '请输入部门排序号(例：1)'
    },
    same: {
        validator: function (value, param) {
            if ($("#" + param[0]).val() != "" && value != "") {
                return $("#" + param[0]).val() == value;
            } else {
                return true;
            }
        },
        message: '两次输入的密码不一致！'
    },
    reg: {
        validator: function (value, param) {
            var regExp = new RegExp(param[0]);
            return regExp.test(value);
        },
        message: '输入格式不正确'
    }
});