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

public class RWVR_SnapToController : RWVR_InteractionObject
{
    public bool hideControllerModel;
    public Vector3 snapPositionOffset;
    public Vector3 snapRotationOffset;

    private Rigidbody rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnTriggerWasPressed(RWVR_InteractionController controller)
    {
        base.OnTriggerWasPressed(controller);

        if (hideControllerModel)
        {
            controller.HideControllerModel();
        }

        ConnectToController(controller);
    }
    
    public override void OnTriggerWasReleased(RWVR_InteractionController controller)
    {
        base.OnTriggerWasReleased(controller);

        if (hideControllerModel)
        {
            controller.ShowControllerModel();
        }

        ReleaseFromController(controller);
    }

    private void ConnectToController(RWVR_InteractionController controller)
    {
        cachedTransform.SetParent(controller.transform);

        cachedTransform.rotation = controller.transform.rotation;
        cachedTransform.Rotate(snapRotationOffset);
        cachedTransform.position = controller.snapColliderOrigin.position;
        cachedTransform.Translate(snapPositionOffset, Space.Self);

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void ReleaseFromController(RWVR_InteractionController controller)
    {
        cachedTransform.SetParent(null);

        rb.useGravity = true;
        rb.isKinematic = false;

        rb.velocity = controller.velocity;
        rb.angularVelocity = controller.angularVelocity;
    }
}
