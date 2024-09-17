const messages = $('.app-messages');

window.addEventListener('load', function () {
    ScrollBottom();
});

function ScrollBottom() {
    messages.scrollTop(messages[0].scrollHeight);
}

$(document).ready(function () {
    $('#buttonSend').click(function () {
        //debugger;
        var action = 'Room/SendMessage';
        var message = $('#message').val();
        $.get(action,
            { message: message },
            function (data) {
                $('.app-messages').html(data); //replaceWith
                ScrollBottom();
            });
    });
});