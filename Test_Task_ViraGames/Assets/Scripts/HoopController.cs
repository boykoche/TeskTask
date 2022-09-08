using DG.Tweening;
using UnityEngine;


public class HoopController : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _hoopPrefab;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private GameObject _borders;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    private HoopData currentHoop;
    private HoopData _previousHoop;
    private HoopData _nextHoop;

    private bool _isTouch = false;
    private bool _flightAbility = false;
    private float _speed;
    private Vector2 _direction;
    private Vector3 _firstTouch;

    public bool hoop = false;

    private void FixedUpdate()
    {
        if (_isTouch && _flightAbility)
        {
            Vector3 difference = Input.mousePosition - _firstTouch;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90;
            if(rotateZ == 90) { rotateZ = 0; }
            currentHoop.transform.rotation = Quaternion.Euler(0, 0, rotateZ);

            if (_speed > 2)
            {
                currentHoop.SetGridSize(CalculateGridSize());
                _trajectoryRenderer.gameObject.SetActive(true);
                _trajectoryRenderer.ShowTrajectory(currentHoop.transform.position, -_direction * _speed);
            }
            else
            {
                _trajectoryRenderer.gameObject.SetActive(false);
            }
        }
    }

    public void SetFirstTouch(Vector3 touch)
    {
        _firstTouch = touch;
    }

    public void TouchDragging(float speed, Vector2 direction)
    {
        _speed = speed;
        _direction = direction.normalized;
       
    }

    public void IsTouching(bool touch)
    {
        _isTouch = touch;
    }

    public void LetGoTouching()
    {
        IsTouching(false);
        if (_speed > 2 && _flightAbility)
        {
            _ball.GetComponent<BallController>().BallThrow(_direction, _speed);
            currentHoop.BallInFlying();
            _flightAbility = false;
            _trajectoryRenderer.gameObject.SetActive(false);
        }
    }
    public void BallInHoop(GameObject currentHoop)
    {
        HoopData newHoop = currentHoop.GetComponent<HoopData>();
        hoop = true;
        if (!newHoop.isCurrentHoop)
        {
            if (this.currentHoop != null)
            {
                _previousHoop = this.currentHoop;
                StartCoroutine(_previousHoop.Destroyer());

                _nextHoop = Instantiate(_hoopPrefab, CalculatePosition(currentHoop.transform),
                    CalculateRotate(currentHoop.transform)).GetComponent<HoopData>();

                int randSpawn = Random.Range(1, 11);
                if (randSpawn < 4) { Instantiate(_starPrefab, _nextHoop.transform); }

                _cameraController.Move();
                _borders.transform.DOMoveY(_ball.transform.position.y, 0);
            }

            newHoop.isCurrentHoop = true;
            this.currentHoop = newHoop;

            StartCoroutine(this.currentHoop.FirstDunk());
            StartCoroutine(this.currentHoop.Score(_ball.GetComponent<BallController>()._scoreCalculator._step));
        }
        _flightAbility = true;
        this.currentHoop.AnimatorEnable(false);
        _ball.GetComponent<BallController>().SetHoop(this.currentHoop.transform, this.currentHoop.InHoopTrigger.transform);
    }

    private Quaternion CalculateRotate(Transform currentHoop)
    {
        float z = Random.Range(0, 45);
        if (currentHoop.position.x > 0) { z *= -1; }
        return Quaternion.Euler(0, 0, z);
    }

    private Vector2 CalculatePosition(Transform currentHoop)
    {
        float x;
        x = Random.Range(17, 21) / 10;
        if (currentHoop.position.x > 0) { x *= -1; }
        float y = currentHoop.position.y + Random.Range(4, 9) / 3;
        return new Vector2(x, y);
    }

    private float CalculateGridSize()
    {
        float size;
        if (_speed > 1)
        {
            size = 1 + _speed / 10;
        }
        else
        {
            size = 1;
        }
        return size;
    }
}
