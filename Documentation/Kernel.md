# Pancakes.Kernel

The Pancakes Kernel is the core of a Pancakes Application.  The application is started by calling Boot() and passing in the desired boot configuration.  A default configuration is available:

    Pancakes.Kernel.Boot(BootConfiguration.DefaultConfiguration);

The Boot() Method is Idempotent, which means that it can safely be called multiple times.  After the first time it is called, the call will simply be ignored by the kernel.  This can be useful in integration testing especially,  but **if you find that you are calling Boot() from several locations in your main application** it is an indication that something is probably wrong.  Ideally, the Boot() Method should only called in the [compositional root](http://blog.ploeh.dk/2011/07/28/CompositionRoot/) of your application.

The Default configuration will do several things: 
* Configure the Service Locator using Ninject
* Load up any available object mappings
* Load up settings from configuration files
* Start logging a heartbeat every 10 seconds
* Run a sanity check against settings to make sure they all exist.

## Customizing the BootConfiguration

The Boot Configuration can be adjusted in a variety of ways using a fluent interface.

* SkipSanityCheck() - The Sanity Check is relatively expensive and you can skip it by calling

    Kernel.Boot(BootConfiguration.DefaultConfiguration.SkipSanityCheck());

 * AddAssemblySearchPattern() - By Default, Pancakes loads up all available ninject modules in all available assemblies.  Sometimes, though, this can go wrong, because the search tries to load a module that has already been loaded.  This can be fixed by passing in assembly search patterns.  An Example would be: "YourApplication.dll".

     Kernel.Boot(BootConfiguration.DefaultConfiguration.AddAssemblySearchPattern("YourApplication.dll"));

 * Be Verbose() - Writes messages to standard out during the boot process.

     Kernel.Boot(BootConfiguration.DefaultConfiguration.BeVerbose());