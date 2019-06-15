var IngredientViewCount = 0;
var DirectionViewCount = 0;

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