﻿/*
* Copyright 2015. Elliemae Inc.
* Autor: Adiel Hercules
* Web Developer
* twitter: @adielhercules
*/



/*! Select2 4.0.0 | https://github.com/select2/select2/blob/master/LICENSE.md */!function (a) { "function" == typeof define && define.amd ? define(["jquery"], a) : a("object" == typeof exports ? require("jquery") : jQuery) }(function (a) {
    var b = function () {
        if (a && a.fn && a.fn.select2 && a.fn.select2.amd) var b = a.fn.select2.amd; var b; return function () { if (!b || !b.requirejs) { b ? c = b : b = {}; var a, c, d; !function (b) { function e(a, b) { return u.call(a, b) } function f(a, b) { var c, d, e, f, g, h, i, j, k, l, m, n = b && b.split("/"), o = s.map, p = o && o["*"] || {}; if (a && "." === a.charAt(0)) if (b) { for (n = n.slice(0, n.length - 1), a = a.split("/"), g = a.length - 1, s.nodeIdCompat && w.test(a[g]) && (a[g] = a[g].replace(w, "")), a = n.concat(a), k = 0; k < a.length; k += 1) if (m = a[k], "." === m) a.splice(k, 1), k -= 1; else if (".." === m) { if (1 === k && (".." === a[2] || ".." === a[0])) break; k > 0 && (a.splice(k - 1, 2), k -= 2) } a = a.join("/") } else 0 === a.indexOf("./") && (a = a.substring(2)); if ((n || p) && o) { for (c = a.split("/"), k = c.length; k > 0; k -= 1) { if (d = c.slice(0, k).join("/"), n) for (l = n.length; l > 0; l -= 1) if (e = o[n.slice(0, l).join("/")], e && (e = e[d])) { f = e, h = k; break } if (f) break; !i && p && p[d] && (i = p[d], j = k) } !f && i && (f = i, h = j), f && (c.splice(0, h, f), a = c.join("/")) } return a } function g(a, c) { return function () { return n.apply(b, v.call(arguments, 0).concat([a, c])) } } function h(a) { return function (b) { return f(b, a) } } function i(a) { return function (b) { q[a] = b } } function j(a) { if (e(r, a)) { var c = r[a]; delete r[a], t[a] = !0, m.apply(b, c) } if (!e(q, a) && !e(t, a)) throw new Error("No " + a); return q[a] } function k(a) { var b, c = a ? a.indexOf("!") : -1; return c > -1 && (b = a.substring(0, c), a = a.substring(c + 1, a.length)), [b, a] } function l(a) { return function () { return s && s.config && s.config[a] || {} } } var m, n, o, p, q = {}, r = {}, s = {}, t = {}, u = Object.prototype.hasOwnProperty, v = [].slice, w = /\.js$/; o = function (a, b) { var c, d = k(a), e = d[0]; return a = d[1], e && (e = f(e, b), c = j(e)), e ? a = c && c.normalize ? c.normalize(a, h(b)) : f(a, b) : (a = f(a, b), d = k(a), e = d[0], a = d[1], e && (c = j(e))), { f: e ? e + "!" + a : a, n: a, pr: e, p: c } }, p = { require: function (a) { return g(a) }, exports: function (a) { var b = q[a]; return "undefined" != typeof b ? b : q[a] = {} }, module: function (a) { return { id: a, uri: "", exports: q[a], config: l(a) } } }, m = function (a, c, d, f) { var h, k, l, m, n, s, u = [], v = typeof d; if (f = f || a, "undefined" === v || "function" === v) { for (c = !c.length && d.length ? ["require", "exports", "module"] : c, n = 0; n < c.length; n += 1) if (m = o(c[n], f), k = m.f, "require" === k) u[n] = p.require(a); else if ("exports" === k) u[n] = p.exports(a), s = !0; else if ("module" === k) h = u[n] = p.module(a); else if (e(q, k) || e(r, k) || e(t, k)) u[n] = j(k); else { if (!m.p) throw new Error(a + " missing " + k); m.p.load(m.n, g(f, !0), i(k), {}), u[n] = q[k] } l = d ? d.apply(q[a], u) : void 0, a && (h && h.exports !== b && h.exports !== q[a] ? q[a] = h.exports : l === b && s || (q[a] = l)) } else a && (q[a] = d) }, a = c = n = function (a, c, d, e, f) { if ("string" == typeof a) return p[a] ? p[a](c) : j(o(a, c).f); if (!a.splice) { if (s = a, s.deps && n(s.deps, s.callback), !c) return; c.splice ? (a = c, c = d, d = null) : a = b } return c = c || function () { }, "function" == typeof d && (d = e, e = f), e ? m(b, a, c, d) : setTimeout(function () { m(b, a, c, d) }, 4), n }, n.config = function (a) { return n(a) }, a._defined = q, d = function (a, b, c) { b.splice || (c = b, b = []), e(q, a) || e(r, a) || (r[a] = [a, b, c]) }, d.amd = { jQuery: !0 } }(), b.requirejs = a, b.require = c, b.define = d } }(), b.define("almond", function () { }), b.define("jquery", [], function () { var b = a || $; return null == b && console && console.error && console.error("Select2: An instance of jQuery or a jQuery-compatible library was not found. Make sure that you are including jQuery before Select2 on your web page."), b }), b.define("select2/utils", ["jquery"], function (a) { function b(a) { var b = a.prototype, c = []; for (var d in b) { var e = b[d]; "function" == typeof e && "constructor" !== d && c.push(d) } return c } var c = {}; c.Extend = function (a, b) { function c() { this.constructor = a } var d = {}.hasOwnProperty; for (var e in b) d.call(b, e) && (a[e] = b[e]); return c.prototype = b.prototype, a.prototype = new c, a.__super__ = b.prototype, a }, c.Decorate = function (a, c) { function d() { var b = Array.prototype.unshift, d = c.prototype.constructor.length, e = a.prototype.constructor; d > 0 && (b.call(arguments, a.prototype.constructor), e = c.prototype.constructor), e.apply(this, arguments) } function e() { this.constructor = d } var f = b(c), g = b(a); c.displayName = a.displayName, d.prototype = new e; for (var h = 0; h < g.length; h++) { var i = g[h]; d.prototype[i] = a.prototype[i] } for (var j = (function (a) { var b = function () { }; a in d.prototype && (b = d.prototype[a]); var e = c.prototype[a]; return function () { var a = Array.prototype.unshift; return a.call(arguments, b), e.apply(this, arguments) } }), k = 0; k < f.length; k++) { var l = f[k]; d.prototype[l] = j(l) } return d }; var d = function () { this.listeners = {} }; return d.prototype.on = function (a, b) { this.listeners = this.listeners || {}, a in this.listeners ? this.listeners[a].push(b) : this.listeners[a] = [b] }, d.prototype.trigger = function (a) { var b = Array.prototype.slice; this.listeners = this.listeners || {}, a in this.listeners && this.invoke(this.listeners[a], b.call(arguments, 1)), "*" in this.listeners && this.invoke(this.listeners["*"], arguments) }, d.prototype.invoke = function (a, b) { for (var c = 0, d = a.length; d > c; c++) a[c].apply(this, b) }, c.Observable = d, c.generateChars = function (a) { for (var b = "", c = 0; a > c; c++) { var d = Math.floor(36 * Math.random()); b += d.toString(36) } return b }, c.bind = function (a, b) { return function () { a.apply(b, arguments) } }, c._convertData = function (a) { for (var b in a) { var c = b.split("-"), d = a; if (1 !== c.length) { for (var e = 0; e < c.length; e++) { var f = c[e]; f = f.substring(0, 1).toLowerCase() + f.substring(1), f in d || (d[f] = {}), e == c.length - 1 && (d[f] = a[b]), d = d[f] } delete a[b] } } return a }, c.hasScroll = function (b, c) { var d = a(c), e = c.style.overflowX, f = c.style.overflowY; return e !== f || "hidden" !== f && "visible" !== f ? "scroll" === e || "scroll" === f ? !0 : d.innerHeight() < c.scrollHeight || d.innerWidth() < c.scrollWidth : !1 }, c.escapeMarkup = function (a) { var b = { "\\": "&#92;", "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;", "/": "&#47;" }; return "string" != typeof a ? a : String(a).replace(/[&<>"'\/\\]/g, function (a) { return b[a] }) }, c.appendMany = function (b, c) { if ("1.7" === a.fn.jquery.substr(0, 3)) { var d = a(); a.map(c, function (a) { d = d.add(a) }), c = d } b.append(c) }, c }), b.define("select2/results", ["jquery", "./utils"], function (a, b) { function c(a, b, d) { this.$element = a, this.data = d, this.options = b, c.__super__.constructor.call(this) } return b.Extend(c, b.Observable), c.prototype.render = function () { var b = a('<ul class="select2-results__options" role="tree"></ul>'); return this.options.get("multiple") && b.attr("aria-multiselectable", "true"), this.$results = b, b }, c.prototype.clear = function () { this.$results.empty() }, c.prototype.displayMessage = function (b) { var c = this.options.get("escapeMarkup"); this.clear(), this.hideLoading(); var d = a('<li role="treeitem" class="select2-results__option"></li>'), e = this.options.get("translations").get(b.message); d.append(c(e(b.args))), this.$results.append(d) }, c.prototype.append = function (a) { this.hideLoading(); var b = []; if (null == a.results || 0 === a.results.length) return void (0 === this.$results.children().length && this.trigger("results:message", { message: "noResults" })); a.results = this.sort(a.results); for (var c = 0; c < a.results.length; c++) { var d = a.results[c], e = this.option(d); b.push(e) } this.$results.append(b) }, c.prototype.position = function (a, b) { var c = b.find(".select2-results"); c.append(a) }, c.prototype.sort = function (a) { var b = this.options.get("sorter"); return b(a) }, c.prototype.setClasses = function () { var b = this; this.data.current(function (c) { var d = a.map(c, function (a) { return a.id.toString() }), e = b.$results.find(".select2-results__option[aria-selected]"); e.each(function () { var b = a(this), c = a.data(this, "data"), e = "" + c.id; null != c.element && c.element.selected || null == c.element && a.inArray(e, d) > -1 ? b.attr("aria-selected", "true") : b.attr("aria-selected", "false") }); var f = e.filter("[aria-selected=true]"); f.length > 0 ? f.first().trigger("mouseenter") : e.first().trigger("mouseenter") }) }, c.prototype.showLoading = function (a) { this.hideLoading(); var b = this.options.get("translations").get("searching"), c = { disabled: !0, loading: !0, text: b(a) }, d = this.option(c); d.className += " loading-results", this.$results.prepend(d) }, c.prototype.hideLoading = function () { this.$results.find(".loading-results").remove() }, c.prototype.option = function (b) { var c = document.createElement("li"); c.className = "select2-results__option"; var d = { role: "treeitem", "aria-selected": "false" }; b.disabled && (delete d["aria-selected"], d["aria-disabled"] = "true"), null == b.id && delete d["aria-selected"], null != b._resultId && (c.id = b._resultId), b.title && (c.title = b.title), b.children && (d.role = "group", d["aria-label"] = b.text, delete d["aria-selected"]); for (var e in d) { var f = d[e]; c.setAttribute(e, f) } if (b.children) { var g = a(c), h = document.createElement("strong"); h.className = "select2-results__group"; { a(h) } this.template(b, h); for (var i = [], j = 0; j < b.children.length; j++) { var k = b.children[j], l = this.option(k); i.push(l) } var m = a("<ul></ul>", { "class": "select2-results__options select2-results__options--nested" }); m.append(i), g.append(h), g.append(m) } else this.template(b, c); return a.data(c, "data", b), c }, c.prototype.bind = function (b) { var c = this, d = b.id + "-results"; this.$results.attr("id", d), b.on("results:all", function (a) { c.clear(), c.append(a.data), b.isOpen() && c.setClasses() }), b.on("results:append", function (a) { c.append(a.data), b.isOpen() && c.setClasses() }), b.on("query", function (a) { c.showLoading(a) }), b.on("select", function () { b.isOpen() && c.setClasses() }), b.on("unselect", function () { b.isOpen() && c.setClasses() }), b.on("open", function () { c.$results.attr("aria-expanded", "true"), c.$results.attr("aria-hidden", "false"), c.setClasses(), c.ensureHighlightVisible() }), b.on("close", function () { c.$results.attr("aria-expanded", "false"), c.$results.attr("aria-hidden", "true"), c.$results.removeAttr("aria-activedescendant") }), b.on("results:toggle", function () { var a = c.getHighlightedResults(); 0 !== a.length && a.trigger("mouseup") }), b.on("results:select", function () { var a = c.getHighlightedResults(); if (0 !== a.length) { var b = a.data("data"); "true" == a.attr("aria-selected") ? c.trigger("close") : c.trigger("select", { data: b }) } }), b.on("results:previous", function () { var a = c.getHighlightedResults(), b = c.$results.find("[aria-selected]"), d = b.index(a); if (0 !== d) { var e = d - 1; 0 === a.length && (e = 0); var f = b.eq(e); f.trigger("mouseenter"); var g = c.$results.offset().top, h = f.offset().top, i = c.$results.scrollTop() + (h - g); 0 === e ? c.$results.scrollTop(0) : 0 > h - g && c.$results.scrollTop(i) } }), b.on("results:next", function () { var a = c.getHighlightedResults(), b = c.$results.find("[aria-selected]"), d = b.index(a), e = d + 1; if (!(e >= b.length)) { var f = b.eq(e); f.trigger("mouseenter"); var g = c.$results.offset().top + c.$results.outerHeight(!1), h = f.offset().top + f.outerHeight(!1), i = c.$results.scrollTop() + h - g; 0 === e ? c.$results.scrollTop(0) : h > g && c.$results.scrollTop(i) } }), b.on("results:focus", function (a) { a.element.addClass("select2-results__option--highlighted") }), b.on("results:message", function (a) { c.displayMessage(a) }), a.fn.mousewheel && this.$results.on("mousewheel", function (a) { var b = c.$results.scrollTop(), d = c.$results.get(0).scrollHeight - c.$results.scrollTop() + a.deltaY, e = a.deltaY > 0 && b - a.deltaY <= 0, f = a.deltaY < 0 && d <= c.$results.height(); e ? (c.$results.scrollTop(0), a.preventDefault(), a.stopPropagation()) : f && (c.$results.scrollTop(c.$results.get(0).scrollHeight - c.$results.height()), a.preventDefault(), a.stopPropagation()) }), this.$results.on("mouseup", ".select2-results__option[aria-selected]", function (b) { var d = a(this), e = d.data("data"); return "true" === d.attr("aria-selected") ? void (c.options.get("multiple") ? c.trigger("unselect", { originalEvent: b, data: e }) : c.trigger("close")) : void c.trigger("select", { originalEvent: b, data: e }) }), this.$results.on("mouseenter", ".select2-results__option[aria-selected]", function () { var b = a(this).data("data"); c.getHighlightedResults().removeClass("select2-results__option--highlighted"), c.trigger("results:focus", { data: b, element: a(this) }) }) }, c.prototype.getHighlightedResults = function () { var a = this.$results.find(".select2-results__option--highlighted"); return a }, c.prototype.destroy = function () { this.$results.remove() }, c.prototype.ensureHighlightVisible = function () { var a = this.getHighlightedResults(); if (0 !== a.length) { var b = this.$results.find("[aria-selected]"), c = b.index(a), d = this.$results.offset().top, e = a.offset().top, f = this.$results.scrollTop() + (e - d), g = e - d; f -= 2 * a.outerHeight(!1), 2 >= c ? this.$results.scrollTop(0) : (g > this.$results.outerHeight() || 0 > g) && this.$results.scrollTop(f) } }, c.prototype.template = function (b, c) { var d = this.options.get("templateResult"), e = this.options.get("escapeMarkup"), f = d(b); null == f ? c.style.display = "none" : "string" == typeof f ? c.innerHTML = e(f) : a(c).append(f) }, c }), b.define("select2/keys", [], function () { var a = { BACKSPACE: 8, TAB: 9, ENTER: 13, SHIFT: 16, CTRL: 17, ALT: 18, ESC: 27, SPACE: 32, PAGE_UP: 33, PAGE_DOWN: 34, END: 35, HOME: 36, LEFT: 37, UP: 38, RIGHT: 39, DOWN: 40, DELETE: 46 }; return a }), b.define("select2/selection/base", ["jquery", "../utils", "../keys"], function (a, b, c) { function d(a, b) { this.$element = a, this.options = b, d.__super__.constructor.call(this) } return b.Extend(d, b.Observable), d.prototype.render = function () { var b = a('<span class="select2-selection" role="combobox" aria-autocomplete="list" aria-haspopup="true" aria-expanded="false"></span>'); return this._tabindex = 0, null != this.$element.data("old-tabindex") ? this._tabindex = this.$element.data("old-tabindex") : null != this.$element.attr("tabindex") && (this._tabindex = this.$element.attr("tabindex")), b.attr("title", this.$element.attr("title")), b.attr("tabindex", this._tabindex), this.$selection = b, b }, d.prototype.bind = function (a) { var b = this, d = (a.id + "-container", a.id + "-results"); this.container = a, this.$selection.on("focus", function (a) { b.trigger("focus", a) }), this.$selection.on("blur", function (a) { b.trigger("blur", a) }), this.$selection.on("keydown", function (a) { b.trigger("keypress", a), a.which === c.SPACE && a.preventDefault() }), a.on("results:focus", function (a) { b.$selection.attr("aria-activedescendant", a.data._resultId) }), a.on("selection:update", function (a) { b.update(a.data) }), a.on("open", function () { b.$selection.attr("aria-expanded", "true"), b.$selection.attr("aria-owns", d), b._attachCloseHandler(a) }), a.on("close", function () { b.$selection.attr("aria-expanded", "false"), b.$selection.removeAttr("aria-activedescendant"), b.$selection.removeAttr("aria-owns"), b.$selection.focus(), b._detachCloseHandler(a) }), a.on("enable", function () { b.$selection.attr("tabindex", b._tabindex) }), a.on("disable", function () { b.$selection.attr("tabindex", "-1") }) }, d.prototype._attachCloseHandler = function (b) { a(document.body).on("mousedown.select2." + b.id, function (b) { var c = a(b.target), d = c.closest(".select2"), e = a(".select2.select2-container--open"); e.each(function () { var b = a(this); if (this != d[0]) { var c = b.data("element"); c.select2("close") } }) }) }, d.prototype._detachCloseHandler = function (b) { a(document.body).off("mousedown.select2." + b.id) }, d.prototype.position = function (a, b) { var c = b.find(".selection"); c.append(a) }, d.prototype.destroy = function () { this._detachCloseHandler(this.container) }, d.prototype.update = function () { throw new Error("The `update` method must be defined in child classes.") }, d }), b.define("select2/selection/single", ["jquery", "./base", "../utils", "../keys"], function (a, b, c) { function d() { d.__super__.constructor.apply(this, arguments) } return c.Extend(d, b), d.prototype.render = function () { var a = d.__super__.render.call(this); return a.addClass("select2-selection--single"), a.html('<span class="select2-selection__rendered"></span><span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span>'), a }, d.prototype.bind = function (a) { var b = this; d.__super__.bind.apply(this, arguments); var c = a.id + "-container"; this.$selection.find(".select2-selection__rendered").attr("id", c), this.$selection.attr("aria-labelledby", c), this.$selection.on("mousedown", function (a) { 1 === a.which && b.trigger("toggle", { originalEvent: a }) }), this.$selection.on("focus", function () { }), this.$selection.on("blur", function () { }), a.on("selection:update", function (a) { b.update(a.data) }) }, d.prototype.clear = function () { this.$selection.find(".select2-selection__rendered").empty() }, d.prototype.display = function (a) { var b = this.options.get("templateSelection"), c = this.options.get("escapeMarkup"); return c(b(a)) }, d.prototype.selectionContainer = function () { return a("<span></span>") }, d.prototype.update = function (a) { if (0 === a.length) return void this.clear(); var b = a[0], c = this.display(b), d = this.$selection.find(".select2-selection__rendered"); d.empty().append(c), d.prop("title", b.title || b.text) }, d }), b.define("select2/selection/multiple", ["jquery", "./base", "../utils"], function (a, b, c) { function d() { d.__super__.constructor.apply(this, arguments) } return c.Extend(d, b), d.prototype.render = function () { var a = d.__super__.render.call(this); return a.addClass("select2-selection--multiple"), a.html('<ul class="select2-selection__rendered"></ul>'), a }, d.prototype.bind = function () { var b = this; d.__super__.bind.apply(this, arguments), this.$selection.on("click", function (a) { b.trigger("toggle", { originalEvent: a }) }), this.$selection.on("click", ".select2-selection__choice__remove", function (c) { var d = a(this), e = d.parent(), f = e.data("data"); b.trigger("unselect", { originalEvent: c, data: f }) }) }, d.prototype.clear = function () { this.$selection.find(".select2-selection__rendered").empty() }, d.prototype.display = function (a) { var b = this.options.get("templateSelection"), c = this.options.get("escapeMarkup"); return c(b(a)) }, d.prototype.selectionContainer = function () { var b = a('<li class="select2-selection__choice"><span class="select2-selection__choice__remove" role="presentation">&times;</span></li>'); return b }, d.prototype.update = function (a) { if (this.clear(), 0 !== a.length) { for (var b = [], d = 0; d < a.length; d++) { var e = a[d], f = this.display(e), g = this.selectionContainer(); g.append(f), g.prop("title", e.title || e.text), g.data("data", e), b.push(g) } var h = this.$selection.find(".select2-selection__rendered"); c.appendMany(h, b) } }, d }), b.define("select2/selection/placeholder", ["../utils"], function () { function a(a, b, c) { this.placeholder = this.normalizePlaceholder(c.get("placeholder")), a.call(this, b, c) } return a.prototype.normalizePlaceholder = function (a, b) { return "string" == typeof b && (b = { id: "", text: b }), b }, a.prototype.createPlaceholder = function (a, b) { var c = this.selectionContainer(); return c.html(this.display(b)), c.addClass("select2-selection__placeholder").removeClass("select2-selection__choice"), c }, a.prototype.update = function (a, b) { var c = 1 == b.length && b[0].id != this.placeholder.id, d = b.length > 1; if (d || c) return a.call(this, b); this.clear(); var e = this.createPlaceholder(this.placeholder); this.$selection.find(".select2-selection__rendered").append(e) }, a }), b.define("select2/selection/allowClear", ["jquery", "../keys"], function (a, b) { function c() { } return c.prototype.bind = function (a, b, c) { var d = this; a.call(this, b, c), null == this.placeholder && this.options.get("debug") && window.console && console.error && console.error("Select2: The `allowClear` option should be used in combination with the `placeholder` option."), this.$selection.on("mousedown", ".select2-selection__clear", function (a) { d._handleClear(a) }), b.on("keypress", function (a) { d._handleKeyboardClear(a, b) }) }, c.prototype._handleClear = function (a, b) { if (!this.options.get("disabled")) { var c = this.$selection.find(".select2-selection__clear"); if (0 !== c.length) { b.stopPropagation(); for (var d = c.data("data"), e = 0; e < d.length; e++) { var f = { data: d[e] }; if (this.trigger("unselect", f), f.prevented) return } this.$element.val(this.placeholder.id).trigger("change"), this.trigger("toggle") } } }, c.prototype._handleKeyboardClear = function (a, c, d) { d.isOpen() || (c.which == b.DELETE || c.which == b.BACKSPACE) && this._handleClear(c) }, c.prototype.update = function (b, c) { if (b.call(this, c), !(this.$selection.find(".select2-selection__placeholder").length > 0 || 0 === c.length)) { var d = a('<span class="select2-selection__clear">&times;</span>'); d.data("data", c), this.$selection.find(".select2-selection__rendered").prepend(d) } }, c }), b.define("select2/selection/search", ["jquery", "../utils", "../keys"], function (a, b, c) { function d(a, b, c) { a.call(this, b, c) } return d.prototype.render = function (b) { var c = a('<li class="select2-search select2-search--inline"><input class="select2-search__field" type="search" tabindex="-1" autocomplete="off" autocorrect="off" autocapitalize="off" spellcheck="false" role="textbox" /></li>'); this.$searchContainer = c, this.$search = c.find("input"); var d = b.call(this); return d }, d.prototype.bind = function (a, b, d) { var e = this; a.call(this, b, d), b.on("open", function () { e.$search.attr("tabindex", 0), e.$search.focus() }), b.on("close", function () { e.$search.attr("tabindex", -1), e.$search.val(""), e.$search.focus() }), b.on("enable", function () { e.$search.prop("disabled", !1) }), b.on("disable", function () { e.$search.prop("disabled", !0) }), this.$selection.on("focusin", ".select2-search--inline", function (a) { e.trigger("focus", a) }), this.$selection.on("focusout", ".select2-search--inline", function (a) { e.trigger("blur", a) }), this.$selection.on("keydown", ".select2-search--inline", function (a) { a.stopPropagation(), e.trigger("keypress", a), e._keyUpPrevented = a.isDefaultPrevented(); var b = a.which; if (b === c.BACKSPACE && "" === e.$search.val()) { var d = e.$searchContainer.prev(".select2-selection__choice"); if (d.length > 0) { var f = d.data("data"); e.searchRemoveChoice(f), a.preventDefault() } } }), this.$selection.on("input", ".select2-search--inline", function () { e.$selection.off("keyup.search") }), this.$selection.on("keyup.search input", ".select2-search--inline", function (a) { e.handleSearch(a) }) }, d.prototype.createPlaceholder = function (a, b) { this.$search.attr("placeholder", b.text) }, d.prototype.update = function (a, b) { this.$search.attr("placeholder", ""), a.call(this, b), this.$selection.find(".select2-selection__rendered").append(this.$searchContainer), this.resizeSearch() }, d.prototype.handleSearch = function () { if (this.resizeSearch(), !this._keyUpPrevented) { var a = this.$search.val(); this.trigger("query", { term: a }) } this._keyUpPrevented = !1 }, d.prototype.searchRemoveChoice = function (a, b) { this.trigger("unselect", { data: b }), this.trigger("open"), this.$search.val(b.text + " ") }, d.prototype.resizeSearch = function () { this.$search.css("width", "25px"); var a = ""; if ("" !== this.$search.attr("placeholder")) a = this.$selection.find(".select2-selection__rendered").innerWidth(); else { var b = this.$search.val().length + 1; a = .75 * b + "em" } this.$search.css("width", a) }, d }), b.define("select2/selection/eventRelay", ["jquery"], function (a) { function b() { } return b.prototype.bind = function (b, c, d) { var e = this, f = ["open", "opening", "close", "closing", "select", "selecting", "unselect", "unselecting"], g = ["opening", "closing", "selecting", "unselecting"]; b.call(this, c, d), c.on("*", function (b, c) { if (-1 !== a.inArray(b, f)) { c = c || {}; var d = a.Event("select2:" + b, { params: c }); e.$element.trigger(d), -1 !== a.inArray(b, g) && (c.prevented = d.isDefaultPrevented()) } }) }, b }), b.define("select2/translation", ["jquery", "require"], function (a, b) { function c(a) { this.dict = a || {} } return c.prototype.all = function () { return this.dict }, c.prototype.get = function (a) { return this.dict[a] }, c.prototype.extend = function (b) { this.dict = a.extend({}, b.all(), this.dict) }, c._cache = {}, c.loadPath = function (a) { if (!(a in c._cache)) { var d = b(a); c._cache[a] = d } return new c(c._cache[a]) }, c }), b.define("select2/diacritics", [], function () { var a = { "â’¶": "A", "ï¼¡": "A", "Ã€": "A", "Ã": "A", "Ã‚": "A", "áº¦": "A", "áº¤": "A", "áºª": "A", "áº¨": "A", "Ãƒ": "A", "Ä€": "A", "Ä‚": "A", "áº°": "A", "áº®": "A", "áº´": "A", "áº²": "A", "È¦": "A", "Ç ": "A", "Ã„": "A", "Çž": "A", "áº¢": "A", "Ã…": "A", "Çº": "A", "Ç": "A", "È€": "A", "È‚": "A", "áº ": "A", "áº¬": "A", "áº¶": "A", "á¸€": "A", "Ä„": "A", "Èº": "A", "â±¯": "A", "êœ²": "AA", "Ã†": "AE", "Ç¼": "AE", "Ç¢": "AE", "êœ´": "AO", "êœ¶": "AU", "êœ¸": "AV", "êœº": "AV", "êœ¼": "AY", "â’·": "B", "ï¼¢": "B", "á¸‚": "B", "á¸„": "B", "á¸†": "B", "Éƒ": "B", "Æ‚": "B", "Æ": "B", "â’¸": "C", "ï¼£": "C", "Ä†": "C", "Äˆ": "C", "ÄŠ": "C", "ÄŒ": "C", "Ã‡": "C", "á¸ˆ": "C", "Æ‡": "C", "È»": "C", "êœ¾": "C", "â’¹": "D", "ï¼¤": "D", "á¸Š": "D", "ÄŽ": "D", "á¸Œ": "D", "á¸": "D", "á¸’": "D", "á¸Ž": "D", "Ä": "D", "Æ‹": "D", "ÆŠ": "D", "Æ‰": "D", "ê¹": "D", "Ç±": "DZ", "Ç„": "DZ", "Ç²": "Dz", "Ç…": "Dz", "â’º": "E", "ï¼¥": "E", "Ãˆ": "E", "Ã‰": "E", "ÃŠ": "E", "á»€": "E", "áº¾": "E", "á»„": "E", "á»‚": "E", "áº¼": "E", "Ä’": "E", "á¸”": "E", "á¸–": "E", "Ä”": "E", "Ä–": "E", "Ã‹": "E", "áºº": "E", "Äš": "E", "È„": "E", "È†": "E", "áº¸": "E", "á»†": "E", "È¨": "E", "á¸œ": "E", "Ä˜": "E", "á¸˜": "E", "á¸š": "E", "Æ": "E", "ÆŽ": "E", "â’»": "F", "ï¼¦": "F", "á¸ž": "F", "Æ‘": "F", "ê»": "F", "â’¼": "G", "ï¼§": "G", "Ç´": "G", "Äœ": "G", "á¸ ": "G", "Äž": "G", "Ä ": "G", "Ç¦": "G", "Ä¢": "G", "Ç¤": "G", "Æ“": "G", "êž ": "G", "ê½": "G", "ê¾": "G", "â’½": "H", "ï¼¨": "H", "Ä¤": "H", "á¸¢": "H", "á¸¦": "H", "Èž": "H", "á¸¤": "H", "á¸¨": "H", "á¸ª": "H", "Ä¦": "H", "â±§": "H", "â±µ": "H", "êž": "H", "â’¾": "I", "ï¼©": "I", "ÃŒ": "I", "Ã": "I", "ÃŽ": "I", "Ä¨": "I", "Äª": "I", "Ä¬": "I", "Ä°": "I", "Ã": "I", "á¸®": "I", "á»ˆ": "I", "Ç": "I", "Èˆ": "I", "ÈŠ": "I", "á»Š": "I", "Ä®": "I", "á¸¬": "I", "Æ—": "I", "â’¿": "J", "ï¼ª": "J", "Ä´": "J", "Éˆ": "J", "â“€": "K", "ï¼«": "K", "á¸°": "K", "Ç¨": "K", "á¸²": "K", "Ä¶": "K", "á¸´": "K", "Æ˜": "K", "â±©": "K", "ê€": "K", "ê‚": "K", "ê„": "K", "êž¢": "K", "â“": "L", "ï¼¬": "L", "Ä¿": "L", "Ä¹": "L", "Ä½": "L", "á¸¶": "L", "á¸¸": "L", "Ä»": "L", "á¸¼": "L", "á¸º": "L", "Å": "L", "È½": "L", "â±¢": "L", "â± ": "L", "êˆ": "L", "ê†": "L", "êž€": "L", "Ç‡": "LJ", "Çˆ": "Lj", "â“‚": "M", "ï¼­": "M", "á¸¾": "M", "á¹€": "M", "á¹‚": "M", "â±®": "M", "Æœ": "M", "â“ƒ": "N", "ï¼®": "N", "Ç¸": "N", "Åƒ": "N", "Ã‘": "N", "á¹„": "N", "Å‡": "N", "á¹†": "N", "Å…": "N", "á¹Š": "N", "á¹ˆ": "N", "È ": "N", "Æ": "N", "êž": "N", "êž¤": "N", "ÇŠ": "NJ", "Ç‹": "Nj", "â“„": "O", "ï¼¯": "O", "Ã’": "O", "Ã“": "O", "Ã”": "O", "á»’": "O", "á»": "O", "á»–": "O", "á»”": "O", "Ã•": "O", "á¹Œ": "O", "È¬": "O", "á¹Ž": "O", "ÅŒ": "O", "á¹": "O", "á¹’": "O", "ÅŽ": "O", "È®": "O", "È°": "O", "Ã–": "O", "Èª": "O", "á»Ž": "O", "Å": "O", "Ç‘": "O", "ÈŒ": "O", "ÈŽ": "O", "Æ ": "O", "á»œ": "O", "á»š": "O", "á» ": "O", "á»ž": "O", "á»¢": "O", "á»Œ": "O", "á»˜": "O", "Çª": "O", "Ç¬": "O", "Ã˜": "O", "Ç¾": "O", "Æ†": "O", "ÆŸ": "O", "êŠ": "O", "êŒ": "O", "Æ¢": "OI", "êŽ": "OO", "È¢": "OU", "â“…": "P", "ï¼°": "P", "á¹”": "P", "á¹–": "P", "Æ¤": "P", "â±£": "P", "ê": "P", "ê’": "P", "ê”": "P", "â“†": "Q", "ï¼±": "Q", "ê–": "Q", "ê˜": "Q", "ÉŠ": "Q", "â“‡": "R", "ï¼²": "R", "Å”": "R", "á¹˜": "R", "Å˜": "R", "È": "R", "È’": "R", "á¹š": "R", "á¹œ": "R", "Å–": "R", "á¹ž": "R", "ÉŒ": "R", "â±¤": "R", "êš": "R", "êž¦": "R", "êž‚": "R", "â“ˆ": "S", "ï¼³": "S", "áºž": "S", "Åš": "S", "á¹¤": "S", "Åœ": "S", "á¹ ": "S", "Å ": "S", "á¹¦": "S", "á¹¢": "S", "á¹¨": "S", "È˜": "S", "Åž": "S", "â±¾": "S", "êž¨": "S", "êž„": "S", "â“‰": "T", "ï¼´": "T", "á¹ª": "T", "Å¤": "T", "á¹¬": "T", "Èš": "T", "Å¢": "T", "á¹°": "T", "á¹®": "T", "Å¦": "T", "Æ¬": "T", "Æ®": "T", "È¾": "T", "êž†": "T", "êœ¨": "TZ", "â“Š": "U", "ï¼µ": "U", "Ã™": "U", "Ãš": "U", "Ã›": "U", "Å¨": "U", "á¹¸": "U", "Åª": "U", "á¹º": "U", "Å¬": "U", "Ãœ": "U", "Ç›": "U", "Ç—": "U", "Ç•": "U", "Ç™": "U", "á»¦": "U", "Å®": "U", "Å°": "U", "Ç“": "U", "È”": "U", "È–": "U", "Æ¯": "U", "á»ª": "U", "á»¨": "U", "á»®": "U", "á»¬": "U", "á»°": "U", "á»¤": "U", "á¹²": "U", "Å²": "U", "á¹¶": "U", "á¹´": "U", "É„": "U", "â“‹": "V", "ï¼¶": "V", "á¹¼": "V", "á¹¾": "V", "Æ²": "V", "êž": "V", "É…": "V", "ê ": "VY", "â“Œ": "W", "ï¼·": "W", "áº€": "W", "áº‚": "W", "Å´": "W", "áº†": "W", "áº„": "W", "áºˆ": "W", "â±²": "W", "â“": "X", "ï¼¸": "X", "áºŠ": "X", "áºŒ": "X", "â“Ž": "Y", "ï¼¹": "Y", "á»²": "Y", "Ã": "Y", "Å¶": "Y", "á»¸": "Y", "È²": "Y", "áºŽ": "Y", "Å¸": "Y", "á»¶": "Y", "á»´": "Y", "Æ³": "Y", "ÉŽ": "Y", "á»¾": "Y", "â“": "Z", "ï¼º": "Z", "Å¹": "Z", "áº": "Z", "Å»": "Z", "Å½": "Z", "áº’": "Z", "áº”": "Z", "Æµ": "Z", "È¤": "Z", "â±¿": "Z", "â±«": "Z", "ê¢": "Z", "â“": "a", "ï½": "a", "áºš": "a", "Ã ": "a", "Ã¡": "a", "Ã¢": "a", "áº§": "a", "áº¥": "a", "áº«": "a", "áº©": "a", "Ã£": "a", "Ä": "a", "Äƒ": "a", "áº±": "a", "áº¯": "a", "áºµ": "a", "áº³": "a", "È§": "a", "Ç¡": "a", "Ã¤": "a", "ÇŸ": "a", "áº£": "a", "Ã¥": "a", "Ç»": "a", "ÇŽ": "a", "È": "a", "Èƒ": "a", "áº¡": "a", "áº­": "a", "áº·": "a", "á¸": "a", "Ä…": "a", "â±¥": "a", "É": "a", "êœ³": "aa", "Ã¦": "ae", "Ç½": "ae", "Ç£": "ae", "êœµ": "ao", "êœ·": "au", "êœ¹": "av", "êœ»": "av", "êœ½": "ay", "â“‘": "b", "ï½‚": "b", "á¸ƒ": "b", "á¸…": "b", "á¸‡": "b", "Æ€": "b", "Æƒ": "b", "É“": "b", "â“’": "c", "ï½ƒ": "c", "Ä‡": "c", "Ä‰": "c", "Ä‹": "c", "Ä": "c", "Ã§": "c", "á¸‰": "c", "Æˆ": "c", "È¼": "c", "êœ¿": "c", "â†„": "c", "â““": "d", "ï½„": "d", "á¸‹": "d", "Ä": "d", "á¸": "d", "á¸‘": "d", "á¸“": "d", "á¸": "d", "Ä‘": "d", "ÆŒ": "d", "É–": "d", "É—": "d", "êº": "d", "Ç³": "dz", "Ç†": "dz", "â“”": "e", "ï½…": "e", "Ã¨": "e", "Ã©": "e", "Ãª": "e", "á»": "e", "áº¿": "e", "á»…": "e", "á»ƒ": "e", "áº½": "e", "Ä“": "e", "á¸•": "e", "á¸—": "e", "Ä•": "e", "Ä—": "e", "Ã«": "e", "áº»": "e", "Ä›": "e", "È…": "e", "È‡": "e", "áº¹": "e", "á»‡": "e", "È©": "e", "á¸": "e", "Ä™": "e", "á¸™": "e", "á¸›": "e", "É‡": "e", "É›": "e", "Ç": "e", "â“•": "f", "ï½†": "f", "á¸Ÿ": "f", "Æ’": "f", "ê¼": "f", "â“–": "g", "ï½‡": "g", "Çµ": "g", "Ä": "g", "á¸¡": "g", "ÄŸ": "g", "Ä¡": "g", "Ç§": "g", "Ä£": "g", "Ç¥": "g", "É ": "g", "êž¡": "g", "áµ¹": "g", "ê¿": "g", "â“—": "h", "ï½ˆ": "h", "Ä¥": "h", "á¸£": "h", "á¸§": "h", "ÈŸ": "h", "á¸¥": "h", "á¸©": "h", "á¸«": "h", "áº–": "h", "Ä§": "h", "â±¨": "h", "â±¶": "h", "É¥": "h", "Æ•": "hv", "â“˜": "i", "ï½‰": "i", "Ã¬": "i", "Ã­": "i", "Ã®": "i", "Ä©": "i", "Ä«": "i", "Ä­": "i", "Ã¯": "i", "á¸¯": "i", "á»‰": "i", "Ç": "i", "È‰": "i", "È‹": "i", "á»‹": "i", "Ä¯": "i", "á¸­": "i", "É¨": "i", "Ä±": "i", "â“™": "j", "ï½Š": "j", "Äµ": "j", "Ç°": "j", "É‰": "j", "â“š": "k", "ï½‹": "k", "á¸±": "k", "Ç©": "k", "á¸³": "k", "Ä·": "k", "á¸µ": "k", "Æ™": "k", "â±ª": "k", "ê": "k", "êƒ": "k", "ê…": "k", "êž£": "k", "â“›": "l", "ï½Œ": "l", "Å€": "l", "Äº": "l", "Ä¾": "l", "á¸·": "l", "á¸¹": "l", "Ä¼": "l", "á¸½": "l", "á¸»": "l", "Å¿": "l", "Å‚": "l", "Æš": "l", "É«": "l", "â±¡": "l", "ê‰": "l", "êž": "l", "ê‡": "l", "Ç‰": "lj", "â“œ": "m", "ï½": "m", "á¸¿": "m", "á¹": "m", "á¹ƒ": "m", "É±": "m", "É¯": "m", "â“": "n", "ï½Ž": "n", "Ç¹": "n", "Å„": "n", "Ã±": "n", "á¹…": "n", "Åˆ": "n", "á¹‡": "n", "Å†": "n", "á¹‹": "n", "á¹‰": "n", "Æž": "n", "É²": "n", "Å‰": "n", "êž‘": "n", "êž¥": "n", "ÇŒ": "nj", "â“ž": "o", "ï½": "o", "Ã²": "o", "Ã³": "o", "Ã´": "o", "á»“": "o", "á»‘": "o", "á»—": "o", "á»•": "o", "Ãµ": "o", "á¹": "o", "È­": "o", "á¹": "o", "Å": "o", "á¹‘": "o", "á¹“": "o", "Å": "o", "È¯": "o", "È±": "o", "Ã¶": "o", "È«": "o", "á»": "o", "Å‘": "o", "Ç’": "o", "È": "o", "È": "o", "Æ¡": "o", "á»": "o", "á»›": "o", "á»¡": "o", "á»Ÿ": "o", "á»£": "o", "á»": "o", "á»™": "o", "Ç«": "o", "Ç­": "o", "Ã¸": "o", "Ç¿": "o", "É”": "o", "ê‹": "o", "ê": "o", "Éµ": "o", "Æ£": "oi", "È£": "ou", "ê": "oo", "â“Ÿ": "p", "ï½": "p", "á¹•": "p", "á¹—": "p", "Æ¥": "p", "áµ½": "p", "ê‘": "p", "ê“": "p", "ê•": "p", "â“ ": "q", "ï½‘": "q", "É‹": "q", "ê—": "q", "ê™": "q", "â“¡": "r", "ï½’": "r", "Å•": "r", "á¹™": "r", "Å™": "r", "È‘": "r", "È“": "r", "á¹›": "r", "á¹": "r", "Å—": "r", "á¹Ÿ": "r", "É": "r", "É½": "r", "ê›": "r", "êž§": "r", "êžƒ": "r", "â“¢": "s", "ï½“": "s", "ÃŸ": "s", "Å›": "s", "á¹¥": "s", "Å": "s", "á¹¡": "s", "Å¡": "s", "á¹§": "s", "á¹£": "s", "á¹©": "s", "È™": "s", "ÅŸ": "s", "È¿": "s", "êž©": "s", "êž…": "s", "áº›": "s", "â“£": "t", "ï½”": "t", "á¹«": "t", "áº—": "t", "Å¥": "t", "á¹­": "t", "È›": "t", "Å£": "t", "á¹±": "t", "á¹¯": "t", "Å§": "t", "Æ­": "t", "Êˆ": "t", "â±¦": "t", "êž‡": "t", "êœ©": "tz", "â“¤": "u", "ï½•": "u", "Ã¹": "u", "Ãº": "u", "Ã»": "u", "Å©": "u", "á¹¹": "u", "Å«": "u", "á¹»": "u", "Å­": "u", "Ã¼": "u", "Çœ": "u", "Ç˜": "u", "Ç–": "u", "Çš": "u", "á»§": "u", "Å¯": "u", "Å±": "u", "Ç”": "u", "È•": "u", "È—": "u", "Æ°": "u", "á»«": "u", "á»©": "u", "á»¯": "u", "á»­": "u", "á»±": "u", "á»¥": "u", "á¹³": "u", "Å³": "u", "á¹·": "u", "á¹µ": "u", "Ê‰": "u", "â“¥": "v", "ï½–": "v", "á¹½": "v", "á¹¿": "v", "Ê‹": "v", "êŸ": "v", "ÊŒ": "v", "ê¡": "vy", "â“¦": "w", "ï½—": "w", "áº": "w", "áºƒ": "w", "Åµ": "w", "áº‡": "w", "áº…": "w", "áº˜": "w", "áº‰": "w", "â±³": "w", "â“§": "x", "ï½˜": "x", "áº‹": "x", "áº": "x", "â“¨": "y", "ï½™": "y", "á»³": "y", "Ã½": "y", "Å·": "y", "á»¹": "y", "È³": "y", "áº": "y", "Ã¿": "y", "á»·": "y", "áº™": "y", "á»µ": "y", "Æ´": "y", "É": "y", "á»¿": "y", "â“©": "z", "ï½š": "z", "Åº": "z", "áº‘": "z", "Å¼": "z", "Å¾": "z", "áº“": "z", "áº•": "z", "Æ¶": "z", "È¥": "z", "É€": "z", "â±¬": "z", "ê£": "z", "Î†": "Î‘", "Îˆ": "Î•", "Î‰": "Î—", "ÎŠ": "Î™", "Îª": "Î™", "ÎŒ": "ÎŸ", "ÎŽ": "Î¥", "Î«": "Î¥", "Î": "Î©", "Î¬": "Î±", "Î­": "Îµ", "Î®": "Î·", "Î¯": "Î¹", "ÏŠ": "Î¹", "Î": "Î¹", "ÏŒ": "Î¿", "Ï": "Ï…", "Ï‹": "Ï…", "Î°": "Ï…", "Ï‰": "Ï‰", "Ï‚": "Ïƒ" }; return a }), b.define("select2/data/base", ["../utils"], function (a) { function b() { b.__super__.constructor.call(this) } return a.Extend(b, a.Observable), b.prototype.current = function () { throw new Error("The `current` method must be defined in child classes.") }, b.prototype.query = function () { throw new Error("The `query` method must be defined in child classes.") }, b.prototype.bind = function () { }, b.prototype.destroy = function () { }, b.prototype.generateResultId = function (b, c) { var d = b.id + "-result-"; return d += a.generateChars(4), d += null != c.id ? "-" + c.id.toString() : "-" + a.generateChars(4) }, b }), b.define("select2/data/select", ["./base", "../utils", "jquery"], function (a, b, c) {
            function d(a, b) { this.$element = a, this.options = b, d.__super__.constructor.call(this) } return b.Extend(d, a), d.prototype.current = function (a) { var b = [], d = this; this.$element.find(":selected").each(function () { var a = c(this), e = d.item(a); b.push(e) }), a(b) }, d.prototype.select = function (a) { var b = this; if (a.selected = !0, c(a.element).is("option")) return a.element.selected = !0, void this.$element.trigger("change"); if (this.$element.prop("multiple")) this.current(function (d) { var e = []; a = [a], a.push.apply(a, d); for (var f = 0; f < a.length; f++) { var g = a[f].id; -1 === c.inArray(g, e) && e.push(g) } b.$element.val(e), b.$element.trigger("change") }); else { var d = a.id; this.$element.val(d), this.$element.trigger("change") } }, d.prototype.unselect = function (a) { var b = this; if (this.$element.prop("multiple")) return a.selected = !1, c(a.element).is("option") ? (a.element.selected = !1, void this.$element.trigger("change")) : void this.current(function (d) { for (var e = [], f = 0; f < d.length; f++) { var g = d[f].id; g !== a.id && -1 === c.inArray(g, e) && e.push(g) } b.$element.val(e), b.$element.trigger("change") }) }, d.prototype.bind = function (a) { var b = this; this.container = a, a.on("select", function (a) { b.select(a.data) }), a.on("unselect", function (a) { b.unselect(a.data) }) }, d.prototype.destroy = function () { this.$element.find("*").each(function () { c.removeData(this, "data") }) }, d.prototype.query = function (a, b) { var d = [], e = this, f = this.$element.children(); f.each(function () { var b = c(this); if (b.is("option") || b.is("optgroup")) { var f = e.item(b), g = e.matches(a, f); null !== g && d.push(g) } }), b({ results: d }) }, d.prototype.addOptions = function (a) { b.appendMany(this.$element, a) }, d.prototype.option = function (a) { var b; a.children ? (b = document.createElement("optgroup"), b.label = a.text) : (b = document.createElement("option"), void 0 !== b.textContent ? b.textContent = a.text : b.innerText = a.text), a.id && (b.value = a.id), a.disabled && (b.disabled = !0), a.selected && (b.selected = !0), a.title && (b.title = a.title); var d = c(b), e = this._normalizeItem(a); return e.element = b, c.data(b, "data", e), d }, d.prototype.item = function (a) {
                var b = {};
                if (b = c.data(a[0], "data"), null != b) return b; if (a.is("option")) b = { id: a.val(), text: a.text(), disabled: a.prop("disabled"), selected: a.prop("selected"), title: a.prop("title") }; else if (a.is("optgroup")) { b = { text: a.prop("label"), children: [], title: a.prop("title") }; for (var d = a.children("option"), e = [], f = 0; f < d.length; f++) { var g = c(d[f]), h = this.item(g); e.push(h) } b.children = e } return b = this._normalizeItem(b), b.element = a[0], c.data(a[0], "data", b), b
            }, d.prototype._normalizeItem = function (a) { c.isPlainObject(a) || (a = { id: a, text: a }), a = c.extend({}, { text: "" }, a); var b = { selected: !1, disabled: !1 }; return null != a.id && (a.id = a.id.toString()), null != a.text && (a.text = a.text.toString()), null == a._resultId && a.id && null != this.container && (a._resultId = this.generateResultId(this.container, a)), c.extend({}, b, a) }, d.prototype.matches = function (a, b) { var c = this.options.get("matcher"); return c(a, b) }, d
        }), b.define("select2/data/array", ["./select", "../utils", "jquery"], function (a, b, c) { function d(a, b) { var c = b.get("data") || []; d.__super__.constructor.call(this, a, b), this.addOptions(this.convertToOptions(c)) } return b.Extend(d, a), d.prototype.select = function (a) { var b = this.$element.find("option").filter(function (b, c) { return c.value == a.id.toString() }); 0 === b.length && (b = this.option(a), this.addOptions(b)), d.__super__.select.call(this, a) }, d.prototype.convertToOptions = function (a) { function d(a) { return function () { return c(this).val() == a.id } } for (var e = this, f = this.$element.find("option"), g = f.map(function () { return e.item(c(this)).id }).get(), h = [], i = 0; i < a.length; i++) { var j = this._normalizeItem(a[i]); if (c.inArray(j.id, g) >= 0) { var k = f.filter(d(j)), l = this.item(k), m = (c.extend(!0, {}, l, j), this.option(l)); k.replaceWith(m) } else { var n = this.option(j); if (j.children) { var o = this.convertToOptions(j.children); b.appendMany(n, o) } h.push(n) } } return h }, d }), b.define("select2/data/ajax", ["./array", "../utils", "jquery"], function (a, b, c) { function d(b, c) { this.ajaxOptions = this._applyDefaults(c.get("ajax")), null != this.ajaxOptions.processResults && (this.processResults = this.ajaxOptions.processResults), a.__super__.constructor.call(this, b, c) } return b.Extend(d, a), d.prototype._applyDefaults = function (a) { var b = { data: function (a) { return { q: a.term } }, transport: function (a, b, d) { var e = c.ajax(a); return e.then(b), e.fail(d), e } }; return c.extend({}, b, a, !0) }, d.prototype.processResults = function (a) { return a }, d.prototype.query = function (a, b) { function d() { var d = f.transport(f, function (d) { var f = e.processResults(d, a); e.options.get("debug") && window.console && console.error && (f && f.results && c.isArray(f.results) || console.error("Select2: The AJAX results did not return an array in the `results` key of the response.")), b(f) }, function () { }); e._request = d } var e = this; null != this._request && (c.isFunction(this._request.abort) && this._request.abort(), this._request = null); var f = c.extend({ type: "GET" }, this.ajaxOptions); "function" == typeof f.url && (f.url = f.url(a)), "function" == typeof f.data && (f.data = f.data(a)), this.ajaxOptions.delay && "" !== a.term ? (this._queryTimeout && window.clearTimeout(this._queryTimeout), this._queryTimeout = window.setTimeout(d, this.ajaxOptions.delay)) : d() }, d }), b.define("select2/data/tags", ["jquery"], function (a) { function b(b, c, d) { var e = d.get("tags"), f = d.get("createTag"); if (void 0 !== f && (this.createTag = f), b.call(this, c, d), a.isArray(e)) for (var g = 0; g < e.length; g++) { var h = e[g], i = this._normalizeItem(h), j = this.option(i); this.$element.append(j) } } return b.prototype.query = function (a, b, c) { function d(a, f) { for (var g = a.results, h = 0; h < g.length; h++) { var i = g[h], j = null != i.children && !d({ results: i.children }, !0), k = i.text === b.term; if (k || j) return f ? !1 : (a.data = g, void c(a)) } if (f) return !0; var l = e.createTag(b); if (null != l) { var m = e.option(l); m.attr("data-select2-tag", !0), e.addOptions([m]), e.insertTag(g, l) } a.results = g, c(a) } var e = this; return this._removeOldTags(), null == b.term || null != b.page ? void a.call(this, b, c) : void a.call(this, b, d) }, b.prototype.createTag = function (b, c) { var d = a.trim(c.term); return "" === d ? null : { id: d, text: d } }, b.prototype.insertTag = function (a, b, c) { b.unshift(c) }, b.prototype._removeOldTags = function () { var b = (this._lastTag, this.$element.find("option[data-select2-tag]")); b.each(function () { this.selected || a(this).remove() }) }, b }), b.define("select2/data/tokenizer", ["jquery"], function (a) { function b(a, b, c) { var d = c.get("tokenizer"); void 0 !== d && (this.tokenizer = d), a.call(this, b, c) } return b.prototype.bind = function (a, b, c) { a.call(this, b, c), this.$search = b.dropdown.$search || b.selection.$search || c.find(".select2-search__field") }, b.prototype.query = function (a, b, c) { function d(a) { e.select(a) } var e = this; b.term = b.term || ""; var f = this.tokenizer(b, this.options, d); f.term !== b.term && (this.$search.length && (this.$search.val(f.term), this.$search.focus()), b.term = f.term), a.call(this, b, c) }, b.prototype.tokenizer = function (b, c, d, e) { for (var f = d.get("tokenSeparators") || [], g = c.term, h = 0, i = this.createTag || function (a) { return { id: a.term, text: a.term } }; h < g.length;) { var j = g[h]; if (-1 !== a.inArray(j, f)) { var k = g.substr(0, h), l = a.extend({}, c, { term: k }), m = i(l); e(m), g = g.substr(h + 1) || "", h = 0 } else h++ } return { term: g } }, b }), b.define("select2/data/minimumInputLength", [], function () { function a(a, b, c) { this.minimumInputLength = c.get("minimumInputLength"), a.call(this, b, c) } return a.prototype.query = function (a, b, c) { return b.term = b.term || "", b.term.length < this.minimumInputLength ? void this.trigger("results:message", { message: "inputTooShort", args: { minimum: this.minimumInputLength, input: b.term, params: b } }) : void a.call(this, b, c) }, a }), b.define("select2/data/maximumInputLength", [], function () { function a(a, b, c) { this.maximumInputLength = c.get("maximumInputLength"), a.call(this, b, c) } return a.prototype.query = function (a, b, c) { return b.term = b.term || "", this.maximumInputLength > 0 && b.term.length > this.maximumInputLength ? void this.trigger("results:message", { message: "inputTooLong", args: { maximum: this.maximumInputLength, input: b.term, params: b } }) : void a.call(this, b, c) }, a }), b.define("select2/data/maximumSelectionLength", [], function () { function a(a, b, c) { this.maximumSelectionLength = c.get("maximumSelectionLength"), a.call(this, b, c) } return a.prototype.query = function (a, b, c) { var d = this; this.current(function (e) { var f = null != e ? e.length : 0; return d.maximumSelectionLength > 0 && f >= d.maximumSelectionLength ? void d.trigger("results:message", { message: "maximumSelected", args: { maximum: d.maximumSelectionLength } }) : void a.call(d, b, c) }) }, a }), b.define("select2/dropdown", ["jquery", "./utils"], function (a, b) { function c(a, b) { this.$element = a, this.options = b, c.__super__.constructor.call(this) } return b.Extend(c, b.Observable), c.prototype.render = function () { var b = a('<span class="select2-dropdown"><span class="select2-results"></span></span>'); return b.attr("dir", this.options.get("dir")), this.$dropdown = b, b }, c.prototype.position = function () { }, c.prototype.destroy = function () { this.$dropdown.remove() }, c }), b.define("select2/dropdown/search", ["jquery", "../utils"], function (a) { function b() { } return b.prototype.render = function (b) { var c = b.call(this), d = a('<span class="select2-search select2-search--dropdown"><input class="select2-search__field" type="search" tabindex="-1" autocomplete="off" autocorrect="off" autocapitalize="off" spellcheck="false" role="textbox" /></span>'); return this.$searchContainer = d, this.$search = d.find("input"), c.prepend(d), c }, b.prototype.bind = function (b, c, d) { var e = this; b.call(this, c, d), this.$search.on("keydown", function (a) { e.trigger("keypress", a), e._keyUpPrevented = a.isDefaultPrevented() }), this.$search.on("input", function () { a(this).off("keyup") }), this.$search.on("keyup input", function (a) { e.handleSearch(a) }), c.on("open", function () { e.$search.attr("tabindex", 0), e.$search.focus(), window.setTimeout(function () { e.$search.focus() }, 0) }), c.on("close", function () { e.$search.attr("tabindex", -1), e.$search.val("") }), c.on("results:all", function (a) { if (null == a.query.term || "" === a.query.term) { var b = e.showSearch(a); b ? e.$searchContainer.removeClass("select2-search--hide") : e.$searchContainer.addClass("select2-search--hide") } }) }, b.prototype.handleSearch = function () { if (!this._keyUpPrevented) { var a = this.$search.val(); this.trigger("query", { term: a }) } this._keyUpPrevented = !1 }, b.prototype.showSearch = function () { return !0 }, b }), b.define("select2/dropdown/hidePlaceholder", [], function () { function a(a, b, c, d) { this.placeholder = this.normalizePlaceholder(c.get("placeholder")), a.call(this, b, c, d) } return a.prototype.append = function (a, b) { b.results = this.removePlaceholder(b.results), a.call(this, b) }, a.prototype.normalizePlaceholder = function (a, b) { return "string" == typeof b && (b = { id: "", text: b }), b }, a.prototype.removePlaceholder = function (a, b) { for (var c = b.slice(0), d = b.length - 1; d >= 0; d--) { var e = b[d]; this.placeholder.id === e.id && c.splice(d, 1) } return c }, a }), b.define("select2/dropdown/infiniteScroll", ["jquery"], function (a) { function b(a, b, c, d) { this.lastParams = {}, a.call(this, b, c, d), this.$loadingMore = this.createLoadingMore(), this.loading = !1 } return b.prototype.append = function (a, b) { this.$loadingMore.remove(), this.loading = !1, a.call(this, b), this.showLoadingMore(b) && this.$results.append(this.$loadingMore) }, b.prototype.bind = function (b, c, d) { var e = this; b.call(this, c, d), c.on("query", function (a) { e.lastParams = a, e.loading = !0 }), c.on("query:append", function (a) { e.lastParams = a, e.loading = !0 }), this.$results.on("scroll", function () { var b = a.contains(document.documentElement, e.$loadingMore[0]); if (!e.loading && b) { var c = e.$results.offset().top + e.$results.outerHeight(!1), d = e.$loadingMore.offset().top + e.$loadingMore.outerHeight(!1); c + 50 >= d && e.loadMore() } }) }, b.prototype.loadMore = function () { this.loading = !0; var b = a.extend({}, { page: 1 }, this.lastParams); b.page++, this.trigger("query:append", b) }, b.prototype.showLoadingMore = function (a, b) { return b.pagination && b.pagination.more }, b.prototype.createLoadingMore = function () { var b = a('<li class="option load-more" role="treeitem"></li>'), c = this.options.get("translations").get("loadingMore"); return b.html(c(this.lastParams)), b }, b }), b.define("select2/dropdown/attachBody", ["jquery", "../utils"], function (a, b) { function c(a, b, c) { this.$dropdownParent = c.get("dropdownParent") || document.body, a.call(this, b, c) } return c.prototype.bind = function (a, b, c) { var d = this, e = !1; a.call(this, b, c), b.on("open", function () { d._showDropdown(), d._attachPositioningHandler(b), e || (e = !0, b.on("results:all", function () { d._positionDropdown(), d._resizeDropdown() }), b.on("results:append", function () { d._positionDropdown(), d._resizeDropdown() })) }), b.on("close", function () { d._hideDropdown(), d._detachPositioningHandler(b) }), this.$dropdownContainer.on("mousedown", function (a) { a.stopPropagation() }) }, c.prototype.position = function (a, b, c) { b.attr("class", c.attr("class")), b.removeClass("select2"), b.addClass("select2-container--open"), b.css({ position: "absolute", top: -999999 }), this.$container = c }, c.prototype.render = function (b) { var c = a("<span></span>"), d = b.call(this); return c.append(d), this.$dropdownContainer = c, c }, c.prototype._hideDropdown = function () { this.$dropdownContainer.detach() }, c.prototype._attachPositioningHandler = function (c) { var d = this, e = "scroll.select2." + c.id, f = "resize.select2." + c.id, g = "orientationchange.select2." + c.id, h = this.$container.parents().filter(b.hasScroll); h.each(function () { a(this).data("select2-scroll-position", { x: a(this).scrollLeft(), y: a(this).scrollTop() }) }), h.on(e, function () { var b = a(this).data("select2-scroll-position"); a(this).scrollTop(b.y) }), a(window).on(e + " " + f + " " + g, function () { d._positionDropdown(), d._resizeDropdown() }) }, c.prototype._detachPositioningHandler = function (c) { var d = "scroll.select2." + c.id, e = "resize.select2." + c.id, f = "orientationchange.select2." + c.id, g = this.$container.parents().filter(b.hasScroll); g.off(d), a(window).off(d + " " + e + " " + f) }, c.prototype._positionDropdown = function () { var b = a(window), c = this.$dropdown.hasClass("select2-dropdown--above"), d = this.$dropdown.hasClass("select2-dropdown--below"), e = null, f = (this.$container.position(), this.$container.offset()); f.bottom = f.top + this.$container.outerHeight(!1); var g = { height: this.$container.outerHeight(!1) }; g.top = f.top, g.bottom = f.top + g.height; var h = { height: this.$dropdown.outerHeight(!1) }, i = { top: b.scrollTop(), bottom: b.scrollTop() + b.height() }, j = i.top < f.top - h.height, k = i.bottom > f.bottom + h.height, l = { left: f.left, top: g.bottom }; c || d || (e = "below"), k || !j || c ? !j && k && c && (e = "below") : e = "above", ("above" == e || c && "below" !== e) && (l.top = g.top - h.height), null != e && (this.$dropdown.removeClass("select2-dropdown--below select2-dropdown--above").addClass("select2-dropdown--" + e), this.$container.removeClass("select2-container--below select2-container--above").addClass("select2-container--" + e)), this.$dropdownContainer.css(l) }, c.prototype._resizeDropdown = function () { this.$dropdownContainer.width(); var a = { width: this.$container.outerWidth(!1) + "px" }; this.options.get("dropdownAutoWidth") && (a.minWidth = a.width, a.width = "auto"), this.$dropdown.css(a) }, c.prototype._showDropdown = function () { this.$dropdownContainer.appendTo(this.$dropdownParent), this._positionDropdown(), this._resizeDropdown() }, c }), b.define("select2/dropdown/minimumResultsForSearch", [], function () { function a(b) { for (var c = 0, d = 0; d < b.length; d++) { var e = b[d]; e.children ? c += a(e.children) : c++ } return c } function b(a, b, c, d) { this.minimumResultsForSearch = c.get("minimumResultsForSearch"), this.minimumResultsForSearch < 0 && (this.minimumResultsForSearch = 1 / 0), a.call(this, b, c, d) } return b.prototype.showSearch = function (b, c) { return a(c.data.results) < this.minimumResultsForSearch ? !1 : b.call(this, c) }, b }), b.define("select2/dropdown/selectOnClose", [], function () { function a() { } return a.prototype.bind = function (a, b, c) { var d = this; a.call(this, b, c), b.on("close", function () { d._handleSelectOnClose() }) }, a.prototype._handleSelectOnClose = function () { var a = this.getHighlightedResults(); a.length < 1 || this.trigger("select", { data: a.data("data") }) }, a }), b.define("select2/dropdown/closeOnSelect", [], function () { function a() { } return a.prototype.bind = function (a, b, c) { var d = this; a.call(this, b, c), b.on("select", function (a) { d._selectTriggered(a) }), b.on("unselect", function (a) { d._selectTriggered(a) }) }, a.prototype._selectTriggered = function (a, b) { var c = b.originalEvent; c && c.ctrlKey || this.trigger("close") }, a }), b.define("select2/i18n/en", [], function () { return { errorLoading: function () { return "The results could not be loaded." }, inputTooLong: function (a) { var b = a.input.length - a.maximum, c = "Please delete " + b + " character"; return 1 != b && (c += "s"), c }, inputTooShort: function (a) { var b = a.minimum - a.input.length, c = "Please enter " + b + " or more characters"; return c }, loadingMore: function () { return "Loading more resultsâ€¦" }, maximumSelected: function (a) { var b = "You can only select " + a.maximum + " item"; return 1 != a.maximum && (b += "s"), b }, noResults: function () { return "No results found" }, searching: function () { return "Searchingâ€¦" } } }), b.define("select2/defaults", ["jquery", "require", "./results", "./selection/single", "./selection/multiple", "./selection/placeholder", "./selection/allowClear", "./selection/search", "./selection/eventRelay", "./utils", "./translation", "./diacritics", "./data/select", "./data/array", "./data/ajax", "./data/tags", "./data/tokenizer", "./data/minimumInputLength", "./data/maximumInputLength", "./data/maximumSelectionLength", "./dropdown", "./dropdown/search", "./dropdown/hidePlaceholder", "./dropdown/infiniteScroll", "./dropdown/attachBody", "./dropdown/minimumResultsForSearch", "./dropdown/selectOnClose", "./dropdown/closeOnSelect", "./i18n/en"], function (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, A, B, C) { function D() { this.reset() } D.prototype.apply = function (l) { if (l = a.extend({}, this.defaults, l), null == l.dataAdapter) { if (l.dataAdapter = null != l.ajax ? o : null != l.data ? n : m, l.minimumInputLength > 0 && (l.dataAdapter = j.Decorate(l.dataAdapter, r)), l.maximumInputLength > 0 && (l.dataAdapter = j.Decorate(l.dataAdapter, s)), l.maximumSelectionLength > 0 && (l.dataAdapter = j.Decorate(l.dataAdapter, t)), l.tags && (l.dataAdapter = j.Decorate(l.dataAdapter, p)), (null != l.tokenSeparators || null != l.tokenizer) && (l.dataAdapter = j.Decorate(l.dataAdapter, q)), null != l.query) { var C = b(l.amdBase + "compat/query"); l.dataAdapter = j.Decorate(l.dataAdapter, C) } if (null != l.initSelection) { var D = b(l.amdBase + "compat/initSelection"); l.dataAdapter = j.Decorate(l.dataAdapter, D) } } if (null == l.resultsAdapter && (l.resultsAdapter = c, null != l.ajax && (l.resultsAdapter = j.Decorate(l.resultsAdapter, x)), null != l.placeholder && (l.resultsAdapter = j.Decorate(l.resultsAdapter, w)), l.selectOnClose && (l.resultsAdapter = j.Decorate(l.resultsAdapter, A))), null == l.dropdownAdapter) { if (l.multiple) l.dropdownAdapter = u; else { var E = j.Decorate(u, v); l.dropdownAdapter = E } if (0 !== l.minimumResultsForSearch && (l.dropdownAdapter = j.Decorate(l.dropdownAdapter, z)), l.closeOnSelect && (l.dropdownAdapter = j.Decorate(l.dropdownAdapter, B)), null != l.dropdownCssClass || null != l.dropdownCss || null != l.adaptDropdownCssClass) { var F = b(l.amdBase + "compat/dropdownCss"); l.dropdownAdapter = j.Decorate(l.dropdownAdapter, F) } l.dropdownAdapter = j.Decorate(l.dropdownAdapter, y) } if (null == l.selectionAdapter) { if (l.selectionAdapter = l.multiple ? e : d, null != l.placeholder && (l.selectionAdapter = j.Decorate(l.selectionAdapter, f)), l.allowClear && (l.selectionAdapter = j.Decorate(l.selectionAdapter, g)), l.multiple && (l.selectionAdapter = j.Decorate(l.selectionAdapter, h)), null != l.containerCssClass || null != l.containerCss || null != l.adaptContainerCssClass) { var G = b(l.amdBase + "compat/containerCss"); l.selectionAdapter = j.Decorate(l.selectionAdapter, G) } l.selectionAdapter = j.Decorate(l.selectionAdapter, i) } if ("string" == typeof l.language) if (l.language.indexOf("-") > 0) { var H = l.language.split("-"), I = H[0]; l.language = [l.language, I] } else l.language = [l.language]; if (a.isArray(l.language)) { var J = new k; l.language.push("en"); for (var K = l.language, L = 0; L < K.length; L++) { var M = K[L], N = {}; try { N = k.loadPath(M) } catch (O) { try { M = this.defaults.amdLanguageBase + M, N = k.loadPath(M) } catch (P) { l.debug && window.console && console.warn && console.warn('Select2: The language file for "' + M + '" could not be automatically loaded. A fallback will be used instead.'); continue } } J.extend(N) } l.translations = J } else { var Q = k.loadPath(this.defaults.amdLanguageBase + "en"), R = new k(l.language); R.extend(Q), l.translations = R } return l }, D.prototype.reset = function () { function b(a) { function b(a) { return l[a] || a } return a.replace(/[^\u0000-\u007E]/g, b) } function c(d, e) { if ("" === a.trim(d.term)) return e; if (e.children && e.children.length > 0) { for (var f = a.extend(!0, {}, e), g = e.children.length - 1; g >= 0; g--) { var h = e.children[g], i = c(d, h); null == i && f.children.splice(g, 1) } return f.children.length > 0 ? f : c(d, f) } var j = b(e.text).toUpperCase(), k = b(d.term).toUpperCase(); return j.indexOf(k) > -1 ? e : null } this.defaults = { amdBase: "./", amdLanguageBase: "./i18n/", closeOnSelect: !0, debug: !1, dropdownAutoWidth: !1, escapeMarkup: j.escapeMarkup, language: C, matcher: c, minimumInputLength: 0, maximumInputLength: 0, maximumSelectionLength: 0, minimumResultsForSearch: 0, selectOnClose: !1, sorter: function (a) { return a }, templateResult: function (a) { return a.text }, templateSelection: function (a) { return a.text }, theme: "default", width: "resolve" } }, D.prototype.set = function (b, c) { var d = a.camelCase(b), e = {}; e[d] = c; var f = j._convertData(e); a.extend(this.defaults, f) }; var E = new D; return E }), b.define("select2/options", ["require", "jquery", "./defaults", "./utils"], function (a, b, c, d) { function e(b, e) { if (this.options = b, null != e && this.fromElement(e), this.options = c.apply(this.options), e && e.is("input")) { var f = a(this.get("amdBase") + "compat/inputData"); this.options.dataAdapter = d.Decorate(this.options.dataAdapter, f) } } return e.prototype.fromElement = function (a) { var c = ["select2"]; null == this.options.multiple && (this.options.multiple = a.prop("multiple")), null == this.options.disabled && (this.options.disabled = a.prop("disabled")), null == this.options.language && (a.prop("lang") ? this.options.language = a.prop("lang").toLowerCase() : a.closest("[lang]").prop("lang") && (this.options.language = a.closest("[lang]").prop("lang"))), null == this.options.dir && (this.options.dir = a.prop("dir") ? a.prop("dir") : a.closest("[dir]").prop("dir") ? a.closest("[dir]").prop("dir") : "ltr"), a.prop("disabled", this.options.disabled), a.prop("multiple", this.options.multiple), a.data("select2Tags") && (this.options.debug && window.console && console.warn && console.warn('Select2: The `data-select2-tags` attribute has been changed to use the `data-data` and `data-tags="true"` attributes and will be removed in future versions of Select2.'), a.data("data", a.data("select2Tags")), a.data("tags", !0)), a.data("ajaxUrl") && (this.options.debug && window.console && console.warn && console.warn("Select2: The `data-ajax-url` attribute has been changed to `data-ajax--url` and support for the old attribute will be removed in future versions of Select2."), a.attr("ajax--url", a.data("ajaxUrl")), a.data("ajax--url", a.data("ajaxUrl"))); var e = {}; e = b.fn.jquery && "1." == b.fn.jquery.substr(0, 2) && a[0].dataset ? b.extend(!0, {}, a[0].dataset, a.data()) : a.data(); var f = b.extend(!0, {}, e); f = d._convertData(f); for (var g in f) b.inArray(g, c) > -1 || (b.isPlainObject(this.options[g]) ? b.extend(this.options[g], f[g]) : this.options[g] = f[g]); return this }, e.prototype.get = function (a) { return this.options[a] }, e.prototype.set = function (a, b) { this.options[a] = b }, e }), b.define("select2/core", ["jquery", "./options", "./utils", "./keys"], function (a, b, c, d) { var e = function (a, c) { null != a.data("select2") && a.data("select2").destroy(), this.$element = a, this.id = this._generateId(a), c = c || {}, this.options = new b(c, a), e.__super__.constructor.call(this); var d = a.attr("tabindex") || 0; a.data("old-tabindex", d), a.attr("tabindex", "-1"); var f = this.options.get("dataAdapter"); this.dataAdapter = new f(a, this.options); var g = this.render(); this._placeContainer(g); var h = this.options.get("selectionAdapter"); this.selection = new h(a, this.options), this.$selection = this.selection.render(), this.selection.position(this.$selection, g); var i = this.options.get("dropdownAdapter"); this.dropdown = new i(a, this.options), this.$dropdown = this.dropdown.render(), this.dropdown.position(this.$dropdown, g); var j = this.options.get("resultsAdapter"); this.results = new j(a, this.options, this.dataAdapter), this.$results = this.results.render(), this.results.position(this.$results, this.$dropdown); var k = this; this._bindAdapters(), this._registerDomEvents(), this._registerDataEvents(), this._registerSelectionEvents(), this._registerDropdownEvents(), this._registerResultsEvents(), this._registerEvents(), this.dataAdapter.current(function (a) { k.trigger("selection:update", { data: a }) }), a.addClass("select2-hidden-accessible"), a.attr("aria-hidden", "true"), this._syncAttributes(), a.data("select2", this) }; return c.Extend(e, c.Observable), e.prototype._generateId = function (a) { var b = ""; return b = null != a.attr("id") ? a.attr("id") : null != a.attr("name") ? a.attr("name") + "-" + c.generateChars(2) : c.generateChars(4), b = "select2-" + b }, e.prototype._placeContainer = function (a) { a.insertAfter(this.$element); var b = this._resolveWidth(this.$element, this.options.get("width")); null != b && a.css("width", b) }, e.prototype._resolveWidth = function (a, b) { var c = /^width:(([-+]?([0-9]*\.)?[0-9]+)(px|em|ex|%|in|cm|mm|pt|pc))/i; if ("resolve" == b) { var d = this._resolveWidth(a, "style"); return null != d ? d : this._resolveWidth(a, "element") } if ("element" == b) { var e = a.outerWidth(!1); return 0 >= e ? "auto" : e + "px" } if ("style" == b) { var f = a.attr("style"); if ("string" != typeof f) return null; for (var g = f.split(";"), h = 0, i = g.length; i > h; h += 1) { var j = g[h].replace(/\s/g, ""), k = j.match(c); if (null !== k && k.length >= 1) return k[1] } return null } return b }, e.prototype._bindAdapters = function () { this.dataAdapter.bind(this, this.$container), this.selection.bind(this, this.$container), this.dropdown.bind(this, this.$container), this.results.bind(this, this.$container) }, e.prototype._registerDomEvents = function () { var b = this; this.$element.on("change.select2", function () { b.dataAdapter.current(function (a) { b.trigger("selection:update", { data: a }) }) }), this._sync = c.bind(this._syncAttributes, this), this.$element[0].attachEvent && this.$element[0].attachEvent("onpropertychange", this._sync); var d = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver; null != d ? (this._observer = new d(function (c) { a.each(c, b._sync) }), this._observer.observe(this.$element[0], { attributes: !0, subtree: !1 })) : this.$element[0].addEventListener && this.$element[0].addEventListener("DOMAttrModified", b._sync, !1) }, e.prototype._registerDataEvents = function () { var a = this; this.dataAdapter.on("*", function (b, c) { a.trigger(b, c) }) }, e.prototype._registerSelectionEvents = function () { var b = this, c = ["toggle"]; this.selection.on("toggle", function () { b.toggleDropdown() }), this.selection.on("*", function (d, e) { -1 === a.inArray(d, c) && b.trigger(d, e) }) }, e.prototype._registerDropdownEvents = function () { var a = this; this.dropdown.on("*", function (b, c) { a.trigger(b, c) }) }, e.prototype._registerResultsEvents = function () { var a = this; this.results.on("*", function (b, c) { a.trigger(b, c) }) }, e.prototype._registerEvents = function () { var a = this; this.on("open", function () { a.$container.addClass("select2-container--open") }), this.on("close", function () { a.$container.removeClass("select2-container--open") }), this.on("enable", function () { a.$container.removeClass("select2-container--disabled") }), this.on("disable", function () { a.$container.addClass("select2-container--disabled") }), this.on("focus", function () { a.$container.addClass("select2-container--focus") }), this.on("blur", function () { a.$container.removeClass("select2-container--focus") }), this.on("query", function (b) { a.isOpen() || a.trigger("open"), this.dataAdapter.query(b, function (c) { a.trigger("results:all", { data: c, query: b }) }) }), this.on("query:append", function (b) { this.dataAdapter.query(b, function (c) { a.trigger("results:append", { data: c, query: b }) }) }), this.on("keypress", function (b) { var c = b.which; a.isOpen() ? c === d.ENTER ? (a.trigger("results:select"), b.preventDefault()) : c === d.SPACE && b.ctrlKey ? (a.trigger("results:toggle"), b.preventDefault()) : c === d.UP ? (a.trigger("results:previous"), b.preventDefault()) : c === d.DOWN ? (a.trigger("results:next"), b.preventDefault()) : (c === d.ESC || c === d.TAB) && (a.close(), b.preventDefault()) : (c === d.ENTER || c === d.SPACE || (c === d.DOWN || c === d.UP) && b.altKey) && (a.open(), b.preventDefault()) }) }, e.prototype._syncAttributes = function () { this.options.set("disabled", this.$element.prop("disabled")), this.options.get("disabled") ? (this.isOpen() && this.close(), this.trigger("disable")) : this.trigger("enable") }, e.prototype.trigger = function (a, b) { var c = e.__super__.trigger, d = { open: "opening", close: "closing", select: "selecting", unselect: "unselecting" }; if (a in d) { var f = d[a], g = { prevented: !1, name: a, args: b }; if (c.call(this, f, g), g.prevented) return void (b.prevented = !0) } c.call(this, a, b) }, e.prototype.toggleDropdown = function () { this.options.get("disabled") || (this.isOpen() ? this.close() : this.open()) }, e.prototype.open = function () { this.isOpen() || (this.trigger("query", {}), this.trigger("open")) }, e.prototype.close = function () { this.isOpen() && this.trigger("close") }, e.prototype.isOpen = function () { return this.$container.hasClass("select2-container--open") }, e.prototype.enable = function (a) { this.options.get("debug") && window.console && console.warn && console.warn('Select2: The `select2("enable")` method has been deprecated and will be removed in later Select2 versions. Use $element.prop("disabled") instead.'), (null == a || 0 === a.length) && (a = [!0]); var b = !a[0]; this.$element.prop("disabled", b) }, e.prototype.data = function () { this.options.get("debug") && arguments.length > 0 && window.console && console.warn && console.warn('Select2: Data can no longer be set using `select2("data")`. You should consider setting the value instead using `$element.val()`.'); var a = []; return this.dataAdapter.current(function (b) { a = b }), a }, e.prototype.val = function (b) { if (this.options.get("debug") && window.console && console.warn && console.warn('Select2: The `select2("val")` method has been deprecated and will be removed in later Select2 versions. Use $element.val() instead.'), null == b || 0 === b.length) return this.$element.val(); var c = b[0]; a.isArray(c) && (c = a.map(c, function (a) { return a.toString() })), this.$element.val(c).trigger("change") }, e.prototype.destroy = function () { this.$container.remove(), this.$element[0].detachEvent && this.$element[0].detachEvent("onpropertychange", this._sync), null != this._observer ? (this._observer.disconnect(), this._observer = null) : this.$element[0].removeEventListener && this.$element[0].removeEventListener("DOMAttrModified", this._sync, !1), this._sync = null, this.$element.off(".select2"), this.$element.attr("tabindex", this.$element.data("old-tabindex")), this.$element.removeClass("select2-hidden-accessible"), this.$element.attr("aria-hidden", "false"), this.$element.removeData("select2"), this.dataAdapter.destroy(), this.selection.destroy(), this.dropdown.destroy(), this.results.destroy(), this.dataAdapter = null, this.selection = null, this.dropdown = null, this.results = null }, e.prototype.render = function () { var b = a('<span class="select2 select2-container"><span class="selection"></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>'); return b.attr("dir", this.options.get("dir")), this.$container = b, this.$container.addClass("select2-container--" + this.options.get("theme")), b.data("element", this.$element), b }, e }), b.define("jquery.select2", ["jquery", "require", "./select2/core", "./select2/defaults"], function (a, b, c, d) { if (b("jquery.mousewheel"), null == a.fn.select2) { var e = ["open", "close", "destroy"]; a.fn.select2 = function (b) { if (b = b || {}, "object" == typeof b) return this.each(function () { { var d = a.extend({}, b, !0); new c(a(this), d) } }), this; if ("string" == typeof b) { var d = this.data("select2"); null == d && window.console && console.error && console.error("The select2('" + b + "') method was called on an element that is not using Select2."); var f = Array.prototype.slice.call(arguments, 1), g = d[b](f); return a.inArray(b, e) > -1 ? this : g } throw new Error("Invalid arguments for Select2: " + b) } } return null == a.fn.select2.defaults && (a.fn.select2.defaults = d), c }), b.define("jquery.mousewheel", ["jquery"], function (a) { return a }), { define: b.define, require: b.require }
    }(), c = b.require("jquery.select2"); return a.fn.select2.amd = b, c
});


/*! iCheck v1.0.2 by Damir Sultanov, http://git.io/arlzeA, MIT Licensed */
(function (f) {
    function A(a, b, d) { var c = a[0], g = /er/.test(d) ? _indeterminate : /bl/.test(d) ? n : k, e = d == _update ? { checked: c[k], disabled: c[n], indeterminate: "true" == a.attr(_indeterminate) || "false" == a.attr(_determinate) } : c[g]; if (/^(ch|di|in)/.test(d) && !e) x(a, g); else if (/^(un|en|de)/.test(d) && e) q(a, g); else if (d == _update) for (var f in e) e[f] ? x(a, f, !0) : q(a, f, !0); else if (!b || "toggle" == d) { if (!b) a[_callback]("ifClicked"); e ? c[_type] !== r && q(a, g) : x(a, g) } } function x(a, b, d) {
        var c = a[0], g = a.parent(), e = b == k, u = b == _indeterminate,
        v = b == n, s = u ? _determinate : e ? y : "enabled", F = l(a, s + t(c[_type])), B = l(a, b + t(c[_type])); if (!0 !== c[b]) { if (!d && b == k && c[_type] == r && c.name) { var w = a.closest("form"), p = 'input[name="' + c.name + '"]', p = w.length ? w.find(p) : f(p); p.each(function () { this !== c && f(this).data(m) && q(f(this), b) }) } u ? (c[b] = !0, c[k] && q(a, k, "force")) : (d || (c[b] = !0), e && c[_indeterminate] && q(a, _indeterminate, !1)); D(a, e, b, d) } c[n] && l(a, _cursor, !0) && g.find("." + C).css(_cursor, "default"); g[_add](B || l(a, b) || ""); g.attr("role") && !u && g.attr("aria-" + (v ? n : k), "true");
        g[_remove](F || l(a, s) || "")
    } function q(a, b, d) { var c = a[0], g = a.parent(), e = b == k, f = b == _indeterminate, m = b == n, s = f ? _determinate : e ? y : "enabled", q = l(a, s + t(c[_type])), r = l(a, b + t(c[_type])); if (!1 !== c[b]) { if (f || !d || "force" == d) c[b] = !1; D(a, e, s, d) } !c[n] && l(a, _cursor, !0) && g.find("." + C).css(_cursor, "pointer"); g[_remove](r || l(a, b) || ""); g.attr("role") && !f && g.attr("aria-" + (m ? n : k), "false"); g[_add](q || l(a, s) || "") } function E(a, b) {
        if (a.data(m)) {
            a.parent().html(a.attr("style", a.data(m).s || "")); if (b) a[_callback](b); a.off(".i").unwrap();
            f(_label + '[for="' + a[0].id + '"]').add(a.closest(_label)).off(".i")
        }
    } function l(a, b, f) { if (a.data(m)) return a.data(m).o[b + (f ? "" : "Class")] } function t(a) { return a.charAt(0).toUpperCase() + a.slice(1) } function D(a, b, f, c) { if (!c) { if (b) a[_callback]("ifToggled"); a[_callback]("ifChanged")[_callback]("if" + t(f)) } } var m = "iCheck", C = m + "-helper", r = "radio", k = "checked", y = "un" + k, n = "disabled"; _determinate = "determinate"; _indeterminate = "in" + _determinate; _update = "update"; _type = "type"; _click = "click"; _touch = "touchbegin.i touchend.i";
    _add = "addClass"; _remove = "removeClass"; _callback = "trigger"; _label = "label"; _cursor = "cursor"; _mobile = /ipad|iphone|ipod|android|blackberry|windows phone|opera mini|silk/i.test(navigator.userAgent); f.fn[m] = function (a, b) {
        var d = 'input[type="checkbox"], input[type="' + r + '"]', c = f(), g = function (a) { a.each(function () { var a = f(this); c = a.is(d) ? c.add(a) : c.add(a.find(d)) }) }; if (/^(check|uncheck|toggle|indeterminate|determinate|disable|enable|update|destroy)$/i.test(a)) return a = a.toLowerCase(), g(this), c.each(function () {
            var c =
            f(this); "destroy" == a ? E(c, "ifDestroyed") : A(c, !0, a); f.isFunction(b) && b()
        }); if ("object" != typeof a && a) return this; var e = f.extend({ checkedClass: k, disabledClass: n, indeterminateClass: _indeterminate, labelHover: !0 }, a), l = e.handle, v = e.hoverClass || "hover", s = e.focusClass || "focus", t = e.activeClass || "active", B = !!e.labelHover, w = e.labelHoverClass || "hover", p = ("" + e.increaseArea).replace("%", "") | 0; if ("checkbox" == l || l == r) d = 'input[type="' + l + '"]'; -50 > p && (p = -50); g(this); return c.each(function () {
            var a = f(this); E(a); var c = this,
            b = c.id, g = -p + "%", d = 100 + 2 * p + "%", d = { position: "absolute", top: g, left: g, display: "block", width: d, height: d, margin: 0, padding: 0, background: "#fff", border: 0, opacity: 0 }, g = _mobile ? { position: "absolute", visibility: "hidden" } : p ? d : { position: "absolute", opacity: 0 }, l = "checkbox" == c[_type] ? e.checkboxClass || "icheckbox" : e.radioClass || "i" + r, z = f(_label + '[for="' + b + '"]').add(a.closest(_label)), u = !!e.aria, y = m + "-" + Math.random().toString(36).substr(2, 6), h = '<div class="' + l + '" ' + (u ? 'role="' + c[_type] + '" ' : ""); u && z.each(function () {
                h +=
                'aria-labelledby="'; this.id ? h += this.id : (this.id = y, h += y); h += '"'
            }); h = a.wrap(h + "/>")[_callback]("ifCreated").parent().append(e.insert); d = f('<ins class="' + C + '"/>').css(d).appendTo(h); a.data(m, { o: e, s: a.attr("style") }).css(g); e.inheritClass && h[_add](c.className || ""); e.inheritID && b && h.attr("id", m + "-" + b); "static" == h.css("position") && h.css("position", "relative"); A(a, !0, _update); if (z.length) z.on(_click + ".i mouseover.i mouseout.i " + _touch, function (b) {
                var d = b[_type], e = f(this); if (!c[n]) {
                    if (d == _click) {
                        if (f(b.target).is("a")) return;
                        A(a, !1, !0)
                    } else B && (/ut|nd/.test(d) ? (h[_remove](v), e[_remove](w)) : (h[_add](v), e[_add](w))); if (_mobile) b.stopPropagation(); else return !1
                }
            }); a.on(_click + ".i focus.i blur.i keyup.i keydown.i keypress.i", function (b) { var d = b[_type]; b = b.keyCode; if (d == _click) return !1; if ("keydown" == d && 32 == b) return c[_type] == r && c[k] || (c[k] ? q(a, k) : x(a, k)), !1; if ("keyup" == d && c[_type] == r) !c[k] && x(a, k); else if (/us|ur/.test(d)) h["blur" == d ? _remove : _add](s) }); d.on(_click + " mousedown mouseup mouseover mouseout " + _touch, function (b) {
                var d =
                b[_type], e = /wn|up/.test(d) ? t : v; if (!c[n]) { if (d == _click) A(a, !1, !0); else { if (/wn|er|in/.test(d)) h[_add](e); else h[_remove](e + " " + t); if (z.length && B && e == v) z[/ut|nd/.test(d) ? _remove : _add](w) } if (_mobile) b.stopPropagation(); else return !1 }
            })
        })
    }
})(window.jQuery || window.Zepto);




/* spin.js */
(function (t, e) { "object" == typeof exports ? module.exports = e() : "function" == typeof define && define.amd ? define(e) : t.Spinner = e() })(this, function () { "use strict"; function t(t, e) { var i, n = document.createElement(t || "div"); for (i in e) n[i] = e[i]; return n } function e(t) { for (var e = 1, i = arguments.length; i > e; e++) t.appendChild(arguments[e]); return t } function i(t, e, i, n) { var r = ["opacity", e, ~~(100 * t), i, n].join("-"), o = .01 + 100 * (i / n), a = Math.max(1 - (1 - t) / e * (100 - o), t), s = u.substring(0, u.indexOf("Animation")).toLowerCase(), l = s && "-" + s + "-" || ""; return c[r] || (p.insertRule("@" + l + "keyframes " + r + "{" + "0%{opacity:" + a + "}" + o + "%{opacity:" + t + "}" + (o + .01) + "%{opacity:1}" + (o + e) % 100 + "%{opacity:" + t + "}" + "100%{opacity:" + a + "}" + "}", p.cssRules.length), c[r] = 1), r } function n(t, e) { var i, n, r = t.style; for (e = e.charAt(0).toUpperCase() + e.slice(1), n = 0; d.length > n; n++) if (i = d[n] + e, void 0 !== r[i]) return i; return void 0 !== r[e] ? e : void 0 } function r(t, e) { for (var i in e) t.style[n(t, i) || i] = e[i]; return t } function o(t) { for (var e = 1; arguments.length > e; e++) { var i = arguments[e]; for (var n in i) void 0 === t[n] && (t[n] = i[n]) } return t } function a(t, e) { return "string" == typeof t ? t : t[e % t.length] } function s(t) { this.opts = o(t || {}, s.defaults, f) } function l() { function i(e, i) { return t("<" + e + ' xmlns="urn:schemas-microsoft.com:vml" class="spin-vml">', i) } p.addRule(".spin-vml", "behavior:url(#default#VML)"), s.prototype.lines = function (t, n) { function o() { return r(i("group", { coordsize: d + " " + d, coordorigin: -u + " " + -u }), { width: d, height: d }) } function s(t, s, l) { e(p, e(r(o(), { rotation: 360 / n.lines * t + "deg", left: ~~s }), e(r(i("roundrect", { arcsize: n.corners }), { width: u, height: n.width, left: n.radius, top: -n.width >> 1, filter: l }), i("fill", { color: a(n.color, t), opacity: n.opacity }), i("stroke", { opacity: 0 })))) } var l, u = n.length + n.width, d = 2 * u, c = 2 * -(n.width + n.length) + "px", p = r(o(), { position: "absolute", top: c, left: c }); if (n.shadow) for (l = 1; n.lines >= l; l++) s(l, -2, "progid:DXImageTransform.Microsoft.Blur(pixelradius=2,makeshadow=1,shadowopacity=.3)"); for (l = 1; n.lines >= l; l++) s(l); return e(t, p) }, s.prototype.opacity = function (t, e, i, n) { var r = t.firstChild; n = n.shadow && n.lines || 0, r && r.childNodes.length > e + n && (r = r.childNodes[e + n], r = r && r.firstChild, r = r && r.firstChild, r && (r.opacity = i)) } } var u, d = ["webkit", "Moz", "ms", "O"], c = {}, p = function () { var i = t("style", { type: "text/css" }); return e(document.getElementsByTagName("head")[0], i), i.sheet || i.styleSheet }(), f = { lines: 12, length: 7, width: 5, radius: 10, rotate: 0, corners: 1, color: "#000", direction: 1, speed: 1, trail: 100, opacity: .25, fps: 20, zIndex: 2e9, className: "spinner", top: "50%", left: "50%", position: "absolute" }; s.defaults = {}, o(s.prototype, { spin: function (e) { this.stop(); var i = this, n = i.opts, o = i.el = r(t(0, { className: n.className }), { position: n.position, width: 0, zIndex: n.zIndex }); if (n.radius + n.length + n.width, r(o, { left: n.left, top: n.top }), e && e.insertBefore(o, e.firstChild || null), o.setAttribute("role", "progressbar"), i.lines(o, i.opts), !u) { var a, s = 0, l = (n.lines - 1) * (1 - n.direction) / 2, d = n.fps, c = d / n.speed, p = (1 - n.opacity) / (c * n.trail / 100), f = c / n.lines; (function h() { s++; for (var t = 0; n.lines > t; t++) a = Math.max(1 - (s + (n.lines - t) * f) % c * p, n.opacity), i.opacity(o, t * n.direction + l, a, n); i.timeout = i.el && setTimeout(h, ~~(1e3 / d)) })() } return i }, stop: function () { var t = this.el; return t && (clearTimeout(this.timeout), t.parentNode && t.parentNode.removeChild(t), this.el = void 0), this }, lines: function (n, o) { function s(e, i) { return r(t(), { position: "absolute", width: o.length + o.width + "px", height: o.width + "px", background: e, boxShadow: i, transformOrigin: "left", transform: "rotate(" + ~~(360 / o.lines * d + o.rotate) + "deg) translate(" + o.radius + "px" + ",0)", borderRadius: (o.corners * o.width >> 1) + "px" }) } for (var l, d = 0, c = (o.lines - 1) * (1 - o.direction) / 2; o.lines > d; d++) l = r(t(), { position: "absolute", top: 1 + ~(o.width / 2) + "px", transform: o.hwaccel ? "translate3d(0,0,0)" : "", opacity: o.opacity, animation: u && i(o.opacity, o.trail, c + d * o.direction, o.lines) + " " + 1 / o.speed + "s linear infinite" }), o.shadow && e(l, r(s("#000", "0 0 4px #000"), { top: "2px" })), e(n, e(l, s(a(o.color, d), "0 0 1px rgba(0,0,0,.1)"))); return n }, opacity: function (t, e, i) { t.childNodes.length > e && (t.childNodes[e].style.opacity = i) } }); var h = r(t("group"), { behavior: "url(#default#VML)" }); return !n(h, "transform") && h.adj ? l() : u = n(h, "animation"), s });

/*!
 * Ladda 0.9.8 (2015-03-19, 17:22)
 * http://lab.hakim.se/ladda
 * MIT licensed
 *
 * Copyright (C) 2015 Hakim El Hattab, http://hakim.se
 */
(function (t, e) { "object" == typeof exports ? module.exports = e(require("spin.js")) : "function" == typeof define && define.amd ? define(["spin"], e) : t.Ladda = e(t.Spinner) })(this, function (t) { "use strict"; function e(t) { if (t === void 0) return console.warn("Ladda button target must be defined."), void 0; t.querySelector(".ladda-label") || (t.innerHTML = '<span class="ladda-label">' + t.innerHTML + "</span>"); var e, n = t.querySelector(".ladda-spinner"); n || (n = document.createElement("span"), n.className = "ladda-spinner"), t.appendChild(n); var r, a = { start: function () { return e || (e = o(t)), t.setAttribute("disabled", ""), t.setAttribute("data-loading", ""), clearTimeout(r), e.spin(n), this.setProgress(0), this }, startAfter: function (t) { return clearTimeout(r), r = setTimeout(function () { a.start() }, t), this }, stop: function () { return t.removeAttribute("disabled"), t.removeAttribute("data-loading"), clearTimeout(r), e && (r = setTimeout(function () { e.stop() }, 1e3)), this }, toggle: function () { return this.isLoading() ? this.stop() : this.start(), this }, setProgress: function (e) { e = Math.max(Math.min(e, 1), 0); var n = t.querySelector(".ladda-progress"); 0 === e && n && n.parentNode ? n.parentNode.removeChild(n) : (n || (n = document.createElement("div"), n.className = "ladda-progress", t.appendChild(n)), n.style.width = (e || 0) * t.offsetWidth + "px") }, enable: function () { return this.stop(), this }, disable: function () { return this.stop(), t.setAttribute("disabled", ""), this }, isLoading: function () { return t.hasAttribute("data-loading") }, remove: function () { clearTimeout(r), t.removeAttribute("disabled", ""), t.removeAttribute("data-loading", ""), e && (e.stop(), e = null); for (var n = 0, i = u.length; i > n; n++) if (a === u[n]) { u.splice(n, 1); break } } }; return u.push(a), a } function n(t, e) { for (; t.parentNode && t.tagName !== e;) t = t.parentNode; return e === t.tagName ? t : void 0 } function r(t) { for (var e = ["input", "textarea", "select"], n = [], r = 0; e.length > r; r++) for (var a = t.getElementsByTagName(e[r]), i = 0; a.length > i; i++) a[i].hasAttribute("required") && n.push(a[i]); return n } function a(t, a) { a = a || {}; var i = []; "string" == typeof t ? i = s(document.querySelectorAll(t)) : "object" == typeof t && "string" == typeof t.nodeName && (i = [t]); for (var o = 0, u = i.length; u > o; o++) (function () { var t = i[o]; if ("function" == typeof t.addEventListener) { var s = e(t), u = -1; t.addEventListener("click", function () { var e = !0, i = n(t, "FORM"); if (i !== void 0) for (var o = r(i), d = 0; o.length > d; d++) "" === o[d].value.replace(/^\s+|\s+$/g, "") && (e = !1), "checkbox" !== o[d].type && "radio" !== o[d].type || o[d].checked || (e = !1), "email" === o[d].type && (e = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/.test(o[d].value)); e && (s.startAfter(1), "number" == typeof a.timeout && (clearTimeout(u), u = setTimeout(s.stop, a.timeout)), "function" == typeof a.callback && a.callback.apply(null, [s])) }, !1) } })() } function i() { for (var t = 0, e = u.length; e > t; t++) u[t].stop() } function o(e) { var n, r = e.offsetHeight; 0 === r && (r = parseFloat(window.getComputedStyle(e).height)), r > 32 && (r *= .8), e.hasAttribute("data-spinner-size") && (r = parseInt(e.getAttribute("data-spinner-size"), 10)), e.hasAttribute("data-spinner-color") && (n = e.getAttribute("data-spinner-color")); var a = 12, i = .2 * r, o = .6 * i, s = 7 > i ? 2 : 3; return new t({ color: n || "#fff", lines: a, radius: i, length: o, width: s, zIndex: "auto", top: "auto", left: "auto", className: "" }) } function s(t) { for (var e = [], n = 0; t.length > n; n++) e.push(t[n]); return e } var u = []; return { bind: a, create: e, stopAll: i } });

/*!
 * Ladda for jQuery
 * http://lab.hakim.se/ladda
 * MIT licensed
 *
 * Copyright (C) 2015 Hakim El Hattab, http://hakim.se
 */
(function (t, e) { if (void 0 === e) return console.error("jQuery required for Ladda.jQuery"); var i = []; e = e.extend(e, { ladda: function (e) { "stopAll" === e && t.stopAll() } }), e.fn = e.extend(e.fn, { ladda: function (n) { var r = i.slice.call(arguments, 1); return "bind" === n ? (r.unshift(e(this).selector), t.bind.apply(t, r)) : e(this).each(function () { var i, o = e(this); void 0 === n ? o.data("ladda", t.create(this)) : (i = o.data("ladda"), i[n].apply(i, r)) }), this } }) })(this.Ladda, this.jQuery);



//check if string is valid email address
function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}


/*
    jQuery Masked Input Plugin
    Copyright (c) 2007 - 2014 Josh Bush (digitalbush.com)
    Licensed under the MIT license (http://digitalbush.com/projects/masked-input-plugin/#license)
    Version: 1.4.0
*/
!function (a) { "function" == typeof define && define.amd ? define(["jquery"], a) : a("object" == typeof exports ? require("jquery") : jQuery) }(function (a) { var b, c = navigator.userAgent, d = /iphone/i.test(c), e = /chrome/i.test(c), f = /android/i.test(c); a.mask = { definitions: { 9: "[0-9]", a: "[A-Za-z]", "*": "[A-Za-z0-9]" }, autoclear: !0, dataName: "rawMaskFn", placeholder: "_" }, a.fn.extend({ caret: function (a, b) { var c; if (0 !== this.length && !this.is(":hidden")) return "number" == typeof a ? (b = "number" == typeof b ? b : a, this.each(function () { this.setSelectionRange ? this.setSelectionRange(a, b) : this.createTextRange && (c = this.createTextRange(), c.collapse(!0), c.moveEnd("character", b), c.moveStart("character", a), c.select()) })) : (this[0].setSelectionRange ? (a = this[0].selectionStart, b = this[0].selectionEnd) : document.selection && document.selection.createRange && (c = document.selection.createRange(), a = 0 - c.duplicate().moveStart("character", -1e5), b = a + c.text.length), { begin: a, end: b }) }, unmask: function () { return this.trigger("unmask") }, mask: function (c, g) { var h, i, j, k, l, m, n, o; if (!c && this.length > 0) { h = a(this[0]); var p = h.data(a.mask.dataName); return p ? p() : void 0 } return g = a.extend({ autoclear: a.mask.autoclear, placeholder: a.mask.placeholder, completed: null }, g), i = a.mask.definitions, j = [], k = n = c.length, l = null, a.each(c.split(""), function (a, b) { "?" == b ? (n--, k = a) : i[b] ? (j.push(new RegExp(i[b])), null === l && (l = j.length - 1), k > a && (m = j.length - 1)) : j.push(null) }), this.trigger("unmask").each(function () { function h() { if (g.completed) { for (var a = l; m >= a; a++) if (j[a] && C[a] === p(a)) return; g.completed.call(B) } } function p(a) { return g.placeholder.charAt(a < g.placeholder.length ? a : 0) } function q(a) { for (; ++a < n && !j[a];); return a } function r(a) { for (; --a >= 0 && !j[a];); return a } function s(a, b) { var c, d; if (!(0 > a)) { for (c = a, d = q(b) ; n > c; c++) if (j[c]) { if (!(n > d && j[c].test(C[d]))) break; C[c] = C[d], C[d] = p(d), d = q(d) } z(), B.caret(Math.max(l, a)) } } function t(a) { var b, c, d, e; for (b = a, c = p(a) ; n > b; b++) if (j[b]) { if (d = q(b), e = C[b], C[b] = c, !(n > d && j[d].test(e))) break; c = e } } function u() { var a = B.val(), b = B.caret(); if (a.length < o.length) { for (A(!0) ; b.begin > 0 && !j[b.begin - 1];) b.begin--; if (0 === b.begin) for (; b.begin < l && !j[b.begin];) b.begin++; B.caret(b.begin, b.begin) } else { for (A(!0) ; b.begin < n && !j[b.begin];) b.begin++; B.caret(b.begin, b.begin) } h() } function v() { A(), B.val() != E && B.change() } function w(a) { if (!B.prop("readonly")) { var b, c, e, f = a.which || a.keyCode; o = B.val(), 8 === f || 46 === f || d && 127 === f ? (b = B.caret(), c = b.begin, e = b.end, e - c === 0 && (c = 46 !== f ? r(c) : e = q(c - 1), e = 46 === f ? q(e) : e), y(c, e), s(c, e - 1), a.preventDefault()) : 13 === f ? v.call(this, a) : 27 === f && (B.val(E), B.caret(0, A()), a.preventDefault()) } } function x(b) { if (!B.prop("readonly")) { var c, d, e, g = b.which || b.keyCode, i = B.caret(); if (!(b.ctrlKey || b.altKey || b.metaKey || 32 > g) && g && 13 !== g) { if (i.end - i.begin !== 0 && (y(i.begin, i.end), s(i.begin, i.end - 1)), c = q(i.begin - 1), n > c && (d = String.fromCharCode(g), j[c].test(d))) { if (t(c), C[c] = d, z(), e = q(c), f) { var k = function () { a.proxy(a.fn.caret, B, e)() }; setTimeout(k, 0) } else B.caret(e); i.begin <= m && h() } b.preventDefault() } } } function y(a, b) { var c; for (c = a; b > c && n > c; c++) j[c] && (C[c] = p(c)) } function z() { B.val(C.join("")) } function A(a) { var b, c, d, e = B.val(), f = -1; for (b = 0, d = 0; n > b; b++) if (j[b]) { for (C[b] = p(b) ; d++ < e.length;) if (c = e.charAt(d - 1), j[b].test(c)) { C[b] = c, f = b; break } if (d > e.length) { y(b + 1, n); break } } else C[b] === e.charAt(d) && d++, k > b && (f = b); return a ? z() : k > f + 1 ? g.autoclear || C.join("") === D ? (B.val() && B.val(""), y(0, n)) : z() : (z(), B.val(B.val().substring(0, f + 1))), k ? b : l } var B = a(this), C = a.map(c.split(""), function (a, b) { return "?" != a ? i[a] ? p(b) : a : void 0 }), D = C.join(""), E = B.val(); B.data(a.mask.dataName, function () { return a.map(C, function (a, b) { return j[b] && a != p(b) ? a : null }).join("") }), B.one("unmask", function () { B.off(".mask").removeData(a.mask.dataName) }).on("focus.mask", function () { if (!B.prop("readonly")) { clearTimeout(b); var a; E = B.val(), a = A(), b = setTimeout(function () { z(), a == c.replace("?", "").length ? B.caret(0, a) : B.caret(a) }, 10) } }).on("blur.mask", v).on("keydown.mask", w).on("keypress.mask", x).on("input.mask paste.mask", function () { B.prop("readonly") || setTimeout(function () { var a = A(!0); B.caret(a), h() }, 0) }), e && f && B.off("input.mask").on("input.mask", u), A() }) } }) });



/// <reference path="../Forms/LOData.ascx" />
function thousands(x) { return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","); }
function phones(x) { return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " "); }


