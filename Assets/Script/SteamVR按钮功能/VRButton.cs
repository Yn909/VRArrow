using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.Extras;

public class VRButton : MonoBehaviour
{
    private SteamVR_LaserPointer SteamVrLaserPointer;
    public UnityEvent mOnEnter = null;
    public UnityEvent mOnClick = null;
    public UnityEvent mOnUp = null;
    public bool isHasColorTrans = true;
    private Image image;
    [Header("放大和缩小参数")]
    public Vector3 Big, little, Click;
    void Awake()
    {
        Big = new Vector3(1.2f, 1.2f, 1.2f);
        Click = new Vector3(0.8f, 0.8f, 0.8f);
        little = new Vector3(1.0f, 1.0f, 1.0f);
        image = this.GetComponent<Image>();
        SteamVrLaserPointer = FindObjectOfType<SteamVR_LaserPointer>();

    }
    void OnEnable()
    {
        SteamVrLaserPointer.PointerClick += SteamVrLaserPointer_PointerClick;
        SteamVrLaserPointer.PointerIn += SteamVrLaserPointer_PointerIn;
        SteamVrLaserPointer.PointerOut += SteamVrLaserPointer_PointerOut;
        mOnEnter.AddListener(OnButtonEnter);
        mOnClick.AddListener(OnButtonClick);
        mOnUp.AddListener(OnButtonUp);
    }

    void OnDestroy()
    {
        SteamVrLaserPointer.PointerClick -= SteamVrLaserPointer_PointerClick;
        SteamVrLaserPointer.PointerIn -= SteamVrLaserPointer_PointerIn;
        SteamVrLaserPointer.PointerOut -= SteamVrLaserPointer_PointerOut;
    }

    private void SteamVrLaserPointer_PointerOut(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject == this.gameObject)
        {
            if (mOnUp != null) mOnUp.Invoke();
        }
    }

    private void SteamVrLaserPointer_PointerIn(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject == this.gameObject)
        {
            if (mOnEnter != null) mOnEnter.Invoke();
        }
    }

    private void SteamVrLaserPointer_PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject == this.gameObject)
        {
            Debug.Log(e);
            if (mOnClick != null)
            {
                mOnClick.Invoke();
            }
        }
    }

    // 按下
    public void OnButtonClick()
    {
        if (isHasColorTrans)
        {
            image.color = new Color(157f / 255f, 157f / 255f, 157f / 255f);
            Debug.Log("OnButtonClick");
            Invoke("OnButtonUp", 0.15f);
        }

    }
    // 经过
    public void OnButtonEnter()
    {

        Sequence seq = DOTween.Sequence();
        if (isHasColorTrans)
        {

            seq.Append(transform.DOScale(Big, 0.07f).SetEase(Ease.OutQuad));
            //image.color = new Color(143f / 255f, 239f / 255f, 255f / 255f);
            //Debug.Log("OnButtonEnter");
        }
        else
        {
            seq.Append(transform.DOScale(little, 0.03f).SetEase(Ease.OutQuad));
        }
    }
    // 抬起
    public void OnButtonUp()
    {
        if (isHasColorTrans)
        {
            image.color = new Color(255, 255, 255);
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(Big, 0.07f).SetEase(Ease.OutQuad));
            seq.Append(transform.DOScale(little, 0.03f).SetEase(Ease.OutQuad));
        }
    }
}
