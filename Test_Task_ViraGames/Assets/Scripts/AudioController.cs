using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _wall;
    [SerializeField] private AudioSource _ringing;
    [SerializeField] private AudioSource _grid;
    [SerializeField] private AudioSource _ball;
    [SerializeField] private AudioSource _fireBall;

    public void WallHit()
    {
        if (PlayerPrefs.GetFloat("music") == 1)
            _wall.Play();
    }

    public void Ringing()
    {
        if (PlayerPrefs.GetFloat("music") == 1)
            _ringing.Play();
    }

    public void GrigHitting()
    {
        if (PlayerPrefs.GetFloat("music") == 1)
            _grid.Play();
    }

    public void BallFlying()
    {
        if (PlayerPrefs.GetFloat("music") == 1)
            _ball.Play();
    }

    public void FireBall()
    {
        if (PlayerPrefs.GetFloat("music") == 1)
            _fireBall.Play();
    }
    public void Vibration()
    {
        if (PlayerPrefs.GetFloat("vibrate") == 1)
            Handheld.Vibrate();
    }

}
