using UnityEngine;

public class PlayerControl3D : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Vector3 currentRotation;
    private IInputHandler inputHandler;

    void Start()
    {
        currentRotation = transform.eulerAngles;
        inputHandler = new PlayerInputHandler();
    }

    void Update()
    {
        Vector3 input = inputHandler.GetRotationInput();
        currentRotation.x += input.x * rotationSpeed * Time.deltaTime;
        currentRotation.z += input.z * rotationSpeed * Time.deltaTime;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -10f, 10f);
        currentRotation.z = Mathf.Clamp(currentRotation.z, -10f, 10f);

        transform.eulerAngles = currentRotation;
    }
}

