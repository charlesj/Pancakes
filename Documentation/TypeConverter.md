# Type Conversion

Type conversion is a fundamental part of our settings loader.  Setting values in application configuration files are basically strings, but in order actually use them, we need them to be strongly typed.  So we build a generic type converter interface on top of the built in .NET type convertors.

We start with the basic ITypeConverter interface, which defines both a object conversion and a generic conversion method.

```
typeConverter.Convert("2014-10-10", typeof(DateTime)); // returns object that needs casting
typeConverter.Convert<DateTime>("2014-10-10"); // returns DateTime
```

These conversions shouldn't be happening very often in your code, but when it is needed, this interface provides a nifty solution that wraps up the "official practice" that I've been able to gather.