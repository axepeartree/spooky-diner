using System.Collections;
using System.Collections.Generic;
using Events;
using EventSystem;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public GameEventExchange MoneyAddedExchange;

    public GameObject fadeoutTarget;

    public float Movespeed = 15.0f;

    public float FadeoutSpeed = 6.0f;

    private bool isMoving = false;

    private bool clicked = false;

    private bool isCoroutineStarted = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (fadeoutTarget == null)
            fadeoutTarget = gameObject;
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
            var direction = (transform.position - fadeoutTarget.transform.position).normalized;
            var velocity = direction * Movespeed * Time.deltaTime;
            transform.position -= velocity;

            // make it transparent
            var renderColor = spriteRenderer.color;
            var alpha = renderColor.a - renderColor.a * FadeoutSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            StartCoroutine(SelfDestruct());
        }
    }

    void OnMouseUpAsButton() {
        if (!clicked)
        {
            isMoving = true;
            MoneyAddedExchange?.Dispatch(new MoneyAddedPayload(100));
        }
        clicked = true;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
