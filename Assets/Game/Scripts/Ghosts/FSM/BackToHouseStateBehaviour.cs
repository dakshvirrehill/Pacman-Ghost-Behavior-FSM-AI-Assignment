using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToHouseStateBehaviour : GhostBehaviour
{
    public float mBTHSpeed;


    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM,false);
        mController.currSpeed = mBTHSpeed;
        mController.moveToLocation = mController.ReturnLocation;
        mController.moveComplete();
        mController.pathCompletedEvent.AddListener(Restart);
    }
    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mController.pathCompletedEvent.RemoveListener(Restart);
    }

    void Restart()
    {
        switch(GameDirector.Instance.state)
        {
            case GameDirector.States.enState_PacmanInvincible:
            case GameDirector.States.enState_Normal:
                {
                    mFSM.SetTrigger(mController.mRestart);
                    break;
                }
            case GameDirector.States.enState_GameOver:
                {
                    break;
                }
        }
        mAnimator.SetBool(mController.mIsDead, false);
    }

}

