using System.Collections;
using Commons;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public PlayerData PlayerData;

    public PrefabFactory PrefabFactory;

    public float Speed = 10.0f;

    public bool Free = true;

    private GameObject popUp;

    private GameObject recipe;

    private Customer targetCustomer;

    private RecipeType recipeType;

    void Start()
    {
        popUp = transform.Find("Pop Up").gameObject;
    }

    public void StartOrder(Customer targetCustomer, RecipeType recipeType)
    {
        this.targetCustomer = targetCustomer;

        // spawn recipe and activate pop up
        var recipePrefab = PrefabFactory.Recipes.Find(r => r.RecipeType == recipeType);
        GameObject gameObject = Instantiate(recipePrefab.Prefab) as GameObject;
        gameObject.transform.position = popUp.transform.position;
        gameObject.transform.parent = popUp.transform;
        recipe = gameObject;

        Free = false;
        StartCoroutine(Cook());
    }

    IEnumerator Cook()
    {
        yield return new WaitForSeconds(10.0f / Speed);
        targetCustomer.ReceiveOrder();
        Destroy(recipe);
        recipe = null;
        Free = true;
    }
}
