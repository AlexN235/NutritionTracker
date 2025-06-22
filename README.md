# About
A personal project that is an application which tracks nutrition consumed by the user and has records of the nutrition facts of different types of foods.

# Features
This app has two main features: a search feature that allows the user to search for a variety of food and their nutrition facts from the database and a recording feature that records the meals the user eats.

## Search
![](https://youtu.be/a8jCKsyJlo0)
Looking up the nutrition facts for an ingredient.

## Record
![](<iframe width="560" height="315" src="https://www.youtube.com/embed/a8jCKsyJlo0?si=A3Yu_d5Q2COn7fDB" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>)
You can add a meal you've eaten by searching and combining a list of ingredients with the total amount. The page also displays a chart of total calories you have recently consumed. 


# Understanding the Code

## Page Hierarchy Diagram.
![Model](https://raw.githubusercontent.com/AlexN235/NutritionTracker/refs/heads/master/docs/images/Page%20Hierarchy.png)

## Class Diagram 
![Model](https://raw.githubusercontent.com/AlexN235/NutritionTracker/refs/heads/master/docs/images/UML%20Class%20Diagram.png)

## Class Summary

**FoodDisplay**<br/>
  Used anywhere information about a food needs to be displayed. It contains the EdibleItem (FoodItem, MealItem) class which contains all the necessary info about that item.

**FoodDisplayGroup**<br/>
  Similar to FoodDisplay but groups them together based on a value. Used exclusively for the MyMeal page to group meals based on a date.

**NutritionDatabase**<br/>
  Has access to the database use for getting all the food information and pulls queries using a name or id. Does this by using SQLite and the FoodNutritionDetail class to pull data from the foodinfo.db file.




