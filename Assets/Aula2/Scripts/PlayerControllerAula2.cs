using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAula2 : MonoBehaviour
{
    [SerializeField] private float velocity = 0.5f;

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        this.transform.position += movement * velocity;
    }
}
