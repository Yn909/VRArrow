
using UnityEngine;
using System.Collections;
using System;

public class RWVR_ArrowInBow : RWVR_InteractionObject
{
    public float minimumPosition;
    public float maximumPosition;

    private Transform attachedBow;
    private const float arrowCorrection = 0.3f;

    public override void Awake()
    {
        base.Awake();
        attachedBow = transform.parent;
    }

    public override void OnTriggerIsBeingPressed(RWVR_InteractionController controller)
    {
        base.OnTriggerIsBeingPressed(controller);

        Vector3 arrowInBowSpace = attachedBow.InverseTransformPoint(controller.transform.position);
        cachedTransform.localPosition = new Vector3(0, 0, arrowInBowSpace.z + arrowCorrection);
    }

    public override void OnTriggerWasReleased(RWVR_InteractionController controller)
    {
      //  attachedBow.GetComponent<Bow>().ShootArrow();
        currentController.Vibrate(3500);
        base.OnTriggerWasReleased(controller);
    }

    void LateUpdate()
    {
        // Limit position
        float zPos = cachedTransform.localPosition.z;
        zPos = Mathf.Clamp(zPos, minimumPosition, maximumPosition);
        cachedTransform.localPosition = new Vector3(0, 0, zPos);

        //Limit rotation
        cachedTransform.localRotation = Quaternion.Euler(Vector3.zero);

        if (currentController)
        {
            currentController.Vibrate(Convert.ToUInt16(500 * -zPos));
        }
    }
}
