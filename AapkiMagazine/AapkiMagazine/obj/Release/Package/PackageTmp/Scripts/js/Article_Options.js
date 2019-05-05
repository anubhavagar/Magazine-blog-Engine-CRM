  
        $(function () {

            $('#btnsubmit-next').click(function (event) {

            });
            $('.close').click(function () {
                //  $(this).parent().removeClass('in'); // hides alert with Bootstrap CSS3 
                $(this).parent().hide('slow', function () { $(this).empty().append($('<button type="button" class="close">&times;</button>')); });
            });


            //            $('#btnvalidateyturl').click(function (event) {
            //                var url = $('#video_youtube_path').val();
            //                if (ytvideocheck(url) !== false) {
            //                    $("#validurlalert").empty().fadeIn(50, function () { });
            //                    $("#validurlalert").append('<p><strong>Valid url!</strong></p>');
            //                    $('#validurlalert').removeClass('alert-danger').addClass('alert-success');
            //                    $('#validurlalert').fadeOut(5000, function () { });                   

            //                } else {
            //                    $("#validurlalert").empty().fadeIn(50, function () { }); ;
            //                    $("#validurlalert").append('<p><strong>Invalid url!</strong></p>');
            //                    $('#validurlalert').removeClass('alert-sucess').addClass('alert-danger');
            //                    $('#validurlalert').fadeOut(5000, function () { });
            //                }
            //            });


            function ytvideocheck(url) {
                var p = /^(?:https?:\/\/)?(?:www\.)?youtube\.com\/watch\?(?=.*v=((\w|-){11}))(?:\S+)?$/;
                return (url.match(p)) ? RegExp.$1 : false;
            }

            //            $('#video_youtube_path').bind("change keyup input", function () {
            $('#video_youtube_path').keyup(function () {
                var url = $(this).val();
                if (url.length != 0) {
                    if (ytvideocheck(url) !== false) {
                        $("#validurlalert").empty().fadeIn(50, function () { });
                        $("#validurlalert").append('<p><strong>Valid url!</strong></p>');
                        $('#validurlalert').removeClass('alert-danger').addClass('alert-success');
                        $("#hdnvalidyturl").attr("value", "True");
                        //$('#validurlalert').fadeOut(5000, function () { });

                    } else {
                        $("#validurlalert").empty().fadeIn(50, function () { }); ;
                        $("#validurlalert").append('<p><strong>Invalid url!</strong></p>');
                        $('#validurlalert').removeClass('alert-sucess').addClass('alert-danger');
                        $("#hdnvalidyturl").attr("value", "False");
                        //$('#validurlalert').fadeOut(5000, function () { });
                    }
                }
                else {
                    $("#validurlalert").empty().fadeOut(50, function () { });
                    $("#hdnvalidyturl").attr("value", "True");
                }

            });

        });
    