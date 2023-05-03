using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Eraser : MonoBehaviour
{
    private void OnEnable() {
        EventHolder.instance.OnObstacleCollided += DropCake;
    }    
    
    private void OnDisable() {
        EventHolder.instance.OnObstacleCollided -= DropCake;
    }

    private void DropCake(Transform cake)
    {
        List<Transform> droppedCakes = new();
        int startIndex = CollectedCoffeeData.instance.CoffeeList.IndexOf(cake);
        if (startIndex <= -1) return;

        int endCount = CollectedCoffeeData.instance.CoffeeList.Count - 1 - startIndex;

        droppedCakes = CollectedCoffeeData.instance.CoffeeList.GetRange(startIndex+1, endCount);

        foreach (var item in droppedCakes)
        {
            item.tag = "Collectable";
        }

        StartAnimation(droppedCakes);
        CollectedCoffeeData.instance.CoffeeList.RemoveRange(startIndex+1, endCount);
    }

    private void StartAnimation(List<Transform> droppedCakes)
    {
        foreach(Transform cake in droppedCakes)
        {
            
            cake.DOJump(new Vector3(cake.position.x + Random.Range(1, 4), cake.position.y, cake.position.z + Random.Range(3, 7)),1f,1,1f);
        }
    }
}
