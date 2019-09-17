# SereniTee
PoEBrowser is a small .NET Core web application that displays data about various different elements from Grinding Gear Games' Path of Exile.
PoEBrowser is currently hosted at: https://poebrowser.azurewebsites.net/

# Current Functionality
Currently, all item bases in the game are browsable, dedicated page displays with item info are still a work in progress.
Skill Gems are browsable and filterable by type, and dedicated pages currently show their description, base stats and level related stats.
Currency and Essence items are currently displayed, but their details are not yet displayed.

# Technical Stuff
PoEBrowser currently makes use of the following technologies:
* .NET Core 2.2 (MVC)
* MongoDB + .NET Driver (Hosted on an Atlas Cluster)
* Azure
* Razor Pages
* Bootstrap

## Special Thanks
Major thanks to GGG for creating Path of Exile:
https://www.pathofexile.com/

OmegaK2 for PyPoE:
https://github.com/OmegaK2/PyPoE

brather1ng for his json data representations at RePoE:
https://github.com/brather1ng/RePoE
