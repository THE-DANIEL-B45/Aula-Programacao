using PowerUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Animator animator;
    [SerializeField] GameObject powerUpUI;
    [SerializeField] GameObject needBiggerBackpackText;

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
        if(movement.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            if (Inventory.instance.InventoryFull())
            {
                StartCoroutine(BiggerBackpackText());
                return;
            }

            Interactable interactable = other.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.Interact();
            }    
        }
    }

    IEnumerator BiggerBackpackText()
    {
        needBiggerBackpackText.SetActive(true);
        yield return new WaitForSeconds(2f);
        needBiggerBackpackText.SetActive(false);
    }

    public IEnumerator ActivePowerUp(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.Velocity:
                velocity = 0.1f;
                powerUpUI.SetActive(true);
                yield return new WaitForSeconds(3f);
                velocity = 0.05f;
                powerUpUI.SetActive(false);
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
