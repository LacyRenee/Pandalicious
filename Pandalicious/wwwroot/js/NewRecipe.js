var IngredientViewCount = 0;
var DirectionViewCount = 0;

// Displays a recipe when it is clicked from the table
$("a.ListOfRecipes").click(function () {
    var id = $(this).attr("data-recipeID");
    $.ajax({
        url: "/Recipe/RecipePV/",
        type: "GET",
        contentType: "application/json",
        data: id,
        success: function (result) {
            $("#DisplaySideRecipe").html(result);
        }
    })
})

// Create the recipe
$("a#FinishRecipe").click(function () {
    var recipe = $("#RecipeForm").serializeArray();
    var data = JSON.stringify(recipe);
    console.log(data);
    var count = 1;
    $.ajax({
        url: "/api/Recipe/CreateRecipe",
        type: "POST",
        contentType: "application/json",
        data: data,
        success: function (result) {
           window.location.href = "/Recipe/Recipes";
        }
    })
})

// Populates the DataList with all of the ingredients from the database
$(document).ready(function () {
   $.ajax({
        url: "/api/Recipe/IngredientList/",
        type: "GET",
        success: function (ingredientList) {
           var list = document.getElementById("ListOfIngredients");

            ingredientList.forEach(function(item){
               var option = document.createElement('option');
               option.value = item.IngredientName;
               list.appendChild(option);
            });
        }
    })
})

// Adds a direction template to the form
$(document).on("click", ".AddDirection", function(){
    DirectionViewCount++;
    var divID = "DirectionView"+ DirectionViewCount;
    var buttonID = "DirectionBtnID" + DirectionViewCount;

    // Retrieve the template data from the HTML (jQuery is used here).
    var template = $("#DirectionHandlebar").html();

    // Compile the template data into a function
    var templateScript = Handlebars.compile(template);

    // Create the content
    var context = {DirectionDivID: divID, DirectionBtnID: buttonID};

    var html = templateScript(context);

    // Insert the HTML code into the page
    $("#directionTemplate").append(html);

    // Change the previous ingredient button to remove
    document.getElementById("DirectionBtnID" + (DirectionViewCount - 1)).innerHTML = "Remove";
    document.getElementById("DirectionBtnID" + (DirectionViewCount - 1)).classList.remove("success-border");
    document.getElementById("DirectionBtnID" + (DirectionViewCount - 1)).classList.add("danger-border");
    document.getElementById("DirectionBtnID" + (DirectionViewCount - 1)).classList.remove("AddDirection");
    document.getElementById("DirectionBtnID" + (DirectionViewCount - 1)).classList.add("RemoveDirection");
    NumberDirections(); 
});

// Removes the direction template from the form
$(document).on("click", ".RemoveDirection", function () {
    var id = jQuery(this).attr("id").match(/\d+/g);
    console.log("DirectionView" + id);
    var directionView = document.getElementById("DirectionView" + id);
    directionView.remove();
    NumberDirections(); 
})

// Adds an ingredient template to the form
$(document).on("click", ".AddIngredient", function(){
    IngredientViewCount++;
    var divID = "IngredientView"+ IngredientViewCount;
    var buttonID = "ButtonId" + IngredientViewCount;
    console.log("I'm in: " + divID + " " + buttonID);

    // Retrieve the template data from the HTML (jQuery is used here).
    var template = $("#IngredientHandlebar").html();

    // Compile the template data into a function
    var templateScript = Handlebars.compile(template);

    // Create the content
    var context = {divID: divID, buttonID: buttonID};

    var html = templateScript(context);

    // Insert the HTML code into the page
    $("#template").append(html);

    // Change the previous ingredient button to remove
    document.getElementById("ButtonId" + (IngredientViewCount - 1)).innerHTML = "Remove";
    document.getElementById("ButtonId" + (IngredientViewCount - 1)).classList.remove("success-border");
    document.getElementById("ButtonId" + (IngredientViewCount - 1)).classList.add("danger-border");
    document.getElementById("ButtonId" + (IngredientViewCount - 1)).classList.remove("AddIngredient");
    document.getElementById("ButtonId" + (IngredientViewCount - 1)).classList.add("RemoveIngredient");
});

// Removes the ingredient template from the form
$(document).on("click", ".RemoveIngredient", function () {
    var id = jQuery(this).attr("id").match(/\d+/g);
    var view = document.getElementById("IngredientView" + id);
    view.remove();
})

// Iterates through the directions and renumbers them
function NumberDirections() {
    var directions = document.getElementsByClassName("directionCount");

    for(var i = 0; i < directions.length; i++)
    {
       directions[i].innerHTML = (i + 1);
    }
}