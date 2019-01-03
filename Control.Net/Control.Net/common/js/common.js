/*
Joint框架通用方法
*/

//判断JS对象是否存在或者null，如果不存在，这返回false
function CheckIsNullOrEmpty(avar) {
    return (
        (avar == null) ||
        (avar == 'null') ||
        (avar == undefined) ||
        (avar.toString() == '')
    );
};

//判断传入的方法名是否存在
function CheckIsJsFunction(fn) {
    return (typeof (fn) == 'function');
};

//函数功能：对Url进行参数值的替换,有则进行替换，没有则进行追加
//参数说明：
//url      目标url 
//arg      需要替换的参数名称
//arg_val  替换后的参数的值
//return url 参数替换后的url
function ChangeURLArg(url, arg, arg_val) {
    var pattern = arg + '=([^&]*)';
    var replaceText = arg + '=' + arg_val;
    if (url.match(pattern)) {
        var tmp = '/(' + arg + '=)([^&]*)/gi';
        tmp = url.replace(eval(tmp), replaceText);
        return tmp;
    } else {
        if (url.match('[\?]')) {
            return url + '&' + replaceText;
        } else {
            return url + '?' + replaceText;
        }
    }

    return url + '\n' + arg + '\n' + arg_val;
};

//函数功能：对Url进行参数值的追加,有则不处理，没有则进行追加
//参数说明：
//url      目标url 
//arg      需要替换的参数名称
//arg_val  替换后的参数的值
//return url 参数替换后的url
function ChangeURLArg2(url, arg, arg_val) {
    var pattern = arg + '=([^&]*)';
    var replaceText = arg + '=' + arg_val;
    if (!url.match(pattern)) {
        if (url.match('[\?]')) {
            return url + '&' + replaceText;
        } else {
            return url + '?' + replaceText;
        }
    }

    return url + '\n' + arg + '\n' + arg_val;
};

//将JS对象转换成Url地址输入串
function JsObjectPackToQry(v_o) {
    var _qry = "";
    for (o_a in v_o) {
        if (typeof (v_o[o_a]) != "function") {
            var _value = encodeURI(v_o[o_a]);
            //alert(_value);
            //_qry += String.format("&{0}={1}", o_a, _value);
            _qry += "&" + o_a + "=" + _value;
        }
    }

    return _qry;
};

//获取地址信息串
function getRequest() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            try {
                theRequest[strs[i].split("=")[0]] = decodeURI(strs[i].split("=")[1]);
            } catch (e) {
                theRequest[strs[i].split("=")[0]] = strs[i].split("=")[1];
            }
        }
    }

    return theRequest;
};

//解析地址，转换成数组，并在每一项前加Url
function getRequestUrl() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            try {
                theRequest["Url" + strs[i].split("=")[0]] = decodeURI(strs[i].split("=")[1]);
            } catch (e) {
                theRequest["Url" + strs[i].split("=")[0]] = strs[i].split("=")[1];
            }
        }
    }

    return theRequest;
};

//单选框控件点击状态控制
fnSwitch2State = function (obj) {
    if ($(obj).hasClass("layui-form-checkbox")) {
        if ($(obj).attr("lay-value") == "1") {
            $(obj).attr("lay-value", 0);
            $(obj).removeClass("layui-form-checked");
        }
        else {
            $(obj).attr("lay-value", 1);
            $(obj).addClass("layui-form-checked");
        }
    }
    else {
        if ($(obj).attr("lay-value") == "1") {
            $(obj).attr("lay-value", 0);
            $(obj).removeClass("layui-form-onswitch");
            $(obj).find("em").html($(obj).attr("lay-text").split('|')[0]);
        }
        else {
            $(obj).attr("lay-value", 1);
            $(obj).addClass("layui-form-onswitch");
            $(obj).find("em").html($(obj).attr("lay-text").split('|')[1]);
        }
    }

    //是否为表格全选
    if ($(obj).attr("layTableAllChoose") == "true") {
        var checked = $(obj).attr("lay-value") == "1";
        var childs = $(obj).parents("div.layui-table-box").find('div[name="layTableCheckbox"]');
        childs.each(function (i, item) {
            if (checked) {
                $(item).attr("lay-value", 1);
                $(item).addClass("layui-form-checked");
            }
            else {
                $(item).attr("lay-value", 0);
                $(item).removeClass("layui-form-checked");
            }
        });
    }
};

