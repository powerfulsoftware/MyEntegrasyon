// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



setInterval(function () {
    $.ajax({
        url: '/files/keep-alive.jpg',
        method: 'GET',
        cache: false,
        success: function () {

        }
    })
},1000)