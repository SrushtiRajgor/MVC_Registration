function fetchEmployeeList() {
    debugger;
    $.ajax({
        url: '/Home/SelectEmployeeList',
        type: 'GET',
        data: null,
        contentType: JSON,
        success: function (data) {
            
            if (data.length > 0) {
                let employees = data;
                $('#userTable tbody').empty();
                employees.forEach(function (employee) {
                    var id = employee.Id;
                    var newRow = '<tr data-id="new" rowIdentifier = ' + id + '>' +
                        '<td>' + employee.FirstName + '</td>' +
                        '<td>' + employee.LastName + '</td>' +
                        '<td>' + employee.Email + '</td>' +
                        '<td>' + employee.Birthdate + '</td>' +
                        '<td>' + employee.PhoneNumber + '</td>' +
                        '<td>' + employee.Address + '</td>' +
                        '<td>' + employee.City + '</td>' +
                        '<td>' + employee.State + '</td>' +
                        '<td><button class="btn btn-primary mb-2 small-btn mr-2"> Update </button> <br/> <button class="btn btn-danger small-btn mr-2"> Delete </button> </td>' +

                        '</tr>';
                    $('#userTable tbody').append(newRow);
                });
            } else {
                alert('Failed to fetch employees: ' + data.message);
            }
        },
        error: function (xhr, status, error) {
       
            alert('An error occurred: ' + error);
        }
    });
}
$(document).ready(function () {
    fetchEmployeeList();
});

