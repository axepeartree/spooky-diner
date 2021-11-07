using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private State state = State.HasNotOrdered;

    private float movespeed = 1.0f;

    private float distanceThreshold = 0.7f;

    private GameObject gameDirector;

    private GameObject table;

    private GameObject baloon;

    private GameObject ellipses;

    void Start()
    {
        var tables = GameObject.Find("Tables");
        var rnd = Random.Range(0, tables.transform.childCount);
        table = tables.transform.GetChild(rnd).gameObject;

        gameDirector = GameObject.Find("Game Director");

        baloon = transform.Find("Recipe Order").gameObject;
        baloon.SetActive(false);

        ellipses = transform.Find("Ellipses").gameObject;
        ellipses.SetActive(false);
    }

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
        else if (state == State.HasNotOrdered)
        {
            state = State.Ordered;
            baloon.SetActive(true);
        }

        if (state == State.Received)
        {
            baloon.SetActive(false);
            ellipses.SetActive(false);
        }
    }

    public void ReceiveOrder()
    {
        state = State.Received;
        gameDirector.GetComponent<GameDirector>().CreateMoney()
    }

    private enum State {
        HasNotOrdered,
        Ordered,
        Received
    }
}
