// Resets the Add Ingredient Modal
$("button#CancelAddIngredientModal").click(function () {
    var form = $('form#AddIngredientForm');
    form.trigger("reset");
    $("#IngredientNameInput").empty();
    $("#IngredientUnitInput").empty();
});

// Edit an existing ingredient
$("button#EditIngredient").click(function () {
    var IngredientId = $(this).val();
    var name = $(this).attr("data-name");
    var unit = $(this).attr("data-unit");
    var array = unit.split(" ");

    $("input#EditIngredientNameInput").val(name);
    $("input#EditIngredientUnitInput").val(array[0]);
    $("input#IngredientId").val(IngredientId);
    $("#EditIngredientModal").modal("toggle");
})

// Save the new ingredient
$("button#SaveIngredientButton").click(function () {
    var IngredientId = $("#IngredientId").val();
    var IngredientName = $("#EditIngredientNameInput").val();
    var IngredientUnit = $("#EditIngredientUnitInput").val() + " " + $("#EditIngredientUnitDropdownList").val();

    var JsonData = JSON.stringify({IngredientId: IngredientId, IngredientName: IngredientName, IngredientUnit: IngredientUnit});

    $.ajax({
        url: "/api/Ingredient/SaveIngredient/",
        type: "POST",
        contentType: "application/json",
        data: JsonData,
        success: function (result) {
            location.reload();
            $("#EditIngredientNameInputErrorMessage").text("");
        },
        error: function (xhr, status, error) {
        }
    })
})

// Add the new ingredient to the database
$("button#CreateIngredientButton").click(function () {
    var IngredientName = $("#IngredientNameInput").val();
    var IngredientUnit = $("#IngredientUnitInput").val() + " " + $("#IngredientUnitDropdownList").val();

    var JsonData = JSON.stringify({IngredientName: IngredientName, IngredientUnit: IngredientUnit});

    $.ajax({
        url: "/api/Ingredient/AddIngredient/",
        type: "POST",
        contentType: "application/json",
        data: JsonData,
        success: function (result) {
            location.reload();
            $("#IngredientNameInputErrorMessage").text("");
        },
        error: function (xhr, status, error) {
            var err = JSON.parse(xhr.responseText);
            $("#IngredientNameInputErrorMessage").html(err.errors.IngredientName[0]);
        }
    })
})

// Remove the ingredient from the database
$("button#RemoveFromList").click(function () {
    var IngredientId = $("#RemoveFromList").attr("data-IngredientId");
    console.log(IngredientId);

    $.ajax({
        url: "/api/Ingredient/DeleteIngredient/",
        type: "POST",
        contentType: "application/json",
        data: IngredientId,
        success: function (result) {
            console.log(result);
            location.reload();
        }
    })
})