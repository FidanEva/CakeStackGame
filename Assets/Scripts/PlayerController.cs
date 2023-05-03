using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using CASP.CameraManager;
using DamageNumbersPro;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Horizontal;
    [SerializeField] float VerticalSpeed;
    public float SpeedMultiplier;
    public List<Transform> Coffees;
    //[SerializeField] float OffsetZ = 2;
    [SerializeField] float LerpSpeed = 1;
    [SerializeField] private float OffSet = 2f;
    public PlayerController Parent;
    //Sequence seq;


    private CollectedCoffee _collectedCoffee;

    [SerializeField] private List<Transform> _cakesTransform;

    private float _timeRate = 0.5f;
    private float _nectTime = 0.5f;
    private GameObject _finalScoreArrow;

    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _arrowScore;

    public static bool _vCam3 = false;
    [SerializeField] private GameObject _moneyParticle;
    [SerializeField] private GameObject _winPanel;

    public DamageNumber numberPrefab;
    public DamageNumber finalNumberPrefab;

    private AudioSource _audio;
    private void Awake()
    {
        Time.timeScale = 0;
    }
    void Start()
    {
        // Coffees.Add(transform.GetChild(0));
        CollectedCoffeeData.instance.CoffeeList.Add(transform.GetChild(0));
        CollectedCoffeeData.instance.CoffeeList[0].gameObject.AddComponent<CollectedCoffee>();
        _collectedCoffee = CollectedCoffeeData.instance.CoffeeList[0].gameObject.GetComponent<CollectedCoffee>();
        //_firstCakeTransform = GameObject.FindWithTag("FirstCakeTransform").transform;
        _finalScoreArrow = GameObject.FindWithTag("FinalScoreArrow");

        numberPrefab = Resources.Load<DamageNumber>("Prefabs/Clear");
        finalNumberPrefab = Resources.Load<DamageNumber>("Prefabs/finalscore");

    }
    //private void OnEnable()
    //{
    //    EventHolder.instance.OnArriveFinish += StopMovement;
    //}
    //private void OnDisable()
    //{
    //    EventHolder.instance.OnArriveFinish -= StopMovement;
    //}

    //private void StopMovement()
    //{
    //    StopAllCoroutines();
    //}

    void Update()
    {
        //StartCoroutine("Stop");
        float s = CollectedCoffee.stop;
        Horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Horizontal / 7, 0, VerticalSpeed * SpeedMultiplier * s * Time.deltaTime);
        float xPos = Mathf.Clamp(transform.position.x, -6.5f, 7f);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        if (CollectedCoffee.isFinished)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }


        if (CollectedCoffeeData.instance.CoffeeList.Count > 1)
        {
            CoffeeFollow();
        }

        //if (_vCam3)
        //{
        //    _vCam3 = false;
        //    EventHolder.instance.ArrowRotateStart();
        //    //_finalScoreArrow.transform.localRotation = Quaternion.Slerp(_finalScoreArrow.transform.localRotation, Quaternion.Euler(_finalScoreArrow.transform.localRotation.x - _arrowScore, -90, -0), Time.deltaTime * _arrowSpeed);
        //    Invoke("VVCam4", 3);

        //}
    }
    IEnumerator VVCam4()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(UIManager._dolarPrice);
        CameraManager.instance.OpenCamera("CM vcam4", 1.5f, CameraEaseStates.EaseInOut);
        DamageNumber finalDamageNumber = finalNumberPrefab.Spawn(new Vector3(_finalScoreArrow.transform.position.x, _finalScoreArrow.transform.position.y + 10, _finalScoreArrow.transform.position.z), UIManager._dolarPrice);
        _audio = finalNumberPrefab.GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
        //_moneyParticle.GetComponent<ParticleSystem>().Play();
        _moneyParticle.SetActive(true);
        Invoke("CallWinPanel", 3);


        _audio = GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
    }

    void CallWinPanel()
    {
        _winPanel.SetActive(true);
        UIManager.coolingDown = true;
    }
    //IEnumerator Stop()
    //{
    //    yield return new WaitForSeconds(0.01f);
    //    Horizontal = Input.GetAxis("Horizontal");
    //    transform.Translate(Horizontal / 7, 0, VerticalSpeed * SpeedMultiplier * Time.deltaTime);
    //    float xPos = Mathf.Clamp(transform.position.x, -6.5f, 7f);
    //    transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            CollectedCoffeeData.instance.CoffeeList.Add(other.gameObject.transform);
            other.gameObject.tag = "Collected";
            other.gameObject.AddComponent<CollectedCoffee>();
            if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            else
            {
                other.gameObject.AddComponent<Rigidbody>().isKinematic = true;

            }
            var seq = DOTween.Sequence();
            seq = DOTween.Sequence();
            seq.Kill();
            seq = DOTween.Sequence();
            for (int i = CollectedCoffeeData.instance.CoffeeList.Count - 1; i > 0; i--)
            {
                seq.WaitForCompletion(true);
                seq.Join(CollectedCoffeeData.instance.CoffeeList[i].DOScale(1.5f, 0.2f));
                seq.AppendInterval(0.05f);
                seq.Join(CollectedCoffeeData.instance.CoffeeList[i].DOScale(1f, 0.2f));
            }

            _audio = other.GetComponent<AudioSource>();
            _audio.PlayOneShot(_audio.clip);
        }

        if (other.gameObject.CompareTag("CakeCabinet"))
        {
            SpeedMultiplier = 0;
            //for (float i = 0; i < 7; i += 6)
            //{
            foreach (Transform cake in _cakesTransform)
            {
                //if (Time.time > _timeRate)
                //{
                //_timeRate += _nectTime;
                if (CollectedCoffeeData.instance.CoffeeList.Count > 0)
                {

                    _audio = other.GetComponent<AudioSource>();
                    _audio.PlayOneShot(_audio.clip);
                    CollectedCoffeeData.instance.CoffeeList[CollectedCoffeeData.instance.CoffeeList.Count - 1].DOMove(cake.position, 0.5f)
                        .OnComplete(() =>
                        {
                            SpeedMultiplier = 40;
                        });
                    CollectedCoffeeData.instance.CoffeeList.RemoveAt(CollectedCoffeeData.instance.CoffeeList.Count - 1);
                    CollectedCoffeeData.instance.CoffeeList.RemoveAll(s => s == null);
                    //}
                }
                else
                {
                    SpeedMultiplier = 0;
                    CameraManager.instance.OpenCamera("CM vcam3", 1, CameraEaseStates.EaseInOut);
                    //_finalScoreArrow.GetComponent<FinalScoreArrow>().RotateScoreArrow();
                    //_vCam3 = true;
                }

                //}
            }

            if (CollectedCoffeeData.instance.CoffeeList.Count <= 0)
            {
                if (!_vCam3)
                {
                    EventHolder.instance.ArrowRotateStart();
                    StartCoroutine("VVCam4");
                    _vCam3 = true;
                }

            }
        }

        _collectedCoffee.TriggerEnterMachines(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _collectedCoffee.TriggerExit(other);

        if (other.gameObject.CompareTag("CakeCabinet"))
        {
            _vCam3 = true;
        }
    }
    public void CoffeeFollow()
    {
        // Checking List for Distance
        for (int i = 1; i < CollectedCoffeeData.instance.CoffeeList.Count; i++)
        {
            Vector3 PrevPos = CollectedCoffeeData.instance.CoffeeList[i - 1].position + Vector3.forward * OffSet;
            CollectedCoffeeData.instance.CoffeeList.RemoveAll(s => s == null);
            PrevPos.y = CollectedCoffeeData.instance.CoffeeList[i].position.y;
            Vector3 CurrentPos = CollectedCoffeeData.instance.CoffeeList[i].transform.position;
            CollectedCoffeeData.instance.CoffeeList[i].transform.position = Vector3.Lerp(CurrentPos, PrevPos, LerpSpeed * Time.deltaTime);
        }
    }
}
