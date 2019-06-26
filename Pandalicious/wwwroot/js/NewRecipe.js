var IngredientViewCount = 0;
var DirectionViewCount = 0;

// Counts how many ingredient views are currently on the form
function CountInstructorViews() {
    var viewCounts = $(".RemoveIngredient").length;
    if (viewCounts == 1) {
        var id = document.getElementsByClassName("RemoveIngredient")[0].id;
        document.getElementById(id).style.visibility = "hidden";
    
    }
    else {
        var viewCounts = document.getElementsByClassName("RemoveIngredient");

        for(var i = 0; i < viewCounts.length; i++)
        {
           var id = document.getElementsByClassName("RemoveIngredient")[i].id;
            document.getElementById(id).style.visibility = "visible";
        }
    }
}

// Counts how many direction views are currently on the form
function CountDirectionViews() {
    var viewCounts = $(".RemoveDirection").length;
    if (viewCounts == 1) {
        var id = document.getElementsByClassName("RemoveDirection")[0].id;
        document.getElementById(id).style.visibility = "hidden";
    
    }
    else {
        var viewCounts = document.getElementsByClassName("RemoveDirection");

        for(var i = 0; i < viewCounts.length; i++)
        {
           var id = document.getElementsByClassName("RemoveDirection")[i].id;
            document.getElementById(id).style.visibility = "visible";
        }
    }
}

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
    var count = 1;
    $.ajax({
        url: "/api/Recipe/CreateRecipe",
        type: "POST",
        contentType: "application/json",
        data: data,
        success: function (result) {
           window.location.href = "/Recipe/AllRecipes";
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
            CountInstructorViews();
            CountDirectionViews();
        }
    })
})

// Adds a direction template to the form
$(document).on("click", ".AddDirection", function(){

    var divID = "DirectionView"+ DirectionViewCount;
    var directionAddBtnID = "DirectionAddBtnID" + DirectionViewCount;
    var directionRemoveBtnID = "DirectionRemoveBtnID" + DirectionViewCount;

    // Retrieve the template data from the HTML (jQuery is used here).
    var template = $("#DirectionHandlebar").html();

    // Compile the template data into a function
    var templateScript = Handlebars.compile(template);

    // Create the content
    var context = {DirectionDivID: divID, directionAddBtnID: directionAddBtnID, directionRemoveBtnID: directionRemoveBtnID};

    var html = templateScript(context);

    // Insert the HTML code into the page
    $("#directionTemplate").append(html);

    // Change the previous ingredient button to remove
    DirectionViewCount++;
    NumberDirections(); 
    CountDirectionViews();

});

// Removes the direction template from the form
$(document).on("click", ".RemoveDirection", function () {
    var id = jQuery(this).attr("id").match(/\d+/g);
    var directionView = document.getElementById("DirectionView" + id);
    directionView.remove();
    NumberDirections(); 
    CountDirectionViews();

})

// Adds an ingredient template to the form
$(document).on("click", ".AddIngredient", function(){

    var divID = "IngredientView"+ IngredientViewCount;
    var addButtonID = "AddButtonId" + IngredientViewCount;
    var removeButtonID = "RemoveButtonId" + IngredientViewCount;

    // Retrieve the template data from the HTML (jQuery is used here).
    var template = $("#IngredientHandlebar").html();

    // Compile the template data into a function
    var templateScript = Handlebars.compile(template);

    // Create the content
    var context = {divID: divID, addButtonID: addButtonID, removeButtonID: removeButtonID};

    var html = templateScript(context);

    // Insert the HTML code into the page
    $("#template").append(html);
    IngredientViewCount++;
                CountInstructorViews();

});

// Removes the ingredient template from the form
$(document).on("click", ".RemoveIngredient", function () {
    var id = jQuery(this).attr("id").match(/\d+/g);
    var view = document.getElementById("IngredientView" + id);
    view.remove();
    CountInstructorViews();

})

// Iterates through the directions and renumbers them
function NumberDirections() {
    var directions = document.getElementsByClassName("directionCount");

    for(var i = 0; i < directions.length; i++)
    {
       directions[i].innerHTML = (i + 1);
    }
}