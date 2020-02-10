using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChaseStateBehaviour : GhostBehaviour
{

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        if (mReverseDirection)
        {
            ReverseDirection();
        }

    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!mController.mIsChasing)
        {
            mReverseDirection = true;
            pFSM.SetTrigger(mController.mScatter);
        }
        else
        {
            if (mController.moveToLocation != (new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y) + 4*mController.PacMan.MoveDirections[(int)mController.PacMan.moveDirection]))
            {
                mController.moveToLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y) + 4 * mController.PacMan.MoveDirections[(int)mController.PacMan.moveDirection];
                mController.moveComplete();
            }
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateExit();
    }

}

