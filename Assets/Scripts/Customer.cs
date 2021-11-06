using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool hasOrdered = false;

    private float movespeed = 1.0f;

    private float distanceThreshold = 0.7f;

    private GameObject table;

    private GameObject baloon;

    private GameObject ellipses;

    // Start is called before the first frame update
    void Start()
    {
        var tables = GameObject.Find("Tables");
        var rnd = Random.Range(0, tables.transform.childCount);
        table = tables.transform.GetChild(rnd).gameObject;

        baloon = transform.Find("Recipe Order").gameObject;
        baloon.SetActive(false);

        ellipses = transform.Find("Ellipses").gameObject;
        ellipses.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var distance = (table.transform.position - transform.position).magnitude;
        if (distance > distanceThreshold)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                table.transform.position,
                movespeed * Time.deltaTime);
        }
        else if (!hasOrdered)
        {
            hasOrdered = true;
            baloon.SetActive(true);
        }
    }
}
