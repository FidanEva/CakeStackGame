using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CremeMachineMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(1, 0.3f));
        seq.SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
