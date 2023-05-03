using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RayFire;
using DamageNumbersPro;
using Unity.VisualScripting;
using CASP.CameraManager;
using Cinemachine;
using UnityEditor;
using System;

public class CollectedCoffee : MonoBehaviour
{
    public static bool isCollectedCake = false;
    //Sequence seq;
    [SerializeField] private GameObject _rayFireCake;
    public DamageNumber numberPrefab;
    public static bool isFinished = false;
    //private Transform _firstCakeTransform;
    public PlayerController _playerScript;
    public static float stop = 1;
    public static bool isSell = false;

    private int _cakePrice = 200;
    private int _chocoPrice = 75;
    private int _cremaPrice = 50;
    private int _strawPrive = 50;
    private int _sellPrice = 450;
    private int _harmPrice = 50;


    private AudioSource _audio;
    private void Start()
    {
        numberPrefab = Resources.Load<DamageNumber>("Prefabs/Clear");
        //_rayFireCake = Resources.Load<GameObject>("Prefabs/RayFireCake");
        _rayFireCake = GameObject.FindWithTag("RayFireCake");
        //_firstCakeTransform = GameObject.FindWithTag("FirstCakeTransform").transform;
        _playerScript = GameObject.FindWithTag("FirstCakeTransform").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterMachines(other);
        // Debug.Log("collectedCoffee");
        if (other.gameObject.CompareTag("Collectable"))
        {
            EventHolder.instance.CakeCollectStart(_cakePrice);
            DamageNumber damageNumber = numberPrefab.Spawn(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), _cakePrice);
            //Instantiate(numberPrefab, transform.position, Quaternion.identity);
            other.gameObject.tag = "Collected";
            CollectedCoffeeData.instance.CoffeeList.Add(other.gameObject.transform);
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

            PlaySound(other);
        }

        if (other.gameObject.CompareTag("Obstacle") && gameObject.layer != 7)
        {
            Debug.Log("Obstacle");
            transform.tag = "Untagged";
            EventHolder.instance.ObstacleEventStart(_harmPrice);
            //add rayfire via script
            //dondurur
            //for (int i = 0; i < 4; i++)
            //{
            //    RayfireRigid _rfr = transform.GetChild(i).gameObject.AddComponent<RayfireRigid>();
            //    _rfr.meshDemolition.amount = 5;
            //    _rfr.Demolish();
            //}
            //RayfireRigid rfr = gameObject.AddComponent<RayfireRigid>();
            //rfr.meshDemolition.amount = 5;
            //rfr.Demolish();            
            EventHolder.instance.ObstacleEventStart(transform);


            //replace model üith rayfire one
            //errorlar verir rigid velocity
            //GameObject go = Instantiate(_rayFireCake, transform.position, Quaternion.identity);
            //Destroy(this.gameObject);
            //Destroy(go, 2);



            CollectedCoffeeData.instance.CoffeeList.Remove(transform);
            CollectedCoffeeData.instance.CoffeeList.RemoveAll(s => s == null);
            Destroy(gameObject);
            for (int i = 0; i < CollectedCoffeeData.instance.pieces.Count; i++)
            {
                GameObject piece = Instantiate(CollectedCoffeeData.instance.pieces[i], transform.position, Quaternion.identity);
                //GameObject piece = Instantiate(_rayFireCake, transform.position, Quaternion.identity);
                //piece.GetComponent<Rigidbody>().AddExplosionForce(0.005f, transform.position, 0.5f, 0.5f);
                //Destroy(CollectedCoffeeData.instance.pieces[i].gameObject, 0.5f);
            }

            PlaySound(other);
        }


        if (other.gameObject.CompareTag("CakeBox"))
        {
            Debug.Log("cakeBox");
            transform.DOMove(new Vector3(other.transform.GetChild(2).transform.position.x, transform.position.y, other.transform.GetChild(2).transform.position.z), 0.5f)
            .OnComplete(() =>
            {
                other.transform.GetChild(1).gameObject.SetActive(false);
                other.transform.GetChild(2).gameObject.SetActive(true);
                other.GetComponent<Animator>().SetTrigger("TakeCake");
                //other.GetComponent<Animator>().enabled = false;
            });
            other.GetComponent<BoxCollider>().enabled = false;
            CollectedCoffeeData.instance.CoffeeList.Remove(transform);
            CollectedCoffeeData.instance.CoffeeList.RemoveAll(s => s == null);

            Destroy(this.gameObject, 0.5f);
        }

