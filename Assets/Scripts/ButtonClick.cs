using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class ButtonClick : MonoBehaviour
{
    private AudioSource _audio;
    public void PlaySound()
    {
        _audio = GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
    }
}
