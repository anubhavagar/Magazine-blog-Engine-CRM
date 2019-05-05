
$(function () {

    $('.close').click(function () {
        //  $(this).parent().removeClass('in'); // hides alert with Bootstrap CSS3 
        $(this).parent().hide('slow', function () { $(this).empty().append($('<button type="button" class="close">&times;</button>')); });
    });

    //    var alreadyselected = '[' + '"arvind-kejriwal"' + ']';
    //    var article_tags = $('#article_tags').magicSuggest({
    //        data: 'http://localhost:1043/Tag/TagData',
    //        sortOrder: 'name',
    //        valueField: 'name',
    //        minChars: 0,
    //        value: [3],
    //        maxResults: false,
    //        name: 'article_tags',
    //        allowFreeEntries: false,
    //        selectionPosition: 'right',
    //        //groupBy: 'name',
    //        maxDropHeight: 200
    //    });

    //    $(article_tags).on('selectionchange', function (event, combo, selection) {
    //        var resultagtstr = '';
    //        $.each(selection, function (index, value) {
    //            //alert(index + ": " + selection[index].name);
    //            resultagtstr = resultagtstr + selection[index].id + ',';
    //        });
    //        alert(resultagtstr);
    //    });

    var jsonData = [];
    jsonData = [{ "id": "india", "name": "india" },
{ 'id': 'arvind-kejriwal', 'name': 'arvind-kejriwal' },
{ 'id': 'corruption', 'name': 'corruption' }, { 'id': 'delhi', 'name': 'delhi' },
{ 'id': 'education', 'name': 'education' }, { 'id': 'elections', 'name': 'elections' },
{ 'id': 'technology', 'name': 'technology' },
{ 'id': 'politics', 'name': 'politics' }, { 'id': 'women', 'name': 'women' },
{ 'id': 'poverty', 'name': 'poverty' }, { 'id': 'aam-aadmi-party', 'name': 'aam-aadmi-party' }, { 'id': 'narendra-modi', 'name': 'narendra-modi' },
{ 'id': 'rahul-gandhi', 'name': 'rahul-gandhi' }, { 'id': 'anna-hazare', 'name': 'anna-hazare' },
{ 'id': 'money', 'name': 'money' }, { 'id': 'congress', 'name': 'congress' },
{ 'id': 'bhartiya-janta-party', 'name': 'bhartiya-janta-party'}];

    //var selectedtags = "[" + "'india', 'education','politics','bhartiya-janta-party'" + "]";

    var article_tags = $('#article_tags').magicSuggest({
        width: 250,
        sortOrder: 'name',
        selectionPosition: 'bottom',
        selectionStacked: true,
        displayField: 'name',
        // value: selectedtags,
        data: jsonData,
        name: 'article_tags', maxDropHeight: 200,
        valueField: 'id', allowFreeEntries: false,
        maxResults: false
    });

    $(article_tags).on('selectionchange', function (event, combo, selection) {

        var selecttags = '';
        if (selection.length > 0) {
            for (var i = 0; i < selection.length; i++) {
                //alert(selection[i].name);
                //selectedtags.push(selection[i].name);   
                selecttags = selecttags + selection[i].name + ',';
            }
            // alert(selecttags);

        }
        $('#hdnTag_Article').val(selecttags);
    });


    $("input:radio[name=category]").click(function () {
        var value = $(this).attr("value");
        var id = $(this).attr("id");
        // var category = id+";"+value;
        $("#hdncategoryid").val(id);
        $("#hdncategoryname").val(value);
    })


    $('#btnsubmit-next').click(function (event) {

        jErrorVal.errors = false;
        jErrorVal.title();

        if (jErrorVal.errors == false) {
            jErrorVal.title_english();
        }
        else {

            return false;
        }


        if (jErrorVal.errors == false) {
            jErrorVal.content();
        }
        else {

            return false;
        }

        if (jErrorVal.errors == false) {
            jErrorVal.author_name();
        }
        else {

            return false;
        }

        if (jErrorVal.errors == false) {
            jErrorVal.company_name();
        }
        else {

            return false;
        }

        if (jErrorVal.errors == false) {
            jErrorVal.category();
        }
        else {

            return false;
        }


        if (jErrorVal.errors == false) {
            var content = $('#txtblogcontent');
            $("#hdnContent").val(content.val());
           
            return true;
        }
        else {
            return false;
        }

    });



    $('#txtblogcontent').htmlarea({})
    var jErrorVal = {
        'title': function () {

            var errorInfo = $('#error_alert');
            var ele = $('#article_title');
            jErrorVal.errors = false;

            if (ele.val().length < 1) {
                jErrorVal.errors = true;
                errorInfo.html('Please enter title of the article').show();
                ele.removeClass('normal').addClass('error');
                //ele.focus();
            } else {
                jErrorVal.errors = false;
                errorInfo.hide();
                ele.removeClass('error').addClass('normal');
            }

        },
         'title_english': function () {

            var errorInfo = $('#error_alert');
            var ele = $('#article_title_english');
            jErrorVal.errors = false;

            if (ele.val().length < 1) {
                jErrorVal.errors = true;
                errorInfo.html('Please enter title of the article in english').show();
                ele.removeClass('normal').addClass('error');
                //ele.focus();
            } else {
                jErrorVal.errors = false;
                errorInfo.hide();
                ele.removeClass('error').addClass('normal');
            }

        },
        'content': function () {

            var errorInfo = $('#error_alert');
            var ele = $('#txtblogcontent');
            jErrorVal.errors = false;

            if (ele.val().length < 1) {
                jErrorVal.errors = true;
                errorInfo.html('Please enter content of the article').show();
                ele.removeClass('normal').addClass('error');
                //ele.focus();
            } else {
                jErrorVal.errors = false;
                errorInfo.hide();
                ele.removeClass('error').addClass('normal');
            }

        },
        'author_name': function () {

            var errorInfo = $('#error_alert');
            var ele = $('#author_name');
            jErrorVal.errors = false;

            if (ele.val().length < 1) {
                jErrorVal.errors = true;
                errorInfo.html('Please enter author of the article').show();
                ele.removeClass('normal').addClass('error');
                //ele.focus();
            } else {
                jErrorVal.errors = false;
                errorInfo.hide();
                ele.removeClass('error').addClass('normal');
            }

        },
        'company_name': function () {

            var errorInfo = $('#error_alert');
            var ele = $('#company_name');
            jErrorVal.errors = false;

            if (ele.val().length < 1) {
                jErrorVal.errors = true;
                errorInfo.html('Please enter company name').show();
                ele.removeClass('normal').addClass('error');
                //ele.focus();
            } else {
                jErrorVal.errors = false;
                errorInfo.hide();
                ele.removeClass('error').addClass('normal');
            }

        },
        'category': function () {
            var errorInfo = $('#error_alert');
            jErrorVal.errors = false;
            if (!$("input[name='category']:checked").val()) {
                jErrorVal.errors = true;
                errorInfo.html('Please select the category of this article').show();
            }
            else {
                jErrorVal.errors = false;
                errorInfo.hide();
            }

        }          
    }



});
/*   
* hoverIntent | Copyright 2011 Brian Cherne   
* http://cherne.net/brian/resources/jquery.hoverIntent.html   
* modified by the jQuery UI team   
*/
$.event.special.hoverintent = { setup: function () {
    $(this).bind("mouseover", jQuery.event.special.hoverintent.handler);
}, teardown: function () {
    $(this).unbind("mouseover", jQuery.event.special.hoverintent.handler);
}, handler: function (event) {
    var currentX, currentY, timeout,
        args = arguments, target = $(event.target), previousX = event.pageX,
                previousY = event.pageY; function track(event) {
                    currentX = event.pageX;
                    currentY = event.pageY;
                }; function clear() {
                    target.unbind("mousemove", track).unbind("mouseout", clear);
                    clearTimeout(timeout);
                } function handler() {
                    var prop, orig = event;
                    if ((Math.abs(previousX - currentX) + Math.abs(previousY - currentY)) < 7) {
                        clear();
                        event = $.Event("hoverintent");
                        for (prop in orig) {
                            if (!(prop in event)) {
                                event[prop] = orig[prop];
                            }
                        }
                        // Prevent accessing the original event since the new event          
                        // is fired asynchronously and the old event is no longer          
                        // usable (#6028)  
                        delete event.originalEvent;
                        target.trigger(event);
                    } else {
                        previousX = currentX;
                        previousY = currentY;
                        timeout = setTimeout(handler, 100);
                    }
                } timeout = setTimeout(handler, 100);
    target.bind({ mousemove: track, mouseout: clear });
}
};


       
    