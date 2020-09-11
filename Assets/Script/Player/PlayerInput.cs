using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
    public bool IsOneStepJump() => Input.GetKeyDown(KeyCode.A);

    public bool IsTwoStepJump() => Input.GetKeyDown(KeyCode.S);

}
