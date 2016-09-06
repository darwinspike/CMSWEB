/*
* Menu Builder, project: webtools
* Copyright (c) Elliemae Inc, 2015.
* Author: Adiel Hercules
* Contact: @adielhercules (twitter), jadher.11x2@gmail.com
*/
; (function (window) {
    var globalOptions = window.themeOptions || {
        supported: false,
        SiteId: "",
        SiteAlias: "",
        clientPath: "",
        stylefile: "",
        WebServicesPath: ""
    };

    var path = {
        HostClient: globalOptions.clientPath || "",
        TemplateName: globalOptions.SiteAlias || "",
        WebServicesPath: globalOptions.WebServicesPath || "",
    };


    var devtools, Status, cola, Que, access, menuData,
    menuDevData, editing = 0, colas = [], view = 'all',
    currentSite, menu = [], activeMenu = 0, menuLoaded = false, isNewItem = false;

    var storedMenusInDB = [];
    var menuIsSupported = globalOptions.supported || false;

    //    var clientURL = (typeof globalOptions.clientPath != "undefined" ? globalOptions.clientPath : "//localhost:14064/");
    var clientURL = (typeof globalOptions.clientPath != "undefined" ? globalOptions.clientPath : "");
    var itemData = {};

//    currentSite = globalOptions.SiteAlias || 'responsive_template04';
    currentSite = globalOptions.SiteAlias || '';

    var SiteID = globalOptions.SiteId || currentSite;


    // Added Darwin (Guid ID)
    function NewGuidID(callback) {
        var gi = "";
        $.ajax({
            url: path.WebServicesPath + "NewGUID.ashx",
            type: "GET",
            success: function (data) {
                if (data.error != null || data.error != "") {
                    gi = data.NewGUID;
                    if (typeof callback == "function") {
                        callback(gi);
                        console.log(gi);
                    }
                }else{
                    gi = data.error;
                    alert("Something occurred. Please try again.");
                }
            }
        });

        return gi;
    }

    /*
    * Item invitation appears when all menu items are removed on a menu, or when the menu is just created and it is empty */
    var newItemInvitation = '<div class="addItemInvitation">'
                            + '<h4><b>No</b> Items in this menu. Add your very first item to the menu.</h4> <a href="#/home" class="btn btn-primary"'
                            + ' onclick="window.removeAndInsertItemInvitation();">Add Menu Item</a></div>';



    /*
    * Create tooltips with this mini plugin (requires css)
    * example: $.miniTip();
    */
    $.fn.miniTip = function () {
        $element = $(this);

        $element.each(function () {
            $current = $(this);

            $current.prepend('<div class="t-c"><div class="t-w">' + $current.attr('title') + '</div></div>');
            $current.addClass('has-minitip').removeAttr('title');
        });
    }

    /*
    * Remove invitation and insert item invitation from current active menu
    */
    window.removeAndInsertItemInvitation = function() {
        $('.btn-addplaceholder-anchor').last().find('.btn-addplaceholder').trigger('click');
    }

    /*
    * Remove item invitation from current active menu
    */
    function removeItemInvitation() {
        $(".editor > ul.active").find('.addItemInvitation').remove();
    }


    /*
    * Add item invitation from current active menu
    */
    function addItemInvitation() {
        $(".editor > ul.active").html(newItemInvitation);
    }


    /*
    * Check if the active menu is empty (doesnt have any item)
    * @return {Boolean}
    */
    function menuIsEmpty() {
        return $(".editor > ul.active").find("li").length == 0;
    }


    /*
    * Generate a timeout
    * @param {Function} callback
    * @param {Number} timer
    * @param {String} name
    */
    function delayed(callback, timer, name) {
        clearTimeout(colas[name]);

        colas[name] = setTimeout(function () {
            if (typeof callback == "function") {
                callback();
            }
        }, timer);
    }




    /*
    * Check if we have multiple menus in same type
    * @param {Object} obj
    * @param {String} type
    * @return {Boolean}
    */
    function multipleInType(obj, type) {

        var concurrencies = [];

        for (var i = 0; i < obj.length; i++) {
            if (obj[i].type == type) {
                concurrencies.push(obj[i]);
            }
        }


        if (concurrencies.length > 1) {
            return true;
        }

        return false;

    }


    /*
    * Get an object of all the menus created of same type
    * @param {String} type (optional)
    * @return {Object}
    * @Summary // you can pass a paramenter "header" for example to get all menus created on same type.
    */
    function getMenuObjectActiveType(type) {
        var menuType = $('.editor > ul.active').attr('data-type');
        var obj = getMenuObject();
        var concurrencies = [];
        var type = type != undefined ? type : menuType;

        for (var i = 0; i < obj.length; i++) {
            if (obj[i].type == type) {
                concurrencies.push(obj[i]);
            }
        }


        return concurrencies;

    }


    /*
    * Generate a Html list of the menus on same type, to change between them which will be public
    */
    function loadMenusToChangePublishedState() {
        var obj = getMenuObjectActiveType();

        $(".menu-type-label").text(obj[0].type);

        var menuTemplate = '<div class="form-group checkbox {{checkedClass}}" rel="{{id}}">'
                        + '<label for="{{id}}"><span class="devicons icon-checkbox{{activeClass}}"></span> {{name}}</label>'
                        + '<input type="radio" name="publishedMenu" id="{{id}}" {{checked}}></div>';
        var formHtml = "";
        var someOnePublished = false;

        for (var i = 0; i < obj.length; i++) {
            formHtml += menuTemplate
                            .replace(/{{id}}/g, obj[i].id)
                            .replace(/{{name}}/g, obj[i].name)
                            .replace(/{{checkedClass}}/g, (obj[i].published ? "checked" : ""))
                            .replace(/{{activeClass}}/g, (obj[i].published ? "-active" : ""))
                            .replace(/{{checked}}/g, (obj[i].published ? ' checked="checked" ' : ""))
            ;

            if (obj[i].published) {
                someOnePublished = true;
            }
        }

        formHtml = menuTemplate
                            .replace(/{{id}}/g, "default-menu")
                            .replace(/{{name}}/g, "Don't use any menu I have created, use default menu of my website template.")
                            .replace(/{{checkedClass}}/g, (!someOnePublished ? "checked" : ""))
                            .replace(/{{activeClass}}/g, (!someOnePublished ? "-active" : ""))
                            .replace(/{{checked}}/g, (!someOnePublished ? ' checked="checked" ' : ""))
                + formHtml;

        $('.menu-list').html(formHtml);

    }







    /*
    * Create an object
    */
    function parseItem(item) {

        var contentNumber = item.attr('data-value'),
            isContent = contentNumber != "" && contentNumber != "0" && item.attr('data-type') == "page";

        itemData = {
            name: item.text().toLowerCase() == "item title" ? "" : item.text(),
            link: {
                isContent: isContent,
                url: isContent ? "{{link}}&pageid=" + contentNumber : item.attr('data-link'),
                content: {
                    id: contentNumber,
                    name: item.attr('data-original-title')
                },
                type: item.attr('data-type') != "" ? item.attr('data-type') : 'link',
                target: item.attr('data-target') == "true"
            },
            filter: {
                isCorporate: item.attr('data-showcorporate') == "true",
                isBranch: item.attr('data-showbranch') == "true",
                isLosite: item.attr('data-showlosite') == "true"
            }
        };

        return itemData;
    }




    function getParsedItem() {

        var name = $('[rel="name"]').val(),
            url = $('#link-url').val(),
            value = $('#link').val(),
            original_title = $('.select2-selection__rendered[id*="link"]').first().text(),
            type = value != "" ? "page" : 'link',
            target = $('[rel="target"] input[type="checkbox"]').is(":checked"),
            showcorporate = $('[rel="showcorporate"] input[type="checkbox"]').is(":checked"),
            showbranch = $('[rel="showbranch"] input[type="checkbox"]').is(":checked"),
            showlosite = $('[rel="showlosite"] input[type="checkbox"]').is(":checked"),
            isContent = $('#link-type').is(':checked');


        itemData = {
            id: editing,
            name: name,
            link: {
                isContent: isContent,
                url: isContent ? "{{link}}&pageid=" + value : url,
                content: {
                    id: value,
                    name: original_title
                },
                type: type,
                target: target
            },
            filter: {
                isCorporate: showcorporate,
                isBranch: showbranch,
                isLosite: showlosite
            }
        };

        return itemData;
    }











    /* =Dropdown */
    var dropdown = {


        formatResult: function (repo) {
            if (repo.loading) return repo.text;

            var markup = repo.name;

            return markup;
        }, formatSelection: function (repo) {
            return repo.name || repo.text;
        },

        create: function () {
            $('.select-dropdown').not('.with-search').select2({ minimumResultsForSearch: Infinity });

            $('.select-dropdown.with-search').select2({
                ajax: {
                    url: path.WebServicesPath + "GetContents.ashx",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page,
                            SiteID: SiteID,
                            s: currentSite
                        };
                    },
                    processResults: function (data, page) {
                        // parse the results into the format expected by Select2.
                        // since we are using custom formatting functions we do not need to
                        // alter the remote JSON data


                        return {
                            results: (function (data) {

                                var d = [];

                                for (var i = 0; i < data.length; i++) {
                                    d.push({ name: data[i].Name, id: data[i].Number });
                                }

                                return d;

                            })(data)
                        };
                    },
                    cache: true
                },
                minimumInputLength: 1,
                templateResult: dropdown.formatResult,
                templateSelection: dropdown.formatSelection
            });
        }, destroy: function () {
            $('.select-dropdown').select2('destroy');
        }, setValue: function ($select, value) {
            $select.val(value).trigger('change');
        }
    };

    function convertToSlug(Text) {
        return Text
            .toLowerCase()
            .replace(/[^\w ]+/g, '')
            .replace(/ +/g, '-')
        ;
    }



    var checkPluginState = function () {
        // console.log('check state');
        devtools.inc.bindMenuHandle('.editor ul');

        $('.has-tooltip').miniTip();
    }

    ''.trim || (String.prototype.trim = // Use the native method if available, otherwise define a polyfill:
        function () { // trim returns a new string (which replace supports)
            return this.replace(/^[\s\uFEFF]+|[\s\uFEFF]+$/g, '') // trim the left and right sides of the string
        });

    Status = {
        Statuss: function () {
            return $('.body').attr('class');
        }, quit: function (statusClass) {
            $('.body').removeClass(statusClass);

            var sep = statusClass.split(" ");
            for (i = 0; i < sep.length; i++) {
                if (sep[i] == "stored") {
                    //Status.notify('Changes saved.');
                }
            }

            return this.Statuss();
        }, add: function (Status) {
            if (Status == undefined) { Status = "ready"; }
            $('.body').addClass(Status);
            return this.Statuss();
        }, is: function (statusClass) {
            return $('.body').hasClass(statusClass);
        }, notify: function (msg) {
            var _id = ('noti_' + Math.random(0, 10)).replace('.', '');
            $('.notifications ul').append('<li id="' + _id + '"><a href="javascript:;" rel="close">&times;</a> ' + msg + '</li>');

            if (colas[_id] != undefined) {
                clearTimeout(colas[_id]);
            }

            colas[_id] = setTimeout(function () {
                var item = $('.notifications ul').find('li#' + _id); item.fadeOut(500, function () {
                    item.remove();

                    if ($('.notifications ul li').length == 0) {
                        Status.quit("notify");
                    }
                });
            }, 3000);

            if (!Status.is('notify')) {
                Status.add("notify");
            }
        }
    }

    Que = function (callback, time) {
        if (cola != undefined) { clearTimeout(cola); }
        cola = setTimeout(function () { callback() }, time);
    }












    devtools = {
        redirectTo: function (to) {
            to = "#/" + to;
            window.location = to;
            return to;
        },

















        inc: {
            init: function () {
                // console.log('init');
                devtools.inc.loadMenu();
                devtools.inc.events();

            }, bindMenuHandle: function (item) {
                $(item).nestedSortable({
                    handle: 'div',
                    listType: 'ul',
                    items: 'li',
                    placeholder: "ui-sortable-placeholder",
                    helper: 'clone',
                    isTree: true,
                    toleranceElement: '> div',
                    maxLevels: 2,
                    opacity: .7,
                    revert: 100
                });
                $(item).on('sortupdate', function (ui, event) {

                    updateMenuOnTheFly();


                    //console.log(ui,event);
                });

            }, returnData: function (id) {
                var data = {};

                var item = $('.editor').find('.item-title[data-id="' + id + '"]').first();

                if (item.length > 0) {
                    data = {
                        "name": item.text(),
                        "url": item.attr('data-link'),
                        "target": item.attr('data-target') == "true",
                        "id": item.attr('data-id'),
                        "showcorporate": item.attr('data-showcorporate') == "true",
                        "showbranch": item.attr('data-showbranch') == "true",
                        "showlosite": item.attr('data-showlosite') == "true",
                        "type": item.attr('data-type'),
                        "original_title": item.attr('data-original-title'),
                        "value": item.attr('data-value'),
                        "parent_id": "",
                        "index": 0,
                        "subtree": []
                    }
                }

                // console.log(data);
                return data;
            }, previewMenu: function (view) {

                var m = devtools.inc.parseTree($('.editor > ul.active > li > div > .item-title')),
                    mm = "";
                m = parseMenuTree(m, function () { }, true);
                //m = menuDevData;

                //m = m.data;
                m = menu[activeMenu].data;
                //console.log('parseMenuTree preview', m);


                for (var i = 0; i < m.length; i++) {


                    m[i].url = m[i].url.replace(/{{link}}/g, "default.aspx?s=site");

                    //show corporate
                    if (view == 'corporate' && m[i].showcorporate) {
                        var hassubtree = m[i].subtree != undefined && m[i].subtree.length > 0;
                        mm += '<li id="' + m[i].id + '" ' + (hassubtree ? 'class="dropdown"' : '') + ' >';
                        mm += '<a href="' + m[i].url + '" ' + (m[i].target ? 'target="_blank"' : '') + ' '
                            + (hassubtree ? 'class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"' : '')
                            + '>' + m[i].name + (hassubtree ? ' <span class="caret"></span>' : '') + '</a>';
                        if (hassubtree) {
                            mm += '<ul class="dropdown-menu" role="menu">';

                            for (0; x < m[i].subtree.length; x++) {
                                m[i].subtree[x].url = m[i].subtree[x].url.replace(/{{link}}/g, "default.aspx?s=site");
                                mm += '<li><a href="' + m[i].subtree[x].url + '" ' + (m[i].subtree[x].target ? 'target="_blank"' : '') + '>'
                                    + m[i].subtree[x].name + '</a></li>';
                            }

                            mm += '</ul>';
                        }
                    } else if (view == 'losite' && m[i].showlosite) {
                        var hassubtree = m[i].subtree != undefined && m[i].subtree.length > 0;
                        mm += '<li id="' + m[i].id + '" ' + (hassubtree ? 'class="dropdown"' : '') + ' >';
                        mm += '<a href="' + m[i].url + '" ' + (m[i].target ? 'target="_blank"' : '') + ' '
                            + (hassubtree ? 'class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"' : '') + '>'
                            + m[i].name + (hassubtree ? ' <span class="caret"></span>' : '') + '</a>';
                        if (hassubtree) {
                            mm += '<ul class="dropdown-menu" role="menu">';

                            for (x = 0; x < m[i].subtree.length; x++) {
                                m[i].subtree[x].url = m[i].subtree[x].url.replace(/{{link}}/g, "default.aspx?s=site");
                                mm += '<li><a href="' + m[i].subtree[x].url + '" ' + (m[i].subtree[x].target ? 'target="_blank"' : '') + '>'
                                    + m[i].subtree[x].name + '</a></li>';
                            }

                            mm += '</ul>';
                        }
                    } else if (view == 'branch' && m[i].showbranch) {
                        var hassubtree = m[i].subtree != undefined && m[i].subtree.length > 0;
                        mm += '<li id="' + m[i].id + '" ' + (hassubtree ? 'class="dropdown"' : '') + ' >';
                        mm += '<a href="' + m[i].url + '" ' + (m[i].target ? 'target="_blank"' : '') + ' '
                            + (hassubtree ? 'class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"' : '') + '>'
                            + m[i].name + (hassubtree ? ' <span class="caret"></span>' : '') + '</a>';
                        if (hassubtree) {
                            mm += '<ul class="dropdown-menu" role="menu">';

                            for (x = 0; x < m[i].subtree.length; x++) {
                                m[i].subtree[x].url = m[i].subtree[x].url.replace(/{{link}}/g, "default.aspx?s=site");
                                mm += '<li><a href="' + m[i].subtree[x].url + '" ' + (m[i].subtree[x].target ? 'target="_blank"' : '') + '>'
                                    + m[i].subtree[x].name + '</a></li>';
                            }

                            mm += '</ul>';
                        }
                    } else if (view == 'all') {
                        var hassubtree = m[i].subtree != undefined && m[i].subtree.length > 0;
                        mm += '<li id="' + m[i].id + '" ' + (hassubtree ? 'class="dropdown"' : '') + ' >';
                        mm += '<a href="' + m[i].url + '" ' + (m[i].target ? 'target="_blank"' : '') + ' '
                            + (hassubtree ? 'class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"' : '') + '>'
                            + m[i].name + (hassubtree ? ' <span class="caret"></span>' : '') + '</a>';
                        if (hassubtree) {
                            mm += '<ul class="dropdown-menu" role="menu">';

                            for (x = 0; x < m[i].subtree.length; x++) {
                                m[i].subtree[x].url = m[i].subtree[x].url.replace(/{{link}}/g, "default.aspx?s=site");
                                mm += '<li><a href="' + m[i].subtree[x].url + '" ' + (m[i].subtree[x].target ? 'target="_blank"' : '')
                                    + '>' + m[i].subtree[x].name + '</a></li>';
                            }

                            mm += '</ul>';
                        }
                    }



                }

                $('.preview-screen [role="navigation"] ul').html(mm);





            }, parseTree: function (ul) {
                var tags = [],
                    _self = this,
                    data = [],
                    id = 0;
                // ul.each(function(_index){
                for (var i = 0; i < ul.length; i++) {
                    data = [];
                    var subtree = ul.eq(i).closest('li').find('ul').find('li').find('.item-title');
                    id = ul.eq(i).attr('data-id');
                    data = devtools.inc.returnData(id);
                    data.index = i;
                    data.parent_id = "";

                    // console.log(data);

                    /* do not generate subtree, but push items as first level with parent reference
                     if(subtree.length > 0){
                     // alert("has subitems");
                     // alert(subtree.length);
                     // alert(data.toString());
                     var subtreetags = [];
                     for(x=0; x<subtree.length; x++){
                     subtreetags.push(devtools.inc.returnData(subtree.eq(x).attr('data-id')));
                     }
                     // data.subtree = devtools.inc.parseTree(subtree);
                     data.subtree = subtreetags;
    
                     }else{
                     // alert(data.toString(), "has not subitems");
                     }*/

                    if (subtree.length > 0) {
                        for (var x = 0; x < subtree.length; x++) {
                            var children = devtools.inc.returnData(subtree.eq(x).attr('data-id'));
                            children.parent_id = id;
                            children.index = x;
                            //console.log(children);
                            tags.push(children);
                        }
                    }



                    tags.push(data);

                    // });
                }

                //console.log('returning tags from parseTree', tags);

                // return data;
                return tags;
            }, editItem: function (id, append) {

                var found = $('.editor .item-title[data-id="' + id + '"]').length > 0,
                    item = $('.editor .item-title[data-id="' + id + '"]');
                append = append != undefined ? append : false;
                var $itemEditor = $('.item-editor');

                if (found) {

                    // console.log(menuData[editIndex]);

                    var data = parseItem(item);

                    $itemEditor.find('[rel="name"]').val(data.name);
                    $itemEditor.find('[rel="link-url"]').val(data.link.url);
                    $itemEditor.find('[rel="value"]').val(data.link.content.id);
                    $itemEditor.find('[rel="original_title"]').text(data.link.content.name);
                    $itemEditor.find('[rel="type"]').text(data.link.type);




                    function appendOption(data) {
                        var $link = $('#link');
                        var hasOption = $link.find('option[value="' + data.link.content.id + '"]').length > 0;
                        if (!hasOption) {
                            $link.append('<option value="' + data.link.content.id + '">' + data.link.content.name + '</option>');
                        }

                        $link.val(data.link.content.id).trigger('change');
                    }



                    /* RESET ALL CHECKS */
                    /* =Reset target checkbox */
                    $('[rel="target"] input').attr('checked', false).prop('checked', false).trigger('change');

                    /* =Reset corporate filter */
                    $('[rel="showcorporate"] input').attr('checked', false).prop('checked', false).trigger('change');

                    /* =Reset branch filter */
                    $('[rel="showbranch"] input').attr('checked', false).prop('checked', false).trigger('change');

                    /* =Reset losite filter */
                    $('[rel="showlosite"] input').attr('checked', false).prop('checked', false).trigger('change');

                    /* =Reset content checbox */
                    $('#link-type').attr('checked', false).prop('checked', false).trigger('change');




                    if (data.link.target) {
                        $('[rel="target"] input').attr('checked', true).prop('checked', true).trigger('change');
                    }

                    if (data.filter.isCorporate) {
                        $('[rel="showcorporate"] input').attr('checked', true).prop('checked', true).trigger('change');
                    }

                    if (data.filter.isBranch) {
                        $('[rel="showbranch"] input').attr('checked', true).prop('checked', true).trigger('change');
                    }

                    if (data.filter.isLosite) {
                        $('[rel="showlosite"] input').attr('checked', true).prop('checked', true).trigger('change');
                    }


                    if (data.link.isContent) {
                        $('#link-url').val('');
                        $('#link-type').attr('checked', true).prop('checked', true).trigger('change');
                        appendOption(data);
                    }


                    $('[rel="editlink"]').attr('href', '#/settings/edit/' + id);


                    editing = id;


                    return true;

                } else {
                    //create new item
                    //console.log("not found", "create new one");

                    //var d = new Date();
                    //var _id = d.format("dMyhs") + '-' + Math.random(0,10);
                    //_id = _id.replace(".",'');

                    var randomNumber = Math.floor(Math.random() * 1000) + 1;
                    randomNumber = randomNumber.pad(0, 5);
                    //                   var _id = hashids.encode([1, randomNumber, (new Date()).getMilliseconds()]);
                    //
                    
                    NewGuidID(mountGUID); 

                    function mountGUID(gui) {
                        var _id = gui;

                        var newItem = '<li class="menu-item"><div><span class="clearfix"></span><div class="opt tooltip-item move-children" title="Move to first level"><span class="devicons icon-move-children-top"></span></div><div class="sort tooltip-item" title="Drag to move"><i class="devicons icon-move-icon"></i></div> <span data-link="" class="item-title" data-target="false" data-value="" data-type="common" data-showbranch="true" data-showcorporate="true" data-showlosite="true" data-original-title="Home" data-id="' + _id + '">Item title</span><div class="opt-group"><a href="javascript:;" class="btn-add opt nofloat" data-id="' + _id + '"><i class="icon-add devicons"></i></a><a href="javascript:;" class="edit-it opt nofloat" data-id="' + _id + '"><i class="icon-edit devicons"></i></a><a href="javascript:;" class="remove-it opt nofloat" data-id="' + _id + '"><i class="devicons icon-remove"></i></a></div></div></li>';
                        editing = _id;
                        if (append)
                            $('.editor > ul.active').append(newItem);
                        else
                            $('.editor > ul.active').prepend(newItem);

                        var $editBtn = $('.edit-it[data-id="' + editing + '"]');
                        $editBtn.addClass('is-new');
                        $editBtn.trigger('click');
                        $('[rel="editlink"]').attr('href', '#/settings/edit/' + _id);
                        $itemEditor.find('[rel="name"]').val("");
                        $itemEditor.find('input[rel="link-url"]').val("");
                        $itemEditor.find('[rel="value"]').val("");
                        $itemEditor.find('[rel="original_title"]').text("");
                        /* =Reset target checkbox */
                        $('[rel="target"] input').attr('checked', false).prop('checked', false).trigger('change');
                        /* =Reset corporate filter */
                        $('[rel="showcorporate"] input').attr('checked', false).prop('checked', false).trigger('change');
                        /* =Reset branch filter */
                        $('[rel="showbranch"] input').attr('checked', false).prop('checked', false).trigger('change');
                        /* =Reset losite filter */
                        $('[rel="showlosite"] input').attr('checked', false).prop('checked', false).trigger('change');
                        /* =Reset content checbox */
                        $('#link-type').attr('checked', false).prop('checked', false).trigger('change');
                    }
                    
                    var _id = "";
                    /*$.ajax({
                        url: clientURL + "UserControl/MortgageCEO_Forms/WebServices/NewGUID.ashx",
                        type: "GET",
                        success: function (data) {
                            if (data.error != null || data.error != "") {
                                _id = data.NewGUID;

                                var newItem = '<li class="menu-item"><div><span class="clearfix"></span><div class="opt tooltip-item move-children" title="Move to first level"><span class="devicons icon-move-children-top"></span></div><div class="sort tooltip-item" title="Drag to move"><i class="devicons icon-move-icon"></i></div> <span data-link="" class="item-title" data-target="false" data-value="" data-type="common" data-showbranch="true" data-showcorporate="true" data-showlosite="true" data-original-title="Home" data-id="' + _id + '">Item title</span><div class="opt-group"><a href="javascript:;" class="btn-add opt nofloat" data-id="' + _id + '"><i class="icon-add devicons"></i></a><a href="javascript:;" class="edit-it opt nofloat" data-id="' + _id + '"><i class="icon-edit devicons"></i></a><a href="javascript:;" class="remove-it opt nofloat" data-id="' + _id + '"><i class="devicons icon-remove"></i></a></div></div></li>';
                                editing = _id;
                                if (append)
                                    $('.editor > ul.active').append(newItem);
                                else
                                    $('.editor > ul.active').prepend(newItem);

                                var $editBtn = $('.edit-it[data-id="' + editing + '"]');
                                $editBtn.addClass('is-new');
                                $editBtn.trigger('click');
                                $('[rel="editlink"]').attr('href', '#/settings/edit/' + _id);
                                $itemEditor.find('[rel="name"]').val("");
                                $itemEditor.find('input[rel="link-url"]').val("");
                                $itemEditor.find('[rel="value"]').val("");
                                $itemEditor.find('[rel="original_title"]').text("");
                                /* =Reset target checkbox *-/
                                $('[rel="target"] input').attr('checked', false).prop('checked', false).trigger('change');
                                /* =Reset corporate filter *-/
                                $('[rel="showcorporate"] input').attr('checked', false).prop('checked', false).trigger('change');
                                /* =Reset branch filter *-/
                                $('[rel="showbranch"] input').attr('checked', false).prop('checked', false).trigger('change');
                                /* =Reset losite filter *-/
                                $('[rel="showlosite"] input').attr('checked', false).prop('checked', false).trigger('change');
                                /* =Reset content checbox *-/
                                $('#link-type').attr('checked', false).prop('checked', false).trigger('change');
                            }
                        }
                    }); */




                    return false;
                }
                $('.minitabs li').first().find('a').trigger('click');
                $('.item-editor [rel="name"]').trigger('focus');
            }, firstTimeMenu: function () {
                /*alert("Trying to find menu settings in DB we found that the table content is being " +
                    "used for something else, do you want to erase the content and format to use it to save menu settings?");*/
                //devtools.redirectTo('settings/create');
                devtools.redirectTo('home');
                Status.add("editor-ready");
                $("#menu").html("");
                checkPluginState();
            }, toggleMenus: function () {
                var value = $('#menu').val(),
                    id = $('#menu option[value="' + value + '"]').attr('data-id'),
                    published = $('.editor ul[data-id="' + id + '"]').attr('data-published') == "true";

                log('pubished', published);

                $('.editor > ul').removeClass('active');
                $('.editor > ul[data-id="' + id + '"]').addClass('active');

                if ($('.editor > ul.active li').length == 0
                    && $('.editor > ul.active .addItemInvitation').lengh == 0) {
                    addItemInvitation();
                }


                $('#published').addClass('js-trigger').prop('checked', published).trigger('change');


                for (var i = 0; i < menu.length; i++) {
                    if (menu[i].id == id) {
                        activeMenu = i;
                    }
                }

            }, loadMenu: function () {
                // console.log(menuData);
                var _self = this;
                var enableMenu = false;
                $.ajax({
                    url: path.WebServicesPath + "MenuBuilder.ashx?s=" + currentSite + "&SiteID=" + SiteID,
                    success: function (res) {
//                        console.log("here");
//                        console.log(res);
                        menuLoaded = true;
                        if (typeof res != "object" || res.error) {
                            //console.log("Menu from CRM is empty.", res);
                            storedMenusInDB = JSON.stringify([]);
                            delayed(function () {
                                preloader.active(false);
                                delayed(function () {
                                    devtools.inc.firstTimeMenu();
                                    Status.add("firstTime");
                                    Status.quit("just-started-loading");
                                    TweenMax.from($(".body > .navigation"), 0.5, {
                                        y: -100,
                                        opacity: 0
                                    });
                                }, 1000, "load-menu-service-final");
                            }, 100, "load-menu-service");
                        } else {
                            console.log("Else");
                            storedMenusInDB = JSON.stringify(res);
                            menuData = res[activeMenu].data;
                            menuDevData = res[activeMenu].data;
                            menu = res;
                            //console.log("Menu from CRM is loaded successfully.");
                            function generateMenu() {
                                setTimeout(function () {
                                    devtools.inc.genMenu();
                                }, 500);
                                return true;
                            }
                            delayed(function () {

                                preloader.active(false);

                                delayed(function () {
                                    menu[activeMenu].data = parseMenuTree(menu, generateMenu);
                                    Status.quit("just-started-loading");

                                    TweenMax.from($(".body > .navigation"), 0.5, {
                                        y: -100,
                                        opacity: 0
                                    });

                                }, 1000, "load-menu-service-final");

                            }, 100, "load-menu-service");
                        }
                    }
                });



            },
            generateSingleMenu: function (index) {
                // console.log(menuData[0].name);
                var items = "";

                menu[index].data = parseMenuTree(menu[index].data, function () { }, true);

                menuData = menu[index].data;
                for (i = 0; i < menuData.length; i++) {
                    // console.log(menuData[i].name);
                    menuData[i].target = menuData[i].target.length == 0 ? "default" : menuData[i].target;
                    menuData[i].value = menuData[i].value != undefined ? menuData[i].value : "";

                    if (menuData[i].type == "link") {
                        menuData[i].url = menuData[i].url.indexOf('//') > -1 || menuData[i].url.indexOf('://') > -1 || menuData[i].url.indexOf('http') > -1 ? menuData[i].url : "http://" + menuData[i].url;
                    } else {
                        //menuData[i].url = menuData[i].url.replace('https://','http://');
                    }
                    items += '<li class="menu-item"><div><span class="clearfix"></span>' +
                        '<div class="opt tooltip-item move-children" title="Move to first level">' +
                        '<span class="devicons icon-move-children-top"></span></div>' +
                        '<div class="sort tooltip-item" title="Drag to move"><i class="devicons icon-move-icon"></i></div> ' +
                        '<span data-link="' + menuData[i].url + '" class="item-title" data-target="' + menuData[i].target + '" data-id="' + menuData[i].id + '" data-value="' + menuData[i].value + '" data-type="' + menuData[i].type + '" data-showbranch="' + menuData[i].showbranch + '" data-showcorporate="' + menuData[i].showcorporate + '" data-showlosite="' + menuData[i].showlosite + '" data-original-title="' + menuData[i].original_title + '">' + menuData[i].name + '</span><div class="opt-group"><a href="javascript:;" class="btn-add opt nofloat" data-id="' + menuData[i].id + '"><i class="icon-add devicons"></i></a><a href="javascript:;" class="edit-it opt nofloat" data-id="' + menuData[i].id + '"><i class="icon-edit devicons"></i></a><a href="javascript:;" class="remove-it opt nofloat" data-id="' + menuData[i].id + '"><i class="devicons icon-remove"></i></a></div></div>';
                    if (menuData[i].subtree != undefined && menuData[i].subtree.length > 0) {
                        items += '<ul>';
                        for (x = 0; x < menuData[i].subtree.length; x++) {
                            // console.log(menuData[i].subtree[x].name);
                            menuData[i].subtree[x].target = menuData[i].subtree[x].target.length == 0 ? "default" : menuData[i].subtree[x].target;
                            menuData[i].subtree[x].value = menuData[i].subtree[x].value != undefined ? menuData[i].subtree[x].value : "";

                            if (menuData[i].subtree[x].type == "link") {
                                menuData[i].subtree[x].url = menuData[i].url.indexOf('//') > -1 || menuData[i].subtree[x].url.indexOf('://') > -1 || menuData[i].subtree[x].url.indexOf('http') > -1 ? menuData[i].subtree[x].url : /*"http://" +*/ menuData[i].subtree[x].url;
                            } else {
                                menuData[i].subtree[x].url = menuData[i].subtree[x].url.replace('https://', '');
                            }
                            items += '<li class="menu-item"><div><span class="clearfix"></span><div class="opt tooltip-item move-children" title="Move to first level"><span class="devicons icon-move-children-top"></span></div><div class="sort tooltip-item" title="Drag to move"><i class="devicons icon-move-icon"></i></div> <span data-link="' + menuData[i].subtree[x].url + '" class="item-title" data-target="' + menuData[i].subtree[x].target + '" data-value="' + menuData[i].subtree[x].value + '" data-type="' + menuData[i].subtree[x].type + '" data-showbranch="' + menuData[i].subtree[x].showbranch + '" data-showcorporate="' + menuData[i].subtree[x].showcorporate + '" data-showlosite="' + menuData[i].subtree[x].showlosite + '" data-original-title="' + menuData[i].subtree[x].original_title + '" data-id="' + menuData[i].subtree[x].id + '">' + menuData[i].subtree[x].name + '</span><div class="opt-group"><a href="javascript:;" class="btn-add opt nofloat" data-id="' + menuData[i].subtree[x].id + '"><i class="icon-add devicons"></i></a><a href="javascript:;" class="edit-it opt nofloat" data-id="' + menuData[i].subtree[x].id + '"><i class="icon-edit devicons"></i></a><a href="javascript:;" class="remove-it opt nofloat" data-id="' + menuData[i].subtree[x].id + '"><i class="devicons icon-remove"></i></a></div></div></li>';
                        }
                        items += '</ul>';
                    }
                    items += '</li>';
                }

                if (items == "") {
                    items = newItemInvitation;
                }
                
                $('.editor').append("<ul class='" + (activeMenu == index ? 'active' : '') + "' " +
                    "data-name='" + menu[index].name + "' data-id='" + menu[index].id + "' " +
                    "data-type='" + menu[index].type + "' data-published='" + menu[index].published + "'>" + items + "</ul>");
            },
            genMenu: function () {
                var items = "", i;
                //menuData = eval(menuData);
                menuData = menu;

                //console.log(menuData);

                //var menuDrop = "<option value='-1'>Create new menu</option>";
                var menuDrop = "";
                for (i = 0; i < menu.length; i++) {
                    (function () {
                        menuDrop += "<option value=" + i + " " +
                            "data-id='" + menu[i].id + "' data-type='" + menu[i].type + "' " +
                            "data-published='" + menu[i].published + "'>" + menu[i].name + " (type: " + menu[i].type + ")</option>";
                        devtools.inc.generateSingleMenu(i);
                    })(i);
                }

                $("#menu").html(menuDrop).val(0).trigger("change");

                Status.add("editor-ready");
                checkPluginState();


            }, events: function () {

                $('.body')

                    .on('change', '#link-type', function () {
                        var isChecked = $(this).is(':checked'),
                            $tabs = $('[rel="content-type"] .single-block');

                        $tabs.first().show().siblings().hide();
                        $('[rel="type"]').text('link');

                        if (isChecked) {
                            $tabs.last().show().siblings().hide();
                            $('[rel="type"]').text('page');
                        }


                    })

                    .on('change', '#menu', function () {
                        if ($(this).val() == -1) {
                            devtools.redirectTo('settings/create');

                            $(this).val(0).trigger('change');

                            return true;
                        }

                        devtools.inc.toggleMenus();
                    })

                    .on('change', '#published', function (e) {
                        var published = $(this).is(':checked'),
                            notFirst = $(this).data('started') != undefined;



                        var obj = getMenuObject();
                        var menuId = $('.editor > ul.active').attr('data-id');
                        var menuName = $('.editor > ul.active').attr('data-name');
                        var menuType = $('.editor > ul.active').attr('data-type');
                        var pub = $('.editor > ul.active').attr('data-published') == "true";

                        //if (multipleInType(obj, menuType)) {

                        if ($(this).hasClass('js-trigger')) {
                            $(this).removeClass('js-trigger');
                            return false;
                        }

                        $(this).prop('checked', pub);
                        if (pub) {
                            $(this).closest('.checkbox').addClass('checked')
                                .find('.devicons').addClass('icon-checkbox-active').removeClass('icon-checbox');
                        } else {
                            $(this).closest('.checkbox').removeClass('checked')
                                .find('.devicons').removeClass('icon-checkbox-active').addClass('icon-checbox');
                        }



                        devtools.redirectTo('settings/change-published-state');

                        return false;

                        /* } else {
     
                             $('.editor > ul.active')
                                 .attr('data-published', published)
                             ;
     
                             var $option = $('#menu option[data-id="' + menuId + '"]'),
                                 value = $option.attr('value');
     
                             $option.text(menuName);
                             $option.attr('data-published', published);
     
     
     
                             $(this).data('started', true);
                         }*/


                    })

                    .on('click', '.removeMenu', function (e) {
                        e.stopPropagation(); e.preventDefault();

                        var message = "Are you sure you want to remove this menu?";

                        function onConfirm() {
                            var $menu = $('.editor > ul.active'),
                                name = $menu.attr('data-name'),
                                dataId = $menu.attr('data-id');

                            $menu.remove();
                            $('#menu option[data-id="' + dataId + '"]').remove();


                            if ($(".editor > ul").length == 0) {
                                Status.add("firstTime");

                                /* Check if db has menus initially */
                                if ((JSON.parse(storedMenusInDB)).length > 0) {
                                    Status.add("allRemoved");
                                }
                            }

                            if ($('#menu option').length > 0) {
                                $('#menu').val($('#menu option').eq(0).attr('value'));
                            } else {
                                $('#menu').val(-1);
                                menu = [];
                            }

                            $('#menu').trigger('change');

                            devtools.redirectTo('home');

                            for (var i = 0; i < menu.length; i++) {
                                if (menu[i].id == dataId) {
                                    menu.splice(i, 1);
                                }
                            }


                        }


                        $('#confirm-modal').confirm({
                            title: 'Delete menu',
                            message: message,
                            onAccept: onConfirm,
                            cancelButton: 'Cancel',
                            acceptButton: 'Yes, remove it'
                        });

                        return false;
                    })

                    .on('submit', '#editMenuForm', function (e) {
                        e.stopPropagation(); e.preventDefault();
                        var menuName = $('#menuNameEdited'),
                            menuId = $('#menuIdEdited'),
                            menuType = $('#menuTypeEdited'),
                            menuPublished = $('#menuPublishedEdited');

                        if (menuName.val().trim().length > 0 && menuType.val().trim().length > 0) {
                            $('.editor > ul[data-id="' + menuId.val() + '"]')
                                .attr('data-name', menuName.val())
                                .attr('data-type', menuType.val())
                                .attr('data-published', menuPublished.val())
                            ;

                            //$("#menu").val(2).trigger('change');
                            var $option = $('#menu option[data-id="' + menuId.val() + '"]'),
                                value = $option.attr('value');

                            $option.text(menuName.val());

                            var $clone = $option.clone();

                            $option.before($clone);
                            $option.remove();

                            $('#menu').val(value).trigger('change');

                            devtools.redirectTo("home");
                        } else {
                            $('#confirm-modal').confirm({
                                title: 'Error',
                                message: 'Please fill the menu name and menu type.'
                            });
                        }


                        return false;
                    })


                    .on('submit', '#newMenuForm', function (e) {
                        e.stopPropagation(); e.preventDefault();
                        //console.log( $(this).find(':input').serialize() + '&index=' + menu.length );
                        var menuName = $('#menuName').val();
                        var randomNumber = Math.floor(Math.random() * 1000) + 1;
                        randomNumber = randomNumber.pad(0, 5);
                        var hashAttributes = [0, randomNumber, (new Date()).getMilliseconds()];

                        $.ajax({
                            url: path.WebServicesPath + "NewGUID.ashx",
                            type: "GET",
                            success: function (data) {
                                var id = data.NewGUID;//hashids.encode(hashAttributes);
                                var slug = id, menuType = $('#menuType').val();
                                if (menuType.trim().length > 1 && menuName.trim() != "" && menuName.trim().length > 0) {
                                    $('.editor').append('<ul class="ui-sortable" data-name="' + menuName + '" ' +
                                        'data-id="' + slug + '" data-type="' + menuType + '" data-published="false">' + newItemInvitation + '</ul>');
                                    $('#menu').append('<option value="' + menu.length + '" ' +
                                        'data-id="' + slug + '" data-type="' + menuType + '" data-published="false">'
                                        + menuName + ' (type: ' + menuType + ')</option>');
                                    $('#menu').val(menu.length).trigger('change');
                                    menu.push({ name: menuName, id: slug, type: menuType, data: [] });
                                    checkPluginState();
                                    $('#menuType').val("").trigger('change');
                                    devtools.redirectTo('home');
                                } else {
                                    $('#confirm-modal').confirm({
                                        title: 'Error',
                                        message: 'Please fill the menu name and menu type.',
                                        onAccept: function () {
                                            $('#menuName').focus()
                                        }, acceptButton: "Ok, Edit"
                                    });
                                }


                            }
                        });


                        return false;
                    })

                    .on('click', '.move-children', function () {
                        var item = $(this).closest('li');

                        if (item.find('ul').length > 0) {
                            var subitems = item.find('ul').find('li');
                            item.after(subitems);
                        }

                        $(this).closest('li').closest('ul').closest('li').after(item);

                        Status.add('nostored');

                    })

                    .on('click', '.remove-it', function () {
                        var item = $(this).closest('li');
                        var id = $(this).attr('data-id');

                        var message = "Are you sure you want to remove this menu item?";

                        function onConfirmItem() {
                            item.remove();
                            Status.quit('editing nostored storing');
                            Status.add('nostored');

                            for (var i = 0; i < menu[activeMenu].data.length; i++) {
                                if (menu[activeMenu].data[i].id == id) {
                                    menu[activeMenu].data.splice(i, 1);
                                } else {
                                    for (var x = 0; x < menu[activeMenu].data[i].subtree.length; x++) {
                                        if (menu[activeMenu].data[i].subtree[x].id == id) {
                                            menu[activeMenu].data[i].subtree.splice(x, 1);
                                        }
                                    }
                                }
                            }

                            if (menuIsEmpty()) {
                                addItemInvitation();
                            }
                        }

                        $('#confirm-modal').confirm({
                            title: 'Remove menu item',
                            message: message,
                            onAccept: onConfirmItem,
                            cancelButton: 'Cancel',
                            acceptButton: 'Yes, remove it'
                        });





                    })

                    .on('change', '.checkbox input[type="checkbox"]', function () {
                        if ($(this).is(":checked")) {
                            $(this).closest('.checkbox').addClass('checked').find('.devicons').addClass('icon-checkbox-active').removeClass('icon-checkbox');
                        } else {
                            $(this).closest('.checkbox').removeClass('checked').find('.devicons').addClass('icon-checkbox').removeClass('icon-checkbox-active');
                        }
                    })


                    .on('change', '.checkbox input[type="radio"]', function () {
                        $(this).closest('.checkbox').addClass('checked').find('.devicons').addClass('icon-checkbox-active').removeClass('icon-checkbox');
                        $(this).closest('.checkbox').siblings().removeClass('checked').find('.devicons').removeClass('icon-checkbox-active').addClass('icon-checkbox');
                    })



                    .on('click', '.set-published-menu', function () {
                        var menus = $('.menu-list input[type="radio"]');

                        menus.each(function () {
                            var current = $(this);
                            var menu = $('.editor > ul[data-id="' + current.attr('id') + '"]');

                            var is_active = menu.hasClass('active');
                            var checked = current.is(':checked');

                            menu.attr('data-published', checked);

                            if (is_active) {
                                $('#published').prop('checked', checked);
                                if (checked) {
                                    $('#published').closest('.checkbox').addClass('checked').find('.devicons')
                                        .addClass('icon-checkbox-active').removeClass('icon-checkbox');
                                } else {
                                    $('#published').closest('.checkbox').removeClass('checked').find('.devicons')
                                        .removeClass('icon-checkbox-active').addClass('icon-checkbox');
                                }
                            }
                        });

                        devtools.redirectTo('home');
                    })





                    .on('click', '.minitabs a', function () {
                        $(this).parent().addClass('active').siblings().removeClass('active');

                        $('.minitab').eq($(this).parent().index()).addClass('active').siblings().removeClass('active');
                    })

                    //edit item, editItem
                    .on('click', '.edit-it', function () {

                        $('.item-editor-header h4').text('edit item');

                        isNewItem = false;

                        if ($(this).hasClass('is-new')) {
                            $(this).removeClass('is-new');
                            isNewItem = true;
                            $('.item-editor-header h4').text('add item');
                        }

                        $('.menu-item > div').removeClass('active');
                        $(this).closest('.menu-item').find('div').first().addClass('active');
                        var _id = $(this).attr('data-id'),
                            ItemTop = $(this).closest('li').position().top;
                        Status.quit("editing");

                        Que(function () {

                            devtools.inc.editItem(_id);
                            positionateItemEditPopUp(ItemTop);

                            Que(function () { $('#title').focus(); }, 200);

                            Status.add("editing");

                        }, 0);
                    })

                    .on('change', '.item-editor .checkbox input[type="checkbox"]', function () {
                        Status.add("nostored");
                    }).on('keyup', '[rel="name"]', function (e) {
                        if ((e.keyCode >= 65 && e.keyCode <= 90) || (e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode == 189 || e.keyCode == 191) || e.keyCode == 8) {
                            // console.log(e.keyCode, "matched");
                            Status.add("nostored");
                        } else {
                            // console.log(e.keyCode, "not matched");
                        }
                    }).on('click', '.btn-save', function () {




                        //save item
                        // console.log(editing, "save");

                        var item = $('.editor .item-title[data-id="' + editing + '"]');
                        // console.log("editing: ", editing, item.length);


                        var data = getParsedItem();

                        var $t = $('#title');

                        if ($t.val().trim() == "" || data.link.url.length == 0) {

                            $('#confirm-modal').confirm({
                                title: 'Invalid inputs',
                                message: "Please fill all the inputs (title and url are required).",
                                onAccept: function () {
                                    $t.focus();
                                },
                                acceptButton: 'Edit'
                            });

                        } else {
                            if (item.length > 0) {
                                item.text(data.name);
                                item.attr('data-link', data.link.url);
                                item.attr('data-type', data.link.type);
                                item.attr('data-target', data.link.target);
                                item.attr('data-value', (data.link.isContent ? data.link.content.id : ""));
                                item.attr('data-showcorporate', data.filter.isCorporate);
                                item.attr('data-showbranch', data.filter.isBranch);
                                item.attr('data-showlosite', data.filter.isLosite);
                                item.attr('data-original-title', (data.link.isContent ? data.link.content.name : ""));

                            }

                            Status.quit("nostored"); Status.add("stored");
                            Status.quit("editing");

                            if (cola["save"] != undefined) { clearTimeout(cola["save"]); }
                            cola["save"] = setTimeout(function () {
                                Status.quit("stored");

                                updateMenuOnTheFly();

                            }, 500);
                        }




                    })
                    .on('click', '.btn-addplaceholder', function () {

                        removeItemInvitation();

                        isNewItem = true;
                        devtools.inc.editItem(0, ($(this).hasClass('item-append')));

                    })

                    .on('click', '.btn-add', function () {
                        //var d = new Date();
                        //var _id = d.format("dMyhs") + '-' + Math.random(0,10);
                        //_id = _id.replace(".",'');
                        var $this = $(this);



                        var randomNumber = Math.floor(Math.random() * 1000) + 1;
                        randomNumber = randomNumber.pad(0, 5);

                        $.ajax({
                            url: path.WebServicesPath + "NewGUID.ashx",
                            type: "GET",
                            success: function (data) {
                                
                                    gi = data.NewGUID;

                                    var _id = data.NewGUID; //hashids.encode([1, randomNumber, (new Date()).getMilliseconds()]);


                                    var newItem = '<li class="menu-item"><div><span class="clearfix"></span>'
                                                    + '<div class="opt tooltip-item move-children" title="Move to first level">'
                                                    + '<span class="devicons icon-move-children-top"></span></div>'
                                                    + '<div class="sort tooltip-item" title="Drag to move">'
                                                    + '<i class="devicons icon-move-icon"></i></div> '
                                                    + '<span data-link="" class="item-title" data-target="false" data-value="" '
                                                    + 'data-type="common" data-showbranch="true" data-showcorporate="true" data-showlosite="true" data-original-title="Home" data-id="' + _id + '">Item title</span><div class="opt-group"><a href="javascript:;" class="btn-add opt nofloat" data-id="' + _id + '"><i class="icon-add devicons"></i></a><a href="javascript:;" class="edit-it opt nofloat" data-id="' + _id + '"><i class="icon-edit devicons"></i></a><a href="javascript:;" class="remove-it opt nofloat" data-id="' + _id + '"><i class="devicons icon-remove"></i></a></div></div></li>';
                                    editing = _id;
                                    isNewItem = true;
                                    var ul = $this.closest('li').find('ul');
                                    if (ul.length > 0) {
                                        ul.append(newItem);
                                    } else {
                                        $this.closest('li').append('<ul class="ui-sortable">' + newItem + '</ul>');
                                    }
                                    var $editBtn = $('.edit-it[data-id="' + editing + '"]');
                                    $editBtn.addClass('is-new');
                                    $editBtn.trigger('click');
                                    $('.item-editor-header h4').text('add item');

                            }
                        });



                    })

                    .on('click', '.cancel-btn', function () {

                        var $t = $('#title');

                        var data = getParsedItem();
                        var oldData = parseItem($('.editor > ul.active .item-title[data-id="' + data.id + '"]'));

                        function removeSingleItem() {
                            $('.editor li [data-id="' + data.id + '"]').closest('li').remove();
                            Status.quit("editing");

                            if (menuIsEmpty()) {
                                addItemInvitation();
                            }
                        }

                        /*if( ( $t.val().trim() == "" || data.link.url.length == 0 ) && isNewItem ){
    
                            $('#confirm-modal').confirm({
                                title: 'Empty fields (title and url are required)',
                                message: "If you continue this item will be removed.",
                                onAccept: function(){
                                    removeSingleItem();
                                },
                                acceptButton: 'Close and Remove',
                                onCancel: function(){
                                    $t.focus();
                                },
                                cancelButton: 'Continue editing'
                            });
    
                        }else{
                            if( isNewItem ){
                                $('.btn-save-inside').trigger('click');
                            }else{
                                Status.quit("editing");
                            }
                        }*/


                        if (isNewItem) {

                            $('#confirm-modal').confirm({
                                title: 'Warning',
                                message: "If you continue this item will not be saved.",
                                onAccept: function () {
                                    removeSingleItem();
                                },
                                acceptButton: 'Close',
                                onCancel: function () {
                                    $t.focus();
                                },
                                cancelButton: 'Continue editing'
                            });

                        } else {



                            if (data.name != oldData.name
                                || (oldData.link.isContent == data.link.isContent
                                    && ((data.link.isContent && data.link.content.id != oldData.link.content.id)
                                        || data.link.url != oldData.link.url))) {

                                $('#confirm-modal').confirm({
                                    title: 'Warning',
                                    message: "Changes were performed, if you continue your data will be lost.",
                                    onAccept: function () {
                                        Status.quit("editing");
                                    },
                                    acceptButton: 'Close',
                                    onCancel: function () {
                                        $t.focus();
                                    },
                                    cancelButton: 'Continue editing'
                                });


                            } else {
                                if (($t.val().trim() == "" || data.link.url.length == 0)) {

                                    $('#confirm-modal').confirm({
                                        title: 'Warning',
                                        message: "Make sure the title and url are not empty.",
                                        onAccept: function () {
                                            Status.quit("editing");
                                        },
                                        acceptButton: 'Close',
                                        onCancel: function () {
                                            $t.focus();
                                        },
                                        cancelButton: 'Continue editing'
                                    });

                                } else {
                                    Status.quit("editing");
                                }
                            }
                        }



                    })

                    .on('click', '.tabs a', function () {
                        $(this).parent().addClass('active').siblings().removeClass('active');
                        var toggle = $(this).attr('data-toggle');
                        switch (toggle) {
                            case "all":
                                $('.editor li').show();
                                view = toggle;
                                break;
                            case "corporate":
                                $('.item-title[data-showbranch], .item-title[data-showlosite], .item-title[data-showcorporate]').closest('li').hide();
                                $('.item-title[data-showcorporate="true"]').closest('li').show();
                                view = toggle;
                                break;
                            case "branch":
                                $('.item-title[data-showbranch], .item-title[data-showlosite], .item-title[data-showcorporate]').closest('li').hide();
                                $('.item-title[data-showbranch="true"]').closest('li').show();
                                view = toggle;
                                break;
                            case "losite":
                                $('.item-title[data-showbranch], .item-title[data-showlosite], .item-title[data-showcorporate]').closest('li').hide();
                                $('.item-title[data-showlosite="true"]').closest('li').show();
                                view = toggle;
                                break;

                        }
                    })

                    .on('change', '#show', function () {
                        var toggle = $(this).val(), $items = [];

                        switch (toggle) {
                            case "all":
                                $('.editor li').show();
                                view = toggle;
                                break;
                            case "corporate":
                                $('.item-title[data-showbranch], .item-title[data-showlosite], .item-title[data-showcorporate]').closest('li').hide();
                                $('.item-title[data-showcorporate="true"]').closest('li').show();
                                view = toggle;
                                break;
                            case "branch":
                                $('.item-title[data-showbranch], .item-title[data-showlosite], .item-title[data-showcorporate]').closest('li').hide();
                                $('.item-title[data-showbranch="true"]').closest('li').show();
                                view = toggle;
                                break;
                            case "losite":
                                $('.item-title[data-showbranch], .item-title[data-showlosite], .item-title[data-showcorporate]').closest('li').hide();
                                $('.item-title[data-showlosite="true"]').closest('li').show();
                                view = toggle;
                                break;

                        }
                    })

                    .on('keyup focus', '#search', function (e) {
                        //if((e.keyCode >= 65 && e.keyCode <= 90) || (e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode == 189 || e.keyCode == 191) || e.keyCode==8 || e.keyCode==undefined){

                        //    if(colas['search']!=undefined){
                        //        clearTimeout(colas['search']);
                        //    }

                        //    var q = $(this).val();

                        //    colas['search'] = setTimeout(function(){
                        //        $.ajax({
                        //            url: 'contents.php?s=' + currentSite + '&q=' + encodeURI(q),
                        //            success: function(data){
                        //                // console.log(data);
                        //                var r = "";
                        //                for(i=0; i<data.length; i++){
                        //                    r += '<a href="javascript:;" data-id="' + data[i].Content.ID + '" data-number="'
                        //                        + data[i].Content.Number
                        //                        + '"><li><span class="rtype">crm</span>' +
                        //                        ' <span class="rtitle">' + data[i].Content.Name + '</span></li></a>';
                        //                }

                        //                $('.results').empty();
                        //                $('.results').html(r);
                        //            }
                        //        });
                        //    }, 500);

                        //}



                    }).on('click', '.filtertabs a', function () {
                        var _i = $(this).parent().index();

                        $(this).parent().addClass('active').siblings().removeClass('active');
                        $('.filtertab').eq(_i).addClass('active').siblings().removeClass('active');
                    }).on('click', '.results a', function () {

                        $(this).addClass('active').siblings().removeClass('active');
                        var number = $(this).attr('data-number'), title = $(this).find('.rtitle').text();

                        Que(function () {
                            devtools.redirectTo("home");

                            $('.item-editor [rel="name"]').val(title);
                            $('.item-editor [rel="original_title"]').text(title);
                            $('.item-editor [rel="type"]').text("crm");
                            $('.item-editor [rel="value"]').val(number);


                            Status.add("nostored");

                        }, 400);

                    }).on('click', '.set-url', function () {
                        var title = $('#curl_title').val(), url = $('#curl_url').val();
                        $('.item-editor [rel="name"]').val(title);
                        $('.item-editor [rel="original_title"]').text(title);
                        $('.item-editor [rel="type"]').text("link");
                        $('.item-editor [rel="value"]').val("0");
                        $('.item-editor [rel="url"]').val(url);

                        Que(function () {
                            devtools.redirectTo("home");
                            Status.add("nostored");
                        }, 400);
                    }).on('click', '[rel="deploybtn"]', function () {
                        //var deployto = $('[rel="deploy"]').val();

                        //if (deployto == "Production") {
                        //    devtools.redirectTo("home");
                        //    Status.notify('This demo has no access to ' + deployto + ' server.<br>Fake notification. Keep in mind this is a demo.');
                        //} else {


                        //    var m = devtools.inc.parseTree($('.editor > ul > li > div > .item-title'));

                        //    $.ajax({
                        //        url: 'saveContent.php?cid=91&s=' + currentSite,
                        //        type: "POST",
                        //        data: {"content": JSON.stringify(m)},
                        //        success: function(data){
                        //            console.log('deployed');
                        //            devtools.redirectTo("home");
                        //            setTimeout(function(){
                        //                // alert("Deployment successfully. ` sent to: " + deployto);
                        //                Status.notify('Deployment successfully. Menu sent to: ' + deployto);
                        //            }, 800);
                        //        }
                        //    });

                        //}
                    }).on('click', '[rel="close"]', function () {
                        var item = $(this).closest('li'), _id = item.attr('id');
                        if (colas[_id] != undefined) {
                            clearTimeout(colas[_id]);
                        }

                        colas[_id] = setTimeout(function () {
                            item.fadeOut(500, function () {
                                item.remove();
                            });
                        }, 10);

                    }).on('click', '.preview-screen a', function (e) {
                        if (!$(this).hasClass('dropdown-toggle')) {
                            e.preventDefault(); e.stopPropagation();
                        }
                    });
            }

        },
























        deniedAccess: function () {
            var isAccess = access == null ? true : access[0];
            return isAccess;
        }, events: function () {
            Status.add('ready');
            window.onload = function () {
                Status.add('loaded');
                Status.quit('loading');
            }

            $('.body').on('click', '.signinbtn', function (e) {
                e.preventDefault(); e.stopPropagation();
                devtools.checkLogin();
            }).on('submit', '#signinform', function (e) {
                e.preventDefault(); e.stopPropagation();
                devtools.checkLogin();
            }).on('keyup', function (e) {
                // console.log(e.keyCode);
                //if (e.keyCode == 27) {
                //    if (Status.is("open")) {
                //        Status.quit("open");
                //        devtools.redirectTo("home");
                //    } else if (Status.is("editing")) {
                //        Status.quit("editing");
                //        $('.menu-item > div').removeClass('active');
                //    }

                //}
            });
        }, visitor: function () {
            this.events();

            if (this.deniedAccess()) {
                this.redirectTo('settings/signin');
            }

            return true;
        }, activateServices: function () {

            //load navigation options
            /*var rightNav = "";
            for(i=0; i<data.menu[0].length; i++){
                rightNav += '<li><a href="#/' + data.menu[0][i]['url'] + '">' + data.menu[0][i]['title'].toUpperCase() + '</a></li>';
            }
            $('.navigation .right-nav ul').html(rightNav);*/

            //load dependencies
            /*for(i=0; i<data.dependencies.length; i++){
                (function(){
                    if(data.dependencies[i]['type'] == "text/javascript"){
                        var currentURL = data.dependencies[i]['url'];
                        var s = document.createElement("script");
                        s.type = "text/javascript";
                        s.src = currentURL;
                        $(".body").append(s);
                        if(currentURL.indexOf('devtools')>-1){
                            devtools.inc.init();
                        }
                    }
                })();
            }*/

            devtools.inc.init();


            //load settings
            /*var settingsnav = "" +
                "<div class='form-group'>" +
                "<select rel='deploy' class='form-control select-dropdown' style='min-width: 200px'>";
            for(i=0; i<data.menu[1].length; i++){
                settingsnav = settingsnav + ""
                    + '<option value="' + data.menu[1][i]['title'] + '">' + data.menu[1][i]['hint'] + '</option>';
            }
            settingsnav = settingsnav + "</select></div><div class='form-group'><a href='javascript:;' class='btn btn-success'" +
                " rel='deploybtn'>Deploy</a></div>"
    
            $('[data-layer="settings"]').append(settingsnav);*/

            $('.only-editor').show();
            dropdown.create();


        }, checkLogin: function () {
            //var info = [$(this).find('[name="username"]').val(), $(this).find('[name="password"]').val()];
            /*$.ajax({
                url: 'js/sign.json',
                type: 'post',
                success: function(data){
                    // console.log(data);
                    if(data.status == "Ok"){
                        devtools.activateServices(data);
                        access = [false, Math.random()];
                        // console.log(access);
                        devtools.redirectTo('home');
                        Status.add('signedin');
                        Status.quit('signedout');
                    }else{
                        devtools.redirectTo("home");
                    }
    
                }
            });*/


            devtools.activateServices();
            Status.add('signedin');
            Status.quit('signedout');

            access = [false, Math.random()];
            // console.log(access);
            devtools.redirectTo('home');

        }, setModal: function (modal) {
            $('.layer-box[data-layer="' + modal + '"]').show().siblings().hide();
        }, loadMenu: function () {
            //console.log("callback");
        }, setCookie: function (cname, cvalue) {
            var exdays = 30;
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toGMTString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        }, getCookie: function (cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return null;
        }
    };








    var home = function () {


        if ($(".editor > ul").length > 0 && (JSON.parse(storedMenusInDB)).length > 0) {
            Status.quit("allRemoved");
        }

        if (getMenuObject().length == 0 && menuLoaded && $(".body").hasClass("signedin")) {
            //devtools.redirectTo('settings/create');
            Status.quit('open');
            $('#confirm-modal').confirm("close");
            //Status.add("firstTime");
            return false;
        } else {
            Status.quit("firstTime");
        }

        Status.quit('open');
        $('#confirm-modal').confirm("close");

        if (devtools.deniedAccess()) {
            Status.quit('open');
            Status.quit("firstTime");
            Que(function () { devtools.redirectTo('settings/signin') }, 800);
        } else {
            Status.quit('open');
            Status.quit("firstTime");
        }
    };

    var homeTabs = function (mode, tab) {
        //console.log(mode, tab);
    }

    var settings = function () {
        if (!devtools.deniedAccess()) {
            devtools.setModal("settings");
            Status.add("open");
        } else {
            devtools.redirectTo("home");
        }
    };
    var insideSettings = function (modal) {
        if (modal == "signin") {
            devtools.setModal(modal);

            if (devtools.deniedAccess()) {
                //Status.add('open');
                devtools.checkLogin();

            } else {
                Status.quit('open');
                devtools.redirectTo('home');
            }
        } else if (modal == "signout") {
            devtools.setModal(modal);
            if (!devtools.deniedAccess()) {
                Status.add('open');
                Que(function () {
                    Status.quit('open');
                    Que(function () {
                        window.location.reload();
                    }, 800);
                }, 1200);
            } else {
                Status.quit("open");
            }
        } else if (modal == "create") {


            if (menuLoaded) {
                $('#menuName').val('');
                $('#menuId').val('');

                devtools.setModal(modal);
                Status.add('open');

            } else {
                devtools.redirectTo("home");
            }


        } else if (modal == "change-published-state") {

            if (menuLoaded) {
                devtools.setModal(modal);
                loadMenusToChangePublishedState();
                Status.add('open');
            } else {
                devtools.redirectTo("home");
            }

        } else if (modal == "menu") {

            $('#menuNameEdited').val($('.editor > ul.active').attr('data-name'));
            $('#menuIdEdited').val($('.editor > ul.active').attr('data-id'));
            $('#menuTypeEdited').val($('.editor > ul.active').attr('data-type')).trigger('change');
            $('#menuPublishedEdited').val($('.editor > ul.active').attr('data-published')).trigger('change');

            var obj = getMenuObject();
            var menuType = $('.editor > ul.active').attr('data-type');

            $('#menuPublishedEdited').closest('.form-group').show();

            if (multipleInType(obj, menuType)) {
                $('#menuPublishedEdited').closest('.form-group').hide();
            }

            devtools.setModal(modal);
            Status.add('open');

        } else if (modal == "preview") {
            devtools.setModal(modal);
            if (!devtools.deniedAccess()) {
                Status.add('open');

                devtools.inc.previewMenu(view);

            } else {
                Status.quit("open");
            }
        } else if (modal == "save") {


            var obj = getMenuObject();


            var message = 'This action will save your menu settings. ';
            var pubs = "";


            for (var s = 0; s < obj.length; s++) {
                if (obj[s].published) {
                    pubs += '<li>'
                    + (obj[s].published ? '<i class="devicons icon-checkbox-active for-list"></i>' : '<i class="devicons icon-checkbox for-list"></i>')
                    + '<b>' + obj[s].name + '</b> - <span style="color: #999">' + obj[s].type + '</span></li>';
                }
            }

            if (pubs != "") {
                message += '<br>Check what menus will be published:';
            }

            message += '<ul>';

            if (pubs != "") {
                message += pubs;
            }


            message += '</ul>';

            $('#confirm-modal').confirm({
                title: 'Continue?',
                message: message,
                onAccept: function () {
                    deployMenu();
                },
                onCancel: function () {
                    devtools.redirectTo('home');
                },
                cancelButton: 'Cancel',
                acceptButton: 'Save'
            });


        }
    };

    var insideSettingsEdit = function (modal) {
        devtools.setModal("edit");
        if (!devtools.deniedAccess()) {
            Status.add("open");
            var name = $('[rel="name"]').val();
            name = name.toLowerCase() == "item title" ? "" : name;
            $('.itemcurrenttitle').text(name).trigger('change keyup keydown keypress');

            $('.itemcurrenttype').text($('[rel="type"]').text());

            if ($('[rel="type"]').text() == "link") {
                $('.filtertabs a').eq(1).trigger('click');

                $('#curl_title').val($('[rel="name"]').val());
                $('#curl_url').val($('[rel="url"]').val());
            } else {
                $('.filtertabs a').eq(0).trigger('click');

                $('#search').val(name).trigger('focus');


                $('#curl_title').val("");
                $('#curl_url').val("");

            }
        } else {
            Status.quit("open");
            devtools.redirectTo("home");
        }
    }

    var routes = {
        '/home': home,
        '/home/:mode/edit/:tab': homeTabs,
        '/settings': settings,
        '/settings/:modal': insideSettings,
        '/settings/edit/:modal': insideSettingsEdit
    };

    var router = Router(routes);
    router.init();






























    /* From devtools.inc.js */
    /* =Devtools-init */

    // Function:        sortData
    // Description:     Sort array according to sortMode
    // Parameters:      j: array containing the items to be sorted
    //                  sortMode: mode to sort; name or date; ascending or descending
    // Return:          Sorted array
    function sortData(entries, sortMode) {
        if (sortMode === "none")
            return;

        function ascSort(a, b) {
            return Number(a.index) - Number(b.index);
        }
        function descSort(a, b) {
            return Number(b.index) - Number(a.index);
        }

        switch (sortMode) {
            case "ASC":
                entries.sort(ascSort);
                break;
            case "DESC":
                entries.sort(descSort);
                break;
        }

        return entries;
    }


    // Function:        positionateItemEditPopUp
    // Description:     set top position centered in the item is being edited
    // Parameters:      ItemTop: int or string plus units
    // Return:          true
    function positionateItemEditPopUp(ItemTop) {
        /*var $itemEditor = $('.item-editor');
    
        var scrollPos = ItemTop - ( ( $itemEditor.outerHeight() - 80 ) / 2 );
        if( scrollPos < 0 ){
            scrollPos = 60;
        }
    
        $itemEditor.css({top: scrollPos});
    
        $(window).scrollTop(scrollPos);*/

        /* DO nothing because now it will be fixed position */

        return true;
    }

    function findIndex(value, key, arr) {
        var index = -1, i, found = false;
        for (i = 0; i < arr.length; i++) {
            if (arr[i][key] == value && !found) {
                index = i;
                found = true;
                //i = arr.length;
            }
        }

        return index;
    }

    function log() {
        if (arguments.length > 0) {
            console.log(arguments);
        }
    }

    function setArrayValue(parent, children, objectData) {

        //console.log('setArrarValue', objectData);
        //console.log('setArrarValue', menu[activeMenu].data);

        if (objectData.length > parent) {
            if (typeof objectData[parent].subtree == "undefined") {
                objectData[parent].subtree = [];
            }

            //add it as children
            objectData[parent].subtree.push(objectData[children]);

            //remove it from first level
            objectData.splice(children, 1);

            log('adding children ', children, 'to parent', parent);
        }

        menu[activeMenu].data = objectData;
        return objectData;
    }






    function parseMenuTree(data) {
        var i,
            read = [],
            parse = [],
            activeMenuEditing = true;

        if (arguments.length > 2 && arguments[2]) {
            read = data;
            activeMenuEditing = false;
        } else {
            read = data[activeMenu].data;
        }

        read = sortData(read, "ASC");


        for (i = 0; i < read.length; i++) {
            //only fathers
            (function (i) {
                if (read[i].parent_id.length == 0 && read[i].parent_id.length < 5 || read[i].parent_id == " ") {
                    parse.push(read[i]);
                }
            })(i);
        }


        for (i = 0; i < read.length; i++) {
            //only fathers
            (function (i) {
                if (read[i].parent_id.length > 0 && read[i].parent_id.length > 5 && read[i].parent_id != " ") {
                    for (var x = 0; x < parse.length; x++) {
                        (function (i, x) {
                            if (parse[x].id == read[i].parent_id) {
                                if (typeof parse[x].subtree == "undefined") {
                                    parse[x].subtree = [];
                                }

                                parse[x].subtree.push(read[i]);
                            }
                        })(i, x);
                    }
                }
            })(i);
        }

        if (activeMenuEditing) {
            menu[activeMenu].data = parse;
        }

        if (arguments.length > 1) {
            arguments[1]();
        }

        return parse;
    }







    function getMenuObject(published) {

        var menuTree = [],
            $menus = $('.editor > ul');

        for (var i = 0; i < $menus.length; i++) {
            (function (i) {
                var m = {
                    name: $menus.eq(i).attr('data-name'),
                    id: $menus.eq(i).attr('data-id'),
                    type: $menus.eq(i).attr('data-type'),
                    published: $menus.eq(i).attr('data-published') == "true",
                    data: devtools.inc.parseTree($('> li > div > .item-title', $menus.eq(i)))
                };

                /* if(m.data.length > 0 ){
                     menuTree.push( m );
                 }*/

                menuTree.push(m);

            })(i);
        }

        //menuTree = {"Content": menuTree};

        return menuTree;
    }





    function deployMenu(published) {
        var published = published != undefined ? published : false;

        var menuTree = [],
            $menus = $('.editor > ul');

        for (var i = 0; i < $menus.length; i++) {
            (function (i) {
                var m = {
                    name: $menus.eq(i).attr('data-name'),
                    id: $menus.eq(i).attr('data-id'),
                    type: $menus.eq(i).attr('data-type'),
                    published: $menus.eq(i).attr('data-published') == "true",
                    data: devtools.inc.parseTree($('> li > div > .item-title', $menus.eq(i)))
                };

                menuTree.push(m);
            })(i);
        }

        log(menuTree);

        Status.quit("allRemoved");

        menuTree = { "Content": menuTree };

        $.ajax({
            url: path.WebServicesPath + 'MenuBuilder.ashx?type=update&SiteID=' + SiteID,
            type: "POST",
            data: { "Content": JSON.stringify(menuTree), "SiteID": SiteID, "s": currentSite },
            success: function (data) {

                if (colas["save"] != undefined) { clearTimeout(colas["save"]); }

                if (typeof data != "object" || data.error) {
                    colas["save"] = setTimeout(function () {
                        Status.quit("stored");

                        Status.notify('An error occurred while trying to update the menu.');

                    }, 500);
                } else {
                    colas["save"] = setTimeout(function () {
                        Status.quit("stored");

                        Status.notify('Menu saved successfully');

                        devtools.redirectTo('home');
                    }, 500);
                }

                Status.quit("nostored"); Status.add("stored");
                Status.quit("editing");
            }
        });

    }




    //update on every event
    function updateMenuOnTheFly() {
        Status.add('nostored');


        var m = devtools.inc.parseTree($('.editor > ul.active > li > div > .item-title')),
            id = $('.editor > ul.active').attr('data-id');
        m = parseMenuTree(m, function () { }, true);
        m = m;

        for (var i = 0; i < menu.length; i++) {
            if (menu[i].id == id) {
                //console.log(m);
                menu[i].data = m;
                //console.log('sort update found', menu[i].data);
            }
        }
    }





    Status.add('loading signedout just-started-loading');
    $(document).on('ready', function () {
        if (menuIsSupported) {
            devtools.visitor();
        } else {
            Status.add('not-supported');
        }
    }).ajaxStart(function () {
        Status.add("loading");
        Pace.restart();
    }).ajaxComplete(function () {
        Status.quit("loading");
    });







    /*! tether 1.1.0 */
    !function (t, e) { "function" == typeof define && define.amd ? define(e) : "object" == typeof exports ? module.exports = e(require, exports, module) : t.Tether = e() }(this, function (t, e, o) { "use strict"; function i(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") } function n(t) { var e = getComputedStyle(t), o = e.position; if ("fixed" === o) return t; for (var i = t; i = i.parentNode;) { var n = void 0; try { n = getComputedStyle(i) } catch (r) { } if ("undefined" == typeof n || null === n) return i; var s = n, a = s.overflow, f = s.overflowX, h = s.overflowY; if (/(auto|scroll)/.test(a + h + f) && ("absolute" !== o || ["relative", "absolute", "fixed"].indexOf(n.position) >= 0)) return i } return document.body } function r(t) { var e = void 0; t === document ? (e = document, t = document.documentElement) : e = t.ownerDocument; var o = e.documentElement, i = {}, n = t.getBoundingClientRect(); for (var r in n) i[r] = n[r]; var s = x(e); return i.top -= s.top, i.left -= s.left, "undefined" == typeof i.width && (i.width = document.body.scrollWidth - i.left - i.right), "undefined" == typeof i.height && (i.height = document.body.scrollHeight - i.top - i.bottom), i.top = i.top - o.clientTop, i.left = i.left - o.clientLeft, i.right = e.body.clientWidth - i.width - i.left, i.bottom = e.body.clientHeight - i.height - i.top, i } function s(t) { return t.offsetParent || document.documentElement } function a() { var t = document.createElement("div"); t.style.width = "100%", t.style.height = "200px"; var e = document.createElement("div"); f(e.style, { position: "absolute", top: 0, left: 0, pointerEvents: "none", visibility: "hidden", width: "200px", height: "150px", overflow: "hidden" }), e.appendChild(t), document.body.appendChild(e); var o = t.offsetWidth; e.style.overflow = "scroll"; var i = t.offsetWidth; o === i && (i = e.clientWidth), document.body.removeChild(e); var n = o - i; return { width: n, height: n } } function f() { var t = arguments.length <= 0 || void 0 === arguments[0] ? {} : arguments[0], e = []; return Array.prototype.push.apply(e, arguments), e.slice(1).forEach(function (e) { if (e) for (var o in e) ({}).hasOwnProperty.call(e, o) && (t[o] = e[o]) }), t } function h(t, e) { if ("undefined" != typeof t.classList) e.split(" ").forEach(function (e) { e.trim() && t.classList.remove(e) }); else { var o = new RegExp("(^| )" + e.split(" ").join("|") + "( |$)", "gi"), i = u(t).replace(o, " "); p(t, i) } } function l(t, e) { if ("undefined" != typeof t.classList) e.split(" ").forEach(function (e) { e.trim() && t.classList.add(e) }); else { h(t, e); var o = u(t) + (" " + e); p(t, o) } } function d(t, e) { if ("undefined" != typeof t.classList) return t.classList.contains(e); var o = u(t); return new RegExp("(^| )" + e + "( |$)", "gi").test(o) } function u(t) { return t.className instanceof SVGAnimatedString ? t.className.baseVal : t.className } function p(t, e) { t.setAttribute("class", e) } function c(t, e, o) { o.forEach(function (o) { -1 === e.indexOf(o) && d(t, o) && h(t, o) }), e.forEach(function (e) { d(t, e) || l(t, e) }) } function i(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") } function g(t, e) { var o = arguments.length <= 2 || void 0 === arguments[2] ? 1 : arguments[2]; return t + o >= e && e >= t - o } function m() { return "undefined" != typeof performance && "undefined" != typeof performance.now ? performance.now() : +new Date } function v() { for (var t = { top: 0, left: 0 }, e = arguments.length, o = Array(e), i = 0; e > i; i++) o[i] = arguments[i]; return o.forEach(function (e) { var o = e.top, i = e.left; "string" == typeof o && (o = parseFloat(o, 10)), "string" == typeof i && (i = parseFloat(i, 10)), t.top += o, t.left += i }), t } function y(t, e) { return "string" == typeof t.left && -1 !== t.left.indexOf("%") && (t.left = parseFloat(t.left, 10) / 100 * e.width), "string" == typeof t.top && -1 !== t.top.indexOf("%") && (t.top = parseFloat(t.top, 10) / 100 * e.height), t } function b(t, e) { return "scrollParent" === e ? e = t.scrollParent : "window" === e && (e = [pageXOffset, pageYOffset, innerWidth + pageXOffset, innerHeight + pageYOffset]), e === document && (e = e.documentElement), "undefined" != typeof e.nodeType && !function () { var t = r(e), o = t, i = getComputedStyle(e); e = [o.left, o.top, t.width + o.left, t.height + o.top], U.forEach(function (t, o) { t = t[0].toUpperCase() + t.substr(1), "Top" === t || "Left" === t ? e[o] += parseFloat(i["border" + t + "Width"]) : e[o] -= parseFloat(i["border" + t + "Width"]) }) }(), e } var w = function () { function t(t, e) { for (var o = 0; o < e.length; o++) { var i = e[o]; i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(t, i.key, i) } } return function (e, o, i) { return o && t(e.prototype, o), i && t(e, i), e } }(), C = void 0; "undefined" == typeof C && (C = { modules: [] }); var O = function () { var t = 0; return function () { return ++t } }(), E = {}, x = function (t) { var e = t._tetherZeroElement; "undefined" == typeof e && (e = t.createElement("div"), e.setAttribute("data-tether-id", O()), f(e.style, { top: 0, left: 0, position: "absolute" }), t.body.appendChild(e), t._tetherZeroElement = e); var o = e.getAttribute("data-tether-id"); if ("undefined" == typeof E[o]) { E[o] = {}; var i = e.getBoundingClientRect(); for (var n in i) E[o][n] = i[n]; T(function () { delete E[o] }) } return E[o] }, A = [], T = function (t) { A.push(t) }, S = function () { for (var t = void 0; t = A.pop() ;) t() }, W = function () { function t() { i(this, t) } return w(t, [{ key: "on", value: function (t, e, o) { var i = arguments.length <= 3 || void 0 === arguments[3] ? !1 : arguments[3]; "undefined" == typeof this.bindings && (this.bindings = {}), "undefined" == typeof this.bindings[t] && (this.bindings[t] = []), this.bindings[t].push({ handler: e, ctx: o, once: i }) } }, { key: "once", value: function (t, e, o) { this.on(t, e, o, !0) } }, { key: "off", value: function (t, e) { if ("undefined" == typeof this.bindings || "undefined" == typeof this.bindings[t]) if ("undefined" == typeof e) delete this.bindings[t]; else for (var o = 0; o < this.bindings[t].length;) this.bindings[t][o].handler === e ? this.bindings[t].splice(o, 1) : ++o } }, { key: "trigger", value: function (t) { if ("undefined" != typeof this.bindings && this.bindings[t]) { for (var e = 0, o = arguments.length, i = Array(o > 1 ? o - 1 : 0), n = 1; o > n; n++) i[n - 1] = arguments[n]; for (; e < this.bindings[t].length;) { var r = this.bindings[t][e], s = r.handler, a = r.ctx, f = r.once, h = a; "undefined" == typeof h && (h = this), s.apply(h, i), f ? this.bindings[t].splice(e, 1) : ++e } } } }]), t }(); C.Utils = { getScrollParent: n, getBounds: r, getOffsetParent: s, extend: f, addClass: l, removeClass: h, hasClass: d, updateClasses: c, defer: T, flush: S, uniqueId: O, Evented: W, getScrollBarSize: a }; var M = function () { function t(t, e) { var o = [], i = !0, n = !1, r = void 0; try { for (var s, a = t[Symbol.iterator]() ; !(i = (s = a.next()).done) && (o.push(s.value), !e || o.length !== e) ; i = !0); } catch (f) { n = !0, r = f } finally { try { !i && a["return"] && a["return"]() } finally { if (n) throw r } } return o } return function (e, o) { if (Array.isArray(e)) return e; if (Symbol.iterator in Object(e)) return t(e, o); throw new TypeError("Invalid attempt to destructure non-iterable instance") } }(), w = function () { function t(t, e) { for (var o = 0; o < e.length; o++) { var i = e[o]; i.enumerable = i.enumerable || !1, i.configurable = !0, "value" in i && (i.writable = !0), Object.defineProperty(t, i.key, i) } } return function (e, o, i) { return o && t(e.prototype, o), i && t(e, i), e } }(); if ("undefined" == typeof C) throw new Error("You must include the utils.js file before tether.js"); var P = C.Utils, n = P.getScrollParent, r = P.getBounds, s = P.getOffsetParent, f = P.extend, l = P.addClass, h = P.removeClass, c = P.updateClasses, T = P.defer, S = P.flush, a = P.getScrollBarSize, k = function () { if ("undefined" == typeof document) return ""; for (var t = document.createElement("div"), e = ["transform", "webkitTransform", "OTransform", "MozTransform", "msTransform"], o = 0; o < e.length; ++o) { var i = e[o]; if (void 0 !== t.style[i]) return i } }(), B = [], _ = function () { B.forEach(function (t) { t.position(!1) }), S() }; !function () { var t = null, e = null, o = null, i = function n() { return "undefined" != typeof e && e > 16 ? (e = Math.min(e - 16, 250), void (o = setTimeout(n, 250))) : void ("undefined" != typeof t && m() - t < 10 || ("undefined" != typeof o && (clearTimeout(o), o = null), t = m(), _(), e = m() - t)) }; "undefined" != typeof window && ["resize", "scroll", "touchmove"].forEach(function (t) { window.addEventListener(t, i) }) }(); var z = { center: "center", left: "right", right: "left" }, F = { middle: "middle", top: "bottom", bottom: "top" }, L = { top: 0, left: 0, middle: "50%", center: "50%", bottom: "100%", right: "100%" }, Y = function (t, e) { var o = t.left, i = t.top; return "auto" === o && (o = z[e.left]), "auto" === i && (i = F[e.top]), { left: o, top: i } }, H = function (t) { var e = t.left, o = t.top; return "undefined" != typeof L[t.left] && (e = L[t.left]), "undefined" != typeof L[t.top] && (o = L[t.top]), { left: e, top: o } }, X = function (t) { var e = t.split(" "), o = M(e, 2), i = o[0], n = o[1]; return { top: i, left: n } }, j = X, N = function () { function t(e) { var o = this; i(this, t), this.position = this.position.bind(this), B.push(this), this.history = [], this.setOptions(e, !1), C.modules.forEach(function (t) { "undefined" != typeof t.initialize && t.initialize.call(o) }), this.position() } return w(t, [{ key: "getClass", value: function () { var t = arguments.length <= 0 || void 0 === arguments[0] ? "" : arguments[0], e = this.options.classes; return "undefined" != typeof e && e[t] ? this.options.classes[t] : this.options.classPrefix ? this.options.classPrefix + "-" + t : t } }, { key: "setOptions", value: function (t) { var e = this, o = arguments.length <= 1 || void 0 === arguments[1] ? !0 : arguments[1], i = { offset: "0 0", targetOffset: "0 0", targetAttachment: "auto auto", classPrefix: "tether" }; this.options = f(i, t); var r = this.options, s = r.element, a = r.target, h = r.targetModifier; if (this.element = s, this.target = a, this.targetModifier = h, "viewport" === this.target ? (this.target = document.body, this.targetModifier = "visible") : "scroll-handle" === this.target && (this.target = document.body, this.targetModifier = "scroll-handle"), ["element", "target"].forEach(function (t) { if ("undefined" == typeof e[t]) throw new Error("Tether Error: Both element and target must be defined"); "undefined" != typeof e[t].jquery ? e[t] = e[t][0] : "string" == typeof e[t] && (e[t] = document.querySelector(e[t])) }), l(this.element, this.getClass("element")), this.options.addTargetClasses !== !1 && l(this.target, this.getClass("target")), !this.options.attachment) throw new Error("Tether Error: You must provide an attachment"); this.targetAttachment = j(this.options.targetAttachment), this.attachment = j(this.options.attachment), this.offset = X(this.options.offset), this.targetOffset = X(this.options.targetOffset), "undefined" != typeof this.scrollParent && this.disable(), "scroll-handle" === this.targetModifier ? this.scrollParent = this.target : this.scrollParent = n(this.target), this.options.enabled !== !1 && this.enable(o) } }, { key: "getTargetBounds", value: function () { if ("undefined" == typeof this.targetModifier) return r(this.target); if ("visible" === this.targetModifier) { if (this.target === document.body) return { top: pageYOffset, left: pageXOffset, height: innerHeight, width: innerWidth }; var t = r(this.target), e = { height: t.height, width: t.width, top: t.top, left: t.left }; return e.height = Math.min(e.height, t.height - (pageYOffset - t.top)), e.height = Math.min(e.height, t.height - (t.top + t.height - (pageYOffset + innerHeight))), e.height = Math.min(innerHeight, e.height), e.height -= 2, e.width = Math.min(e.width, t.width - (pageXOffset - t.left)), e.width = Math.min(e.width, t.width - (t.left + t.width - (pageXOffset + innerWidth))), e.width = Math.min(innerWidth, e.width), e.width -= 2, e.top < pageYOffset && (e.top = pageYOffset), e.left < pageXOffset && (e.left = pageXOffset), e } if ("scroll-handle" === this.targetModifier) { var t = void 0, o = this.target; o === document.body ? (o = document.documentElement, t = { left: pageXOffset, top: pageYOffset, height: innerHeight, width: innerWidth }) : t = r(o); var i = getComputedStyle(o), n = o.scrollWidth > o.clientWidth || [i.overflow, i.overflowX].indexOf("scroll") >= 0 || this.target !== document.body, s = 0; n && (s = 15); var a = t.height - parseFloat(i.borderTopWidth) - parseFloat(i.borderBottomWidth) - s, e = { width: 15, height: .975 * a * (a / o.scrollHeight), left: t.left + t.width - parseFloat(i.borderLeftWidth) - 15 }, f = 0; 408 > a && this.target === document.body && (f = -11e-5 * Math.pow(a, 2) - .00727 * a + 22.58), this.target !== document.body && (e.height = Math.max(e.height, 24)); var h = this.target.scrollTop / (o.scrollHeight - a); return e.top = h * (a - e.height - f) + t.top + parseFloat(i.borderTopWidth), this.target === document.body && (e.height = Math.max(e.height, 24)), e } } }, { key: "clearCache", value: function () { this._cache = {} } }, { key: "cache", value: function (t, e) { return "undefined" == typeof this._cache && (this._cache = {}), "undefined" == typeof this._cache[t] && (this._cache[t] = e.call(this)), this._cache[t] } }, { key: "enable", value: function () { var t = arguments.length <= 0 || void 0 === arguments[0] ? !0 : arguments[0]; this.options.addTargetClasses !== !1 && l(this.target, this.getClass("enabled")), l(this.element, this.getClass("enabled")), this.enabled = !0, this.scrollParent !== document && this.scrollParent.addEventListener("scroll", this.position), t && this.position() } }, { key: "disable", value: function () { h(this.target, this.getClass("enabled")), h(this.element, this.getClass("enabled")), this.enabled = !1, "undefined" != typeof this.scrollParent && this.scrollParent.removeEventListener("scroll", this.position) } }, { key: "destroy", value: function () { var t = this; this.disable(), B.forEach(function (e, o) { return e === t ? void B.splice(o, 1) : void 0 }) } }, { key: "updateAttachClasses", value: function (t, e) { var o = this; t = t || this.attachment, e = e || this.targetAttachment; var i = ["left", "top", "bottom", "right", "middle", "center"]; "undefined" != typeof this._addAttachClasses && this._addAttachClasses.length && this._addAttachClasses.splice(0, this._addAttachClasses.length), "undefined" == typeof this._addAttachClasses && (this._addAttachClasses = []); var n = this._addAttachClasses; t.top && n.push(this.getClass("element-attached") + "-" + t.top), t.left && n.push(this.getClass("element-attached") + "-" + t.left), e.top && n.push(this.getClass("target-attached") + "-" + e.top), e.left && n.push(this.getClass("target-attached") + "-" + e.left); var r = []; i.forEach(function (t) { r.push(o.getClass("element-attached") + "-" + t), r.push(o.getClass("target-attached") + "-" + t) }), T(function () { "undefined" != typeof o._addAttachClasses && (c(o.element, o._addAttachClasses, r), o.options.addTargetClasses !== !1 && c(o.target, o._addAttachClasses, r), delete o._addAttachClasses) }) } }, { key: "position", value: function () { var t = this, e = arguments.length <= 0 || void 0 === arguments[0] ? !0 : arguments[0]; if (this.enabled) { this.clearCache(); var o = Y(this.targetAttachment, this.attachment); this.updateAttachClasses(this.attachment, o); var i = this.cache("element-bounds", function () { return r(t.element) }), n = i.width, f = i.height; if (0 === n && 0 === f && "undefined" != typeof this.lastSize) { var h = this.lastSize; n = h.width, f = h.height } else this.lastSize = { width: n, height: f }; var l = this.cache("target-bounds", function () { return t.getTargetBounds() }), d = l, u = y(H(this.attachment), { width: n, height: f }), p = y(H(o), d), c = y(this.offset, { width: n, height: f }), g = y(this.targetOffset, d); u = v(u, c), p = v(p, g); for (var m = l.left + p.left - u.left, b = l.top + p.top - u.top, w = 0; w < C.modules.length; ++w) { var O = C.modules[w], E = O.position.call(this, { left: m, top: b, targetAttachment: o, targetPos: l, elementPos: i, offset: u, targetOffset: p, manualOffset: c, manualTargetOffset: g, scrollbarSize: A, attachment: this.attachment }); if (E === !1) return !1; "undefined" != typeof E && "object" == typeof E && (b = E.top, m = E.left) } var x = { page: { top: b, left: m }, viewport: { top: b - pageYOffset, bottom: pageYOffset - b - f + innerHeight, left: m - pageXOffset, right: pageXOffset - m - n + innerWidth } }, A = void 0; return document.body.scrollWidth > window.innerWidth && (A = this.cache("scrollbar-size", a), x.viewport.bottom -= A.height), document.body.scrollHeight > window.innerHeight && (A = this.cache("scrollbar-size", a), x.viewport.right -= A.width), (-1 === ["", "static"].indexOf(document.body.style.position) || -1 === ["", "static"].indexOf(document.body.parentElement.style.position)) && (x.page.bottom = document.body.scrollHeight - b - f, x.page.right = document.body.scrollWidth - m - n), "undefined" != typeof this.options.optimizations && this.options.optimizations.moveElement !== !1 && "undefined" == typeof this.targetModifier && !function () { var e = t.cache("target-offsetparent", function () { return s(t.target) }), o = t.cache("target-offsetparent-bounds", function () { return r(e) }), i = getComputedStyle(e), n = o, a = {}; if (["Top", "Left", "Bottom", "Right"].forEach(function (t) { a[t.toLowerCase()] = parseFloat(i["border" + t + "Width"]) }), o.right = document.body.scrollWidth - o.left - n.width + a.right, o.bottom = document.body.scrollHeight - o.top - n.height + a.bottom, x.page.top >= o.top + a.top && x.page.bottom >= o.bottom && x.page.left >= o.left + a.left && x.page.right >= o.right) { var f = e.scrollTop, h = e.scrollLeft; x.offset = { top: x.page.top - o.top + f - a.top, left: x.page.left - o.left + h - a.left } } }(), this.move(x), this.history.unshift(x), this.history.length > 3 && this.history.pop(), e && S(), !0 } } }, { key: "move", value: function (t) { var e = this; if ("undefined" != typeof this.element.parentNode) { var o = {}; for (var i in t) { o[i] = {}; for (var n in t[i]) { for (var r = !1, a = 0; a < this.history.length; ++a) { var h = this.history[a]; if ("undefined" != typeof h[i] && !g(h[i][n], t[i][n])) { r = !0; break } } r || (o[i][n] = !0) } } var l = { top: "", left: "", right: "", bottom: "" }, d = function (t, o) { var i = "undefined" != typeof e.options.optimizations, n = i ? e.options.optimizations.gpu : null; if (n !== !1) { var r = void 0, s = void 0; t.top ? (l.top = 0, r = o.top) : (l.bottom = 0, r = -o.bottom), t.left ? (l.left = 0, s = o.left) : (l.right = 0, s = -o.right), l[k] = "translateX(" + Math.round(s) + "px) translateY(" + Math.round(r) + "px)", "msTransform" !== k && (l[k] += " translateZ(0)") } else t.top ? l.top = o.top + "px" : l.bottom = o.bottom + "px", t.left ? l.left = o.left + "px" : l.right = o.right + "px" }, u = !1; if ((o.page.top || o.page.bottom) && (o.page.left || o.page.right) ? (l.position = "absolute", d(o.page, t.page)) : (o.viewport.top || o.viewport.bottom) && (o.viewport.left || o.viewport.right) ? (l.position = "fixed", d(o.viewport, t.viewport)) : "undefined" != typeof o.offset && o.offset.top && o.offset.left ? !function () { l.position = "absolute"; var i = e.cache("target-offsetparent", function () { return s(e.target) }); s(e.element) !== i && T(function () { e.element.parentNode.removeChild(e.element), i.appendChild(e.element) }), d(o.offset, t.offset), u = !0 }() : (l.position = "absolute", d({ top: !0, left: !0 }, t.page)), !u) { for (var p = !0, c = this.element.parentNode; c && "BODY" !== c.tagName;) { if ("static" !== getComputedStyle(c).position) { p = !1; break } c = c.parentNode } p || (this.element.parentNode.removeChild(this.element), document.body.appendChild(this.element)) } var m = {}, v = !1; for (var n in l) { var y = l[n], b = this.element.style[n]; "" !== b && "" !== y && ["top", "left", "bottom", "right"].indexOf(n) >= 0 && (b = parseFloat(b), y = parseFloat(y)), b !== y && (v = !0, m[n] = y) } v && T(function () { f(e.element.style, m) }) } } }]), t }(); N.modules = [], C.position = _; var R = f(N, C), M = function () { function t(t, e) { var o = [], i = !0, n = !1, r = void 0; try { for (var s, a = t[Symbol.iterator]() ; !(i = (s = a.next()).done) && (o.push(s.value), !e || o.length !== e) ; i = !0); } catch (f) { n = !0, r = f } finally { try { !i && a["return"] && a["return"]() } finally { if (n) throw r } } return o } return function (e, o) { if (Array.isArray(e)) return e; if (Symbol.iterator in Object(e)) return t(e, o); throw new TypeError("Invalid attempt to destructure non-iterable instance") } }(), P = C.Utils, r = P.getBounds, f = P.extend, c = P.updateClasses, T = P.defer, U = ["left", "top", "right", "bottom"]; C.modules.push({ position: function (t) { var e = this, o = t.top, i = t.left, n = t.targetAttachment; if (!this.options.constraints) return !0; var s = this.cache("element-bounds", function () { return r(e.element) }), a = s.height, h = s.width; if (0 === h && 0 === a && "undefined" != typeof this.lastSize) { var l = this.lastSize; h = l.width, a = l.height } var d = this.cache("target-bounds", function () { return e.getTargetBounds() }), u = d.height, p = d.width, g = [this.getClass("pinned"), this.getClass("out-of-bounds")]; this.options.constraints.forEach(function (t) { var e = t.outOfBoundsClass, o = t.pinnedClass; e && g.push(e), o && g.push(o) }), g.forEach(function (t) { ["left", "top", "right", "bottom"].forEach(function (e) { g.push(t + "-" + e) }) }); var m = [], v = f({}, n), y = f({}, this.attachment); return this.options.constraints.forEach(function (t) { var r = t.to, s = t.attachment, f = t.pin; "undefined" == typeof s && (s = ""); var l = void 0, d = void 0; if (s.indexOf(" ") >= 0) { var c = s.split(" "), g = M(c, 2); d = g[0], l = g[1] } else l = d = s; var w = b(e, r); ("target" === d || "both" === d) && (o < w[1] && "top" === v.top && (o += u, v.top = "bottom"), o + a > w[3] && "bottom" === v.top && (o -= u, v.top = "top")), "together" === d && (o < w[1] && "top" === v.top && ("bottom" === y.top ? (o += u, v.top = "bottom", o += a, y.top = "top") : "top" === y.top && (o += u, v.top = "bottom", o -= a, y.top = "bottom")), o + a > w[3] && "bottom" === v.top && ("top" === y.top ? (o -= u, v.top = "top", o -= a, y.top = "bottom") : "bottom" === y.top && (o -= u, v.top = "top", o += a, y.top = "top")), "middle" === v.top && (o + a > w[3] && "top" === y.top ? (o -= a, y.top = "bottom") : o < w[1] && "bottom" === y.top && (o += a, y.top = "top"))), ("target" === l || "both" === l) && (i < w[0] && "left" === v.left && (i += p, v.left = "right"), i + h > w[2] && "right" === v.left && (i -= p, v.left = "left")), "together" === l && (i < w[0] && "left" === v.left ? "right" === y.left ? (i += p, v.left = "right", i += h, y.left = "left") : "left" === y.left && (i += p, v.left = "right", i -= h, y.left = "right") : i + h > w[2] && "right" === v.left ? "left" === y.left ? (i -= p, v.left = "left", i -= h, y.left = "right") : "right" === y.left && (i -= p, v.left = "left", i += h, y.left = "left") : "center" === v.left && (i + h > w[2] && "left" === y.left ? (i -= h, y.left = "right") : i < w[0] && "right" === y.left && (i += h, y.left = "left"))), ("element" === d || "both" === d) && (o < w[1] && "bottom" === y.top && (o += a, y.top = "top"), o + a > w[3] && "top" === y.top && (o -= a, y.top = "bottom")), ("element" === l || "both" === l) && (i < w[0] && "right" === y.left && (i += h, y.left = "left"), i + h > w[2] && "left" === y.left && (i -= h, y.left = "right")), "string" == typeof f ? f = f.split(",").map(function (t) { return t.trim() }) : f === !0 && (f = ["top", "left", "right", "bottom"]), f = f || []; var C = [], O = []; o < w[1] && (f.indexOf("top") >= 0 ? (o = w[1], C.push("top")) : O.push("top")), o + a > w[3] && (f.indexOf("bottom") >= 0 ? (o = w[3] - a, C.push("bottom")) : O.push("bottom")), i < w[0] && (f.indexOf("left") >= 0 ? (i = w[0], C.push("left")) : O.push("left")), i + h > w[2] && (f.indexOf("right") >= 0 ? (i = w[2] - h, C.push("right")) : O.push("right")), C.length && !function () { var t = void 0; t = "undefined" != typeof e.options.pinnedClass ? e.options.pinnedClass : e.getClass("pinned"), m.push(t), C.forEach(function (e) { m.push(t + "-" + e) }) }(), O.length && !function () { var t = void 0; t = "undefined" != typeof e.options.outOfBoundsClass ? e.options.outOfBoundsClass : e.getClass("out-of-bounds"), m.push(t), O.forEach(function (e) { m.push(t + "-" + e) }) }(), (C.indexOf("left") >= 0 || C.indexOf("right") >= 0) && (y.left = v.left = !1), (C.indexOf("top") >= 0 || C.indexOf("bottom") >= 0) && (y.top = v.top = !1), (v.top !== n.top || v.left !== n.left || y.top !== e.attachment.top || y.left !== e.attachment.left) && e.updateAttachClasses(y, v) }), T(function () { e.options.addTargetClasses !== !1 && c(e.target, m, g), c(e.element, m, g) }), { top: o, left: i } } }); var P = C.Utils, r = P.getBounds, c = P.updateClasses, T = P.defer; C.modules.push({ position: function (t) { var e = this, o = t.top, i = t.left, n = this.cache("element-bounds", function () { return r(e.element) }), s = n.height, a = n.width, f = this.getTargetBounds(), h = o + s, l = i + a, d = []; o <= f.bottom && h >= f.top && ["left", "right"].forEach(function (t) { var e = f[t]; (e === i || e === l) && d.push(t) }), i <= f.right && l >= f.left && ["top", "bottom"].forEach(function (t) { var e = f[t]; (e === o || e === h) && d.push(t) }); var u = [], p = [], g = ["left", "top", "right", "bottom"]; return u.push(this.getClass("abutted")), g.forEach(function (t) { u.push(e.getClass("abutted") + "-" + t) }), d.length && p.push(this.getClass("abutted")), d.forEach(function (t) { p.push(e.getClass("abutted") + "-" + t) }), T(function () { e.options.addTargetClasses !== !1 && c(e.target, p, u), c(e.element, p, u) }), !0 } }); var M = function () { function t(t, e) { var o = [], i = !0, n = !1, r = void 0; try { for (var s, a = t[Symbol.iterator]() ; !(i = (s = a.next()).done) && (o.push(s.value), !e || o.length !== e) ; i = !0); } catch (f) { n = !0, r = f } finally { try { !i && a["return"] && a["return"]() } finally { if (n) throw r } } return o } return function (e, o) { if (Array.isArray(e)) return e; if (Symbol.iterator in Object(e)) return t(e, o); throw new TypeError("Invalid attempt to destructure non-iterable instance") } }(); return C.modules.push({ position: function (t) { var e = t.top, o = t.left; if (this.options.shift) { var i = this.options.shift; "function" == typeof this.options.shift && (i = this.options.shift.call(this, { top: e, left: o })); var n = void 0, r = void 0; if ("string" == typeof i) { i = i.split(" "), i[1] = i[1] || i[0]; var s = i, a = M(s, 2); n = a[0], r = a[1], n = parseFloat(n, 10), r = parseFloat(r, 10) } else n = i.top, r = i.left; return e += n, o += r, { top: e, left: o } } } }), R });

    /*! tether-drop 1.2.2 */
    !function (t, e) { "function" == typeof define && define.amd ? define(["tether"], e) : "object" == typeof exports ? module.exports = e(require("tether")) : t.Drop = e(t.Tether) }(this, function (t) { "use strict"; function e(t, e) { if (!(t instanceof e)) throw new TypeError("Cannot call a class as a function") } function n(t, e) { if ("function" != typeof e && null !== e) throw new TypeError("Super expression must either be null or a function, not " + typeof e); t.prototype = Object.create(e && e.prototype, { constructor: { value: t, enumerable: !1, writable: !0, configurable: !0 } }), e && (t.__proto__ = e) } function o(t) { var e = t.split(" "), n = a(e, 2), o = n[0], i = n[1]; if (["left", "right"].indexOf(o) >= 0) { var s = [i, o]; o = s[0], i = s[1] } return [o, i].join(" ") } function i(t, e) { for (var n = void 0, o = []; -1 !== (n = t.indexOf(e)) ;) o.push(t.splice(n, 1)); return o } function s() { var a = void 0 === arguments[0] ? {} : arguments[0], u = function () { for (var t = arguments.length, e = Array(t), n = 0; t > n; n++) e[n] = arguments[n]; return new (r.apply(b, [null].concat(e))) }; h(u, { createContext: s, drops: [], defaults: {} }); var m = { classPrefix: "drop", defaults: { position: "bottom left", openOn: "click", beforeClose: null, constrainToScrollParent: !0, constrainToWindow: !0, classes: "", remove: !1, tetherOptions: {} } }; h(u, m, a), h(u.defaults, m.defaults, a.defaults), "undefined" == typeof x[u.classPrefix] && (x[u.classPrefix] = []), u.updateBodyClasses = function () { for (var t = !1, e = x[u.classPrefix], n = e.length, o = 0; n > o; ++o) if (e[o].isOpened()) { t = !0; break } t ? p(document.body, u.classPrefix + "-open") : c(document.body, u.classPrefix + "-open") }; var b = function (s) { function r(t) { if (e(this, r), d(Object.getPrototypeOf(r.prototype), "constructor", this).call(this), this.options = h({}, u.defaults, t), this.target = this.options.target, "undefined" == typeof this.target) throw new Error("Drop Error: You must provide a target."); this.options.classes && this.options.addTargetClasses !== !1 && p(this.target, this.options.classes), u.drops.push(this), x[u.classPrefix].push(this), this._boundEvents = [], this.bindMethods(), this.setupElements(), this.setupEvents(), this.setupTether() } return n(r, s), l(r, [{ key: "_on", value: function (t, e, n) { this._boundEvents.push({ element: t, event: e, handler: n }), t.addEventListener(e, n) } }, { key: "bindMethods", value: function () { this.transitionEndHandler = this._transitionEndHandler.bind(this) } }, { key: "setupElements", value: function () { var t = this; if (this.drop = document.createElement("div"), p(this.drop, u.classPrefix), this.options.classes && p(this.drop, this.options.classes), this.content = document.createElement("div"), p(this.content, u.classPrefix + "-content"), "function" == typeof this.options.content) { var e = function () { var e = t.options.content.call(t, t); if ("string" == typeof e) t.content.innerHTML = e; else { if ("object" != typeof e) throw new Error("Drop Error: Content function should return a string or HTMLElement."); t.content.innerHTML = "", t.content.appendChild(e) } }; e(), this.on("open", e.bind(this)) } else "object" == typeof this.options.content ? this.content.appendChild(this.options.content) : this.content.innerHTML = this.options.content; this.drop.appendChild(this.content) } }, { key: "setupTether", value: function () { var e = this.options.position.split(" "); e[0] = O[e[0]], e = e.join(" "); var n = []; n.push(this.options.constrainToScrollParent ? { to: "scrollParent", pin: "top, bottom", attachment: "together none" } : { to: "scrollParent" }), n.push(this.options.constrainToWindow !== !1 ? { to: "window", attachment: "together" } : { to: "window" }); var i = { element: this.drop, target: this.target, attachment: o(e), targetAttachment: o(this.options.position), classPrefix: u.classPrefix, offset: "0 0", targetOffset: "0 0", enabled: !1, constraints: n, addTargetClasses: this.options.addTargetClasses }; this.options.tetherOptions !== !1 && (this.tether = new t(h({}, i, this.options.tetherOptions))) } }, { key: "setupEvents", value: function () { var t = this; if (this.options.openOn) { if ("always" === this.options.openOn) return void setTimeout(this.open.bind(this)); var e = this.options.openOn.split(" "); if (e.indexOf("click") >= 0) for (var n = function (e) { t.toggle(e), e.preventDefault() }, o = function (e) { t.isOpened() && (e.target === t.drop || t.drop.contains(e.target) || e.target === t.target || t.target.contains(e.target) || t.close(e)) }, i = 0; i < y.length; ++i) { var s = y[i]; this._on(this.target, s, n), this._on(document, s, o) } e.indexOf("hover") >= 0 && !function () { var e = !1, n = function (n) { e = !0, t.open(n) }, o = null, i = function (n) { e = !1, "undefined" != typeof o && clearTimeout(o), o = setTimeout(function () { e || t.close(n), o = null }, 50) }; t._on(t.target, "mouseover", n), t._on(t.drop, "mouseover", n), t._on(t.target, "mouseout", i), t._on(t.drop, "mouseout", i) }() } } }, { key: "isOpened", value: function () { return this.drop ? f(this.drop, u.classPrefix + "-open") : void 0 } }, { key: "toggle", value: function (t) { this.isOpened() ? this.close(t) : this.open(t) } }, { key: "open", value: function (t) { var e = this; this.isOpened() || (this.drop.parentNode || document.body.appendChild(this.drop), "undefined" != typeof this.tether && this.tether.enable(), p(this.drop, u.classPrefix + "-open"), p(this.drop, u.classPrefix + "-open-transitionend"), setTimeout(function () { e.drop && p(e.drop, u.classPrefix + "-after-open") }), "undefined" != typeof this.tether && this.tether.position(), this.trigger("open"), u.updateBodyClasses()) } }, { key: "_transitionEndHandler", value: function (t) { t.target === t.currentTarget && (f(this.drop, u.classPrefix + "-open") || c(this.drop, u.classPrefix + "-open-transitionend"), this.drop.removeEventListener(g, this.transitionEndHandler)) } }, { key: "beforeCloseHandler", value: function (t) { var e = !0; return this.isClosing || "function" != typeof this.options.beforeClose || (this.isClosing = !0, e = this.options.beforeClose(t, this) !== !1), this.isClosing = !1, e } }, { key: "close", value: function (t) { this.isOpened() && this.beforeCloseHandler(t) && (c(this.drop, u.classPrefix + "-open"), c(this.drop, u.classPrefix + "-after-open"), this.drop.addEventListener(g, this.transitionEndHandler), this.trigger("close"), "undefined" != typeof this.tether && this.tether.disable(), u.updateBodyClasses(), this.options.remove && this.remove(t)) } }, { key: "remove", value: function (t) { this.close(t), this.drop.parentNode && this.drop.parentNode.removeChild(this.drop) } }, { key: "position", value: function () { this.isOpened() && "undefined" != typeof this.tether && this.tether.position() } }, { key: "destroy", value: function () { this.remove(), "undefined" != typeof this.tether && this.tether.destroy(); for (var t = 0; t < this._boundEvents.length; ++t) { var e = this._boundEvents[t], n = e.element, o = e.event, s = e.handler; n.removeEventListener(o, s) } this._boundEvents = [], this.tether = null, this.drop = null, this.content = null, this.target = null, i(x[u.classPrefix], this), i(u.drops, this) } }]), r }(v); return u } var r = Function.prototype.bind, a = function () { function t(t, e) { var n = [], o = !0, i = !1, s = void 0; try { for (var r, a = t[Symbol.iterator]() ; !(o = (r = a.next()).done) && (n.push(r.value), !e || n.length !== e) ; o = !0); } catch (l) { i = !0, s = l } finally { try { !o && a["return"] && a["return"]() } finally { if (i) throw s } } return n } return function (e, n) { if (Array.isArray(e)) return e; if (Symbol.iterator in Object(e)) return t(e, n); throw new TypeError("Invalid attempt to destructure non-iterable instance") } }(), l = function () { function t(t, e) { for (var n = 0; n < e.length; n++) { var o = e[n]; o.enumerable = o.enumerable || !1, o.configurable = !0, "value" in o && (o.writable = !0), Object.defineProperty(t, o.key, o) } } return function (e, n, o) { return n && t(e.prototype, n), o && t(e, o), e } }(), d = function (t, e, n) { for (var o = !0; o;) { var i = t, s = e, r = n; a = d = l = void 0, o = !1, null === i && (i = Function.prototype); var a = Object.getOwnPropertyDescriptor(i, s); if (void 0 !== a) { if ("value" in a) return a.value; var l = a.get; return void 0 === l ? void 0 : l.call(r) } var d = Object.getPrototypeOf(i); if (null === d) return void 0; t = d, e = s, n = r, o = !0 } }, u = t.Utils, h = u.extend, p = u.addClass, c = u.removeClass, f = u.hasClass, v = u.Evented, y = ["click"]; "ontouchstart" in document.documentElement && y.push("touchstart"); var m = { WebkitTransition: "webkitTransitionEnd", MozTransition: "transitionend", OTransition: "otransitionend", transition: "transitionend" }, g = ""; for (var b in m) if ({}.hasOwnProperty.call(m, b)) { var E = document.createElement("p"); "undefined" != typeof E.style[b] && (g = m[b]) } var O = { left: "right", right: "left", top: "bottom", bottom: "top", middle: "middle", center: "center" }, x = {}, P = s(); return document.addEventListener("DOMContentLoaded", function () { P.updateBodyClasses() }), P });

    /*! tether-tooltip 1.1.0 */
    !function (t, o) { "function" == typeof define && define.amd ? define(["tether-drop", "tether"], o) : "object" == typeof exports ? module.exports = o(require("tether-drop"), require("tether")) : t.Tooltip = o(t.Drop, t.Tether) }(this, function (t, o) { "use strict"; function e(t, o) { if (!(t instanceof o)) throw new TypeError("Cannot call a class as a function") } var n = function () { function t(t, o) { for (var e = 0; e < o.length; e++) { var n = o[e]; n.enumerable = n.enumerable || !1, n.configurable = !0, "value" in n && (n.writable = !0), Object.defineProperty(t, n.key, n) } } return function (o, e, n) { return e && t(o.prototype, e), n && t(o, n), o } }(), i = o.Utils.extend, r = t.createContext({ classPrefix: "tooltip" }), s = { position: "top center", openOn: "hover", classes: "tooltip-theme-arrows", constrainToWindow: !0, constrainToScrollParent: !1 }, p = function () { function t(o) { if (e(this, t), this.options = o, !this.options.target) throw new Error("Tooltip Error: You must provide a target for Tooltip to attach to"); var n = this.options.target.getAttribute("data-tooltip-position"); n && "undefined" == typeof this.options.position && (this.options.position = n); var p = this.options.target.getAttribute("data-tooltip"); if (p && "undefined" == typeof this.options.content && (this.options.content = p), !this.options.content) throw new Error("Tooltip Error: You must provide content for Tooltip to display"); this.options = i({}, s, this.options), this.drop = new r(this.options) } return n(t, [{ key: "close", value: function () { this.drop.close() } }, { key: "open", value: function () { this.drop.open() } }, { key: "toggle", value: function () { this.drop.toggle() } }, { key: "remove", value: function () { this.drop.remove() } }, { key: "destroy", value: function () { this.drop.destroy() } }, { key: "position", value: function () { this.drop.position() } }]), t }(), a = []; return p.init = function () { for (var t = document.querySelectorAll("[data-tooltip]"), o = t.length, e = 0; o > e; ++e) { var n = t[e]; -1 === a.indexOf(n) && (new p({ target: n }), a.push(n)) } }, document.addEventListener("DOMContentLoaded", function () { p.autoinit !== !1 && p.init() }), p });




    /*
    
        Hashids
        http://hashids.org/javascript
        (c) 2013 Ivan Akimov
    
        https://github.com/ivanakimov/hashids.js
        hashids may be freely distributed under the MIT license.
    
    */
    var Hashids = function () { "use strict"; function Hashids(salt, minHashLength, alphabet) { var uniqueAlphabet, i, j, len, sepsLength, diff, guardCount; this.version = "1.0.2"; this.minAlphabetLength = 16; this.sepDiv = 3.5; this.guardDiv = 12; this.errorAlphabetLength = "error: alphabet must contain at least X unique characters"; this.errorAlphabetSpace = "error: alphabet cannot contain spaces"; this.alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; this.seps = "cfhistuCFHISTU"; this.minHashLength = parseInt(minHashLength, 10) > 0 ? minHashLength : 0; this.salt = typeof salt === "string" ? salt : ""; if (typeof alphabet === "string") { this.alphabet = alphabet } for (uniqueAlphabet = "", i = 0, len = this.alphabet.length; i !== len; i++) { if (uniqueAlphabet.indexOf(this.alphabet.charAt(i)) === -1) { uniqueAlphabet += this.alphabet.charAt(i) } } this.alphabet = uniqueAlphabet; if (this.alphabet.length < this.minAlphabetLength) { throw this.errorAlphabetLength.replace("X", this.minAlphabetLength) } if (this.alphabet.search(" ") !== -1) { throw this.errorAlphabetSpace } for (i = 0, len = this.seps.length; i !== len; i++) { j = this.alphabet.indexOf(this.seps.charAt(i)); if (j === -1) { this.seps = this.seps.substr(0, i) + " " + this.seps.substr(i + 1) } else { this.alphabet = this.alphabet.substr(0, j) + " " + this.alphabet.substr(j + 1) } } this.alphabet = this.alphabet.replace(/ /g, ""); this.seps = this.seps.replace(/ /g, ""); this.seps = this.consistentShuffle(this.seps, this.salt); if (!this.seps.length || this.alphabet.length / this.seps.length > this.sepDiv) { sepsLength = Math.ceil(this.alphabet.length / this.sepDiv); if (sepsLength === 1) { sepsLength++ } if (sepsLength > this.seps.length) { diff = sepsLength - this.seps.length; this.seps += this.alphabet.substr(0, diff); this.alphabet = this.alphabet.substr(diff) } else { this.seps = this.seps.substr(0, sepsLength) } } this.alphabet = this.consistentShuffle(this.alphabet, this.salt); guardCount = Math.ceil(this.alphabet.length / this.guardDiv); if (this.alphabet.length < 3) { this.guards = this.seps.substr(0, guardCount); this.seps = this.seps.substr(guardCount) } else { this.guards = this.alphabet.substr(0, guardCount); this.alphabet = this.alphabet.substr(guardCount) } } Hashids.prototype.encode = function () { var ret = "", i, len, numbers = Array.prototype.slice.call(arguments); if (!numbers.length) { return ret } if (numbers[0] instanceof Array) { numbers = numbers[0] } for (i = 0, len = numbers.length; i !== len; i++) { if (typeof numbers[i] !== "number" || numbers[i] % 1 !== 0 || numbers[i] < 0) { return ret } } return this._encode(numbers) }; Hashids.prototype.decode = function (hash) { var ret = []; if (!hash.length || typeof hash !== "string") { return ret } return this._decode(hash, this.alphabet) }; Hashids.prototype.encodeHex = function (str) { var i, len, numbers; str = str.toString(); if (!/^[0-9a-fA-F]+$/.test(str)) { return "" } numbers = str.match(/[\w\W]{1,12}/g); for (i = 0, len = numbers.length; i !== len; i++) { numbers[i] = parseInt("1" + numbers[i], 16) } return this.encode.apply(this, numbers) }; Hashids.prototype.decodeHex = function (hash) { var ret = [], i, len, numbers = this.decode(hash); for (i = 0, len = numbers.length; i !== len; i++) { ret += numbers[i].toString(16).substr(1) } return ret }; Hashids.prototype._encode = function (numbers) { var ret, lottery, i, len, number, buffer, last, sepsIndex, guardIndex, guard, halfLength, excess, alphabet = this.alphabet, numbersSize = numbers.length, numbersHashInt = 0; for (i = 0, len = numbers.length; i !== len; i++) { numbersHashInt += numbers[i] % (i + 100) } lottery = ret = alphabet.charAt(numbersHashInt % alphabet.length); for (i = 0, len = numbers.length; i !== len; i++) { number = numbers[i]; buffer = lottery + this.salt + alphabet; alphabet = this.consistentShuffle(alphabet, buffer.substr(0, alphabet.length)); last = this.hash(number, alphabet); ret += last; if (i + 1 < numbersSize) { number %= last.charCodeAt(0) + i; sepsIndex = number % this.seps.length; ret += this.seps.charAt(sepsIndex) } } if (ret.length < this.minHashLength) { guardIndex = (numbersHashInt + ret[0].charCodeAt(0)) % this.guards.length; guard = this.guards[guardIndex]; ret = guard + ret; if (ret.length < this.minHashLength) { guardIndex = (numbersHashInt + ret[2].charCodeAt(0)) % this.guards.length; guard = this.guards[guardIndex]; ret += guard } } halfLength = parseInt(alphabet.length / 2, 10); while (ret.length < this.minHashLength) { alphabet = this.consistentShuffle(alphabet, alphabet); ret = alphabet.substr(halfLength) + ret + alphabet.substr(0, halfLength); excess = ret.length - this.minHashLength; if (excess > 0) { ret = ret.substr(excess / 2, this.minHashLength) } } return ret }; Hashids.prototype._decode = function (hash, alphabet) { var ret = [], i = 0, lottery, len, subHash, buffer, r = new RegExp("[" + this.guards + "]", "g"), hashBreakdown = hash.replace(r, " "), hashArray = hashBreakdown.split(" "); if (hashArray.length === 3 || hashArray.length === 2) { i = 1 } hashBreakdown = hashArray[i]; if (typeof hashBreakdown[0] !== "undefined") { lottery = hashBreakdown[0]; hashBreakdown = hashBreakdown.substr(1); r = new RegExp("[" + this.seps + "]", "g"); hashBreakdown = hashBreakdown.replace(r, " "); hashArray = hashBreakdown.split(" "); for (i = 0, len = hashArray.length; i !== len; i++) { subHash = hashArray[i]; buffer = lottery + this.salt + alphabet; alphabet = this.consistentShuffle(alphabet, buffer.substr(0, alphabet.length)); ret.push(this.unhash(subHash, alphabet)) } if (this._encode(ret) !== hash) { ret = [] } } return ret }; Hashids.prototype.consistentShuffle = function (alphabet, salt) { var integer, j, temp, i, v, p; if (!salt.length) { return alphabet } for (i = alphabet.length - 1, v = 0, p = 0; i > 0; i--, v++) { v %= salt.length; p += integer = salt.charAt(v).charCodeAt(0); j = (integer + v + p) % i; temp = alphabet.charAt(j); alphabet = alphabet.substr(0, j) + alphabet.charAt(i) + alphabet.substr(j + 1); alphabet = alphabet.substr(0, i) + temp + alphabet.substr(i + 1) } return alphabet }; Hashids.prototype.hash = function (input, alphabet) { var hash = "", alphabetLength = alphabet.length; do { hash = alphabet.charAt(input % alphabetLength) + hash; input = parseInt(input / alphabetLength, 10) } while (input); return hash }; Hashids.prototype.unhash = function (input, alphabet) { var number = 0, pos, i; for (i = 0; i < input.length; i++) { pos = alphabet.indexOf(input[i]); number += pos * Math.pow(alphabet.length, input.length - i - 1) } return number }; if (typeof define === "function" && typeof define.amd === "object" && define.amd) { define(function () { return Hashids }) } return Hashids }();

    var hashids = new Hashids(currentSite + SiteID, 10, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890");





    /*PACE */

    var paceOptions = {
        ajax: true, // disabled
        document: true, // disabled
        eventLag: true, // disabled
    };

    /*! pace 1.0.0 */
    (function () { var a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X = [].slice, Y = {}.hasOwnProperty, Z = function (a, b) { function c() { this.constructor = a } for (var d in b) Y.call(b, d) && (a[d] = b[d]); return c.prototype = b.prototype, a.prototype = new c, a.__super__ = b.prototype, a }, $ = [].indexOf || function (a) { for (var b = 0, c = this.length; c > b; b++) if (b in this && this[b] === a) return b; return -1 }; for (u = { catchupTime: 100, initialRate: .03, minTime: 250, ghostTime: 100, maxProgressPerFrame: 20, easeFactor: 1.25, startOnPageLoad: !0, restartOnPushState: !0, restartOnRequestAfter: 500, target: "body", elements: { checkInterval: 100, selectors: ["body"] }, eventLag: { minSamples: 10, sampleCount: 3, lagThreshold: 3 }, ajax: { trackMethods: ["GET"], trackWebSockets: !0, ignoreURLs: [] } }, C = function () { var a; return null != (a = "undefined" != typeof performance && null !== performance && "function" == typeof performance.now ? performance.now() : void 0) ? a : +new Date }, E = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.msRequestAnimationFrame, t = window.cancelAnimationFrame || window.mozCancelAnimationFrame, null == E && (E = function (a) { return setTimeout(a, 50) }, t = function (a) { return clearTimeout(a) }), G = function (a) { var b, c; return b = C(), (c = function () { var d; return d = C() - b, d >= 33 ? (b = C(), a(d, function () { return E(c) })) : setTimeout(c, 33 - d) })() }, F = function () { var a, b, c; return c = arguments[0], b = arguments[1], a = 3 <= arguments.length ? X.call(arguments, 2) : [], "function" == typeof c[b] ? c[b].apply(c, a) : c[b] }, v = function () { var a, b, c, d, e, f, g; for (b = arguments[0], d = 2 <= arguments.length ? X.call(arguments, 1) : [], f = 0, g = d.length; g > f; f++) if (c = d[f]) for (a in c) Y.call(c, a) && (e = c[a], null != b[a] && "object" == typeof b[a] && null != e && "object" == typeof e ? v(b[a], e) : b[a] = e); return b }, q = function (a) { var b, c, d, e, f; for (c = b = 0, e = 0, f = a.length; f > e; e++) d = a[e], c += Math.abs(d), b++; return c / b }, x = function (a, b) { var c, d, e; if (null == a && (a = "options"), null == b && (b = !0), e = document.querySelector("[data-pace-" + a + "]")) { if (c = e.getAttribute("data-pace-" + a), !b) return c; try { return JSON.parse(c) } catch (f) { return d = f, "undefined" != typeof console && null !== console ? console.error("Error parsing inline pace options", d) : void 0 } } }, g = function () { function a() { } return a.prototype.on = function (a, b, c, d) { var e; return null == d && (d = !1), null == this.bindings && (this.bindings = {}), null == (e = this.bindings)[a] && (e[a] = []), this.bindings[a].push({ handler: b, ctx: c, once: d }) }, a.prototype.once = function (a, b, c) { return this.on(a, b, c, !0) }, a.prototype.off = function (a, b) { var c, d, e; if (null != (null != (d = this.bindings) ? d[a] : void 0)) { if (null == b) return delete this.bindings[a]; for (c = 0, e = []; c < this.bindings[a].length;) e.push(this.bindings[a][c].handler === b ? this.bindings[a].splice(c, 1) : c++); return e } }, a.prototype.trigger = function () { var a, b, c, d, e, f, g, h, i; if (c = arguments[0], a = 2 <= arguments.length ? X.call(arguments, 1) : [], null != (g = this.bindings) ? g[c] : void 0) { for (e = 0, i = []; e < this.bindings[c].length;) h = this.bindings[c][e], d = h.handler, b = h.ctx, f = h.once, d.apply(null != b ? b : this, a), i.push(f ? this.bindings[c].splice(e, 1) : e++); return i } }, a }(), j = window.Pace || {}, window.Pace = j, v(j, g.prototype), D = j.options = v({}, u, window.paceOptions, x()), U = ["ajax", "document", "eventLag", "elements"], Q = 0, S = U.length; S > Q; Q++) K = U[Q], D[K] === !0 && (D[K] = u[K]); i = function (a) { function b() { return V = b.__super__.constructor.apply(this, arguments) } return Z(b, a), b }(Error), b = function () { function a() { this.progress = 0 } return a.prototype.getElement = function () { var a; if (null == this.el) { if (a = document.querySelector(D.target), !a) throw new i; this.el = document.createElement("div"), this.el.className = "pace pace-active", document.body.className = document.body.className.replace(/pace-done/g, ""), document.body.className += " pace-running", this.el.innerHTML = '<div class="pace-progress">\n  <div class="pace-progress-inner"></div>\n</div>\n<div class="pace-activity"></div>', null != a.firstChild ? a.insertBefore(this.el, a.firstChild) : a.appendChild(this.el) } return this.el }, a.prototype.finish = function () { var a; return a = this.getElement(), a.className = a.className.replace("pace-active", ""), a.className += " pace-inactive", document.body.className = document.body.className.replace("pace-running", ""), document.body.className += " pace-done" }, a.prototype.update = function (a) { return this.progress = a, this.render() }, a.prototype.destroy = function () { try { this.getElement().parentNode.removeChild(this.getElement()) } catch (a) { i = a } return this.el = void 0 }, a.prototype.render = function () { var a, b, c, d, e, f, g; if (null == document.querySelector(D.target)) return !1; for (a = this.getElement(), d = "translate3d(" + this.progress + "%, 0, 0)", g = ["webkitTransform", "msTransform", "transform"], e = 0, f = g.length; f > e; e++) b = g[e], a.children[0].style[b] = d; return (!this.lastRenderedProgress || this.lastRenderedProgress | 0 !== this.progress | 0) && (a.children[0].setAttribute("data-progress-text", "" + (0 | this.progress) + "%"), this.progress >= 100 ? c = "99" : (c = this.progress < 10 ? "0" : "", c += 0 | this.progress), a.children[0].setAttribute("data-progress", "" + c)), this.lastRenderedProgress = this.progress }, a.prototype.done = function () { return this.progress >= 100 }, a }(), h = function () { function a() { this.bindings = {} } return a.prototype.trigger = function (a, b) { var c, d, e, f, g; if (null != this.bindings[a]) { for (f = this.bindings[a], g = [], d = 0, e = f.length; e > d; d++) c = f[d], g.push(c.call(this, b)); return g } }, a.prototype.on = function (a, b) { var c; return null == (c = this.bindings)[a] && (c[a] = []), this.bindings[a].push(b) }, a }(), P = window.XMLHttpRequest, O = window.XDomainRequest, N = window.WebSocket, w = function (a, b) { var c, d, e, f; f = []; for (d in b.prototype) try { e = b.prototype[d], f.push(null == a[d] && "function" != typeof e ? a[d] = e : void 0) } catch (g) { c = g } return f }, A = [], j.ignore = function () { var a, b, c; return b = arguments[0], a = 2 <= arguments.length ? X.call(arguments, 1) : [], A.unshift("ignore"), c = b.apply(null, a), A.shift(), c }, j.track = function () { var a, b, c; return b = arguments[0], a = 2 <= arguments.length ? X.call(arguments, 1) : [], A.unshift("track"), c = b.apply(null, a), A.shift(), c }, J = function (a) { var b; if (null == a && (a = "GET"), "track" === A[0]) return "force"; if (!A.length && D.ajax) { if ("socket" === a && D.ajax.trackWebSockets) return !0; if (b = a.toUpperCase(), $.call(D.ajax.trackMethods, b) >= 0) return !0 } return !1 }, k = function (a) { function b() { var a, c = this; b.__super__.constructor.apply(this, arguments), a = function (a) { var b; return b = a.open, a.open = function (d, e) { return J(d) && c.trigger("request", { type: d, url: e, request: a }), b.apply(a, arguments) } }, window.XMLHttpRequest = function (b) { var c; return c = new P(b), a(c), c }; try { w(window.XMLHttpRequest, P) } catch (d) { } if (null != O) { window.XDomainRequest = function () { var b; return b = new O, a(b), b }; try { w(window.XDomainRequest, O) } catch (d) { } } if (null != N && D.ajax.trackWebSockets) { window.WebSocket = function (a, b) { var d; return d = null != b ? new N(a, b) : new N(a), J("socket") && c.trigger("request", { type: "socket", url: a, protocols: b, request: d }), d }; try { w(window.WebSocket, N) } catch (d) { } } } return Z(b, a), b }(h), R = null, y = function () { return null == R && (R = new k), R }, I = function (a) { var b, c, d, e; for (e = D.ajax.ignoreURLs, c = 0, d = e.length; d > c; c++) if (b = e[c], "string" == typeof b) { if (-1 !== a.indexOf(b)) return !0 } else if (b.test(a)) return !0; return !1 }, y().on("request", function (b) { var c, d, e, f, g; return f = b.type, e = b.request, g = b.url, I(g) ? void 0 : j.running || D.restartOnRequestAfter === !1 && "force" !== J(f) ? void 0 : (d = arguments, c = D.restartOnRequestAfter || 0, "boolean" == typeof c && (c = 0), setTimeout(function () { var b, c, g, h, i, k; if (b = "socket" === f ? e.readyState < 2 : 0 < (h = e.readyState) && 4 > h) { for (j.restart(), i = j.sources, k = [], c = 0, g = i.length; g > c; c++) { if (K = i[c], K instanceof a) { K.watch.apply(K, d); break } k.push(void 0) } return k } }, c)) }), a = function () { function a() { var a = this; this.elements = [], y().on("request", function () { return a.watch.apply(a, arguments) }) } return a.prototype.watch = function (a) { var b, c, d, e; return d = a.type, b = a.request, e = a.url, I(e) ? void 0 : (c = "socket" === d ? new n(b) : new o(b), this.elements.push(c)) }, a }(), o = function () { function a(a) { var b, c, d, e, f, g, h = this; if (this.progress = 0, null != window.ProgressEvent) for (c = null, a.addEventListener("progress", function (a) { return h.progress = a.lengthComputable ? 100 * a.loaded / a.total : h.progress + (100 - h.progress) / 2 }, !1), g = ["load", "abort", "timeout", "error"], d = 0, e = g.length; e > d; d++) b = g[d], a.addEventListener(b, function () { return h.progress = 100 }, !1); else f = a.onreadystatechange, a.onreadystatechange = function () { var b; return 0 === (b = a.readyState) || 4 === b ? h.progress = 100 : 3 === a.readyState && (h.progress = 50), "function" == typeof f ? f.apply(null, arguments) : void 0 } } return a }(), n = function () { function a(a) { var b, c, d, e, f = this; for (this.progress = 0, e = ["error", "open"], c = 0, d = e.length; d > c; c++) b = e[c], a.addEventListener(b, function () { return f.progress = 100 }, !1) } return a }(), d = function () { function a(a) { var b, c, d, f; for (null == a && (a = {}), this.elements = [], null == a.selectors && (a.selectors = []), f = a.selectors, c = 0, d = f.length; d > c; c++) b = f[c], this.elements.push(new e(b)) } return a }(), e = function () { function a(a) { this.selector = a, this.progress = 0, this.check() } return a.prototype.check = function () { var a = this; return document.querySelector(this.selector) ? this.done() : setTimeout(function () { return a.check() }, D.elements.checkInterval) }, a.prototype.done = function () { return this.progress = 100 }, a }(), c = function () { function a() { var a, b, c = this; this.progress = null != (b = this.states[document.readyState]) ? b : 100, a = document.onreadystatechange, document.onreadystatechange = function () { return null != c.states[document.readyState] && (c.progress = c.states[document.readyState]), "function" == typeof a ? a.apply(null, arguments) : void 0 } } return a.prototype.states = { loading: 0, interactive: 50, complete: 100 }, a }(), f = function () { function a() { var a, b, c, d, e, f = this; this.progress = 0, a = 0, e = [], d = 0, c = C(), b = setInterval(function () { var g; return g = C() - c - 50, c = C(), e.push(g), e.length > D.eventLag.sampleCount && e.shift(), a = q(e), ++d >= D.eventLag.minSamples && a < D.eventLag.lagThreshold ? (f.progress = 100, clearInterval(b)) : f.progress = 100 * (3 / (a + 3)) }, 50) } return a }(), m = function () { function a(a) { this.source = a, this.last = this.sinceLastUpdate = 0, this.rate = D.initialRate, this.catchup = 0, this.progress = this.lastProgress = 0, null != this.source && (this.progress = F(this.source, "progress")) } return a.prototype.tick = function (a, b) { var c; return null == b && (b = F(this.source, "progress")), b >= 100 && (this.done = !0), b === this.last ? this.sinceLastUpdate += a : (this.sinceLastUpdate && (this.rate = (b - this.last) / this.sinceLastUpdate), this.catchup = (b - this.progress) / D.catchupTime, this.sinceLastUpdate = 0, this.last = b), b > this.progress && (this.progress += this.catchup * a), c = 1 - Math.pow(this.progress / 100, D.easeFactor), this.progress += c * this.rate * a, this.progress = Math.min(this.lastProgress + D.maxProgressPerFrame, this.progress), this.progress = Math.max(0, this.progress), this.progress = Math.min(100, this.progress), this.lastProgress = this.progress, this.progress }, a }(), L = null, H = null, r = null, M = null, p = null, s = null, j.running = !1, z = function () { return D.restartOnPushState ? j.restart() : void 0 }, null != window.history.pushState && (T = window.history.pushState, window.history.pushState = function () { return z(), T.apply(window.history, arguments) }), null != window.history.replaceState && (W = window.history.replaceState, window.history.replaceState = function () { return z(), W.apply(window.history, arguments) }), l = { ajax: a, elements: d, document: c, eventLag: f }, (B = function () { var a, c, d, e, f, g, h, i; for (j.sources = L = [], g = ["ajax", "elements", "document", "eventLag"], c = 0, e = g.length; e > c; c++) a = g[c], D[a] !== !1 && L.push(new l[a](D[a])); for (i = null != (h = D.extraSources) ? h : [], d = 0, f = i.length; f > d; d++) K = i[d], L.push(new K(D)); return j.bar = r = new b, H = [], M = new m })(), j.stop = function () { return j.trigger("stop"), j.running = !1, r.destroy(), s = !0, null != p && ("function" == typeof t && t(p), p = null), B() }, j.restart = function () { return j.trigger("restart"), j.stop(), j.start() }, j.go = function () { var a; return j.running = !0, r.render(), a = C(), s = !1, p = G(function (b, c) { var d, e, f, g, h, i, k, l, n, o, p, q, t, u, v, w; for (l = 100 - r.progress, e = p = 0, f = !0, i = q = 0, u = L.length; u > q; i = ++q) for (K = L[i], o = null != H[i] ? H[i] : H[i] = [], h = null != (w = K.elements) ? w : [K], k = t = 0, v = h.length; v > t; k = ++t) g = h[k], n = null != o[k] ? o[k] : o[k] = new m(g), f &= n.done, n.done || (e++, p += n.tick(b)); return d = p / e, r.update(M.tick(b, d)), r.done() || f || s ? (r.update(100), j.trigger("done"), setTimeout(function () { return r.finish(), j.running = !1, j.trigger("hide") }, Math.max(D.ghostTime, Math.max(D.minTime - (C() - a), 0)))) : c() }) }, j.start = function (a) { v(D, a), j.running = !0; try { r.render() } catch (b) { i = b } return document.querySelector(".pace") ? (j.trigger("start"), j.go()) : setTimeout(j.start, 50) }, "function" == typeof define && define.amd ? define(function () { return j }) : "object" == typeof exports ? module.exports = j : D.startOnPageLoad && j.start() }).call(this);




    //Pure JS, completely customizable preloader from GreenSock.
    //Once you create an instance like var preloader = new GSPreloader(), call preloader.active(true) to open it, preloader.active(false) to close it, and preloader.active() to get the current status. Only requires TweenLite and CSSPlugin (http://www.greensock.com/gsap/)
    var preloader = new GSPreloader({
        radius: 42,
        dotSize: 15,
        dotCount: 10,
        colors: ["#939be8", "#555"], //have as many or as few colors as you want.
        boxOpacity: 0.2,
        parent: document.getElementById("preloader"),
        boxBorder: "1px solid #FFF",
        animationOffset: 1.8, //jump 1.8 seconds into the animation for a more active part of the spinning initially (just looks a bit better in my opinion)
    });

    //open the preloader
    if (menuIsSupported) {
        preloader.active(true);
    }

    //this is the whole preloader class/function
    function GSPreloader(options) {
        options = options || {};
        var parent = options.parent || document.body,
            element = this.element = document.createElement("div"),
            radius = options.radius || 42,
            dotSize = options.dotSize || 15,
            animationOffset = options.animationOffset || 1.8, //jumps to a more active part of the animation initially (just looks cooler especially when the preloader isn't displayed for very long)
            createDot = function (rotation) {
                var dot = document.createElement("div");
                element.appendChild(dot);
                TweenLite.set(dot, { width: dotSize, height: dotSize, transformOrigin: (-radius + "px 0px"), x: radius, backgroundColor: colors[colors.length - 1], borderRadius: "50%", force3D: true, position: "absolute", rotation: rotation });
                dot.className = options.dotClass || "preloader-dot";
                return dot;
            },
            i = options.dotCount || 10,
            rotationIncrement = 360 / i,
            colors = options.colors || ["#61AC27", "black"],
            animation = new TimelineLite({ paused: true }),
            dots = [],
            isActive = false,
            box = document.createElement("div"),
            tl, dot, closingAnimation, j;
        colors.push(colors.shift());

        //setup background box
        TweenLite.set(box, { width: radius * 2 + 70, height: radius * 2 + 70, borderRadius: "50%", backgroundColor: options.boxColor || "white", border: options.boxBorder || "1px solid #AAA", position: "absolute", xPercent: -50, yPercent: -50, opacity: ((options.boxOpacity != null) ? options.boxOpacity : 0.3) });
        box.className = options.boxClass || "preloader-box";
        element.appendChild(box);

        parent.appendChild(element);
        TweenLite.set(element, { position: "relative", top: "70px", left: "50%", perspective: 600, overflow: "visible", zIndex: 2000 });
        animation.from(box, 0.1, { opacity: 0, scale: 0.1, ease: Power1.easeOut }, animationOffset);
        while (--i > -1) {
            dot = createDot(i * rotationIncrement);
            dots.unshift(dot);
            animation.from(dot, 0.1, { scale: 0.01, opacity: 0, ease: Power1.easeOut }, animationOffset);
            //tuck the repeating parts of the animation into a nested TimelineMax (the intro shouldn't be repeated)
            tl = new TimelineMax({ repeat: -1, repeatDelay: 0.25 });
            for (j = 0; j < colors.length; j++) {
                tl.to(dot, 2.5, { rotation: "-=360", ease: Power2.easeInOut }, j * 2.9)
                  .to(dot, 1.2, { skewX: "+=360", backgroundColor: colors[j], ease: Power2.easeInOut }, 1.6 + 2.9 * j);
            }
            //stagger its placement into the master timeline
            animation.add(tl, i * 0.07);
        }
        if (TweenLite.render) {
            TweenLite.render(); //trigger the from() tweens' lazy-rendering (otherwise it'd take one tick to render everything in the beginning state, thus things may flash on the screen for a moment initially). There are other ways around this, but TweenLite.render() is probably the simplest in this case.
        }

        //call preloader.active(true) to open the preloader, preloader.active(false) to close it, or preloader.active() to get the current state.
        this.active = function (show) {
            if (!arguments.length) {
                return isActive;
            }
            if (isActive != show) {
                isActive = show;
                if (closingAnimation) {
                    closingAnimation.kill(); //in case the preloader is made active/inactive/active/inactive really fast and there's still a closing animation running, kill it.
                }
                if (isActive) {
                    element.style.visibility = "visible";
                    TweenLite.set([element, box], { rotation: 0 });
                    animation.play(animationOffset);
                } else {
                    closingAnimation = new TimelineLite();
                    if (animation.time() < animationOffset + 0.3) {
                        animation.pause();
                        closingAnimation.to(element, 1, { rotation: -360, ease: Power1.easeInOut }).to(box, 1, { rotation: 360, ease: Power1.easeInOut }, 0);
                    }
                    closingAnimation.staggerTo(dots, 0.3, { scale: 0.01, opacity: 0, ease: Power1.easeIn, overwrite: false }, 0.05, 0).to(box, 0.4, { opacity: 0, scale: 0.2, ease: Power2.easeIn, overwrite: false }, 0).call(function () { animation.pause(); closingAnimation = null; }).set(element, { visibility: "hidden" });
                }
            }
            return this;
        };
    }






    /*
    Array Map pollyfill
    */
    [].map || (Array.prototype.map = function (a) { for (var b = this, c = b.length, d = [], e = 0, f; e < b;) d[e] = e in b ? a.call(arguments[1], b[e], e++, b) : f; return d })


    /*
    Number padding
    */
    Number.prototype.pad = function (padString, length) {
        var str = new String(this);
        while (str.length < length)
            str = str + padString;
        return parseInt(str);
    }

})(window);