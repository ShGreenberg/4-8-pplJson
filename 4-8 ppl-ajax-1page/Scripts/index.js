$(() => {
    const addPplToTable = person => {
        $('#ppl-table').append(`<tr>
                                    <td>${person.FirstName}</td >
                                    <td>${person.LastName}</td>
                                    <td>${person.Age}</td>
                                    <td><button data-target="#edit-modal" 
                                        data-id="${person.Id}" class="btn btn-success btn-edit">Edit</button></td>
                                    <td><button data-id="${person.Id}" class="btn btn-danger btn-delete">
                                         Delete</button></td>
                                    </tr>`);
        //data - toggle="modal"
    }

    $.get("/home/getppl", function (people) {
        people.forEach(addPplToTable);
    });

    $("#add-person").on('click', function () {

        FirstName = $("#first-name").val();
        LastName = $("#last-name").val();
        Age = $("#age").val();

        $.get("/home/addperson", { FirstName, LastName, Age }, function (people) {
            $("#ppl-table").find("tr:gt(0)").remove();
            people.forEach(addPplToTable);

            FirstName = $("#first-name").val("");
            LastName = $("#last-name").val("");
            Age = $("#age").val("");
        });



    });


    $("#ppl-table").on('click', '.btn-edit', function () {

   // })
    //$(".btn-edit").on('click', function () {
        console.log("hi");
        const id = $(this).data("id"); 

        $.get("/home/getperson", { id }, function (person) {

            $("#edit-fn").val(person.FirstName);
            $("#edit-ln").val(person.LastName);
            $("#edit-age").val(person.Age);
            $("#edit-modal").modal('toggle');
        });
        $("#submit-edit").on('click', function () {
            console.log("hello");
            const FirstName = $("#edit-fn").val();
            const LastName = $("#edit-ln").val();
            const Age = $("#edit-age").val();
            const Id = id;
            $.post("/home/updateperson", { FirstName, LastName, Age, Id  }, function (people) {
                $("#ppl-table").find("tr:gt(0)").remove();
                people.forEach(addPplToTable);
                $("#edit-modal").modal('toggle');
            });
        });

    });

    $("#ppl-table").on('click', '.btn-delete', function () {
        console.log('hi');
        const id = $(this).data("id");
        $.get("/home/delete", { id }, function (people) {
            $("#ppl-table").find("tr:gt(0)").remove();
            people.forEach(addPplToTable);
        });
    });
   
});