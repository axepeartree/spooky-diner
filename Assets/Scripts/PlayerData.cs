using System.Collections.Generic;
using Commons;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int Money;
    public List<CustomerType> PotentialCustomers;
    public List<RecipeType> PotentialRecipes;

    public void Reset()
    {
        Money = 0;
        PotentialCustomers = new List<CustomerType> { CustomerType.Ghost };
        PotentialRecipes = new List<RecipeType> { RecipeType.Fries };
    }
}