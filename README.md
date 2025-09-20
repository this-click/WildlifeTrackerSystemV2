# WildlifeTrackerSystem
Description:
    A helpful app for the responsible critter guardian.
    Allows you to keep track of the animals under your care, who is hungry and what they'll have for dinner.

Functionalities:

    add a new animal
    change/delete animals from keeper's care
    load/save animals from file
    add/update/delete meals for animals
    upload pictures for animals
    reset data in the app
    
System design:
    MVVM architecture, shell navigation between pages.
    Views are selectively rendered based on user choice to increase performance. 
    Last visited page is removed from the navigation stack to increase performance (see https://github.com/dotnet/maui/issues/10164).
    All animals implement IAnimal interface and types are dynamically bound.
    Container classes, except FileManager implement IListManager's generic collections.
    Container classes are injected as dependencies throughout the app. 
    Files use JSON serialization/deserialization and handle exceptions.

System requirements:
- OS: Windows
- Visual Studio with .Net 9 
- .Net Multi-platform App UI development

How to install and run the project:

    Click on Code and select Download zip.
    Extract the archive and open Visual Studio.
    Click Open > Project/Solution and select WildlifeTrackerSystem.sln from your local directory.
    After the project is loaded, click on Debug > Start debugging or Start without debugging.

Screenshots:
