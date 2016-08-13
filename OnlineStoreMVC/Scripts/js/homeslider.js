$(document).ready(function () {
    if (typeof (homeslider_speed) == 'undefined')
        homeslider_speed = 500; if (typeof (homeslider_pause) == 'undefined')
            homeslider_pause = 3000; if (typeof (homeslider_loop) == 'undefined')
                homeslider_loop = true; var tl = new TimelineMax(); tl.from(".homeslider-description h2", 0.3, { top: -100, autoAlpha: 0 }).from(".homeslider-description p", 0.3, { left: -600, autoAlpha: 0 }).from(".homeslider-description button", 0.1, { bottom: -100, autoAlpha: 0 }); function pagSel() {
                    $('#bx-pager-thumb a').removeClass('prev , next'); if (!$('#bx-pager-thumb a:first-child').hasClass('active')) { $('#bx-pager-thumb a.active').prev().addClass('prev'); } else { $('#bx-pager-thumb a:last-child').addClass('prev'); }
                    if (!$('#bx-pager-thumb a:last-child').hasClass('active')) { $('#bx-pager-thumb a.active').next().addClass('next'); } else { $('#bx-pager-thumb a:first-child').addClass('next'); }
                }
    if (!!$.prototype.bxSlider)
        $('#homeslider').bxSlider({ mode: 'fade', useCSS: false, maxSlides: 1, slideWidth: homeslider_width, infiniteLoop: homeslider_loop, hideControlOnEnd: true, pager: true, autoHover: true, autoControls: false, auto: homeslider_loop, speed: parseInt(homeslider_speed), pause: homeslider_pause, controls: true, startText: '', stopText: '', pagerCustom: '#bx-pager-thumb', onSliderLoad: function () { tl.play(); $('#bx-pager-thumb a:last-child').addClass('prev'); $('#bx-pager-thumb a').eq(1).addClass('next'); }, onSlideBefore: function () { tl.restart() }, onSlideAfter: function () { pagSel(); } }); $('.homeslider-description').click(function () { window.location.href = $(this).prev('a').prop('href'); }); $("#homepage-slider").on("mouseover", function () { $("#bx-pager-thumb", this).addClass("active"); })
    $("#homepage-slider").on("mouseout", function () { $("#bx-pager-thumb", this).removeClass("active"); })
});