/*
Variable to store the status about if user checked the "I want attach files" checkbox
*/
var wantUploadFiles = false;

/*
Function to detect if the form has file inputs and ensures that everyone is filled (not empty)
@return Boolean
*/
function hasFilledFiles() {

    if (wantUploadFiles) {
        var errors = 0;
        if ($(".file-row").length > 0) {
            if ($(".file-row").last().find('input[type="file"]').val() == "" || $(".file-row.invalid-file").length > 0) {
                $(".file-row").each(function () {

                    var file = new FileInfo((this.getElementsByTagName("input")[0]).files[0]);

                    if ($(this).find('input[type="file"]').val() == "" || !isAllowedFile(file.extension)) {
                        $(this).remove();
                    } else {
                        errors++;
                    }
                });

                Validations.removeValidation(allowedError);
                Validations.removeValidation(oversizeError);

                return errors > 0;
            } else {
                $(".file-row").each(function () {
                    var $cur = $(this);
                    if ($cur.find('input[type="file"]').val() == "") {
                        errors++;
                    }
                });

                return errors == 0;
            }
        }

        return false;
    }

    return true;
}






/*
Check status for custom errors
*/
function CheckErrors() {

    var error = $(".careersForm").find('.error');
    error.removeClass('show');
    error.find(".custom-error").remove();

    //Validations.tryResolve(validateFilesIndex, hasFilledFiles);
    foreach(Validations.validations, function (item, i) {
        if (!item.resolved && i != validateFilesIndex ) {
            error.append("<div class='custom-error'>" + item.text + "</div>");
        }
    });

    if (error.find(".custom-error").length > 0) {
        error.addClass("show");
    }
}



