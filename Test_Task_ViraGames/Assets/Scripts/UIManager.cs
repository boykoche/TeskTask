using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _loseLevel;
    [SerializeField] private GameObject _starsCount;
    [SerializeField] private GameObject _logo;
    [SerializeField] private GameObject _inGame;
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _bestScore;
    [SerializeField] private GameObject _scoreInGame;
    [SerializeField] private GameObject _dragController;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _lampOn;
    [SerializeField] private TextMeshProUGUI _soundsOn;
    [SerializeField] private TextMeshProUGUI _vibrationOn;
    [SerializeField] private TextMeshProUGUI _nightModeOn;

    private TextMeshProUGUI _scoreTMP;
    private TextMeshProUGUI _starsTMP;

    private Color _whiteTheme= new Color(0.9f,0.9f,0.9f);
    private Color _nightTheme= new Color(0.2f,0.2f,0.2f);

    private void Awake()
    {
        Time.timeScale = 0;
        _menu.SetActive(true);
        _logo.SetActive(true) ;
        _scoreTMP = _score.GetComponent<TextMeshProUGUI>();
        _starsTMP = _starsCount.GetComponent<TextMeshProUGUI>();
        _starsTMP.text = PlayerPrefs.GetInt("stars").ToString();
        if (PlayerPrefs.GetInt("nightMode") == 1)
        {
            _lampOn.SetActive(true);
            _camera.GetComponent<Camera>().backgroundColor = _whiteTheme;
        }
        else
        {
            _lampOn.SetActive(false);
            _camera.GetComponent<Camera>().backgroundColor = _nightTheme;
        }
    }


    public void FirstDrag()
    {
        Time.timeScale = 1;
        _menu.SetActive(false);
        _logo.SetActive(false);
        _inGame.SetActive(true);
    }

    public void ChangeScore(int score)
    {
        _inGame.SetActive(true);
        _scoreTMP.text = score.ToString();
    }
    public void GameOver()
    {
        _dragController.SetActive(false);
        _loseLevel.SetActive(true);
        _inGame.SetActive(false);
        _scoreInGame.GetComponent<TextMeshProUGUI>().text = _scoreTMP.text;
        _bestScore.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("score").ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        _dragController.SetActive(true);
        _pause.SetActive(false);
        _pauseButton.SetActive(true);
    }

    public void MainMenu()
    {
        Restart();
        _pause.SetActive(false);
    }
    public void Settings()
    {
        _settings.SetActive(true);
        float m = PlayerPrefs.GetFloat("music");
        float n = PlayerPrefs.GetInt("nightMode");
        float v = PlayerPrefs.GetFloat("vibrate");
        if(m == 0) { _soundsOn.text = "OFF"; }
        else{ _soundsOn.text = "ON"; }
        if (n == 0) { _nightModeOn.text = "ON"; }
        else { _nightModeOn.text = "OFF"; }
        if (v == 0) { _vibrationOn.text = "OFF"; }
        else { _vibrationOn.text = "ON"; }
    }
    public void Back()
    {
        _dragController.SetActive(true);
        _settings.SetActive(false);
    }
    public void Pause()
    {
        _dragController.SetActive(false);
        _pause.SetActive(true);
        _pauseButton.SetActive(false);

    }
    public void StarsPlusing()
    {
        _starsTMP.text = PlayerPrefs.GetInt("stars").ToString();
    }

    public void SetLightMode()
    {
        if (PlayerPrefs.GetInt("nightMode")==0)
        {
            _camera.GetComponent<Camera>().backgroundColor = _whiteTheme;
            PlayerPrefs.SetInt("nightMode", 1);
            _lampOn.SetActive(true);
        }
        else
        {
            _camera.GetComponent<Camera>().backgroundColor = _nightTheme;
            PlayerPrefs.SetInt("nightMode", 0);
            _lampOn.SetActive(false);
        }
    }

    public void VibrationEnable()
    {
        float vibrate = PlayerPrefs.GetFloat("vibrate") == 1 ? 0 : 1;
        PlayerPrefs.SetFloat("vibrate", vibrate);
        Settings();

    }
    public void MusicEnable()
    {
        float vibrate = PlayerPrefs.GetFloat("music") == 1 ? 0 : 1;
        PlayerPrefs.SetFloat("music", vibrate);
        Settings();
    }
}
