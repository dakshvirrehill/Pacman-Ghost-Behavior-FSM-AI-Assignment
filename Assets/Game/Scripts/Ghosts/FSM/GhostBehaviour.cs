using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : StateMachineBehaviour
{
    protected Transform mGhostTransform;
    protected GhostController mController;
    protected Animator mAnimator;
    protected Animator mFSM;

    protected bool mReverseDirection = true;

    protected void SetupComponentReferences(Animator pFSM)
    {
        if(mController != null)
        {
            return;
        }
        mGhostTransform = pFSM.transform.parent;
        mFSM = pFSM;
        mController = mGhostTransform.GetComponent<GhostController>();
        mAnimator = mGhostTransform.GetComponent<Animator>();
        GameDirector.Instance.GameStateChanged.AddListener(GameStateChangeCallback);
    }

    protected void StateExit()
    {
        GameDirector.Instance.GameStateChanged.RemoveListener(GameStateChangeCallback);
    }

    void GameStateChangeCallback(GameDirector.States pState)
    {
        if (pState == GameDirector.States.enState_PacmanInvincible)
        {
            mReverseDirection = false;

        }
    }

    protected void ReverseDirection()
    {
        Vector2 aReversePos = Vector2.zero;
        switch(mController.mDirection)
        {
            case GhostController.MovementDirection.Down:
                {
                    aReversePos = Vector2.up;
                    break;
                }
            case GhostController.MovementDirection.Up:
                {
                    aReversePos = Vector2.down;
                    break;
                }
            case GhostController.MovementDirection.Left:
                {
                    aReversePos = Vector2.right;
                    break;
                }
            case GhostController.MovementDirection.Right:
                {
                    aReversePos = Vector2.left;
                    break;
                }
        }
        aReversePos += new Vector2(mGhostTransform.position.x, mGhostTransform.position.y);
        mController.moveToLocation = aReversePos;
        mController.moveComplete();
    }


}
