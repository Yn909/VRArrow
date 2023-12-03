/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System;
using Valve.VR;
public class RWVR_InteractionController : MonoBehaviour
{
    public Transform snapColliderOrigin;
    public GameObject ControllerModel;

    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Vector3 angularVelocity;

    private RWVR_InteractionObject objectBeingInteractedWith;

    private SteamVR_TrackedObject trackedObj;
    public SteamVR_Action_Pose poseAction;
    public SteamVR_Input_Sources inputSource; // 指定输入来源


    public RWVR_InteractionObject InteractionObject
    {
        get { return objectBeingInteractedWith; }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        // 注册输入事件，监听触发器按钮按下
        triggerAction.AddOnStateDownListener(TriggerButtonDown, inputSource);
        // 注册输入事件，监听触发器按钮释放
        triggerAction.AddOnStateUpListener(TriggerButtonUp, inputSource);
        // 初始化触觉反馈动作
        hapticAction = SteamVR_Actions.default_Haptic;

    }

    private void CheckForInteractionObject()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(snapColliderOrigin.position, snapColliderOrigin.lossyScale.x / 2f);

        foreach (Collider overlappedCollider in overlappedColliders)
        {
            if (overlappedCollider.CompareTag("InteractionObject") && overlappedCollider.GetComponent<RWVR_InteractionObject>().IsFree())
            {
                objectBeingInteractedWith = overlappedCollider.GetComponent<RWVR_InteractionObject>();
                objectBeingInteractedWith.OnTriggerWasPressed(this);
                return;
            }
        }
    }
    public SteamVR_Action_Boolean triggerAction;
     void TriggerButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        // 处理触发器按钮按下的逻辑
        CheckForInteractionObject();
    }
    void TriggerButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        // 处理触发器按钮释放的逻辑
        if (objectBeingInteractedWith)
        {
            objectBeingInteractedWith.OnTriggerWasReleased(this);
            objectBeingInteractedWith = null;
        }
    }
    void Update()
    {
        // 查询触发器按钮的当前状态
        bool isHairTriggerDown = triggerAction.GetState(inputSource);

        // 处理触发器按钮状态的逻辑
        if (isHairTriggerDown)
        {
            if (objectBeingInteractedWith)
            {
                objectBeingInteractedWith.OnTriggerIsBeingPressed(this);
            }
        }
    }

    private void UpdateVelocity()
    {
        velocity = poseAction.velocity;
        angularVelocity = poseAction.angularVelocity;
    }

    void FixedUpdate()
    {
        UpdateVelocity();
    }

    public void HideControllerModel()
    {
        ControllerModel.SetActive(false);
    }

    public void ShowControllerModel()
    {
        ControllerModel.SetActive(true);
    }
    public SteamVR_Action_Vibration hapticAction;
    public void Vibrate(ushort strength)
    {
        // 触发触觉反馈
        hapticAction.Execute(0, strength, 1, 1, inputSource);
    }

    public void SwitchInteractionObjectTo(RWVR_InteractionObject interactionObject)
    {
        objectBeingInteractedWith = interactionObject;
        objectBeingInteractedWith.OnTriggerWasPressed(this);
    }
}
