using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BowFunc : MonoBehaviour
{
    public Transform StartPoint, EndPoint;
    public Transform attachedArrow; // 1
    public SkinnedMeshRenderer BowSkinnedMesh; // 2

    public float blendMultiplier = 255f; // 3
    public GameObject realArrowPrefab; // 4

    public float maxShootSpeed = 50; // 5

    public AudioClip fireSound; // 6
    bool IsArmed()
    {
        return attachedArrow.gameObject.activeSelf;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, attachedArrow.position); // 1
        BowSkinnedMesh.SetBlendShapeWeight(0, Mathf.Max(0, distance * blendMultiplier)); // 2
    }
    private void Arm() // 1
    {
        attachedArrow.gameObject.SetActive(true);
    }

    private void Disarm()
    {
        BowSkinnedMesh.SetBlendShapeWeight(0, 0); // 2
        attachedArrow.position = transform.position; // 3
        attachedArrow.gameObject.SetActive(false); // 4
    }
    
    ////计算，hand当前的位置，位于AB两点的中间的百分比，靠近A返回趋近0（小于返回负），靠近B返回趋近1（大于返回大于1的值）
    //private float CalculateLocation01(Transform hand)
    //{
    //    //获取起点指向终点的向量
    //    Vector3 OriginVec3 = EndPoint.position - StartPoint.position;

    //    //获取长度
    //    float length = OriginVec3.magnitude;
    //    OriginVec3.Normalize();

    //    //获取起始点，指向手的向量
    //    Vector3 InPos2Hand = hand.position - StartPoint.position;

    //    //借助向量投影，计算手的当前位置，相对于AB两点的哪里，并抽象成0-1的值。小于A点为小于0的值，大于B点为大于1的值
    //    return Vector3.Dot(InPos2Hand, OriginVec3) / length;
    //}
    //protected virtual void HandAttachedUpdate(Hand hand)
    //{
    //    //计算位置 0-1
    //    float currentLocation = CalculateLocation01(hand.transform);
    //    //通过Vec3.Lerp，靠近0趋近A点，靠近1趋近B点，来设置物品的位置
    //    transform.position = Vector3.Lerp(StartPoint.position, EndPoint.position, currentLocation);
    //    //Debug.Log(currentLocation);
    //}
}

