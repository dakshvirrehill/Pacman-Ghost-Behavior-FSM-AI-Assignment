using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyChaseStateBehaviour : ChaseStateBehaviour
{
    public Transform mBlinkyTransform;


    protected override void UpdatePath()
    {
        Vector2 aBlinkyTransform = new Vector2(mBlinkyTransform.position.x, mBlinkyTransform.position.y);
        Vector2 aPacmanTransform = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
        Vector2 aDesiredLocation = aBlinkyTransform + ((aPacmanTransform + mController.PacMan.MoveDirections[(int)mController.PacMan.moveDirection] * 2) - aBlinkyTransform) * 2;
        if(aDesiredLocation != mController.moveToLocation)
        {
            mController.moveToLocation = aDesiredLocation;
            mController.moveComplete();
        }
    }
}

