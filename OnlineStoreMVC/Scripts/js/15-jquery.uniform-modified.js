(function (wind, $, undef) {
    "use strict"; function attrOrProp($el) {
        var args = Array.prototype.slice.call(arguments, 1); if ($el.prop) { return $el.prop.apply($el, args); }
        return $el.attr.apply($el, args);
    }
    function bindMany($el, options, events) { var name, namespaced; for (name in events) { if (events.hasOwnProperty(name)) { namespaced = name.replace(/ |$/g, options.eventNamespace); $el.bind(namespaced, events[name]); } } }
    function bindUi($el, $target, options) { bindMany($el, options, { focus: function () { $target.addClass(options.focusClass); }, blur: function () { $target.removeClass(options.focusClass); $target.removeClass(options.activeClass); }, mouseenter: function () { $target.addClass(options.hoverClass); }, mouseleave: function () { $target.removeClass(options.hoverClass); $target.removeClass(options.activeClass); }, "mousedown touchbegin": function () { if (!$el.is(":disabled")) { $target.addClass(options.activeClass); } }, "mouseup touchend": function () { $target.removeClass(options.activeClass); } }); }
    function classClearStandard($el, options) { $el.removeClass(options.hoverClass + " " + options.focusClass + " " + options.activeClass); }
    function classUpdate($el, className, enabled) { if (enabled) { $el.addClass(className); } else { $el.removeClass(className); } }
    function classUpdateChecked($tag, $el, options) {
        var c = "checked", isChecked = $el.is(":" + c); if ($el.prop) { $el.prop(c, isChecked); } else { if (isChecked) { $el.attr(c, c); } else { $el.removeAttr(c); } }
        classUpdate($tag, options.checkedClass, isChecked);
    }
    function classUpdateDisabled($tag, $el, options) { classUpdate($tag, options.disabledClass, $el.is(":disabled")); }
    function divSpanWrap($el, $container, method) {
        switch (method) { case "after": $el.after($container); return $el.next(); case "before": $el.before($container); return $el.prev(); case "wrap": $el.wrap($container); return $el.parent(); }
        return null;
    }
    function divSpan($el, options, divSpanConfig) {
        var $div, $span, id; if (!divSpanConfig) { divSpanConfig = {}; }
        divSpanConfig = $.extend({ bind: {}, divClass: null, divWrap: "wrap", spanClass: null, spanHtml: null, spanWrap: "wrap" }, divSpanConfig); $div = $('<div />'); $span = $('<span />'); if (options.autoHide && $el.is(':hidden') && $el.css('display') === 'none') { $div.hide(); }
        if (divSpanConfig.divClass) { $div.addClass(divSpanConfig.divClass); }
        if (options.wrapperClass) { $div.addClass(options.wrapperClass); }
        if (divSpanConfig.spanClass) { $span.addClass(divSpanConfig.spanClass); }
        id = attrOrProp($el, 'id'); if (options.useID && id) { attrOrProp($div, 'id', options.idPrefix + '-' + id); }
        if (divSpanConfig.spanHtml) { $span.html(divSpanConfig.spanHtml); }
        $div = divSpanWrap($el, $div, divSpanConfig.divWrap); $span = divSpanWrap($el, $span, divSpanConfig.spanWrap); classUpdateDisabled($div, $el, options); return { div: $div, span: $span };
    }
    function wrapWithWrapperClass($el, options) {
        var $span; if (!options.wrapperClass) { return null; }
        $span = $('<span />').addClass(options.wrapperClass); $span = divSpanWrap($el, $span, "wrap"); return $span;
    }
    function highContrast() {
        var c, $div, el, rgb; rgb = 'rgb(120,2,153)'; $div = $('<div style="width:0;height:0;color:' + rgb + '">'); $('body').append($div); el = $div.get(0); if (wind.getComputedStyle) { c = wind.getComputedStyle(el, '').color; } else { c = (el.currentStyle || el.style || {}).color; }
        $div.remove(); return c.replace(/ /g, '') !== rgb;
    }
    function htmlify(text) {
        if (!text) { return ""; }
        return $('<span />').text(text).html();
    }
    function isMsie() { return navigator.cpuClass && !navigator.product; }
    function isMsieSevenOrNewer() {
        if (wind.XMLHttpRequest !== undefined) { return true; }
        return false;
    }
    function isMultiselect($el) {
        var elSize; if ($el[0].multiple) { return true; }
        elSize = attrOrProp($el, "size"); if (!elSize || elSize <= 1) { return false; }
        return true;
    }
    function returnFalse() { return false; }
    function noSelect($elem, options) { var none = 'none'; bindMany($elem, options, { 'selectstart dragstart mousedown': returnFalse }); $elem.css({ MozUserSelect: none, msUserSelect: none, webkitUserSelect: none, userSelect: none }); }
    function setFilename($el, $filenameTag, options) {
        var filename = $el.val(); if (filename === "") { filename = options.fileDefaultHtml; } else { filename = filename.split(/[\/\\]+/); filename = filename[(filename.length - 1)]; }
        $filenameTag.text(filename);
    }
    function swap($elements, newCss, callback) { var restore, item; restore = []; $elements.each(function () { var name; for (name in newCss) { if (Object.prototype.hasOwnProperty.call(newCss, name)) { restore.push({ el: this, name: name, old: this.style[name] }); this.style[name] = newCss[name]; } } }); callback(); while (restore.length) { item = restore.pop(); item.el.style[item.name] = item.old; } }
    function sizingInvisible($el, callback) { var targets; targets = $el.parents(); targets.push($el[0]); targets = targets.not(':visible'); swap(targets, { visibility: "hidden", display: "block", position: "absolute" }, callback); }
    function unwrapUnwrapUnbindFunction($el, options) { return function () { $el.unwrap().unwrap().unbind(options.eventNamespace); }; }
    var allowStyling = true, highContrastTest = false, uniformHandlers = [{
        match: function ($el) { return $el.is("a, button, :submit, :reset, input[type='button']"); }, apply: function ($el, options) {
            var $div, defaultSpanHtml, ds, getHtml, doingClickEvent; defaultSpanHtml = options.submitDefaultHtml; if ($el.is(":reset")) { defaultSpanHtml = options.resetDefaultHtml; }
            if ($el.is("a, button")) { getHtml = function () { return $el.html() || defaultSpanHtml; }; } else { getHtml = function () { return htmlify(attrOrProp($el, "value")) || defaultSpanHtml; }; }
            ds = divSpan($el, options, { divClass: options.buttonClass, spanHtml: getHtml() }); $div = ds.div; bindUi($el, $div, options); doingClickEvent = false; bindMany($div, options, {
                "click touchend": function () {
                    var ev, res, target, href; if (doingClickEvent) { return; }
                    if ($el.is(':disabled')) { return; }
                    doingClickEvent = true; if ($el[0].dispatchEvent) { ev = document.createEvent("MouseEvents"); ev.initEvent("click", true, true); res = $el[0].dispatchEvent(ev); if ($el.is('a') && res) { target = attrOrProp($el, 'target'); href = attrOrProp($el, 'href'); if (!target || target === '_self') { document.location.href = href; } else { wind.open(href, target); } } } else { $el.click(); }
                    doingClickEvent = false;
                }
            }); noSelect($div, options); return { remove: function () { $div.after($el); $div.remove(); $el.unbind(options.eventNamespace); return $el; }, update: function () { classClearStandard($div, options); classUpdateDisabled($div, $el, options); $el.detach(); ds.span.html(getHtml()).append($el); } };
        }
    }, { match: function ($el) { return $el.is(":checkbox"); }, apply: function ($el, options) { var ds, $div, $span; ds = divSpan($el, options, { divClass: options.checkboxClass }); $div = ds.div; $span = ds.span; bindUi($el, $div, options); bindMany($el, options, { "click touchend": function () { classUpdateChecked($span, $el, options); } }); classUpdateChecked($span, $el, options); return { remove: unwrapUnwrapUnbindFunction($el, options), update: function () { classClearStandard($div, options); $span.removeClass(options.checkedClass); classUpdateChecked($span, $el, options); classUpdateDisabled($div, $el, options); } }; } }, {
        match: function ($el) { return $el.is(":file"); }, apply: function ($el, options) {
            var ds, $div, $filename, $button; ds = divSpan($el, options, { divClass: options.fileClass, spanClass: options.fileButtonClass, spanHtml: options.fileButtonHtml, spanWrap: "after" }); $div = ds.div; $button = ds.span; $filename = $("<span />").html(options.fileDefaultHtml); $filename.addClass(options.filenameClass); $filename = divSpanWrap($el, $filename, "after"); if (!attrOrProp($el, "size")) {
                attrOrProp($el, "size", $div.width() / 10);
            }
            function filenameUpdate() { setFilename($el, $filename, options); }
            bindUi($el, $div, options); filenameUpdate(); if (isMsie()) { bindMany($el, options, { click: function () { $el.trigger("change"); setTimeout(filenameUpdate, 0); } }); } else { bindMany($el, options, { change: filenameUpdate }); }
            noSelect($filename, options); noSelect($button, options); return { remove: function () { $filename.remove(); $button.remove(); return $el.unwrap().unbind(options.eventNamespace); }, update: function () { classClearStandard($div, options); setFilename($el, $filename, options); classUpdateDisabled($div, $el, options); } };
        }
    }, {
        match: function ($el) {
            if ($el.is("input")) { var t = (" " + attrOrProp($el, "type") + " ").toLowerCase(), allowed = " color date datetime datetime-local email month number password search tel text time url week "; return allowed.indexOf(t) >= 0; }
            return false;
        }, apply: function ($el, options) {
            var elType, $wrapper; elType = attrOrProp($el, "type"); $el.addClass(options.inputClass); $wrapper = wrapWithWrapperClass($el, options); bindUi($el, $el, options); if (options.inputAddTypeAsClass) { $el.addClass(elType); }
            return {
                remove: function () {
                    $el.removeClass(options.inputClass); if (options.inputAddTypeAsClass) { $el.removeClass(elType); }
                    if ($wrapper) { $el.unwrap(); }
                }, update: returnFalse
            };
        }
    }, { match: function ($el) { return $el.is(":radio"); }, apply: function ($el, options) { var ds, $div, $span; ds = divSpan($el, options, { divClass: options.radioClass }); $div = ds.div; $span = ds.span; bindUi($el, $div, options); bindMany($el, options, { "click touchend": function () { $.uniform.update($(':radio[name="' + attrOrProp($el, "name") + '"]')); } }); classUpdateChecked($span, $el, options); return { remove: unwrapUnwrapUnbindFunction($el, options), update: function () { classClearStandard($div, options); classUpdateChecked($span, $el, options); classUpdateDisabled($div, $el, options); } }; } }, {
        match: function ($el) {
            if ($el.is("select") && !isMultiselect($el)) { return true; }
            return false;
        }, apply: function ($el, options) {
            var ds, $div, $span, origElemWidth; if (options.selectAutoWidth) { sizingInvisible($el, function () { origElemWidth = $el.width(); }); }
            ds = divSpan($el, options, { divClass: options.selectClass, spanHtml: ($el.find(":selected:first") || $el.find("option:first")).html(), spanWrap: "before" }); $div = ds.div; $span = ds.span; if (options.selectAutoWidth) { sizingInvisible($el, function () { swap($([$span[0], $div[0]]), { display: "block" }, function () { var spanPad; spanPad = $span.outerWidth() - $span.width(); $div.width(origElemWidth); $span.width(origElemWidth - spanPad); }); }); } else { $div.addClass('fixedWidth'); }
            bindUi($el, $div, options); bindMany($el, options, { change: function () { $span.html($el.find(":selected").html()); $div.removeClass(options.activeClass); }, "click touchend": function () { var selHtml = $el.find(":selected").html(); if ($span.html() !== selHtml) { $el.trigger('change'); } }, keyup: function () { $span.html($el.find(":selected").html()); } }); noSelect($span, options); return { remove: function () { $span.remove(); $el.unwrap().unbind(options.eventNamespace); return $el; }, update: function () { if (options.selectAutoWidth) { $.uniform.restore($el); $el.uniform(options); } else { classClearStandard($div, options); $span.html($el.find(":selected").html()); classUpdateDisabled($div, $el, options); } } };
        }
    }, {
        match: function ($el) {
            if ($el.is("select") && isMultiselect($el)) { return true; }
            return false;
        }, apply: function ($el, options) { var $wrapper; $el.addClass(options.selectMultiClass); $wrapper = wrapWithWrapperClass($el, options); bindUi($el, $el, options); return { remove: function () { $el.removeClass(options.selectMultiClass); if ($wrapper) { $el.unwrap(); } }, update: returnFalse }; }
    }, { match: function ($el) { return $el.is("textarea"); }, apply: function ($el, options) { var $wrapper; $el.addClass(options.textareaClass); $wrapper = wrapWithWrapperClass($el, options); bindUi($el, $el, options); return { remove: function () { $el.removeClass(options.textareaClass); if ($wrapper) { $el.unwrap(); } }, update: returnFalse }; } }]; if (isMsie() && !isMsieSevenOrNewer()) { allowStyling = false; }
    $.uniform = { defaults: { activeClass: "active", autoHide: true, buttonClass: "button", checkboxClass: "checker", checkedClass: "checked", disabledClass: "disabled", eventNamespace: ".uniform", fileButtonClass: "action", fileButtonHtml: "Choose File", fileClass: "uploader", fileDefaultHtml: "No file selected", filenameClass: "filename", focusClass: "focus", hoverClass: "hover", idPrefix: "uniform", inputAddTypeAsClass: true, inputClass: "uniform-input", radioClass: "radio", resetDefaultHtml: "Reset", resetSelector: false, selectAutoWidth: true, selectClass: "selector", selectMultiClass: "uniform-multiselect", submitDefaultHtml: "Submit", textareaClass: "uniform", useID: true, wrapperClass: null }, elements: [] }; $.fn.uniform = function (options) {
        var el = this; options = $.extend({}, $.uniform.defaults, options); if (!highContrastTest) { highContrastTest = true; if (highContrast()) { allowStyling = false; } }
        if (!allowStyling) { return this; }
        if (options.resetSelector) { $(options.resetSelector).mouseup(function () { wind.setTimeout(function () { $.uniform.update(el); }, 10); }); }
        return this.each(function () {
            var $el = $(this), i, handler, callbacks; if ($el.data("uniformed")) { $.uniform.update($el); return; }
            for (i = 0; i < uniformHandlers.length; i = i + 1) { handler = uniformHandlers[i]; if (handler.match($el, options)) { callbacks = handler.apply($el, options); $el.data("uniformed", callbacks); $.uniform.elements.push($el.get(0)); return; } }
        });
    }; $.uniform.restore = $.fn.uniform.restore = function (elem) {
        if (elem === undef) { elem = $.uniform.elements; }
        $(elem).each(function () {
            var $el = $(this), index, elementData; elementData = $el.data("uniformed"); if (!elementData) { return; }
            elementData.remove(); index = $.inArray(this, $.uniform.elements); if (index >= 0) { $.uniform.elements.splice(index, 1); }
            $el.removeData("uniformed");
        });
    }; $.uniform.update = $.fn.uniform.update = function (elem) {
        if (elem === undef) { elem = $.uniform.elements; }
        $(elem).each(function () {
            var $el = $(this), elementData; elementData = $el.data("uniformed"); if (!elementData) { return; }
            elementData.update($el, elementData.options);
        });
    };
}(this, jQuery)); if (typeof isMobile != 'undefined' && !isMobile) { $(window).load(function () { $("select.form-control,input[type='checkbox']:not(.comparator), input[type='radio'],input#id_carrier2, input[type='file']").uniform(); }); }