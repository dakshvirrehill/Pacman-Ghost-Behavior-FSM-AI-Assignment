using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChaseStateBehaviour : ChaseStateBehaviour
{
    protected override void UpdatePath()
    {
        if (mController.moveToLocation.x != mController.PacMan.transform.position.x)
        {
            mController.moveToLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
            mController.moveComplete();
        }
        mCurrentTimer = 0.0f;
    }

}

