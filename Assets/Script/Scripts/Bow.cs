using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
[ExecuteInEditMode]
public class Bow : MonoBehaviour
{
    public Transform attachedArrow; // 1
    public SkinnedMeshRenderer BowSkinnedMesh; // 2

    public float blendMultiplier = 353f; // 3
    public GameObject realArrowPrefab; // 4
    public Transform PullPoint;
    public float maxShootSpeed = 50; // 5
    public GameObject RightHand;
    public SteamVR_Input_Sources handType;  // 选择左手或右手
    //public AudioClip fireSound; // 6
    bool IsArmed()
    {
        return attachedArrow.gameObject.activeSelf;
    }
    // Update is called once per framefloat b;
    float b;
    void Update()
    {
        if (Vector3.Distance(RightHand.transform.position, PullPoint.position) < 0.1f && SteamVR_Input.GetState("default", "GrabGrip", handType))
        {
            PullPoint.position = new Vector3(PullPoint.position.x, PullPoint.position.y, RightHand.transform.position.z) /*new Vector3(RightHand.transform.position.z)*/;
        }
        //float distance = Vector3.Distance(PullPoint.position, attachedArrow.position); // 1

        Vector3 relativePos = PullPoint.InverseTransformPoint(attachedArrow.position) * attachedArrow.localScale.x;
        Vector3 forward = transform.position - PullPoint.position;
        float distance = Mathf.Abs(relativePos.z);
        b = Mathf.Max(0, distance * blendMultiplier);
        b = Mathf.Clamp(b, 0, 100);
        BowSkinnedMesh.SetBlendShapeWeight(0, b); // 2
        BowSkinnedMesh.gameObject.transform.up = forward;
        PullPoint.forward = forward;
    }

}
