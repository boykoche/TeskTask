using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragController : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private  HoopController _hoopController;
    [SerializeField] private  UIManager _UImanager;

    private Vector2 _startPoint;
    private Vector2 _direction;

    private float _dragWidthMin;
    private float _dragWidthMax;
    private float _dragStep;

    private const int _maxSpeed = 7;

    private bool _firstDrag = false;

    private void Awake()
    {
        float minResolutionValue = Mathf.Min(Screen.width, Screen.height);
        _dragWidthMax = minResolutionValue/2;
        _dragWidthMin = minResolutionValue / 10;
        _dragStep = (_dragWidthMax - _dragWidthMin)/(_maxSpeed - 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _hoopController.SetFirstTouch(eventData.position);
        _startPoint = eventData.position;
        _hoopController.IsTouching(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_firstDrag ) { _firstDrag = true; _UImanager.FirstDrag(); }
        _direction = eventData.position - _startPoint;
        _hoopController.TouchDragging(SetSpeed(), _direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _hoopController.LetGoTouching();
        _hoopController.TouchDragging(0,Vector2.zero);
    }

    private float SetSpeed()
    {
        float speed;
        float maxValue = Mathf.Max(Mathf.Abs(_direction.x), Mathf.Abs(_direction.y));
        if(maxValue > _dragWidthMax)
        {
            speed = _maxSpeed;
        }
        else if (maxValue > _dragWidthMin)
        {
            speed = maxValue / _dragStep;
            if(speed > _maxSpeed) { speed = _maxSpeed; }
        }
        else
        {
            speed = 0;
        }
        return speed;  
    }
}