//递增
fnAddOneByOne = function (t) {
    var target = $(t.parentElement).prev();
    if (target.val() === "") {
        target.val(1);
    }
    else {
        target.val(parseInt(target.val()) + 1);
    }

    target.focus();
};

//递减
fnDecOneByOne = function (t) {
    var target = $(t.parentElement).prev();
    if (target.val() === "") {
        target.val(0);
    }
    else {
        target.val(parseInt(target.val()) - 1);
    }

    target.focus();
};

//数字录入控件
CheckIsNumberChar = function (object) {
    if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40)
        return false;
    var allowLessZero = $(object).attr("lesszero") == "true";
    var isFloat = $(object).attr("isfloat") == "true";

    var elv = object.value.replace(/[^0-9-.]+/, '');
    if (!allowLessZero) {
        elv = object.value.replace(/[^0-9.]+/, '');
    }

    if (!isFloat) {
        elv = elv.replace(/[^0-9-]+/, '');
    }

    if ((event.keyCode == 109 || event.keyCode == 189) && allowLessZero) {
        if (elv.indexOf('-') == elv.lastIndexOf('-')) {
            object.value = "-" + elv.replace(/[^0-9.]+/, '');
        }
        else {
            object.value = elv.substring(0, elv.length - 1);
        }
    }
    else if ((event.keyCode == 110 || event.keyCode == 190) && isFloat) {
        if (elv.indexOf('.') == elv.lastIndexOf('.')) {
            object.value = elv;
        }
        else {
            object.value = elv.substring(0, elv.length - 1);
        }
    }
    else {
        object.value = elv;
    }
};

//单选框组控件点击事件控制
fnChooseOneByGroup = function (cthis) {
    if (!$(cthis).hasClass("layui-form-radioed")) {
        $(cthis).parent().find(">div.layui-form-radioed").each(function () {
            $(this).removeClass("layui-form-radioed");
            $(this).find("i").removeClass("layui-anim-scaleSpring").removeClass("layui-icon-radio").addClass("layui-icon-unradio");
        });

        $(cthis).addClass("layui-form-radioed");
        $(cthis).find("i").addClass("layui-anim-scaleSpring").addClass("layui-icon-radio").removeClass("layui-icon-unradio");
    }
};

//LayEditor初始化
fnLayEditorInit = function (iframe, value, readonly) {
    var conts = iframe.contents();
    var iframeWin = iframe.prop('contentWindow');
    var head = conts.find('head');
    var style = $(['<style>'
        , '*{margin: 0; padding: 0;}'
        , 'body{padding: 10px; line-height: 20px; overflow-x: hidden; word-wrap: break-word; font: 14px Helvetica Neue,Helvetica,PingFang SC,Microsoft YaHei,Tahoma,Arial,sans-serif; -webkit-box-sizing: border-box !important; -moz-box-sizing: border-box !important; box-sizing: border-box !important;}'
        , 'a{color:#01AAED; text-decoration:none;}a:hover{color:#c00}'
        , 'p{margin-bottom: 10px;}'
        , 'img{display: inline-block; border: none; vertical-align: middle;}'
        , 'pre{margin: 10px 0; padding: 10px; line-height: 20px; border: 1px solid #ddd; border-left-width: 6px; background-color: #F2F2F2; color: #333; font-family: Courier New; font-size: 12px;}'
        , '</style>'].join(''));
    var body = conts.find('body');

    head.append(style);
    if (!readonly) {
        body.attr('contenteditable', 'true');
    }
    body.attr('id', iframe.attr("id") + "_body").css({
        'min-height': iframe.height()
    }).html(value || '');
};

