using System;
using System.Collections.Generic;
using Commons;
using UnityEngine;

[CreateAssetMenu]
public class PrefabFactory : ScriptableObject
{
    public List<CustomerPrefab> Customers;

    public List<RecipePrefab> Recipes;

    public List<TablePrefab> Tables;

    [Serializable]
    public class CustomerPrefab
    {
        public GameObject Prefab;
        public CustomerType CustomerType;
    }

    [Serializable]
    public class RecipePrefab
    {
        public GameObject Prefab;
        public RecipeType RecipeType;
    }

    [Serializable]
    public class TablePrefab
    {
        public GameObject Prefab;
        public TableType TableType;
    }
}
