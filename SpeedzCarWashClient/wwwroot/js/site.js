// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteWasher(element) {
    const id = $(element).data('id');

    Swal.fire({
        title: 'Are you sure you want to delete this washer?',
        text: 'This action cannot be undone.',
        icon: 'warning',
        buttons: ['Cancel', 'Delete'],
        dangerMode: true
    }).then((result) => {
        if (result.isConfirmed) {
            // Delete the washer from the database
            // ...
        }
    });
}