//layEdit编辑器命令
fnLayEditorCommand = function (iframe, cmd, flg) {
    var editor = document.getElementById(iframe).contentWindow;//获取iframe Window 对象

    if (flg) {//document命令
        editor.document.execCommand(cmd);
        editor.document.focus();
    }
    else {//对齐方式
        editor.document.getElementById(iframe + "_body").style.textAlign = cmd;
    }
};

//星形评价控件初始化
fnJointStarInit = function (id) {
    var obj = eval(id + "_OBJ");
    var ul = $("#" + id).find("ul");
    var wide = ul.find("i").width();
    if (obj.readonly == "true")
        return false;

    ul.find("li").each(function (i, d) {
        var ind = i + 1;
        $(d).on('click', function (e) {
            //将当前点击li的索引值赋给value
            var value = ind;
            if (obj.half == "true") {
                //获取鼠标在li上的位置
                var x = e.pageX - $(this).offset().left;
                if (x <= wide / 2) {
                    value = value - 0.5;
                }
            }
            obj.value = value;
            if (obj.hint == "true") {
                if (!CheckIsNullOrEmpty(obj.data)) {
                    if (obj.half == "true")
                        $(d).parent().next("span").text(obj.data.split('^')[value / 0.5 - 1]);
                    else
                        $(d).parent().next("span").text(obj.data.split('^')[value - 1]);
                }
                else {
                    $(d).parent().next("span").text(value + "星");
                }
            }
        });
        $(d).on('mousemove', function (e) {
            ul.find("i").each(function () {
                $(this).addClass("layui-icon-rate").removeClass("layui-icon-rate-solid layui-icon-rate-half");
            });
            ul.find("i:lt(" + ind + ")").each(function () {
                $(this).addClass("layui-icon-rate-solid").removeClass("layui-icon-rate layui-icon-rate-half");
            });
            // 如果设置可选半星，那么判断鼠标相对li的位置
            if (obj.half == "true") {
                var x = e.pageX - $(this).offset().left;
                if (x <= wide / 2) {
                    $(d).children("i").addClass("layui-icon-rate-half").removeClass("layui-icon-rate-solid");
                }
            }
        });
        $(d).on('mouseleave', function (e) {
            var value = obj.value;
            ul.find("i").each(function () {
                $(this).addClass("layui-icon-rate").removeClass("layui-icon-rate-solid layui-icon-rate-half");
            });
            ul.find("i:lt(" + Math.floor(value) + ")").each(function () {
                $(this).addClass("layui-icon-rate-solid").removeClass("layui-icon-rate layui-icon-rate-half");
            });
            //如果设置可选半星，根据分数判断是否有半星
            if (obj.half == "true") {
                if (parseInt(value) !== value) {
                    ul.children("li:eq(" + Math.floor(value) + ")").children("i").addClass("layui-icon-rate-half").removeClass("layui-icon-rate-solid layui-icon-rate");
                }
            }
        });
    });
};

//释放鼠标移动事件
document.onmouseup = function () {
    document.onmousemove = null;
    $(".layui-slider-pop").hide();
};

//滑轮控件赋值处理
fnSetSliderMoveState = function (e, way, div, bar, tip) {
    var moveX = e.clientX - way.offsetLeft;
    var pec = Math.round((moveX / way.offsetWidth) * 100);

    if (pec >= 100) {
        pec = 100;
    }
    if ($(div).attr('read') != 'true') {
        div.style.left = (pec + "%");
        bar.style.width = (pec + "%");
        $(tip).find('span').text(pec);
        tip.style.left = (pec + "%");
    }

    tip.style.display = "block";
};

