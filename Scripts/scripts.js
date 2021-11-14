//Function to display spinner
$(document).ready(function () {
    $("#loaderbody").addClass("hide");

    $(document).bind('ajaxStart', function () {
        $('#loaderbody').removeClass('hide');
    }).bind('ajaxStop', function () {
        $('#loaderbody').addClass('hide');
    });
});

//Script for jQuery Ajax CRUD operation

//Function to show preview of selected image
function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr("src", e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}
var dataTable;
//Function to Post form data to controller
function jQueryAjaxPost(form) {
    //ensure client side validation is done
    $.validator.unobtrusive.parse(form);
    //check if validation successfull
    if ($(form).valid())
    {
        var ajaxConfig = {
            method: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (response) {
                if (response) {
                    $('#firstTab').html(response);
                    
                    //Function call here
                    refreshAddNewTab($(form).attr('data-resetUrl'), true);
                    //success message
                    $.notify(response.message, "success");

                    if (typeof activatejQueryTable !== "undefined" && $.isFunction(activatejQueryTable)) {
                        activatejQueryTable();
                        dataTable.ajax.reload();
                    }
                }
                else {
                    //error message
                    $.notify(response.message, "error");
                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}

//To refresh the Add new Tab after inserting a record and Show fresh form.
function refreshAddNewTab(resetUrl, showViewTab)
{
    $.ajax({
        method: "GET",
        url: resetUrl,
        success: function (response) {
            $('#secondTab').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if(showViewTab)
            {
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
            }
        }
    });
}

//To Edit record
function Edit(url) {
    $.ajax({
        method: "GET",
        url: url,
        success: function (response) {
            $('#secondTab').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
            
        }
    });
}

//To Delete record
function Delete(url)
{
    if (confirm('Are you Sure to Delete this record ?') == true)
    {
        $.ajax({
            method: "POST",
            url: url,
            success: function (response) {
                if (response.success) {
                    $('#firstTab').html(response.html);
                    /*$('#firstTab').ajax.reload();*/
                    //warning message
                    $.notify(response.message, "warn");
                    if (typeof activatejQueryTable !== "undefined" && $.isFunction(activatejQueryTable)) {
                        activatejQueryTable();
                    }
                    
                }

                else {
                    //error message
                    $.notify(response.message, "error");
                }

            }
        });
    }
}
