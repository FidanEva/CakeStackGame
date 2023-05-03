using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class BoxMovement : MonoBehaviour
{
    private AudioSource _audio;

    public void HandGetBox()
    {
        //transform.GetChild(2).parent = transform.GetChild(0);
        transform.GetChild(2).DOLocalMoveX(-4, 0.2f);
        _audio = GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
    }
}
