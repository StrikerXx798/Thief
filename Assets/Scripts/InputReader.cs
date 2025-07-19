using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public event Action<Vector3> MovementInputReceived;
    public event Action<Vector3> LookInputReceived;

    private void Update()
    {
        ReadMovementInput();
        ReadLookInput();
    }

    private void ReadMovementInput()
    {
        var movement = new Vector3(Input.GetAxis(Horizontal), 0f, Input.GetAxis(Vertical));
        MovementInputReceived?.Invoke(movement);
    }

    private void ReadLookInput()
    {
        var look = new Vector3(Input.GetAxis(MouseX), Input.GetAxis(MouseY), 0f);
        LookInputReceived?.Invoke(look);
    }
}