using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _dollarText;
    public static int _dolarPrice;

    [SerializeField] private TextMeshPro _countText;
    public static int _countPrice;

    [SerializeField]private GameObject _finalScoreArrow;
    private GameObject _tapToStart;

    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _arrowScore;
    private AudioSource _audio;

    private GameObject _shop;
    private GameObject _store;


    public Image cooldown;
    public static bool coolingDown = false;
    public float waitTime = 30.0f;

    private void OnEnable()
    {
        _finalScoreArrow = GameObject.FindWithTag("FinalScoreArrow");
        _tapToStart = GameObject.FindWithTag("TapToStart"); 
           
        _store = GameObject.FindWithTag("StoreLeft");
        _shop = GameObject.FindWithTag("ShopRight");

        EventHolder.instance.OnCakeCollected += AddCakePrice;
        EventHolder.instance.OnAddChocolate += AddChocolate;
        EventHolder.instance.OnAddCrema += AddCrema;
        EventHolder.instance.OnAddStrawberry += AddStrawberry;
        EventHolder.instance.OnGetHarmFromObstacle += CollideObstacle;
        EventHolder.instance.OnSelled += SellCake;
        EventHolder.instance.OnRotateArrow += RotateArrow;
    }
    private void Start()
    {
        //_tapToStart.transform.DOScale(2, 1).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        foreach (var item in _dollarText)
        {
            item.text = _dolarPrice.ToString();
        }
       // _countText.text = (_countPrice * 200).ToString();
        _countText.text = _countPrice.ToString();


        if (coolingDown == true)
        {
            cooldown.fillAmount += 10.0f / waitTime * Time.deltaTime;
        }

    }
    private void RotateArrow()
    {
        Debug.Log("reotate");
        float angelStep = 180 / 12;
        float moveAngel = (float)(_countPrice / 1000) * angelStep;
        Vector3 rotate = new Vector3(_finalScoreArrow.transform.rotation.eulerAngles.x - moveAngel, -90,0);
        _finalScoreArrow.transform.DOLocalRotate(rotate, 3);
        Invoke("PlaySound", 1);
        //_finalScoreArrow.transform.localRotation = Quaternion.Slerp(_finalScoreArrow.transform.localRotation, Quaternion.Euler(_finalScoreArrow.transform.localRotation.x - _arrowScore, -90, -0), Time.deltaTime * _arrowSpeed);
    }

    //public void FillAmountWinCake()
    //{
    //    if (coolingDown == true)
    //    {
    //        cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
    //    }
    //}
    private void PlaySound()
    {
        _audio = _finalScoreArrow.GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
    }
    private void SellCake(int sellPrice)
    {
        _dolarPrice += sellPrice;
        _countPrice += sellPrice;
    }

    private void CollideObstacle(int harmPrice)
    {
        _dolarPrice -= harmPrice;
        _countPrice -= harmPrice;
    }

    private void AddStrawberry(int strawPrice)
    {
        _dolarPrice += strawPrice;
        _countPrice += strawPrice;
    }
    private void AddCrema(int cremaPrice)
    {
        _dolarPrice += cremaPrice;
        _countPrice += cremaPrice;
    }

    private void AddChocolate(int chocoPrice)
    {
        _dolarPrice += chocoPrice;
        _countPrice += chocoPrice;
    }

    private void AddCakePrice(int cakePrice)
    {
        _dolarPrice += cakePrice;
        _countPrice += cakePrice;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void MoveHome()
    {
        _store.transform.DOMoveX(_store.transform.position.x - 600, 3);
        _shop.transform.DOMoveX(_shop.transform.position.x + 600, 3);

        _audio = GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CollectedCoffee.isFinished = false;

        PlayerController._vCam3 = false;
        Vector3 rotate = new Vector3(70, -90, 0);
        _finalScoreArrow.transform.DOLocalRotate(rotate, 3);

        _countPrice= 0;
    }
}
