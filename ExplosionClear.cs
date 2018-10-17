using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionClear : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }
}
