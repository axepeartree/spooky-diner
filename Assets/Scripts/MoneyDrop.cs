using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    public float movespeed = 15.0f;

    public float fadeoutSpeed = 6.0f;

    private bool isMoving = false;

    private bool isCoroutineStarted = false;

    private bool clicked = false;

    private GameDirector gameDirector;

    private Vector3 moneyUiPosition;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        var moneyUi = GameObject
            .Find("UI")
            .transform
            .Find("Money")
            .gameObject;
        gameDirector = GameObject.Find("Game Director").GetComponent<GameDirector>();
        moneyUiPosition = Camera.main.ScreenToWorldPoint(moneyUi.transform.position);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // move object towards UI money counter and fade out
        if (isMoving)
        {
            // start coroutine to kill object
            if (!isCoroutineStarted)
            {
                StartCoroutine(SelfDestruct());
                isCoroutineStarted = true;
            }

            // move object towards UI
            var direction = (transform.position - moneyUiPosition).normalized;
            var velocity = direction * movespeed * Time.deltaTime;
            transform.position -= velocity;

            // make it transparent
            var renderColor = spriteRenderer.color;
            var alpha = renderColor.a - renderColor.a * fadeoutSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            StartCoroutine(SelfDestruct());
        }
    }

    void OnMouseUpAsButton() {
        if (!clicked)
        {
            isMoving = true;
            gameDirector.AddMoney(100);
        }
        clicked = true;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
