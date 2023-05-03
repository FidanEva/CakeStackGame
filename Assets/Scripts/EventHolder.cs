using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventHolder : MonoBehaviour
{
    public event Action<Transform> OnObstacleCollided;
    //public event Action OnArriveFinish;
    public event Action<int> OnCakeCollected;
    public event Action<int> OnAddChocolate;
    public event Action<int> OnAddCrema;
    public event Action<int> OnAddStrawberry;
    public event Action<int> OnSelled;
    public event Action<int> OnGetHarmFromObstacle;
    public event Action OnRotateArrow;

    private static EventHolder _instance;
    public static EventHolder instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<EventHolder>();
            }
            return _instance;
        }
    }
    private void Awake() 
    {
        if (_instance == null) 
        {
            _instance = this;
        }
    }

    public void ObstacleEventStart( Transform cake)
    {
        OnObstacleCollided?.Invoke(cake);

    }

    public void CakeCollectStart(int cakePrice)
    {
        OnCakeCollected?.Invoke(cakePrice);
    }

    public void ChocolateEventStart(int chocoPrice)
    {
        OnAddChocolate?.Invoke(chocoPrice);
    }    

    public void CremaEventStart(int cremaPrice)
    {
        OnAddCrema?.Invoke(cremaPrice);
    }

    public void StrawEventStart(int strawberryPrice)
    {
        OnAddStrawberry?.Invoke(strawberryPrice);
    }
    public void SellEventStart(int sellPrice)
    {
        OnSelled?.Invoke(sellPrice);
    }
    public void ObstacleEventStart(int harmPrice)
    {
        OnGetHarmFromObstacle?.Invoke(harmPrice);
    }

    public void ArrowRotateStart()
    {
        OnRotateArrow?.Invoke();
    }    
    //public void ArriveFinishEventStart()
    //{
    //    OnArriveFinish?.Invoke();
    //}

}
