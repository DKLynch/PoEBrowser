# PoEBrowser
PoEBrowser is a small .NET Core web application that displays data about various different elements from Grinding Gear Games' Path of Exile.
PoEBrowser is currently hosted at: https://poebrowser.azurewebsites.net/

Note: Due to hosting for free (both on Azure and Atlas), first requests to the site or database may take a little longer as the instances spin up from inactivity. Subsequent requests should have a much lower overhead.

# Current Functionality
*This application is an ongoing project*

Currently, all item bases in the game are browsable, filterable by type and individual pages display their properties and requirements.

Skill and support gems are browsable and filterable by type, and dedicated pages currently show their description, base stats and level related stats.

Currency items can be browsed, with pages displaying their descriptions, how to use them and their stack sizes.

Essence items are currently browsable and their individual effects and properties are displayed.

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

Rob Garrison (Mottie) for jQuery TableSorter:
https://github.com/Mottie/tablesorter
