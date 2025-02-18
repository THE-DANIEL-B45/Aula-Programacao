using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorAula4 : MonoBehaviour
{
    private void Awake()
    {
        transform.position = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }
}
