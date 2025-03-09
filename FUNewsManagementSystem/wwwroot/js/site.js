"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();

connection.on("LoadNewsArticlesItem", function () {
    location.reload();
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

const jQueryModalGet = (url, title) => {
    try {
        $.ajax({
            type: 'GET',
            url: url,
            contentType: false,
            processData: false,
            success: function (res) {
                $('#form-modal .modal-body').html(res.html);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal('show');
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }

}

const jQueryModalPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#viewAll').html(res.html)
                    $('#form-modal').modal('hide');
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
const jQueryModalDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#viewAll').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}