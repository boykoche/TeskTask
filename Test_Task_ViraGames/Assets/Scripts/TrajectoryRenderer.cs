using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRendererComponent;

    private float _power = 1.2f;
    private float _borderX;

    private bool _drag = true;

    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }
    public void SetBorderX(float value)
    {
        _borderX = value;
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        if (_drag)
        {
            Vector3[] points = new Vector3[70];
            lineRendererComponent.positionCount = points.Length;

            for (int i = 0; i < points.Length; i++)
            {
                float time = i * 0.01f;

                Vector3 position = origin + speed * time * _power + Physics.gravity * time * time / 2f;
                if (Mathf.Abs(position.x) > _borderX)
                {
                    int cofficient = position.x > 0 ? 1 : -1;
                    float difference = Mathf.Abs(position.x) - _borderX;
                    position = new Vector3(_borderX * cofficient - difference * cofficient, position.y, 0);
                }
                points[i] = position;

            }

            lineRendererComponent.SetPositions(points);
        }   
    }

    public void IsDrag(bool d)
    {
        _drag = d;
    }
}

