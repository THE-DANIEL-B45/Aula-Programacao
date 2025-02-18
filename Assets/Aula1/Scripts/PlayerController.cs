using PowerUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public float velocity = 0.5f;

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * velocity, Input.GetAxis("Vertical") * velocity, 0);
        this.transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            Interactable interactable = other.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.Interact();
            }    
        }
    }

    public IEnumerator ActivePowerUp(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.Velocity:
                velocity = 2f;
                yield return new WaitForSeconds(3f);
                velocity = 0.5f;
                break;

        }

        yield return null;
    }
}
namespace PowerUp
{
 public enum PowerUpType
    {
        Velocity,
        Invencibility
    }

}