/*
Function to get all file information, via input:file
@param File file
@return Object
*/
function FileInfo(file) {
    
    if (file == undefined) {
        this.filesize = 0;
        this.filesizeInteger = 0;
        this.name = "";
        this.type = "";
        this.extension = "";
        this.basename = "";

        return false;
    }

    var filesize = file.size || file.fileSize,
        filesize = filesize / 1024;

    function getExtension(basename) {
        var bs = basename.split(".");
        return (bs[bs.length - 1]);
    }

    function basename(filename) {
        var bs = filename.split("/"),
            ext = getExtension(filename);
        return (bs[bs.length - 1]).replace("."+ext,"");
    }

    this.filesize = filesize;
    this.filesizeInteger = parseInt(filesize);
    this.name = file.name||"";
    this.type = file.type||"";
    this.extension = getExtension(this.name).toLowerCase();
    this.basename = basename(this.name);
}


/*
Function to check if selected file is allowed file type
@param String extension
@return Boolean
*/
function isAllowedFile(extension) {
    var allowedTypes = [
        "jpg",
        "png",
        "jpeg",
        "gif",
        "bmp",
        "pdf",
        "doc",
        "docx"
    ];

    return allowedTypes.indexOf(extension.toLowerCase()) > -1;
}



/*
Object collection allows me to add, remove and resolve custom validations for forms and other customized stuff
*/
var Validations = {
    /*
        Here will be stored all validations
    */
    validations: [],

    /*
    Function to add a new validation by default unresolved
    @param String msg
    @return Number
    */
    addValidation: function (msg) {
        if (msg) {
            var size = this.validations.length;
            this.validations.push({ text: msg, resolved: false });
            return size;
        }
    },

    /*
    Function to try resolve a custom validation passing a condition or syced function to return if is valid or not current validation
    @param Number|String key
    @param Function|Boolean condition
    */
    tryResolve: function (key, condition) {
        var conditionResolver = typeof condition == "function" ? condition 
            : (typeof condition == "boolean" ? condition : false );
        
        if (typeof key == "number") {
            if (this.validations.length > 0 && this.validations[key] != "undefined") {
                this.validations[key].resolved = conditionResolver();
                return this.validations[key];
            }
        }

        if (typeof key == "string") {
            for (var f = false, i = 0; i < this.validations.length; i++) {
                if (this.validations[i].text == key) {
                    f = i;
                }
            }

            if (f != false) {
                this.validations[f].resolved = conditionResolver();
            }
        }
    },

    /*
    Function to remove a validation
    @param Number|String key
    */
    removeValidation: function (key) {
        if (typeof key == "number") {
            if (this.validations.length > 0 && this.validations[key] != "undefined") {
                this.validations[key].resolved = true;
            }
        }

        if (typeof key == "string") {
            for (var f = false, i = 0; i < this.validations.length; i++) {
                if (this.validations[i].text == key) {
                    f = i;
                }
            }

            if (f != false) {
                this.validations[f].resolved = true;
            }
        }
    },

    /*
    Function will reset current validation, will set it as resolved without any condition
    @param Number|String key
    */
    resetValidation: function (key) {
        if (typeof key == "number") {
            if (this.validations.length > 0 && this.validations[key] != "undefined") {
                this.validations[key].resolved = false;
            }
        }

        if (typeof key == "string") {
            var f = false;
            for (var i = 0; i < this.validations.length; i++) {
                if (this.validations[i].text == key) {
                    f = i;
                }
            }

            if (f != false) {
                this.validations[f].resolved = false;
            }
        }
    },

    /*
    Check if validation exists
    @param Number|String key
    @return Number
    */
    hasValidation: function (key) {
        var f = -1;

        if (typeof key == "number") {
            if (this.validations.length > 0 && this.validations[key] != "undefined") {
                f = key;
            }
        }

        if (typeof key == "string") {
            for (var i = 0; i < this.validations.length; i++) {
                if (this.validations[i].text == key) {
                    f = i;
                }
            }
        }

        return f;
    },

    /*
    Function to check if a validation is resolved or not
    @param Number|String key
    @return Boolean
    */
    isResolved: function (key) {
        if (typeof key == "number") {
            return this.validations[key].resolved;
        }

        if (typeof key == "string") {
            for (var f=false, i = 0; i < this.validations.length; i++) {
                if (this.validations[i].text == key) {
                    f = i;
                }
            }

            return f != false ? this.validations[f].resolved : f;
        }
    },

    /*
    Get all unresolved validations text messages
    @return Array
    */
    getMessages: function () {
        var msgs = [];
        for (var i = 0; i > this.validations.length; i++) {
            if( !this.validations[i].resolved ){
                msgs.push(this.validations[i].text);
            }
        }

        return msgs;
    }
};