//轮播控件初始化
fnCarouselInit = function (id) {
    //前后指示箭头
    var elem = $("#" + id);
    elem.find("[lay-type=sub]").on("click", function (e) {
        fnCarouselSlide(id, true);
    });

    elem.find("[lay-type=add]").on("click", function (e) {
        fnCarouselSlide(id, false);
    });

    //底部指示器
    elem.find(".layui-carousel-ind > ul > li").on("click", function (e) {
        var now = $("#" + id).find("[carousel-item] div.layui-this").index() + 1;
        if (now != $(this).index()) {
            if ($(this).index() > now) {
                fnCarouselSlideTo(id, now, $(this).index(), true);
            }
            else {
                fnCarouselSlideTo(id, now, $(this).index(), false);
            }
        }
    });

    //初始化计时器
    eval("var " + id + "_pause = false");
    eval("var " + id + "_timer = setInterval(function () {if(!" + id + "_pause){fnCarouselSlide('" + id + "', true);}}, elem.attr('interval'));");
    //鼠标滑过时暂停计时器
    elem.on('mouseenter', function () {
        eval(id + "_pause = true");
    }).on('mouseleave', function () {
        eval(id + "_pause = false");
    });
};

//轮播滑动处理
fnCarouselSlide = function (id, flg) {
    var next = 0, count = $("#" + id).attr("count"), now = $("#" + id).find("[carousel-item] div.layui-this").index() + 1;
    if (flg) { //正向，向左滑动
        next = ((now + 1 > count) ? 0 : (now));
    }
    else { //反向，向右滑动
        next = ((now - 1 < 0) ? (count - 1) : (now - 2));
    }

    fnCarouselSlideTo(id, now, next, flg);
};

//跳转到指定序号
fnCarouselSlideTo = function (id, now, index, flg) {
    var THIS = "layui-this", ELEM_LEFT = 'layui-carousel-left', ELEM_RIGHT = 'layui-carousel-right', ELEM_PREV = 'layui-carousel-prev', ELEM_NEXT = 'layui-carousel-next';
    //过渡动画
    var elemItem = $("#" + id).find("[carousel-item] > div"), eleLi = $("#" + id).find(".layui-carousel-ind > ul > li");
    if (flg) {
        elemItem.eq(index).addClass(ELEM_NEXT);
        setTimeout(function () {
            elemItem.eq(now).addClass(ELEM_LEFT);
            elemItem.eq(index).addClass(ELEM_LEFT);
        }, 50);
    }
    else {
        elemItem.eq(index).addClass(ELEM_PREV);
        setTimeout(function () {
            elemItem.eq(now).addClass(ELEM_RIGHT);
            elemItem.eq(index).addClass(ELEM_RIGHT);
        }, 50);
    }

    setTimeout(function () {
        elemItem.removeClass(THIS + ' ' + ELEM_PREV + ' ' + ELEM_NEXT + ' ' + ELEM_LEFT + ' ' + ELEM_RIGHT);
        elemItem.eq(index).addClass(THIS);

        eleLi.removeClass(THIS);
        eleLi.eq(index).addClass(THIS);
    }, 300);
};

//布局的收缩与显示
function fnLayout_Click(col, pos, set) {
    if ($(col).attr("collapse") == "true") {
        fnLayout_Hide(col, pos, set);
    }
    else {
        fnLayout_Show(col, pos, set);
    }
};
function fnLayout_Show(col, pos, set) {
    var layTop = null;
    if (pos == 'Top' || pos == 'Bottom') {
        layTop = $(col).parent().parent();
        var initHeight = parseInt(layTop.parent().parent().attr("initheight"));
        layTop.animate({ "height": initHeight }, 600);
        layTop.parent().parent().animate({ "height": initHeight }, 600, null, function () {
            $(layTop.children()[1]).toggle();
        });
    }
    else {
        layTop = $(col).parent().parent();
        var initWidth = $(layTop.parent().parent().children()[0]).attr("initwidth");
        layTop.parent().animate({ "width": initWidth }, 600, null, function () {
            $(layTop.children()[1]).toggle();
        });
        $(layTop.parent()).css({ "background-color": "" });
    }

    $(col).removeClass("layout_Button_" + set);
    $(col).addClass("layout_Button_" + pos);
    $(col).attr("collapse", "true");
};
function fnLayout_Hide(col, pos, set) {
    var layTop = null;
    if (pos == 'Top' || pos == 'Bottom') {
        layTop = $(col).parent().parent();
        layTop.animate({ "height": "25px" }, 600);
        layTop.parent().parent().animate({ "height": "25px" }, 600);
        $(layTop.children()[1]).toggle();
    }
    else {
        layTop = $(col).parent().parent();
        layTop.parent().animate({ "width": "25px" }, 600);
        layTop.parent().css({ "background-color": "#eee" });
        $(layTop.children()[1]).toggle();
    }

    $(col).removeClass("layout_Button_" + pos);
    $(col).addClass("layout_Button_" + set);
    $(col).attr("collapse", "false");
};

