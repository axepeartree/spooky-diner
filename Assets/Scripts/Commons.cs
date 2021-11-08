using System;

namespace Commons
{
    public enum CustomerState
    {
        Moving,
        Ordering,
        Waiting,
        Leaving
    }

    public enum CustomerType
    {
        Ghost,
        Demon,
        Zombie,
        Vampire
    }

    public enum Location
    {
        Restaurant,
        Kitchen
    }

    public enum RecipeType
    {
        Burger,
        Fries,
        Soup
    }

    public enum TableType
    {
        A,
        B
    }

    public enum OvenType
    {
        A
    }
}