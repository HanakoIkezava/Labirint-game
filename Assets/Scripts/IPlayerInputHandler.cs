using UnityEngine;

public interface IInputHandler
{
    Vector3 GetRotationInput();
}

public class PlayerInputHandler : IInputHandler
{
    public Vector3 GetRotationInput()
    {
        float rotateX = 0f;
        float rotateZ = 0f;

        if (Input.GetAxis("Vertical") > 0) rotateX = 1f;
        if (Input.GetAxis("Vertical") < 0) rotateX = -1f;
        if (Input.GetAxis("Horizontal") > 0) rotateZ = -1f;
        if (Input.GetAxis("Horizontal") < 0) rotateZ = 1f;

        return new Vector3(rotateX, 0, rotateZ);
    }
}

