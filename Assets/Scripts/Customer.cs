using System.Linq;
using Commons;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public PlayerData PlayerData;

    public PrefabFactory PrefabFactory;

    public float maxDistanceThreshold = 0.1f;

    public float movespeed = 2.0f;

    private CustomerState CustomerState;

    private GameObject popUp;

    private Vector3? target;

    void Start()
    {
        popUp = transform.Find("Pop Up").gameObject;
        popUp.SetActive(false);
        CustomerState = CustomerState.Moving;

        // select random recipe
        var recipeType = PlayerData.PotentialRecipes[Random.Range(0, PlayerData.PotentialRecipes.Count)];
        var recipe = PrefabFactory.Recipes.Find(t => t.RecipeType == recipeType);
        GameObject recipeObj = Instantiate(recipe.Prefab) as GameObject;
        recipeObj.transform.position = popUp.transform.position;
        recipeObj.transform.parent = popUp.transform;
    }

    void Update()
    {
        UpdateTarget();

        switch (CustomerState)
        {
            case CustomerState.Moving:
                if (target == null)
                    return;
                var distance = Vector3.Distance(transform.position, target.Value);
                if (distance > maxDistanceThreshold)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        target.Value,
                        movespeed * Time.deltaTime);
                }
                else
                {
                    CustomerState = CustomerState.Ordering;
                }
                break;
            case CustomerState.Ordering:
                popUp.SetActive(true);
                break;
            default:
                break;
        }
    }

    void UpdateTarget()
    {
        if (target != null)
            return;

        var tables = GetTables().Select(t => t.GetComponent<Table>());
        if (tables.Count() == 0)
            return;

        var freeTable = tables.FirstOrDefault(t => t.IsTableFree());
        if (freeTable == null)
            return;
        
        freeTable.TryUseTable(); // already know it's free
        target = freeTable.gameObject.transform.position;
    }

    // when recipe is clicked
    void OnMouseUpAsButton()
    {
        if (CustomerState == CustomerState.Ordering)
        {
            // check if you can order and send an order event
        }
    }

    private GameObject[] GetTables() => GameObject.FindGameObjectsWithTag("Table");
}
