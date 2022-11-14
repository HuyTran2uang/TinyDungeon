using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    private void Update()
    {
        //flip
        if (transform.parent.localScale.x > 0)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y);
        if (transform.parent.localScale.x < 0)
            transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y);
    }
}
