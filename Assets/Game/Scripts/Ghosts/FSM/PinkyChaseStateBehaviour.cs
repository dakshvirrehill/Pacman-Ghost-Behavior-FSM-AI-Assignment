using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChaseStateBehaviour : ChaseStateBehaviour
{

    protected override void UpdatePath()
    {
        Vector2 aExpectedMoveLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y) + 4 * mController.PacMan.MoveDirections[(int)mController.PacMan.moveDirection];
        if (mController.moveToLocation != aExpectedMoveLocation)
        {
            mController.moveToLocation = aExpectedMoveLocation;
            mController.moveComplete();
        }
        mCurrentTimer = 0.0f;
    }

}

