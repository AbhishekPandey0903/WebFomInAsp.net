<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="LearningTaskWebform.Index" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Details</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Message">
                <asp:Button ID="Button1" runat="server" BackColor="Black" ForeColor="Lime" OnClick="Button1_Click" Text="Create" />
            </div>
          <table class="table table-success table-striped">
              <thead>
                  <tr>
                      <th scope="col">#</th>
                      <th scope="col">FirstName</th>
                      <th scope="col">LastName</th>
                      <th scope="col">DOB</th>
                      <th scope="col">Gender</th>
                      <th scope="col">Category</th>
                      <th scope="col">Contact</th>
                      <th scope="col">UploadStudentImage</th>
                      <th scope="col" colspan="2" style="margin-left:50%">Action</th>
                  </tr>
              </thead>
                <tbody id="stuData">
                </tbody>
            </table>
        </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
 <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        $(document).ready(function () {
            GetData();
        });
         function EditData(Id){ debugger
                
             var postData = { Id: Id };
             $.ajax({
                 type: "POST",
                 data: JSON.stringify(postData),
                 url: "Edit.aspx/EditData?Id=1",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     if (response.d) {
                         GetData();
                         $("#Message").html('<div class="alert alert-success" role="alert">Data Updated!</div>');
                         $("#Message").show().delay(2000).fadeOut()
                     }
                     else {
                         $("#Message").html('<div class="alert alert-danger" role="alert">Something went wrong!</div>');
                         $("#Message").show().delay(2000).fadeOut()
                     }
                 },
                 failure: function (response) {
                     alert(response.d);
                 }
             });
        };

        function DeleteData(Id) {
            var result = confirm("Are you sure?");
            if (result) {
                var postData = { Id: Id };
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(postData),
                    url: "Index.aspx/DeleteData?Id=1",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            GetData();
                            $("#Message").html('<div class="alert alert-success" role="alert">data Deleted!</div>');
                            $("#Message").show().delay(2000).fadeOut()
                        }
                        else {
                            $("#Message").html('<div class="alert alert-danger" role="alert">Something went wrong!</div>');
                            $("#Message").show().delay(2000).fadeOut()
                        }
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                }); 
            }
        };
        function GetData() {
            $.ajax({
                type: "POST",
                url: "Index.aspx/GetData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('#stuData').empty();
                    var row = "";
                    console.log(response.d[0])
                    for (i = 0; i < response.d.length; i++)
                    {
                        row += "<tr><td>" + (i+1) + "</td>"
                        row = row + "<td>" + response.d[i].FirstName + "</td>";
                        row = row + "<td>" + response.d[i].LastName + "</td>";
                        row = row + "<td>" + response.d[i].DOB + "</td>";
                        row = row + "<td>" + response.d[i].Gender + "</td>";
                        row = row + "<td>" + response.d[i].Category + "</td>";
                        row = row + "<td>" + response.d[i].Contact + "</td>";
                        row = row + "<td>" + response.d[i].UploadStudentImage + "</td>";
                        row = row + "<td><button type='button' onclick=EditData("+ response.d[i].Id +") class='btn btn-primary'>Edit</button></td>";
                        row = row + "<td><button type='button' onclick=DeleteData("+ response.d[i].Id +") class='btn btn-danger'>Delete</button></td>";
                        row = row + "</tr>";
                    } 
                    $(row).appendTo($('#stuData'));
                },
                failure: function (response) {
                    alert(response.d);
                }
            }); 
        };
    </script> 
    </form>
    </body>
</html>
