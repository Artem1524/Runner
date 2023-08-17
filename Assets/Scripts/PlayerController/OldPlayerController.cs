using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerController : BasePlayerController
{
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        float direction = Input.GetAxis("Horizontal");

        MoveSide(direction);
    }
}
