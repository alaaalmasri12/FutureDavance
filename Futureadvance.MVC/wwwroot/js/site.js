function loadusers() {
    $.ajax({
        url: '/Admin/AdminDashbored/GetCurrentUser',
        type: 'GET', // or 'POST' if needed
        success: function (response, statusText, xhr) {
            console.log(response)
            $('#userstable').DataTable({
                "ajax": {
                    url: "https://localhost:7241/api/userAuth/GetAllusers",
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json",
                    data: { UserID: response.id }, // Parameters to pass to the controller action

                },
                "searching": true, // Enable search feature
                "columnDefs": [{
                    "targets": 'form-control', // Apply search to columns with class 'searchable'
                    "searchable": true
                }],
                "columns": [
                    { data: 'userName' },
                    { data: 'email' },
                    { data: 'role' },
                    {
                        data: null,
                        render: function (row) {
                            return '<div class="m-75 btn-group"><a href=/Admin/AdminDashbored/Edit/' + row.id + ' class="btn btn-primary mx-2"> Edit</a > <a href=/Admin/AdminDashbored/Delete/' + row.id + ' class="btn btn-danger mx-2">Delete</a></div > '
                        }
                    } // Ensure the property names match the actual JSON response

                ]
            });
            $('div.dataTables_filter input').addClass('form-control');
            $('div.dataTables_length select').addClass('form-control');
        },
        error: function (xhr, status, error) {
            // Handle errors
            if (xhr.status == 409) {
                toastr.error('You Already liked the post')
            }
            console.log(status);
            console.error(error);
        }
    });
    
}