        if (other.gameObject.CompareTag("FreeHand") && gameObject.layer != 7)
        {
            Debug.Log("cakeBox");
            transform.DOMove(new Vector3(other.transform.GetChild(2).transform.position.x, transform.position.y, other.transform.GetChild(2).transform.position.z), 0.5f)
            .OnComplete(() =>
            {
                other.transform.GetChild(1).gameObject.SetActive(false);
                other.transform.GetChild(2).gameObject.SetActive(true);
                other.GetComponent<Animator>().SetTrigger("TakeCake");
                //other.GetComponent<Animator>().enabled = false;
            });
            other.GetComponent<BoxCollider>().enabled = false;
            CollectedCoffeeData.instance.CoffeeList.Remove(transform);
            CollectedCoffeeData.instance.CoffeeList.RemoveAll(s => s == null);

            Destroy(this.gameObject, 0.5f);

        }


        if (other.gameObject.CompareTag("Finish"))
        {
            //EventHolder.instance.ArriveFinishEventStart();
            Debug.Log("finish");
            isFinished = true;
            CameraManager.instance.OpenCamera("CM vcam2", 0.5f, CameraEaseStates.Linear);
            //CinemachineBrain.ActiveVirtualCamera.Follow = GetComponent<PlayerController>().gameObject;

        }

        if (other.gameObject.CompareTag("SellGate") && gameObject.layer != 7)
        {
            EventHolder.instance.SellEventStart(_sellPrice);
            DamageNumber damageNumber = numberPrefab.Spawn(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), _sellPrice);

            transform.tag = "Untagged";
            DropCake(transform);

            CollectedCoffeeData.instance.CoffeeList.Remove(transform);
            CollectedCoffeeData.instance.CoffeeList.RemoveAll(s => s == null);
            //transform.position += new Vector3(transform.position.x - 1, transform.position.y, transform.position.z) * Time.deltaTime;
            var seq = DOTween.Sequence();
            seq.Append(transform.DOMoveX(transform.position.x - 10, 0.5f)).Join(transform.DOMoveY(transform.position.y + 1, 0.1f)).OnComplete(() =>
            {
                Destroy(gameObject);
            });

            PlaySound(other);
        }
    }

    public void TriggerEnterMachines(Collider other)
    {
        if (other.gameObject.CompareTag("ChocolateMachine"))
        {
            EventHolder.instance.CakeCollectStart(_chocoPrice);

            transform.GetChild(1).gameObject.SetActive(true);
            DamageNumber damageNumber = numberPrefab.Spawn(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), _chocoPrice);

            PlaySound(other);
        }

        if (other.gameObject.CompareTag("CremaMachine"))
        {
            EventHolder.instance.CremaEventStart(_cremaPrice);

            transform.GetChild(2).gameObject.SetActive(true);
            DamageNumber damageNumber = numberPrefab.Spawn(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), _cremaPrice);

            PlaySound(other);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit(other);
    }

    public void TriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("StrawberryMachine"))
        {
            transform.GetChild(3).gameObject.SetActive(true);
            EventHolder.instance.StrawEventStart(_strawPrive);
            DamageNumber damageNumber = numberPrefab.Spawn(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), _strawPrive);
            PlaySound(other);

        }

    }

    public void PlaySound(Collider other)
    {
        _audio = other.GetComponent<AudioSource>();
        _audio.PlayOneShot(_audio.clip);
    }
    private void DropCake(Transform cake)
    {
        List<Transform> droppedCakes = new();
        int startIndex = CollectedCoffeeData.instance.CoffeeList.IndexOf(cake);
        if (startIndex <= -1) return;

        int endCount = CollectedCoffeeData.instance.CoffeeList.Count - 1 - startIndex;

        droppedCakes = CollectedCoffeeData.instance.CoffeeList.GetRange(startIndex + 1, endCount);

        foreach (var item in droppedCakes)
        {
            item.tag = "Collectable";
        }

        StartAnimation(droppedCakes);
        CollectedCoffeeData.instance.CoffeeList.RemoveRange(startIndex + 1, endCount);
    }

    private void StartAnimation(List<Transform> droppedCakes)
    {
        foreach (Transform cake in droppedCakes)
        {

            cake.DOJump(new Vector3(cake.position.x + UnityEngine.Random.Range(1, 4), cake.position.y, cake.position.z + UnityEngine.Random.Range(3, 7)), 1f, 1, 1f);
        }
    }
}
