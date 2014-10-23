# Pancakes

Pancakes brings together well known .NET libraries into a cogent stack that can be used immediately. 

The core entry into the system is the Boostrapper.  An application that uses Pancakes is going to have a call to bootstrapper.boot within a few lines of the entrypoint into their application.

```
Bootstrapper.Boot(BootConfiguration.DefaultConfiguration);
```

The BootConfiguration provides a Default Configuration that will get you started in the most basic of situations.  As your application grows more complicated, you may need to change this configuration.  There is a fluent interface for these changes.

## Chapters

* [[Kernel]]

* [[TypeConverter]]

* Settings

* Service Locator

* Logging

* Mapping

* Validation

* Exceptions

* Extensions

* Customization