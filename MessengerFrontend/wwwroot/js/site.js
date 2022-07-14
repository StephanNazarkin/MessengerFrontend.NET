// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.modal-link').click(function (e) {
        e.preventDefault();
        var page = $(this).attr('href');
        $('.modal-content-body').load(page);
    });

    loadMessages();
});

function loadMessages() {
    $('#messages-list-wrapper').ready(function (e) {
        let path = window.location.pathname;
        let chatId = path.substring(path.lastIndexOf('/') + 1);
        $('#messages-list-wrapper').load(window.location.origin + '/Chat/GetAllMessages/' + chatId);
    });
}