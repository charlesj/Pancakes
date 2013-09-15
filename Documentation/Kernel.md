# Pancakes.Kernel

The Pancakes Kernel is the core of a Pancakes Application.  The application is started by calling Boot() and passing in the desired boot configuration.  A default configuration is available:

    Pancakes.Kernel.Boot(BootConfiguration.DefaultConfiguration);

The Default configuration will do several things: 
* Configure the Service Locator using Ninject
* Load up any available object mappings
* Load up settings from configuration files
* Start logging a heartbeat every 10 seconds
* Run a sanity check against settings to make sure they all exist.

## Customizing the BootConfiguration

The Boot Configuration can be adjusted in a variety of ways using a fluent interface.