using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl3D : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Vector3 currentRotation;

    void Start()
    {
        currentRotation = transform.eulerAngles;
    }

    void Update()
    {
        float rotateX = 0f;
        float rotateZ = 0f;

        if (Input.GetAxis("Vertical") > 0) rotateX = 1f;
        if (Input.GetAxis("Vertical") < 0) rotateX = -1f;
        if (Input.GetAxis("Horizontal") > 0) rotateZ = -1f;
        if (Input.GetAxis("Horizontal") < 0) rotateZ = 1f;

        currentRotation.x += rotateX * rotationSpeed * Time.deltaTime;
        currentRotation.z += rotateZ * rotationSpeed * Time.deltaTime;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -10f, 10f);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -10f, 10f);

        transform.eulerAngles = currentRotation;
    }
}
