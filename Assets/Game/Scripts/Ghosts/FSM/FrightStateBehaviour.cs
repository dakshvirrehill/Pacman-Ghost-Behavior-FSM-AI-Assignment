using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightStateBehaviour : GhostBehaviour
{
    public float mSpeedUpdater;

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        GameDirector.Instance.GameStateChanged.AddListener(OnStateChange);
        mController.currSpeed = mController.speed * mSpeedUpdater;
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(mAnimator.GetBool(mController.mIsDead))
        {
            mFSM.SetTrigger(mController.mToHouse);
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateExit();
        GameDirector.Instance.GameStateChanged.RemoveListener(OnStateChange);
    }

    void OnStateChange(GameDirector.States pState)
    {
        if(pState == GameDirector.States.enState_Normal)
        {
            if (mController.mIsChasing)
            {
                mFSM.SetTrigger(mController.mChase);
            }
            else
            {
                mFSM.SetTrigger(mController.mScatter);
            }
        }
    }

}