/*
Variable to store the custom validation index for file uploads
*/
var validateFilesIndex = 0;

/*
Try to detect bootstrap css framweork inserted in the DOM and iff exists, add a "no-bootstrap" class to the body
*/
$(document).on("ready", function () { 0 < $('body link[href*="bootstrap"]').not('[href*="bootstrap-responsive"]').not('[href*="font"]').length ? $("body").addClass("has-bootstrap") : $("body").addClass("no-bootstrap"); });


/*
Custom validations error messages
*/
var allowedError = "The file type of your attachment is not allowed. Allowed file formats are: jpg, jpeg, gif, png, bmp, pdf, doc, docx.",
    oversizeError = "The file size of your attachment is larger than allowed. Maximun allowed file size is 3MB";


/*
All form specific functionality initializes here
*/
$(document).on("ready", function () {
    $(".choose-files").append($('#mytemplate').html());

    $("body").on("change", ".file-row input[type='file']", function () {

        if ($(this).val() == "") {
            return false;
        }

        var $filerow = $(this).closest(".file-row");

        var f = this.files[0],
            runatAttr = 'runat="server"',
            rowIndex = $(".file-row").length,
            file = new FileInfo(f);

        if (!isAllowedFile(file.extension) || file.filesizeInteger > 3000) {
            $filerow.addClass("invalid-file");
        } 

        /*
            If File Type is not allowed dont add this file
        */
        if (!isAllowedFile(file.extension)) {
            if (Validations.hasValidation(allowedError) > -1) {
                Validations.resetValidation(allowedError);
            } else {
                Validations.addValidation(allowedError); 
            } 

            CheckErrors();

            return false;
        }


        if (file.filesizeInteger > 3000) {
            if (Validations.hasValidation(oversizeError) > -1) {
                Validations.resetValidation(oversizeError);
            } else {
                Validations.addValidation(oversizeError);
            }

            CheckErrors();

            return false;
        }


        /*
        If is valid valid reset validations and continue
        */
        if (Validations.hasValidation(allowedError) > -1) {
            Validations.removeValidation(allowedError);
        }

        if (Validations.hasValidation(oversizeError) > -1) {
            Validations.removeValidation(oversizeError);
        }

        CheckErrors();



        var icon = !isAllowedFile(file.extension) ? "" : '<i class="inline-icon type-' + file.extension + '"></i>';

        var infoField = " ";
        infoField += "<strong>" + icon + file.name + "</strong>"
                    + "<strong></strong> <label class=''>" + file.filesizeInteger
                    + "kb</label> <strong></strong> Description: <a href='javascript:;' class='add-description--anchor'>Add description</a>"
                    + '<input name="description' + rowIndex + '" ' + runatAttr
                    + ' type="text" placeholder="Write a description" class="DescriptionField add-description--field" />';

        $filerow.find(".file-name").show().html(infoField);

        $("#btnAddFile, #btnAddFileInner").addClass("css-button-disabled");
        if ($(".file-row").length < 10) {
            $("#btnAddFile, #btnAddFileInner").removeClass("css-button-disabled");
        }

        $(this).closest("button").find("span").text("Selected: \"" + file.name + "\"");

        $("#btnAddFile, #btnAddFileInner").removeClass("css-button-disabled");
        $filerow.addClass("has-description").removeClass("no-file").find("input[type='text']").trigger("focus");

    })

        .on("click", ".add-description--anchor", function () {
            $(this).closest(".file-row").addClass("has-description").find("input[type='text']").trigger("focus");
        })

        .on("blur", ".add-description--field", function () {
            if (!$(this).val()) {
                $(this).closest(".file-row").removeClass("has-description");
            }
        })

        .on("click", ".add-file", function () {
            if ($(".file-row").length < 10 && !$(this).hasClass("css-button-disabled")) {

                if ($(".file-row").last().hasClass("no-file")) {

                    $(".file-row").last().find("input").trigger("click");

                } else {

                    if ($(".file-row").last().find("input").val() == "") {
                        $("#btnAddFile, #btnAddFileInner").addClass("css-button-disabled");
                    } else {
                        $(".choose-files").append($('#mytemplate').html());
                        $("#btnAddFile, #btnAddFileInner").removeClass("css-button-disabled");

                        $(".file-row").last().find("input").trigger("click");
                    }

                }

           
        } else {
            $("#btnAddFile, #btnAddFileInner").addClass("css-button-disabled");
        }
    }).on("click", ".remove-file", function () {
        $(this).closest(".file-row").remove();
        $("#btnAddFile, #btnAddFileInner").removeClass("css-button-disabled");
    });
});




