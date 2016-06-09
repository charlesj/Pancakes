using System;

namespace Pancakes.Utility
{
    public interface IClock
    {
        DateTimeOffset Now();
    }
}