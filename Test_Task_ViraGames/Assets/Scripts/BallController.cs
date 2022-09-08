using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private HoopController _hoopController;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private AudioController _audio;
    [SerializeField] private GameObject _fire;
    [SerializeField] private GameObject _smoke;

    public ScoreCalculator _scoreCalculator = new ScoreCalculator();
    private Rigidbody2D rb;
    public Transform _hoopTransform;

    private int power = 60;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void BallThrow(Vector2 direction, float speed)
    {
        transform.SetParent(_hoopTransform, false);
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.AddForce(-direction * speed * power, ForceMode2D.Force);
        rb.angularVelocity = _hoopTransform.localRotation.z * speed * power;
        _audio.BallFlying();
    }

    public void SetHoop(Transform hoop, Transform InHoop)
    {
        _hoopTransform = hoop;
        transform.SetParent(InHoop);
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        transform.localPosition = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Borders")
        {
            Vector2 currentVelocity = rb.velocity;
            rb.velocity = new Vector2(-currentVelocity.x, currentVelocity.y);
            _scoreCalculator.BallHitBorder();
            _audio.WallHit();

        }
        if (collision.transform.tag == "InHoop")
        {
            _audio.GrigHitting();
            _hoopController.BallInHoop(collision.transform.parent.gameObject);
            _UIManager.ChangeScore(_scoreCalculator.PlusingScore(_hoopTransform.gameObject));
            if (_scoreCalculator._step > 3) { _smoke.SetActive(true); }
            if (_scoreCalculator._step > 4) { _fire.SetActive(true); _audio.Vibration(); _audio.FireBall(); }

        }
        if (collision.transform.tag == "GameOver")
        {
            _UIManager.GameOver();
        }
        if (collision.transform.tag == "Star")
        {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("stars", PlayerPrefs.GetInt("stars") + 1);
            _UIManager.StarsPlusing();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Sides")
        {
            _scoreCalculator.BallHitSides();
            _audio.Ringing();
            _fire.SetActive(false);
            _smoke.SetActive(false);
        }
    }
}

public class ScoreCalculator
{
    private GameObject _currentGO = null;

    private int _score = -1;
    public int _step = 1;

    private bool _IsBounce = false;
    public bool smoke = false;
    public bool fire = false;

    public int PlusingScore(GameObject GO)
    {
        if (_currentGO != GO)
        {
            _currentGO = GO;
            int bounceBonus = _IsBounce ? 2 : 1;
            _score += _step * bounceBonus;
            if (PlayerPrefs.GetInt("score") < _score) { PlayerPrefs.SetInt("score", _score); }
            _IsBounce = false;
            _step++;
        }
        return _score;
    }

    public void BallHitBorder()
    {
        _IsBounce = true;
    }

    public void BallHitSides()
    {
        _step = 1;
    }
}
