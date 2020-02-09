using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : StateMachineBehaviour
{
    protected Transform mGhostTransform;
    protected GhostController mController;
    protected Animator mAnimator;
    protected Animator mFSM;

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
    }

}
