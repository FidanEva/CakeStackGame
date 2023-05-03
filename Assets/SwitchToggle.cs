using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SwitchToggle : MonoBehaviour
{
    private int switchState = 1;
    public GameObject switchBtn;

    public void OnSwitchButtonClicked()
    {
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, 0.2f);
        switchState = Math.Sign(-switchBtn.transform.localPosition.x);
    }

}









//using UnityEngine;
 //using UnityEngine.UI;
 //using DG.Tweening;

//public class SwitchToggle : MonoBehaviour
//{
//    [SerializeField] RectTransform uiHandleRectTransform;
//    [SerializeField] Color backgroundActiveColor;
//    [SerializeField] Color handleActiveColor;

//    Image backgroundImage, handleImage;

//    Color backgroundDefaultColor, handleDefaultColor;

//    Toggle toggle;

//    Vector2 handlePosition;

//    void Awake()
//    {
//        toggle = GetComponent<Toggle>();

//        handlePosition = uiHandleRectTransform.anchoredPosition;

//        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
//        handleImage = uiHandleRectTransform.GetComponent<Image>();

//        backgroundDefaultColor = backgroundImage.color;
//        handleDefaultColor = handleImage.color;

//        toggle.onValueChanged.AddListener(OnSwitch);

//        if (toggle.isOn)
//            OnSwitch(true);
//    }

//    void OnSwitch(bool on)
//    {
//        //uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; // no anim
//        uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);

//        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
//        backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, .6f);

//        //handleImage.color = on ? handleActiveColor : handleDefaultColor ; // no anim
//        handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .4f);
//    }

//    void OnDestroy()
//    {
//        toggle.onValueChanged.RemoveListener(OnSwitch);
//    }
//}