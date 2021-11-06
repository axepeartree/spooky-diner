using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeOrder : MonoBehaviour
{
    private bool ordering = false;

    private GameDirector gameDirector;

    private GameObject ellipses;

    private RecipeType recipeType;

    public GameObject friesPrefab;

    public GameObject burgerPrefab;

    public GameObject soupPrefab;

    void Start()
    {
        gameDirector = GameObject.Find("Game Director").GetComponent<GameDirector>();
        ellipses = transform.parent.Find("Ellipses").gameObject;

        var recipes = gameDirector.GetAvailableRecipes();
        var rnd = Random.Range(0, recipes.Length);
        recipeType = recipes[rnd];

        switch (recipeType)
        {
            case RecipeType.Fries:
                InstantiateRecipe(friesPrefab);
                break;
            case RecipeType.Burger:
                InstantiateRecipe(burgerPrefab);
                break;
            case RecipeType.Soup:
                InstantiateRecipe(soupPrefab);
                break;
        }
    }

    void Update()
    {
        if (ordering)
            gameObject.SetActive(false);
    }

    void InstantiateRecipe(GameObject prefab)
    {
        var obj = Instantiate(prefab) as GameObject;
        obj.transform.position = transform.position;
        obj.transform.parent = transform;
    }

    void OnMouseUpAsButton()
    {
        ordering = true;
        gameDirector.EnqueueRecipeOrder(gameObject, recipeType);
        ellipses.SetActive(true);
    }
}
