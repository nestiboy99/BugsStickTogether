using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public float LifeTime;

    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
    }
}
