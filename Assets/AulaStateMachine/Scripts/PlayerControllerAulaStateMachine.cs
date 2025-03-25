using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAulaStateMachine : MonoBehaviour
{
    public Vector2 movementSpeed = new Vector2(100.0f, 100.0f);

    private new Rigidbody2D rigidbody2D;

    private Vector2 inputVector = new Vector2(0, 0);

    //Sneak Attack Variables
    public GameObject attackHitbox;
    public float attackDuration = 0.3f;
    public bool canAttack = false;
    private bool holdingMouse = false;

    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        rigidbody2D.angularDrag = 0;
        rigidbody2D.gravityScale = 0;

        if(attackHitbox != null)
        {
            attackHitbox.SetActive(false);
        }
    }

    private void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(attackHitbox != null && canAttack)
        {
            RotateAttackHitboxTowardsMouse();

            if(Input.GetMouseButton(0) && !holdingMouse)
            {
                holdingMouse = true;
                StartCoroutine(PerformAttack());
            }
        }

        if(Input.GetMouseButtonUp(0) && holdingMouse)
        {
            holdingMouse = false;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + (inputVector * movementSpeed * Time.fixedDeltaTime));
    }

    void RotateAttackHitboxTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) *  Mathf.Rad2Deg;

        attackHitbox.transform.rotation = Quaternion.Euler(0, 0, angle);

        float hitboxDistance = 1.0f;
        attackHitbox.transform.position = transform.position + (Vector3)direction * hitboxDistance;
    }

    IEnumerator PerformAttack()
    {
        canAttack = false;

        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false);

        canAttack = true;
    }
}
