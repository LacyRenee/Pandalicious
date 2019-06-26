var IngredientViewCount = 0;
var DirectionViewCount = 0;


$(document).on("click", "button#deleteRecipe", function () {
    var id = $(this).val();
    $.ajax({
        url: "/api/Recipe/DeleteRecipe",
        type: "POST",
        contentType: "application/json",
        data: id,
        success: function (result) {
            location.reload();
        }
    })
})

// Displays a recipe when it is clicked from the table
$("a.ListOfRecipes").click(function () {
    var id = $(this).attr("data-recipeID");
    console.log(id);
    $.ajax({
        url: "/Recipe/RecipePV/",
        type: "POST",
        contentType: "application/json",
        data: id,
        success: function (result) {
            $("#DisplaySideRecipe").html(result);
        }
    })
})