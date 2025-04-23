function addEmployee() {
    debugger;
    var formdata = new FormData();
    formdata.append("id", $("#id").val());
    formdata.append("FirstName", $("#FirstName").val());
    formdata.append("LastName", $("#LastName").val());
    formdata.append("Email", $("#Email").val());
    formdata.append("Birthdate", $("#Birthdate").val());
    formdata.append("PhoneNumber", $("#PhoneNumber").val());
    formdata.append("Address", $("#Address").val());
    formdata.append("City", $("#City").val());
    formdata.append("State", $("#State").val());
    formdata.append("Password", $("#Password").val());
    formdata.append("ConfirmPassword", $("#ConfirmPassword").val());

    //// Fix: Correct file input selector
    //var imageFile = $("#ImagePath")[0].files[0];
    //if (imageFile) {
    //    formdata.append("ImagePath", imageFile);
    //}

    $.ajax({
        url: '/Home/SaveEmployeeData',
        type: 'POST',
        data: formdata,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.Success) {
                alert(response.message);
                if (response.isLoggedIn) {
                    window.location.href = '/Home/Index';
                } else {
                    window.location.href = '/Home/Login';
                }
            } else {
                alert("Registration failed: ");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("Error: " + textStatus + " - " + errorThrown);
        }
    });
}

function updateEmployee(id) {
    debugger;
    var formdata = new FormData();
    formdata.append("Id", id);
    formdata.append("FirstName", $('#FirstName').val());
    formdata.append("LastName", $('#LastName').val());
    formdata.append("Email", $('#Email').val());
    formdata.append("Birthdate", $('#Birthdate').val());
    formdata.append("PhoneNumber", $('#PhoneNumber').val());
    formdata.append("Address", $('#Address').val());
    formdata.append("City", $('#City').val());
    formdata.append("State", $('#State').val());
    formdata.append("Password", $('#Password').val());
    formdata.append("ConfirmPassword", $('#ConfirmPassword').val());

    var row = $('tr[data-id="' + id + '"]');

    $.ajax({
        url: '/Home/UpdateEmployee',
        type: 'POST',
        data: formdata,
        processData: false,
        contentType: false, 
        success: function (data) {
            if (data.Success) {
                alert(data.message);
                row.find('td:nth-child(1)').text($('#FirstName').val());
                row.find('td:nth-child(2)').text($('#LastName').val());
                row.find('td:nth-child(3)').text($('#Email').val());
                row.find('td:nth-child(4)').text($('#Birthdate').val());
                row.find('td:nth-child(5)').text($('#PhoneNumber').val());
                row.find('td:nth-child(6)').text($('#Address').val());
                row.find('td:nth-child(7)').text($('#City').val());
                row.find('td:nth-child(8)').text($('#State').val());
            } else {
                alert('Failed to update: ' + data.message);
            }
        },
        error: function (xhr, status, error) {
            alert('An error occurred: ' + error);
        }
    });
}
    $('#InsertButton').click(function (e) {
    
    e.preventDefault(); 
    var form = $("#registrationForm");
   /*form.validate().form();
*/    if (form.valid())
    {
        var id = $('#Id').val();
        if (id > 0) {
            updateEmployee(id);
        } else {
            addEmployee();
        }
}
/*else {
        alert("Please fix validation errors before submitting.");
    }
*/
});

    $('#ClearButton').click(function () {
        debugger;
        $('#FirstName').val("");
        $('#LastName').val("");
        $('#Email').val("");
        $('#Birthdate').val("");
        $('#PhoneNumber').val("");
        $('#Address').val("");
        $('#City').val("");
        $('#State').val("");
        $('#Password').val("");
        $('#ConfirmPassword').val("");
        alert("Form has been cleared!");
    });
$(document).on('click', '.UpdateButton', function () {
    debugger;
    var row = $(this).closest('tr');
    var id = row.attr('rowIdentifier');
    var FirstName = row.find('td:nth-child(1)').text();
    var LastName = row.find('td:nth-child(2)').text();
    var Email = row.find('td:nth-child(3)').text();
    var Birthdate = row.find('td:nth-child(4)').text();
    var PhoneNumber = row.find('td:nth-child(5)').text();
    var Address = row.find('td:nth-child(6)').text();
    var City = row.find('td:nth-child(7)').text();
    var State = row.find('td:nth-child(8)').text();

    var updatedData = {
        Id: id,
        FirstName: FirstName,
        LastName: LastName,
        Email: Email,
        Birthdate: Birthdate,
        PhoneNumber: PhoneNumber,
        Address: Address,
        City: City,
        State: State
    };

    $('#FirstName').val(row.find('td:nth-child(1)').text());
    $('#LastName').val(row.find('td:nth-child(2)').text());
    $('#Email').val(row.find('td:nth-child(3)').text());
    $('#Birthdate').val(row.find('td:nth-child(4)').text());
    $('#PhoneNumber').val(row.find('td:nth-child(5)').text());
    $('#Address').val(row.find('td:nth-child(6)').text());
    $('#City').val(row.find('td:nth-child(7)').text());
    $('#State').val(row.find('td:nth-child(8)').text());
    $('#Id').val(row.attr('rowIdentifier'));

});
$(document).on('click', '.DeleteButton', function () {
    var row = $(this).closest('tr');
    var id = row.attr('rowIdentifier');

    if (confirm('Are you sure you want to delete this employee?')) {

        $.ajax({
            url: '/Home/DeleteEmployee',
            type: 'POST',
            data: { id: id },
            success: function (data) {
                if (data.Success) {
                    row.remove();
                    alert(data.message);
                } else {
                    alert('Failed to delete: ' + data.message);
                }
            },
            error: function (xhr, status, error) {
                alert('An error occurred: ' + error);
            }
        });
    }
});
