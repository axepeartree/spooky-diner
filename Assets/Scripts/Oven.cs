using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public float speed = 5.0f;
    public bool free = true;

    public IEnumerator CookOrder(GameDirector director, EnqueuedRecipeOrder order)
    {
        free = false;
        Debug.Log("Cooking order!");
        yield return new WaitForSeconds(10.0f / speed); // TODO: 
        // customer.DropMoney(order.reward);
        order.customer.ReceiveOrder();
        Debug.Log("Order cooked!");
        free = true;
    }
}
