/*
var ps = new PerfectScrollbar('.sidebar-nav');
var ps = new PerfectScrollbar('.notifications-box');
//Sidebar Toogle
(function() {
    $('.menu-link-wrapper').on('click.mobileNav', function(){
        $('.sidebar').animate({width:'toggle'},0);
        $('.menu-link-wrapper .menu-link').toggleClass('menu-trigger-open');
        var sidebarstatus = $('#wrap').hasClass('sidebar-active');
        if(sidebarstatus===true){
            $('#wrap').removeClass('sidebar-active');
        }else{
            $('#wrap').addClass('sidebar-active');
        }
    });
})();
function menu(){
    if($(window).width()>768){
        $('.sidebar').show();
        $('#wrap').addClass('sidebar-active');
        $('.menu-link').addClass('menu-trigger-open');
    }else{
        $('.sidebar').hide();
        $('#wrap').removeClass('sidebar-active');
        $('.menu-link').removeClass('menu-trigger-open');
    }
}
$(document).ready(function () {
    menu();
});
window.onresize = function (event) {
    menu();
}
$(document).ready(function () {
    //Submenu
    var i = 1;
    $('.submenu').each(function () {
        $(this).children('.collapse').before('<div class="clearfix"></div>');
        $(this).children('a').attr("data-toggle","collapse");
        $(this).children('a').attr("href","#navsubmenu"+i+"");
        $(this).children('a').attr("aria-controls","navsubmenu"+i+"");
        $(this).children('.collapse').attr("id","navsubmenu"+i+"");
        if($(this).hasClass('active')==true){
            $(this).children('.nav-link').attr("aria-expanded","true");
            $(this).children('.collapse').addClass('show');
        }else{
            $(this).children('.nav-link').attr("aria-expanded","false");
        }
        i = i+1;
    });
    $(".submenu .nav-link").click(function () {
        $('.submenu').each(function () {
            $(this).removeClass('active');
        });
        if($(this).attr('aria-expanded')=='false'){
            $(this).parent().addClass('active');
        }else{
            $(this).parent().removeClass('active');
        }
    });
    //Fullscreen
    $(".full-screen-open a").click(function () {
        $('body').fullscreen();
        $('body').addClass('fullscreen');
        return false;
    });
    // Exit Fullscreen
    $(".full-screen-close a").click(function () {
        $.fullscreen.exit();
        $('body').removeClass('fullscreen');
        return false;
    });
    // slideDown animation to Bootstrap dropdown
    $('.dropdown').on('show.bs.dropdown', function() {
        $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
    });
    // slideUp animation to Bootstrap dropdown
    $('.dropdown').on('hide.bs.dropdown', function() {
        $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
    });
    //Tooltip
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    });
});*/
var ps=new PerfectScrollbar(".sidebar-nav");ps=new PerfectScrollbar(".notifications-box");function menu(){$(window).width()>768?($(".sidebar").show(),$("#wrap").addClass("sidebar-active"),$(".menu-link").addClass("menu-trigger-open")):($(".sidebar").hide(),$("#wrap").removeClass("sidebar-active"),$(".menu-link").removeClass("menu-trigger-open"))}$(".menu-link-wrapper").on("click.mobileNav",function(){$(".sidebar").animate({width:"toggle"},0),$(".menu-link-wrapper .menu-link").toggleClass("menu-trigger-open"),!0===$("#wrap").hasClass("sidebar-active")?$("#wrap").removeClass("sidebar-active"):$("#wrap").addClass("sidebar-active")}),$(document).ready(function(){menu()}),window.onresize=function(e){menu()},$(document).ready(function(){var e=1;$(".submenu").each(function(){$(this).children(".collapse").before('<div class="clearfix"></div>'),$(this).children("a").attr("data-toggle","collapse"),$(this).children("a").attr("href","#navsubmenu"+e),$(this).children("a").attr("aria-controls","navsubmenu"+e),$(this).children(".collapse").attr("id","navsubmenu"+e),1==$(this).hasClass("active")?($(this).children(".nav-link").attr("aria-expanded","true"),$(this).children(".collapse").addClass("show")):$(this).children(".nav-link").attr("aria-expanded","false"),e+=1}),$(".submenu .nav-link").click(function(){$(".submenu").each(function(){$(this).removeClass("active")}),"false"==$(this).attr("aria-expanded")?$(this).parent().addClass("active"):$(this).parent().removeClass("active")}),$(".full-screen-open a").click(function(){return $("body").fullscreen(),$("body").addClass("fullscreen"),!1}),$(".full-screen-close a").click(function(){return $.fullscreen.exit(),$("body").removeClass("fullscreen"),!1}),$(".dropdown").on("show.bs.dropdown",function(){$(this).find(".dropdown-menu").first().stop(!0,!0).slideDown()}),$(".dropdown").on("hide.bs.dropdown",function(){$(this).find(".dropdown-menu").first().stop(!0,!0).slideUp()}),$(function(){$('[data-toggle="tooltip"]').tooltip()})});