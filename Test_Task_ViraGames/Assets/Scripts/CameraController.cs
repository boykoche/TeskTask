using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    
    public void Move()
    {
        transform.DOMoveY(_ball.transform.position.y + 1f, 0.4f);
    }
}
