using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIOnClik : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("adad");
        transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.1f).SetEase(Ease.OutQuad).OnComplete(() => { Debug.Log("End"); });
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
       
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1f), 0.07f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.03f).SetEase(Ease.OutQuad));
    }
}
