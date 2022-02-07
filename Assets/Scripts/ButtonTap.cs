using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonTap : MonoBehaviour
{
    public float duration;
    public Ease enEase;
    public GameEvent Event;

    public void DoAnim()
    {
        transform.localScale = Vector3.one * 0.7f;
        transform.DOScale(Vector3.one, duration).SetEase(enEase).SetUpdate(true);
        StartCoroutine(RaiseEvent(duration * 0.5f));
    }

    IEnumerator RaiseEvent(float waitTime)
    {
        if(Event == null) yield break;

        yield return new WaitForSeconds(waitTime);

        Event.Raise();
    }

}