const messages = $('.app-messages');

window.addEventListener('load', function () {
    messages.scrollTop(messages[0].scrollHeight);
});