﻿namespace Pancakes.ErrorCodes
{
    public class CoreErrorCodes : ErrorCode
    {
        public static ErrorCode NullTypeConversion = new CoreErrorCodes(100, "A conversion was attempted using a null value");
        public static ErrorCode InvalidTypeConversion = new CoreErrorCodes(101, "Attempted to convert to a type that was not valid.");
        public static ErrorCode CannotConfigurePostBoot = new CoreErrorCodes(102, "Attempted to adjust the boot configuration after boot().");
        public static ErrorCode AccessServiceLocatorPreBoot = new CoreErrorCodes(103, "Attempted to access the service locator before booting");
        public static ErrorCode InvalidCommandRegistration = new CoreErrorCodes(104, "Attempted to register a command that is not an ICommand.");
        public static ErrorCode InvalidCommandLocationString = new CoreErrorCodes(105, "Attempted to locate a command with an empty string.");
        public static ErrorCode ShieldCaughtBadString = new CoreErrorCodes(106, "Shield blocked a bad string");
        public static ErrorCode ShieldCaughtNullObject = new CoreErrorCodes(107, "Shield blocked a null object");
        public static ErrorCode IllegalSanityCheck = new CoreErrorCodes(108, "Cannot add a sanity check that doesn't implement ICheckSanity");
        public static ErrorCode InsaneKernel = new CoreErrorCodes(109, "Kernel boot failed sanity checks");
        public static ErrorCode CannotBootNonSealedConfig = new CoreErrorCodes(110, "Kernel needs a sealed configuration, protected against changes.");

        private CoreErrorCodes(int identifier, string description) : base($"CORE{identifier}", description)
        {
        }
    }
}
