using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tip : MonoBehaviour
{
    public float Movespeed = 15.0f;

    public float FadeoutSpeed = 6.0f;

    public TipCollectedEvent TipCollectedEvent;

    private GameObject fadeoutTarget;

    private GameDirector gameDirector;

    private bool isClicked = false;

    private bool isSelfDestructing = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeoutTarget = GameObject.FindGameObjectsWithTag("Tip Target").FirstOrDefault();
        if (fadeoutTarget == null)
            fadeoutTarget = gameObject;
        gameDirector = GameObject
            .FindGameObjectsWithTag("Game Director")
            .FirstOrDefault()?
            .GetComponent<GameDirector>();
    }

    void Update()
    {
        // move object towards UI money counter and fade out
        if (isClicked)
        {
            // start coroutine to kill object
            if (!isSelfDestructing)
            {
                StartCoroutine(SelfDestruct());
                isSelfDestructing = true;
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
        if (!isClicked)
        {
            isClicked = true;
            gameDirector.AddMoney(1000);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}

public class TipCollectedEvent : UnityEvent<int> {}