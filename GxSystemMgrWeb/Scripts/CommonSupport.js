$('#btnLogin').click(function () {
    $.messager.progress({ text: '正在请求数据...', interval: 100 });
    $.ajax({
        url: '/passport/login',
        type: 'post',
        data: { loginname: $('#loginuser').val(), loginpass: $('#loginpwd').val() },
        success: function (rs) {
            if (rs.IsError) {
                $.messager.progress('close');
                window.location.href = '/Main/index';
            } else {
                alert(rs.Message);
            }
        },
        error: function () {
            $.messager.progress('close');
        }
    });
});