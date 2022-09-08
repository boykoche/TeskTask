using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarController : MonoBehaviour
{
    private Vector3 _moveVector = new Vector3(0, 0.01f, 0);
    private int _direction = 1;

    private void Awake()
    {
        transform.localPosition = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (transform.localPosition.y > 1.1f || transform.localPosition.y < 0) { _direction *= -1; }
        transform.localPosition += _moveVector * _direction;
    }
}
