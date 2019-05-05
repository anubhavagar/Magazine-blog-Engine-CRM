
$(document).ajaxStart(function () { $('#ajax_loader_div').show(); });
$(document).ajaxStop(function () {
    setTimeout(Hide_ajax_loader, 1000);
    function Hide_ajax_loader() {
        $('#ajax_loader_div').hide();
    }
});

$(function () {

    $('button[data-loading-text]').click(function () {
        var btn = $(this)
        btn.button('loading')
        setTimeout(function () {
            btn.button('reset')
        }, 3000)
    });

    $('.btnpreviewArticle').click(function () {
        var url = $(this).data('url');
        //alert(url);
        $.get(url, function (data) {
            $('#EntryPostbody').html(data);

            $('#previewEntryPostModal').modal('show');
        });
    });

    $('.close').click(function () {
        //  $(this).parent().removeClass('in'); // hides alert with Bootstrap CSS3 
        $(this).parent().hide('slow', function () { $(this).empty().append($('<button type="button" class="close">&times;</button>')); });
    });

    $(document).on('click', '.nav-tabs a', function (e) {
        e.preventDefault(); $(this).tab('show');
    });

    $("#tblarticles").tablesorter({
        theme: 'blue',

        // hidden filter input/selects will resize the columns, so try to minimize the change
        widthFixed: true,

        // initialize zebra striping and filter widgets
        widgets: ["zebra", "filter"],

        // headers: { 5: { sorter: false, filter: false } },

        widgetOptions: {

            // extra css class applied to the table row containing the filters & the inputs within that row
            filter_cssFilter: '',

            // If there are child rows in the table (rows with class name from "cssChildRow" option)
            // and this option is true and a match is found anywhere in the child row, then it will make that row
            // visible; default is false
            filter_childRows: false,

            // if true, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: false,

            // Set this option to false to make the searches case sensitive
            filter_ignoreCase: true,

            // jQuery selector string of an element used to reset the filters
            filter_reset: '.reset',

            // Delay in milliseconds before the filter widget starts searching; This option prevents searching for
            // every character while typing and should make searching large tables faster.
            filter_searchDelay: 300,

            // Set this option to true to use the filter to find text from the start of the column
            // So typing in "a" will find "albert" but not "frank", both have a's; default is false
            filter_startsWith: false,

            // if false, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: false,

            // Add select box to 4th column (zero-based index)
            // each option has an associated function that returns a boolean
            // function variables:
            // e = exact text from cell
            // n = normalized value returned by the column parser
            // f = search filter input value
            // i = column index
            filter_functions: {



            }

        }

    });

    $("#tblarticlesPublished").tablesorter({
        theme: 'blue',

        // hidden filter input/selects will resize the columns, so try to minimize the change
        widthFixed: true,

        // initialize zebra striping and filter widgets
        widgets: ["zebra", "filter"],

        // headers: { 5: { sorter: false, filter: false } },

        widgetOptions: {

            // extra css class applied to the table row containing the filters & the inputs within that row
            filter_cssFilter: '',

            // If there are child rows in the table (rows with class name from "cssChildRow" option)
            // and this option is true and a match is found anywhere in the child row, then it will make that row
            // visible; default is false
            filter_childRows: false,

            // if true, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: false,

            // Set this option to false to make the searches case sensitive
            filter_ignoreCase: true,

            // jQuery selector string of an element used to reset the filters
            filter_reset: '.reset',

            // Delay in milliseconds before the filter widget starts searching; This option prevents searching for
            // every character while typing and should make searching large tables faster.
            filter_searchDelay: 300,

            // Set this option to true to use the filter to find text from the start of the column
            // So typing in "a" will find "albert" but not "frank", both have a's; default is false
            filter_startsWith: false,

            // if false, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: false,

            // Add select box to 4th column (zero-based index)
            // each option has an associated function that returns a boolean
            // function variables:
            // e = exact text from cell
            // n = normalized value returned by the column parser
            // f = search filter input value
            // i = column index
            filter_functions: {



            }

        }

    });


    $("#tblarticlesArchived").tablesorter({
        theme: 'blue',

        // hidden filter input/selects will resize the columns, so try to minimize the change
        widthFixed: true,

        // initialize zebra striping and filter widgets
        widgets: ["zebra", "filter"],

        // headers: { 5: { sorter: false, filter: false } },

        widgetOptions: {

            // extra css class applied to the table row containing the filters & the inputs within that row
            filter_cssFilter: '',

            // If there are child rows in the table (rows with class name from "cssChildRow" option)
            // and this option is true and a match is found anywhere in the child row, then it will make that row
            // visible; default is false
            filter_childRows: false,

            // if true, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: false,

            // Set this option to false to make the searches case sensitive
            filter_ignoreCase: true,

            // jQuery selector string of an element used to reset the filters
            filter_reset: '.reset',

            // Delay in milliseconds before the filter widget starts searching; This option prevents searching for
            // every character while typing and should make searching large tables faster.
            filter_searchDelay: 300,

            // Set this option to true to use the filter to find text from the start of the column
            // So typing in "a" will find "albert" but not "frank", both have a's; default is false
            filter_startsWith: false,

            // if false, filters are collapsed initially, but can be revealed by hovering over the grey bar immediately
            // below the header row. Additionally, tabbing through the document will open the filter row when an input gets focus
            filter_hideFilters: false,

            // Add select box to 4th column (zero-based index)
            // each option has an associated function that returns a boolean
            // function variables:
            // e = exact text from cell
            // n = normalized value returned by the column parser
            // f = search filter input value
            // i = column index
            filter_functions: {



            }

        }

    });

    if ($('#userprofiles').length > 0) {
        $("#tblUserProfiles").tablesorter({
            theme: 'blue',

            // hidden filter input/selects will resize the columns, so try to minimize the change
            widthFixed: true,

            // initialize zebra striping and filter widgets
            widgets: ["zebra", "filter"]


        });

    }



    if ($('#Categories').length > 0) {
        $("#tblCatagories").tablesorter({
            theme: 'blue',

            // hidden filter input/selects will resize the columns, so try to minimize the change
            widthFixed: true,

            // initialize zebra striping and filter widgets
            widgets: ["zebra", "filter"],
            widgetOptions: {
                filter_columnFilters: false
            }
        });
    }



    if ($('#frontpage').length > 0) {
        $("#tblBreakingNews").tablesorter({
            theme: 'blue',
            //        headers: { 0: { filter: false} },
            //        headers: { 1: { filter: false} },
            //        headers: { 2: { filter: false} },
            // hidden filter input/selects will resize the columns, so try to minimize the change
            widthFixed: true,

            // initialize zebra striping and filter widgets
            widgets: ["zebra", "filter"],
            widgetOptions: {
                filter_columnFilters: false
            }
        });
        for (var i = 0; i < 5; i++) {
            var value = $('#ddl' + i).val();
            if (value.length != 0) {
                $('#txtnews' + i).val('');
                $('#txtnews' + i).prop('disabled', true);
            } else {
                $('#txtnews' + i).prop('disabled', false);
            }
        }

        $('#ddl0').change(function () {
            var value = $(this).val();
            if (value.length != 0) {
                $('#txtnews0').val('');
                $('#txtnews0').prop('disabled', true);
            } else {
                $('#txtnews0').prop('disabled', false);
            }
        });
        $('#ddl1').change(function () {
            var value = $(this).val();
            if (value.length != 0) {
                $('#txtnews1').val('');
                $('#txtnews1').prop('disabled', true);
            } else {
                $('#txtnews1').prop('disabled', false);
            }
        });
        $('#ddl2').change(function () {
            var value = $(this).val();
            if (value.length != 0) {
                $('#txtnews2').val('');
                $('#txtnews2').prop('disabled', true);
            } else {
                $('#txtnews2').prop('disabled', false);
            }
        });
        $('#ddl3').change(function () {
            var value = $(this).val();
            if (value.length != 0) {
                $('#txtnews3').val('');
                $('#txtnews3').prop('disabled', true);
            } else {
                $('#txtnews3').prop('disabled', false);
            }
        });
        $('#ddl4').change(function () {
            var value = $(this).val();
            if (value.length != 0) {
                $('#txtnews4').val('');
                $('#txtnews4').prop('disabled', true);
            } else {
                $('#txtnews4').prop('disabled', false);
            }
        });
    }





    $('a[data-rel^="prettyPhoto"]').each(function () {

        $(this).attr('rel', $(this).data('rel'));

    });
    $("a[rel^='prettyPhoto']").prettyPhoto({
        animation_speed: 'fast', /* fast/slow/normal */
        slideshow: 5000, /* false OR interval time in ms */
        autoplay_slideshow: false, /* true/false */
        opacity: 0.80, /* Value between 0 and 1 */
        show_title: true, /* true/false */
        allow_resize: true, /* Resize the photos bigger than viewport. true/false */
        default_width: 500,
        default_height: 344,
        counter_separator_label: '/', /* The separator for the gallery counter 1 "of" 2 */
        theme: 'dark_square', /* light_rounded / dark_rounded / light_square / dark_square / facebook */
        horizontal_padding: 5, /* The padding on each side of the picture */
        hideflash: false, /* Hides all the flash object on a page, set to TRUE if flash appears over prettyPhoto */
        wmode: 'opaque', /* Set the flash wmode attribute */
        autoplay: true, /* Automatically start videos: True/False */
        modal: false, /* If set to true, only the close button will close the window */
        deeplinking: true, /* Allow prettyPhoto to update the url to enable deeplinking. */
        overlay_gallery: true, /* If set to true, a gallery will overlay the fullscreen image on mouse over */
        keyboard_shortcuts: true, /* Set to false if you open forms inside prettyPhoto */
        changepicturecallback: function () { }, /* Called everytime an item is shown/changed */
        callback: function () { }, /* Called when prettyPhoto is closed */
        ie6_fallback: true,
        markup: '<div class="pp_pic_holder"> \
						<div class="ppt">&nbsp;</div> \
						<div class="pp_top"> \
							<div class="pp_left"></div> \
							<div class="pp_middle"></div> \
							<div class="pp_right"></div> \
						</div> \
						<div class="pp_content_container"> \
							<div class="pp_left"> \
							<div class="pp_right"> \
								<div class="pp_content"> \
									<div class="pp_loaderIcon"></div> \
									<div class="pp_fade"> \
										<div class="pp_topHoverContainer"> \
										<a href="#" class="pp_expand" title="Expand the image"><i class="icon-fullscreen"></i></a> \
										<a class="pp_close" href="#"><i class="icon-remove"></i></a> \
										</div> \
										<div class="pp_hoverContainer"> \
											<a class="pp_next" href="#"><span><i class="icon-chevron-right"></i></span></a> \
											<a class="pp_previous" href="#"><span><i class="icon-chevron-left"></i></span></a> \
										</div> \
										<div id="pp_full_res"></div> \
										<div class="pp_details"> \
											<p class="pp_description"></p> \
										</div> \
									</div> \
								</div> \
							</div> \
							</div> \
						</div> \
						<div class="pp_bottom"> \
							<div class="pp_left"></div> \
							<div class="pp_middle"></div> \
							<div class="pp_right"></div> \
						</div> \
					</div> \
					<div class="pp_overlay"></div>',
        gallery_markup: '<div class="pp_gallery"> \
								<a href="#" class="pp_arrow_previous">Previous</a> \
								<div> \
									<ul> \
										{gallery} \
									</ul> \
								</div> \
								<a href="#" class="pp_arrow_next">Next</a> \
							</div>',
        image_markup: '<img id="fullResImage" src="{path}" />',
        flash_markup: '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="{width}" height="{height}"><param name="wmode" value="{wmode}" /><param name="allowfullscreen" value="true" /><param name="allowscriptaccess" value="always" /><param name="movie" value="{path}" /><embed src="{path}" type="application/x-shockwave-flash" allowfullscreen="true" allowscriptaccess="always" width="{width}" height="{height}" wmode="{wmode}"></embed></object>',
        quicktime_markup: '<object classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" codebase="http://www.apple.com/qtactivex/qtplugin.cab" height="{height}" width="{width}"><param name="src" value="{path}"><param name="autoplay" value="{autoplay}"><param name="type" value="video/quicktime"><embed src="{path}" height="{height}" width="{width}" autoplay="{autoplay}" type="video/quicktime" pluginspage="http://www.apple.com/quicktime/download/"></embed></object>',
        iframe_markup: '<iframe src ="{path}" width="{width}" height="{height}" frameborder="no"></iframe>',
        inline_markup: '<div class="pp_inline">{content}</div>',
        custom_markup: '',
        social_tools: '<div class="pp_social"><div class="twitter"><a href="http://twitter.com/share" class="twitter-share-button" data-count="none">Tweet</a><script type="text/javascript" src="http://platform.twitter.com/widgets.js"></script></div><div class="facebook"><iframe src="http://www.facebook.com/plugins/like.php?locale=en_US&href=' + location.href + '&amp;layout=button_count&amp;show_faces=true&amp;width=500&amp;action=like&amp;font&amp;colorscheme=light&amp;height=23" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:500px; height:23px;" allowTransparency="true"></iframe></div></div>' /* html or false to disable */
    });



    //    for (var i = 0; i < 5; ++i) {
    //        $('#ddl' + i).change(function () {
    //            var value = $(this).val();
    //            var customnews;
    //            if (i == 0) customnews = $('#txtnews0');
    //            if (i == 1) customnews = $('#txtnews1');
    //            if (i == 2) customnews = $('#txtnews2');
    //            if (i == 3) customnews = $('#txtnews3');
    //            if (i == 4) customnews = $('#txtnews4');

    //            if (value.length != 0) {
    //                //alert(value.length);

    //                alert(customnews.val());
    //                //customnews.disabled = true;
    //                //customnews.val('');
    //                //$('#txtnews' + i).val('bkjbbkb');
    //                //$('#txtnews' + i).prop('disabled', true);

    //            } else {
    //                alert(value.length);
    //                $('#txtnews' + i).prop('disabled', false);
    //            }
    //            // alert($(this).val());

    //        });
    //    }



});



        // Avoid `console` errors in browsers that lack a console.
        if (!(window.console && console.log)) {
            (function () {
                var noop = function () { };
                var methods = ['assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error', 'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log', 'markTimeline', 'profile', 'profileEnd', 'markTimeline', 'table', 'time', 'timeEnd', 'timeStamp', 'trace', 'warn'];
                var length = methods.length;
                var console = window.console = {};
                while (length--) {
                    console[methods[length]] = noop;
                }
            } ());
        }

 