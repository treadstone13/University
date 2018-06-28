//$(document).ready(function () {
//    $('.update').on('click', function () {
//        var bid = $(this).closest('td').parent();
//        //console.log(bid);
//        document.getElementById('EnrollmentID').value = bid[0].firstElementChild.innerHTML;
//        document.getElementById('StudentID').value = bid[0].firstElementChild.innerHTML;
//        document.getElementById('CourseID').value = bid[0].firstElementChild.innerHTML;
//        $("#myModal").modal();
//    })

//})



$(document).ready(function () {
    $('.updateC').on('click', function () {
         var bid = $(this).closest('td').parent();
         //console.log(bid);
         document.getElementById('CourseID').value = bid[0].firstElementChild.innerHTML;
         $("#myModal").modal();
    })
})
    

$(document).ready(function () {
    $('.updateS').on('click', function () {
         var bid = $(this).closest('td').parent();
         //console.log(bid);
         document.getElementById('StudentID').value = bid[0].firstElementChild.innerHTML;
         $("#myModal").modal();
    })
})           
$(document).ready(function () {
    $('.updateE').on('click', function () {
         var bid = $(this).closest('td').parent();
         //console.log(bid);
         document.getElementById('EnrollmentID').value = bid[0].firstElementChild.innerHTML;
         $("#myModal").modal();
    })
})


$(document).ready(function () {
    $('#createRoleModal').on('click', function () {                   
        $("#myModalAdd").modal();
    })
})

$(document).ready(function () {
    $('.addEditApplicationRoleModal').on('click', function () {
        var bid = $(this).closest('td').parent();
        //console.log(bid);
        document.getElementById('IdRoleView').value = bid[0].firstElementChild.innerHTML;
        $("#myModalAdd").modal();
    })
})


$(document).ready(function () {
    $('.addEditUserModal').on('click', function () {
        var bid = $(this).closest('td').parent();
        //console.log(bid);
        document.getElementById('IdRoleView').value = bid[0].firstElementChild.innerHTML;
        $("#myModalAdd").modal();
    })
})

