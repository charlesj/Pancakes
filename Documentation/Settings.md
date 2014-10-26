# Settings

Pancake Settings are strongly typed representations of your application
configuration files.

All Applications built with Pancakes is expected to have at least two
application configuration values, ApplicationName, and InstanceName.  These
values are used by the logger to write instance specific log entries.

When the Pancake Kernel boots, it checks to make sure all configuration options
are present, and loads them into an instance of settings using reflection.

## Service Binding

There is a default instance of ISettings bound to the service locator.  In your
application, you should create your own ISettings interface that inherits from
Pancakes.ISettings.  This way you can easily add your own application specific
properties, and still get all the benefits of the logger and reflective settings
loader.
