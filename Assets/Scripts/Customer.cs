using System.Linq;
using Commons;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public PlayerData PlayerData;

    public PrefabFactory PrefabFactory;

    public GameObject TipSpawnPoint;

    public Vector3 ExitPoint;

    public float maxDistanceThreshold = 0.1f;

    public float movespeed = 2.0f;

    private CustomerState customerState;

    private RecipeType recipeType;

    private GameObject popUp;

    private GameObject ellipses;

    private Vector3? target;

    void Start()
    {
        popUp = transform.Find("Pop Up").gameObject;
        popUp.SetActive(false);
        customerState = CustomerState.Moving;

        ellipses = transform.Find("Ellipses").gameObject;
        ellipses.SetActive(false);

        // select random recipe
        recipeType = PlayerData.PotentialRecipes[Random.Range(0, PlayerData.PotentialRecipes.Count)];
        var recipe = PrefabFactory.Recipes.Find(t => t.RecipeType == recipeType);
        GameObject recipeObj = Instantiate(recipe.Prefab) as GameObject;
        recipeObj.transform.position = popUp.transform.position;
        recipeObj.transform.parent = popUp.transform;
    }

    void Update()
    {
        UpdateTarget();

        switch (customerState)
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
                    customerState = CustomerState.Ordering;
                    popUp.SetActive(true);
                }
                break;
            case CustomerState.Leaving:
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    ExitPoint,
                    movespeed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    public void ReceiveOrder()
    {
        // TODO: drop tip!
        customerState = CustomerState.Leaving;
        ellipses.SetActive(false);
        GameObject gameObject = Instantiate(PrefabFactory.TipPrefab) as GameObject;
        gameObject.transform.position = TipSpawnPoint.transform.position;
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

    void OnMouseUpAsButton()
    {
        if (customerState == CustomerState.Ordering)
        {
            var oven = GetOvens()
                .Select(o => o.GetComponent<Oven>())
                .OrderByDescending(o => o.Speed)
                .FirstOrDefault(o => o.Free);
            if (oven != null)
            {
                oven.StartOrder(this, recipeType);
                customerState = CustomerState.Waiting;
                ellipses.SetActive(true);
                popUp.SetActive(false);
            }
        }
    }

    private GameObject[] GetTables() => GameObject.FindGameObjectsWithTag("Table");

    private GameObject[] GetOvens() => GameObject.FindGameObjectsWithTag("Oven");
}