//布局控件拖拽功能实现
fnLayoutSplitMove = function (mthis, type, id) {
    switch (type) {
        case "top":
            if ($(id).hasClass('layout-top-hide'))
                return false;
            var prev = $(mthis).prevAll('.layout-top:first');
            var next = $(mthis).nextAll('.layout-center:first');
            var offset = $(mthis).parent().offset().top;
            $(document).mousemove(function (e) {
                $(mthis).css("top", (e.clientY - offset) + "px");
                prev.css("height", (e.clientY - offset) + "px");
                next.css("top", (e.clientY - offset + 4) + "px");
            });
            break;
        case "bottom":
            if ($(id).hasClass('layout-bottom-hide'))
                return false;
            var initMargin = event.clientY;
            var initBottom = $(mthis).css("bottom").replace("px", "") * 1;
            var prev = $(mthis).prevAll('.layout-center:first');
            var next = $(mthis).nextAll('.layout-bottom:first');
            $(document).mousemove(function (e) {
                var moveMargin = e.clientY - initMargin;
                $(mthis).css("bottom", (initBottom - moveMargin) + "px");
                prev.css("bottom", (initBottom - moveMargin + 4) + "px");
                next.css("height", (initBottom - moveMargin) + "px");
            });
            break;
        case "left":
            if ($(id).hasClass('layout-left-hide'))
                return false;
            var prev = $(mthis).prevAll('.layout-left:first');
            var next = $(mthis).nextAll('.layout-center:first');
            var offset = $(mthis).parent().offset().left;
            $(document).mousemove(function (e) {
                $(mthis).css("left", (e.clientX - offset) + "px");
                prev.css("width", (e.clientX - offset) + "px");
                next.css("left", (e.clientX - offset + 4) + "px");
            });
            break;
        case "right":
            if ($(id).hasClass('layout-right-hide'))
                return false;
            var initMargin = event.clientX;
            var initRight = $(mthis).css("right").replace("px", "") * 1;
            var prev = $(mthis).prevAll('.layout-center:first');
            var next = $(mthis).nextAll('.layout-right:first');
            $(document).mousemove(function (e) {
                var moveMargin = e.clientX - initMargin;
                $(mthis).css("right", (initRight - moveMargin) + "px");
                prev.css("right", (initRight - moveMargin + 4) + "px");
                next.css("width", (initRight - moveMargin) + "px");
            });
            break;
    }

    //释放MouseMove
    $(document).mouseup(function () {
        $(document).unbind("mousemove");
    });
};

//折叠面板收缩处理
fnShowAndHideAccord = function (mthis) {
    var parent = $(mthis).parent().parent();
    var col = parent.attr("accordion") == "true";
    var i = $(mthis).find(">.layui-colla-icon");
    var c = $(mthis).next();
    if (col) { //开启手风琴
        var ai = parent.find(">div>h2>.layui-colla-icon");
        var ac = ai.parent().next();
        var nh = i.hasClass("layui-icon-down");
        ai.removeClass("layui-icon-down").addClass("layui-icon-right");
        ac.removeClass("layui-show");

        if (!nh) {
            i.removeClass("layui-icon-right").addClass("layui-icon-down");
            c.addClass("layui-show");
        }
    }
    else {
        if (i.hasClass("layui-icon-down")) {
            i.removeClass("layui-icon-down").addClass("layui-icon-right");
            c.removeClass("layui-show");
        }
        else {
            i.removeClass("layui-icon-right").addClass("layui-icon-down");
            c.addClass("layui-show");
        }
    }
};

