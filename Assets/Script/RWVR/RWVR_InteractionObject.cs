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

public abstract class RWVR_InteractionObject : MonoBehaviour
{
    protected Transform cachedTransform;
    [HideInInspector]
    public  RWVR_InteractionController currentController;

    public virtual void OnTriggerWasPressed(RWVR_InteractionController controller)
    {
        currentController = controller;
    }

    public virtual void OnTriggerIsBeingPressed(RWVR_InteractionController controller)
    {
    }

    public virtual void OnTriggerWasReleased(RWVR_InteractionController controller)
    {
        currentController = null;
    }

    public virtual void Awake()
    {
        cachedTransform = transform;
        if (!gameObject.CompareTag("InteractionObject"))
        {
            Debug.LogWarning("This InteractionObject does not have the correct tag, setting it now.", gameObject);
            gameObject.tag = "InteractionObject";
        }
    }

    public bool IsFree()
    {
        return currentController == null;
    }

    public virtual void OnDestroy()
    {
        if (currentController)
        {
            OnTriggerWasReleased(currentController);
        }
    }
}
