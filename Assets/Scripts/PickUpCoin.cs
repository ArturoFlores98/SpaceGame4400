using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : Pickup
{
    public override void PickMeUp()
    {
        Inventory.currentCoins++;
        UIController.UpdateCoins();
        Destroy(gameObject);
    }
}
