using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class ObstacleMovement : MonoBehaviour
{
    private AudioSource _audio;
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Insert(0.1f, transform.DOLocalMoveY(0, 0.45f)).Insert(0.1f, transform.DOScaleY(1.2f, 0.45f));
        seq.SetLoops(-1, LoopType.Yoyo);

        //_audio = GetComponent<AudioSource>();
        //_audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
