using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeFrightStateBehaviour : FrightStateBehaviour
{
    public float mMoveUpdateTimer;
    float mCurrentTimer = 0.0f;
    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mCurrentTimer = 0.0f;
        mController.pathCompletedEvent.AddListener(UpdatePath);
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(pFSM, stateInfo, layerIndex);
        mCurrentTimer += Time.deltaTime;
        if (mCurrentTimer >= mMoveUpdateTimer)
        {
            UpdatePath();
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(pFSM, stateInfo, layerIndex);
        mController.pathCompletedEvent.RemoveListener(UpdatePath);

    }

    void UpdatePath()
    {
        mCurrentTimer = 0.0f;
        Vector2 aPacmanPos = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
        Vector2 aOpPacmanDir = -mController.PacMan.MoveDirections[(int)mController.PacMan.moveDirection] * 5;
        mController.moveToLocation = aPacmanPos + aOpPacmanDir;
        mController.moveComplete();
    }


}