/*
Custom function foreach iterator
@param Object|Array arr
@param Function callback
*/
function foreach(arr, callback) {
    if (arr && typeof arr == "array" || typeof arr == "object") {

        for (var i = 0; i < arr.length; i++) {
            if (typeof callback == "function") {
                callback(arr[i], i);
            }
        }

        return;
    }

    throw new Error("foreach parameter 1 is invalid, Array expected.");
}



/*
Function to validate form elements inside a wrapper
@param Element elements
@return Boolean
*/
var elements;
function validate(elements) {
    var value;
    var response = 0;
    var error = elements.find('.error');

    error.removeClass('show');
    error.find(".custom-error").remove();

    elements.find('input[aria-required="true"], select[aria-required="true"]').each(function () {
        var value = $(this);
        if (value.val().length == 0 || value.val() == " ") {
            response = response + 1;
        }

        if (error.data("default") == undefined) {
            error.data("default", error.text());
        }

        if (value.hasClass("valid-email") && !validateEmail(value.val())) {
            response = response + 1;

        }
        //error.find(".custom-error").remove();

    });

    Validations.tryResolve(validateFilesIndex, hasFilledFiles);
    foreach(Validations.validations, function (item, i) {
        if (!item.resolved) {
            ++response;
            error.append("<div class='custom-error'>" + item.text + "</div>");
        }
    });




    if (response > 0) {

        elements.find('input[aria-required="true"], select[aria-required="true"]').each(function () {
            var value = $(this);
            if (value.val().length == 0 || value.val() == " " || (value.hasClass("valid-email") && !validateEmail(value.val()))) {
                $(this).addClass("invalid");

                if ($(this).is("select")) {
                    $(this).next(".select2-container").addClass("invalid");
                }
            } else {
                value.removeClass("invalid");
                if ($(this).is("select")) {
                    $(this).next(".select2-container").removeClass("invalid");
                }
            }
        });


        elements.find(".invalid").each(function () {
            $(this).attr("data-error") && error.append("<div class='custom-error'>" + $(this).attr("data-error") + "</div>");
        });


        error.addClass('show');


        return false;
    } else {
        elements.find('input[aria-required="true"], select[aria-required="true"]').each(function () {
            $(this).removeClass("invalid");
            if ($(this).is("select")) {
                $(this).next(".select2-container").removeClass("invalid");
            }
        });

        var error = elements.find('.error');
        error.removeClass('show');
        return true;
    }
}