//蒙板控件
var JointMaskPanel = {
    Prop: function (id) {
        var obj = eval(id + "_JSDS");
        obj.FIndex = layer.open({
            type: 1,
            title: (obj.Title == "" ? "信息" : obj.Title),
            content: $('#' + id),
            skin: obj.LaySkin,
            area: ['' + obj.WHSize[0].toString() + '', '' + obj.WHSize[1].toString() + ''],
            btn: ['' + obj.BuutonArray[0].toString() + '', '' + obj.BuutonArray[1].toString() + ''],
            closeBtn: obj.CloseSkin
        },
            function (index, layero) {
                //do somethings
            },
            function (index, layero) {
                //do somethings
            });
    },
    Hide: function (id) {
        var obj = eval(id + "_JSDS");
        layer.close(obj.FIndex);
    }
};

//循环每个项目
layeach = function (obj, fn) {
    var key, that = this;
    if (typeof fn !== 'function') return that;
    obj = obj || [];
    if (obj.constructor === Object) {
        for (key in obj) {
            if (fn.call(obj[key], key, obj[key])) break;
        }
    } else {
        for (key = 0; key < obj.length; key++) {
            if (fn.call(obj[key], key, obj[key])) break;
        }
    }
    return that;
};

//操作系统信息
device = function (key) {
    var agent = navigator.userAgent.toLowerCase()

        //获取版本号
        , getVersion = function (label) {
            var exp = new RegExp(label + '/([^\\s\\_\\-]+)');
            label = (agent.match(exp) || [])[1];
            return label || false;
        }

        //返回结果集
        , result = {
            os: function () { //底层操作系统
                if (/windows/.test(agent)) {
                    return 'windows';
                } else if (/linux/.test(agent)) {
                    return 'linux';
                } else if (/iphone|ipod|ipad|ios/.test(agent)) {
                    return 'ios';
                } else if (/mac/.test(agent)) {
                    return 'mac';
                }
            }()
            , ie: function () { //ie版本
                return (!!win.ActiveXObject || "ActiveXObject" in win) ? (
                    (agent.match(/msie\s(\d+)/) || [])[1] || '11' //由于ie11并没有msie的标识
                ) : false;
            }()
            , weixin: getVersion('micromessenger')  //是否微信
        };

    //任意的key
    if (key && !result[key]) {
        result[key] = getVersion(key);
    }

    //移动设备
    result.android = /android/.test(agent);
    result.ios = result.os === 'ios';

    return result;
};

//将数组中的对象按其某个成员排序
laysort = sort = function (obj, key, desc) {
    var clone = JSON.parse(
        JSON.stringify(obj || [])
    );

    if (!key) return clone;

    //如果是数字，按大小排序，如果是非数字，按字典序排序
    clone.sort(function (o1, o2) {
        var isNum = /^-?\d+$/
            , v1 = o1[key]
            , v2 = o2[key];

        if (isNum.test(v1)) v1 = parseFloat(v1);
        if (isNum.test(v2)) v2 = parseFloat(v2);

        if (v1 && !v2) {
            return 1;
        } else if (!v1 && v2) {
            return -1;
        }

        if (v1 > v2) {
            return 1;
        } else if (v1 < v2) {
            return -1;
        } else {
            return 0;
        }
    });

    desc && clone.reverse(); //倒序
    return clone;
};

//阻止事件冒泡
laystope = function (thisEvent) {
    thisEvent = thisEvent || win.event;
    try { thisEvent.stopPropagation() } catch (e) {
        thisEvent.cancelBubble = true;
    }
};

