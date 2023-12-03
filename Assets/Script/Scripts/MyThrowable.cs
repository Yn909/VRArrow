using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class MyThrowable : Throwable
{
    public SteamVR_Input_Sources handType;  // 选择左手或右手
    public SteamVR_Action_Boolean grabAction;  // 抓取动作
    protected override void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (startingGrabType != GrabTypes.None && SteamVR_Input.GetState("default", "GrabGrip", handType))
        {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            hand.HideGrabHint();
        }
    }
    //粘住被抓取物体
    protected override void HandAttachedUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (startingGrabType != GrabTypes.None && SteamVR_Input.GetState("default", "GrabGrip", handType))
        {
            //hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            //hand.HideGrabHint();
        }
    }
    //避免换手
    protected override void OnHandFocusAcquired(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (startingGrabType != GrabTypes.None && SteamVR_Input.GetState("default", "GrabGrip", handType))
        {
            //hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            //hand.HideGrabHint();
        }
    }
}
