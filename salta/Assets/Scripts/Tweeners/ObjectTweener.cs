using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectTweener : MonoBehaviour
{
    [SerializeField] private float speed;

     [SerializeField] private float height;

    public void moveTo(Transform pTransform, Vector3 pTargetPosition) {
        float distance = Vector3.Distance(pTargetPosition, pTransform.position);      
      
        transform.position = pTargetPosition;
        //transform.DOMove(pTargetPosition, distance / speed);
    }
    public void jumpTo(Transform pTransform, Vector3 pTargetPosition) {
        float distance = Vector3.Distance(pTargetPosition, pTransform.position);
        transform.position = pTargetPosition;
        //transform.DOJump(pTargetPosition, height, 1, distance / speed);
    }
}