//自定义模块事件
layonevent = function (modName, events, callback) {
    if (typeof modName !== 'string'
        || typeof callback !== 'function') return this;

    return layevent(modName, events, null, callback);
};

//执行自定义模块事件
var layconfig = { event: {} };
layevent = function (modName, events, params, fn) {
    var that = this
        , result = null
        , filter = events.match(/\((.*)\)$/) || [] //提取事件过滤器字符结构，如：select(xxx)
        , eventName = (modName + '.' + events).replace(filter[0], '') //获取事件名称，如：form.select
        , filterName = filter[1] || '' //获取过滤器名称,，如：xxx
        , callback = function (_, item) {
            var res = item && item.call(that, params);
            res === false && result === null && (result = false);
        };

    //添加事件
    if (fn) {
        layconfig.event[eventName] = layconfig.event[eventName] || {};

        layconfig.event[eventName][filterName] = [fn];
        return this;
    }

    //执行事件回调
    layeach(layconfig.event[eventName], function (key, item) {
        //执行当前模块的全部事件
        if (filterName === '{*}') {
            layeach(item, callback);
            return;
        }

        //执行指定事件
        key === '' && layeach(item, callback);
        key === filterName && layeach(item, callback);
    });

    return result;
};

laydata = function (table, settings, storage) {
    table = table || 'layui';
    storage = storage || localStorage;

    if (!window.JSON || !window.JSON.parse) return;

    //如果settings为null，则删除表
    if (settings === null) {
        return delete storage[table];
    }

    settings = typeof settings === 'object'
        ? settings
        : { key: settings };

    try {
        var data = JSON.parse(storage[table]);
    } catch (e) {
        var data = {};
    }

    if ('value' in settings) data[settings.key] = settings.value;
    if (settings.remove) delete data[settings.key];
    storage[table] = JSON.stringify(data);

    return settings.key ? data[settings.key] : data;
};

//数组或对象的拷贝
var ObjectCopy = function (obj) {
    if (obj === null) return null
    if (typeof obj !== 'object') return obj;
    if (obj.constructor === Date) return new Date(obj);
    if (obj.constructor === RegExp) return new RegExp(obj);
    var newObj = new obj.constructor();  //保持继承链
    for (var key in obj) {
        if (obj.hasOwnProperty(key)) {   //不遍历其原型链上的属性
            var val = obj[key];
            newObj[key] = typeof val === 'object' ? arguments.callee(val) : val; // 使用arguments.callee解除与函数名的耦合
        }
    }

    return newObj;
};

//获取导出列名
fnGetExportColsName = function (rows) {
    var colsName = '';
    var rows = c_MainGrid_RowData[0];

    for (var c in rows) {
        if (rows[c].type != 'radio' && rows[c].type != 'checkbox') {
            colsName += ('^' + rows[c].field + '|' + rows[c].title)
        }
    }

    return colsName.slice(1);
};

//iframe实现文件下载
fnDoFileDownLoad = function (filename) {
    var elemIF = document.createElement("iframe");
    elemIF.src = filename;
    elemIF.style.display = "none";
    document.body.appendChild(elemIF);
};

//处理退格键的页面回退功能
fnDoBackspce = function (e) {
    var ev = e || window.event;//获取event对象     
    var obj = ev.target || ev.srcElement;//获取事件源     

    var t = obj.type || obj.getAttribute('type');//获取事件源类型    

    //获取作为判断条件的事件类型  
    var vReadOnly = obj.getAttribute('readonly');
    var vEnabled = obj.getAttribute('enabled');
    //处理null值情况  
    vReadOnly = (vReadOnly == null) ? false : vReadOnly;
    vEnabled = (vEnabled == null) ? true : vEnabled;

    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，  
    //并且readonly属性为true或enabled属性为false的，则退格键失效  
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效  
    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea")
        ? true : false;
    if (flag2) {
        return false;
    }

    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea")
        && (vReadOnly == true || vEnabled != true)) ? true : false;
    if (flag1) {
        return false;
    }

    return true;
};

