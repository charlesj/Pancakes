#Pancakes


Pancakes brings together well known .NET libraries into a cogent stack that can be used immediately.

##.NET Libraries Used

* **Ninject** - Dependency Injection / Inversion of Control Container
* **AutoMapper** -- Object Mapping
* **Fluent Validation** -- Object Validation
* **NLog - Logging

##Pancakes Kernel
The kernel is at the heart of a Pancakes application.  Calling Boot() sets things in motion, loading up all your settings, setting up the IoC container, setting up validatin, and optionally running a sanity check on it all.  If something isn't right, you find out then, not later on.

    Kernel.Boot(BootConfiguration.DefaultConfiguration);
    
The Default configuration provides simple defaults, but you can override using a fluent interface.
    
    BootConfiguration.DefaultConfiguration.SkipSanityCheck();
    

##Settings
Application Settings are a core part of Pancakes.  Every application needs access to settings, and far too often we see ConfigurationManager.AppSettings hard coded inline.  The Settings object in Pancakes are better.  They automatically load the settings from your app.config (or web.config), and handle the conversion to strongly typed settings as well as error checking.  You receive an error fast when something is wrong with your settings.
