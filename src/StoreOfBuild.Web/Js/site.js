// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formOnFail(error) {
    if (error && error.status == 500)
        toastr.error(error.responseText);
}