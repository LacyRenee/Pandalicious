var IngredientViewCount = 0;

$(document).on("click", ".AddIngredient", function(){
    IngredientViewCount++;
    var divID = "IngredientView"+ IngredientViewCount;
    var buttonID = "ButtonId" + IngredientViewCount;
    console.log("I'm in: " + divID + " " + buttonID);

    // Retrieve the template data from the HTML (jQuery is used here).
    var template = $('#handlebars-demo').html();

    // Compile the template data into a function
    var templateScript = Handlebars.compile(template);

    // Create the content
    var context = {divID: divID, buttonID: buttonID};
    // html = 'My name is Ritesh Kumar. I am a developer.'
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

$(document).on("click", ".RemoveIngredient", function () {
    alert("It's working...");
})