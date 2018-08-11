using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGlue : PieceBase {

    public enum GlueState
    {
        DEACTIVATED,
        WARMUP,
        IN_USE
    }

    [SerializeField] private FixedJoint2D fixedJoint;

    public float glueWarmUp = .2f;
    public float glueDuration = .1f;
    public GlueState state = GlueState.DEACTIVATED;

    private float startUseTime;

    public override void Activated()
    {
        base.Activated();
        state = GlueState.WARMUP;
    }

    // Update is called once per frame
    void Update () {
        switch (state)
        {
            case GlueState.DEACTIVATED:
                break;
            case GlueState.WARMUP:
                if (activatedTime + glueWarmUp < Time.time)
                {
                    fixedJoint.enabled = true;
                    startUseTime = Time.time;
                    state = GlueState.IN_USE;
                }
                break;
            case GlueState.IN_USE:
                if (startUseTime + glueDuration < Time.time)
                {
                    fixedJoint.enabled = false;
                    state = GlueState.DEACTIVATED;
                }
                break;
        }
        
	}
}
