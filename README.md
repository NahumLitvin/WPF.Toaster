WPF.Toast Notifications
==============

Description
------
ver 1.2
Lightweight growl notifications for WPF projects based
on https://github.com/IvanLeonenko/WPFGrowlNotification
sperated project to a useable library and a test project.
added easy support for choosing the location of the notifications.
added subtitle and color support for all Toast components rebranded.

Requirements
----------------------------------
Tested on Visaul Studio 2013 with .Net 4.5 On Windows 7

![Image of Yaktocat](https://cloud.githubusercontent.com/assets/14837912/11917527/631fe884-a713-11e5-98ab-e80ffd405b7d.png)

How To Use:
----------------------------------

```c#
            _toastsManager = new ToastsManager(NotificationLocation.TopRight);//create new manager and choose where messages appear
            var uri = new Uri("pack://application:,,,/Resources/notification-icon.png");
            var bitmap = new BitmapImage(uri);//load icon
            _toastsManager.AddNotification(new Toaster.Toast
            {
                Title = "Mesage #1",
                Image = bitmap,
                Message = "Lorem ipsum dolor sit amet, consectetur adipi."
                ,
                TextColor = Colors.Green
                ,
                SubTitle = "SubTitleText"
                ,
                BackgroundColor = Colors.NavajoWhite
            });
```
