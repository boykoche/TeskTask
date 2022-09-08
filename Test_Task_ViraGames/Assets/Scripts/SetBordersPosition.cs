using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBordersPosition : MonoBehaviour
{
    [SerializeField] private GameObject _leftBorder;
    [SerializeField] private GameObject _rightBorder;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    private const float _RESOLUTION_RATIO = 0.5f;
    private const float _POS_X = 2.6f;
    private float _currentResolutionRatio;

    private void Awake()
    {
        float width = Screen.width;
        float height = Screen.height;
        _currentResolutionRatio = width / height;
        float currentPosX = _currentResolutionRatio * _POS_X / _RESOLUTION_RATIO;
        _leftBorder.transform.position = new Vector3(-currentPosX, 0, 0);
        _rightBorder.transform.position = new Vector3(currentPosX, 0, 0);
        _trajectoryRenderer.SetBorderX(currentPosX);
    }
}
