﻿jQuery, $.fn.BreakingNews = function (l) { return l = $.extend({ background: "#FFF", title: "NEWS", titlecolor: "#FFF", titlebgcolor: "#5aa628", linkcolor: "#333", linkhovercolor: "#5aa628", fonttextsize: 16, isbold: !1, border: "none", width: "100%", autoplay: !0, timer: 3e3, modulid: "brekingnews", effect: "fade" }, l), this.each(function () { l.modulid = "#" + $(this).attr("id"); var t = l.modulid, i = 1; function o(t) { "next" == t ? $(l.modulid + " ul li").length > i ? i++ : i = 1 : i - 2 == -1 ? i = $(l.modulid + " ul li").length : i -= 1, "fade" == l.effect ? ($(l.modulid + " ul li").css({ display: "none" }), $(l.modulid + " ul li").eq(parseInt(i - 1)).fadeIn()) : $(l.modulid + " ul").animate({ marginTop: -($(l.modulid + " ul li").height() + 20) * (i - 1) }) } 1 == l.isbold ? fontw = "bold" : fontw = "normal", "slide" == l.effect ? $(l.modulid + " ul li").css({ display: "block" }) : $(l.modulid + " ul li").css({ display: "none" }), $(l.modulid + " .bn-title").html(l.title), $(l.modulid).css({ width: l.width, background: l.background, border: l.border, "font-size": l.fonttextsize }), $(l.modulid + " ul").css({ left: $(l.modulid + " .bn-title").width() + 40 }), $(l.modulid + " .bn-title").css({ background: l.titlebgcolor, color: l.titlecolor, "font-weight": fontw }), $(l.modulid + " ul li a").css({ color: l.linkcolor, "font-weight": fontw, height: parseInt(l.fonttextsize) + 6 }), $(l.modulid + " ul li").eq(parseInt(i - 1)).css({ display: "block" }), $(l.modulid + " ul li a").hover(function () { $(this).css({ color: l.linkhovercolor }) }, function () { $(this).css({ color: l.linkcolor }) }), $(l.modulid + " .bn-arrows span").click(function (l) { "bn-arrows-left" == $(this).attr("class") ? o("prev") : o("next") }), 1 == l.autoplay ? (t = setInterval(function () { o("next") }, l.timer), $(l.modulid).hover(function () { clearInterval(t) }, function () { t = setInterval(function () { o("next") }, l.timer) })) : clearInterval(t), $(window).resize(function (t) { $(l.modulid).width() < 360 ? ($(l.modulid + " .bn-title").html("&nbsp;"), $(l.modulid + " .bn-title").css({ width: "4px", padding: "10px 0px" }), $(l.modulid + " ul").css({ left: 4 })) : ($(l.modulid + " .bn-title").html(l.title), $(l.modulid + " .bn-title").css({ width: "auto", padding: "10px 20px" }), $(l.modulid + " ul").css({ left: $(l.modulid + " .bn-title").width() + 40 })) }) }) };
