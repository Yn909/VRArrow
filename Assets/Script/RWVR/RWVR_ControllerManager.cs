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

public class RWVR_ControllerManager : MonoBehaviour
{
    public static RWVR_ControllerManager Instance;

    public RWVR_InteractionController leftController;
    public RWVR_InteractionController rightController;

    private void Awake()
    {
        Instance = this;
    }

    //Generic method to find out if any of the controllers is interacting with an object with a certain script attached
    public bool AnyControllerIsInteractingWith<T>()
    {
        if (leftController.InteractionObject && leftController.InteractionObject.GetComponent<T>() != null)
        {
            return true;
        }

        if (rightController.InteractionObject && rightController.InteractionObject.GetComponent<T>() != null)
        {
            return true;
        }

        return false;
    }
}