//如果WEB报表客户端程序，先通过URL协议启动WEB报表客户端程序
//通知WEB报表客户端程序根据模板与数据的URL进行报表生成，
function webapp_urlprotocol_run(args) {
    window.location.href = "grwebapp://" + (args ? JSON.stringify(args) : "");
};

//根据当前网页URL获取到当前WEB服务器的根URL
fnGetRootUrl = function () {
    var rootURL = '';
    var path = window.location.pathname, index = path.lastIndexOf("/");

    //根据当前网页URL获取到当前WEB服务器的根URL，并记录在 rootURL 中
    rootURL = window.location.protocol + "//" + window.location.host;
    if (index >= 0) {
        path = path.substr(0, index);

        //demmo的根目录在当前页面的1级目录之上
        index = path.lastIndexOf("/");
        if (index >= 0) {
            path = path.substr(0, index);
        }

        rootURL += path;
    }
    rootURL += "/";

    return rootURL;
};

//用正则表达式实现html转码
htmlEncodeByRegExp = function (str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&/g, "&amp;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    s = s.replace(/\'/g, "&#39;");
    s = s.replace(/\"/g, "&quot;");

    return s;
};

//用正则表达式实现html解码
htmlDecodeByRegExp = function (str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&amp;/g, "&");
    s = s.replace(/&lt;/g, "<").replace(/&#60;/g, "<");
    s = s.replace(/&gt;/g, ">").replace(/&#62;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    s = s.replace(/&#39;/g, "\'");
    s = s.replace(/&quot;/g, "\"").replace(/&#34;/g, "\"");

    return s;
};

//表单主题色控制
function SetFormsControlTheme() {
    var local = laydata('layuiAdmin').theme.color
        , id = 'LAY_layadmin_Form_theme'
        , style = document.createElement('style')
        , styleText = '.layui-form-select dl dd.layui-this {background-color: ' + local.selected + ';}' +
            '.layui-btn {background-color: ' + local.selected + '; }' +
            '.layui-form-onswitch {border-color:' + local.selected + ';background-color:' + local.selected + ';}' +
            '.layui-form-radio>i:hover, .layui-form-radioed>i {color:' + local.selected + ';}' +
            '.layui-form-checked:hover{border-color:' + local.selected + '}.layui-form-checked:hover span{background-color: ' + local.selected + ';}.layui-form-checked i,.layui-form-checked:hover i{color: ' + local.selected + ';}' +
            '.layui-form-checked[lay-skin=primary] i {border-color:' + local.selected + ';background-color:' + local.selected + ';}' +
            '.layui-form-checkbox[lay-skin=primary]:hover i {border-color: ' + local.selected + ';}' +
            '.layui-laydate-header{background-color:' + local.selected + ';}' +
            '.layui-laydate-content layui-this{background-color:' + local.selected + ';}' +
            '.layui-input:hover,.layui-textarea:hover,.layui-input:focus,.layui-textarea:focus{border: solid 1px ' + local.selected + '!important;-webkit-box-shadow: 0 0 10px ' + local.selectedsd + ';-moz-box-shadow: 0 0 10px ' + local.selectedsd + ';box-shadow: 0 0 10px ' + local.selectedsd + ';}' +
            '.layui-laypage .layui-laypage-curr .layui-laypage-em{background-color:' + local.selected + ';}' +
            '.layui-form-checked span, .layui-form-checked:hover span{background-color:' + local.selected + ';}'
        , styleElem = document.getElementById(id);

    //添加主题样式
    if ('styleSheet' in style) {
        style.setAttribute('type', 'text/css');
        style.styleSheet.cssText = styleText;
    } else {
        style.innerHTML = styleText;
    }
    style.id = id;

    styleElem && $('body')[0].removeChild(styleElem);
    $('body')[0].appendChild(style);
    $('body').attr('themealias', local.alias);
};
