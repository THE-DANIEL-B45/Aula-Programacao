using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerUp;

public class VelocityPowerUp : CollectableItem
{
    public override void Interact()
    {
        base.Interact();
        PlayerController.instance.StartCoroutine(PlayerController.instance.ActivePowerUp(PowerUpType.Velocity));
    }


}
