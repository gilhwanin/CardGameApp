using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        if(CardList.instance != null)
        {
            CardList.instance.DestroyThis();
        }
    }
    void Update()
    { 
    }
}
