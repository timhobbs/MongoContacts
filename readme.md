MongoContacts
=============

MongoContacts is a simple contact list using ASP.NET MVC, C# and MongoDB. The contact list isn't really anything special, I just wanted to start something so I could play around with MongoDB. I guess I should probably have made a todo list, right?

There is a demo running up on AppHarbor: <a href="http://mongocontacts.apphb.com">http://mongocontacts.apphb.com</a>. Check it out to get an idea what the app can do.

What's in the box
-----------------
ASP.NET MVC, C#, jQuery + validation, Twitter Bootstrap, Automapper and a Bootstrap Datepicker

Installation
------------
Of course you'll need .NET and MongoDB. With the AppHarbor demo I am using MongoLab, so you can go that route if you don't want to install MongoDB locally or if you cannot for some reason.

The solution uses Nuget package restore, so the first build will pull down the necessary packages. You may need to either enable the solution to allow package restore or enable your environment to enable package restore (if you haven't already).

Release notes
-------------
The initial release is pretty basic: a contact, email addresses and websites. I have plans to expand to allow phone numbers, IM accounts and grouping (tags), but I have not yet implemented. I probably will depending on if real work gets in the way or not. This *IS* just a demo project - so it may die a horrible death.

### 2013.06.21
- Added phone number and IM support

### 2013.06.22
- Added "about" page
- Pushed demo to AppHarbor