function checkSticky() {
    var _scroll = $(window).scrollTop();

    
    var pos = $(".careersForm .b-errors").offset().top,
        erheigh = $(".careersForm .error").outerHeight();

    if ( ((_scroll + window.innerHeight ) - erheigh) < pos) {
        $(".careersForm .error").addClass("js-stick");
    } else {
        $(".careersForm .error").removeClass("js-stick");
    }
}

function addPositions() {
    var $dropdown = $('#apply-form select[id*="CareerPositions"]'),
        hasPositions = $dropdown.length > 0;
    if (hasPositions && $('.__get--positions').length > 0) {
        var $positions = $positions = $dropdown.find('option'),
            positions = "";

        $positions.each(function () {
            var $cur = $(this);
            if ($cur.attr("value") != "") {
                positions += '<li data-original-value="' + $cur.attr('value') + '"><a href="javascript:;">'
                + $cur.text() + '</a></li>';
            }
        });

        if ($positions.length > 0 || ($positions.first().attr("value") != "")) {
            $('.__get--positions').html("").append(positions);
        } else {
            $('.__get--positions').html("<li>No job positions available.</li>");
        }
    }
}




$(document).on('ready', function () {


    if (typeof $().chosen != undefined) {
        $(".careersForm select").select2({
            minimumResultsForSearch: Infinity
        });
    }


    $('body [rel="phone"]').mask("(999) 999-9999");

    /*
    -- Code Refactored
    Validate zipcode inputs to allow only 5 chars length using only digits, it was updated because by default numpad was not being validated so the client
    could enter wharever values using the numpad + ascii codes with "alt"
    */
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57) || $(evt.target).val().length == 5)
            return false;

        return true;
    }
    $('body [rel="zip"]').keypress(function (e) {
        return isNumberKey(e);
    });



    $('body')
    .on('click', 'a[href*="#"]', function (e) {
        var hash = $(this).attr('href').split('#')[1],
            $element = $('body').find('[id="' + hash + '"]');

        if ($element.length > 0) {
            e.preventDefault(); e.stopPropagation();

            $('body,html').stop(true, false).animate({
                scrollTop: $element.offset().top
            }, 500);

            return false;
        }
    })


    .on("ifChecked", ".checbox-hasFiles input", function () {
        $(".file-uploads, .show-if-has-files").show();

        wantUploadFiles = true;

        var requiredFilesError = "You must select at least one file, or uncheck the Attachments checkbox";

        if (Validations.hasValidation(requiredFilesError) < 0) {
            validateFilesIndex = Validations.addValidation(requiredFilesError);
        }

        Validations.tryResolve(validateFilesIndex, hasFilledFiles);
    })

     .on("ifUnchecked", ".checbox-hasFiles input", function () {
         $(".file-uploads, .show-if-has-files").hide();

         wantUploadFiles = false;
     })


    ;



    $('.careersForm input[type="text"]').on("keypress", function (e) {
        if (e.keyCode == 13) {
            
            $('form').trigger('submit');

            e.preventDefault();
            e.stopPropagation();
            return false;
        }
    });


    $(".css-button.css-submit-button").on("click", function (e) {
        if ($(this).is("[data-loading]")) {
            e.preventDefault();
            e.stopPropagation();
            return false;
        }

        var elements = $('.careersForm');
        if (!validate(elements)) {
            e.preventDefault(); e.stopPropagation();
            return false;
        } else {
            var l = $(this).ladda();
            // Start loading
            l.ladda('start');
        }
    });



    $('form').on('submit', function (e) {
        var elements = $('.careersForm');
        if (!validate(elements)) {
            e.preventDefault(); e.stopPropagation();
            return false;
        } else {
            var l = $(".css-button.css-submit-button").ladda();
            // Start loading
            l.ladda('start');
        }

    });

    $(".css-label").on("click", function () {
        $(this).next(":input").trigger("focus");

        if ($(this).next(":input").is("select")) {
            $(this).next(":input").select2("open");
        }
    });


    $('.careersForm input[type="radio"], .careersForm input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '20%' // optional
    });

    $('.HasAttachments').prop("checked", false).removeAttr("checked").iCheck("uncheck");

    $(".careersForm .error").before('<span class="b-errors"></span>');
});

$(window)
    .on('load', function () {
        checkSticky();
        addPositions();
    })
    .on('scroll', function () {
        checkSticky();
    })
;