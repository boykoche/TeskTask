using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class HoopData : MonoBehaviour
{
    [SerializeField] private GameObject _grid;
    [SerializeField] private GameObject _circle;
    [SerializeField] private TextMeshProUGUI _flyScore;
    [SerializeField] private GameObject _flyScoreGO; 
    [SerializeField] private TextMeshProUGUI _textAnoncer;
    [SerializeField] private GameObject _textAnoncerGO;
    [SerializeField] private SpriteRenderer[] _imgBeforeDunk;
    [SerializeField] private Sprite[] _imgAfterDunk;
    [SerializeField] private Animator _animator;
    List<string> textVariants = new List<string> { "PERFECT", "EXCELENT", "GOOD" };

    public GameObject InHoopTrigger;
    public bool isCurrentHoop;

    public void SetGridSize(float size)
    {
        _grid.transform.localScale = new Vector3(1, size, 1);
    }

    public void BallInFlying()
    {
        AnimatorEnable(true);
        SetGridSize(1);
    }

    public void AnimatorEnable(bool enable)
    {
        _animator.SetBool("BallOut", enable);
    }

    public IEnumerator FirstDunk()
    {
        if (_circle != null)
            _circle.SetActive(true);
        _circle.transform.DOScale(new Vector3(2, 1.8f, 1), 0.2f);
        _circle.GetComponent<SpriteRenderer>().DOColor(new Color(0.99f, 0.63f, 0.63f, 0.3f), 0.2f);

        for (int i = 0; i < 2; i++)
        {
            _imgBeforeDunk[i].color = Color.gray;
        }

        yield return new WaitForSeconds(0.2f);
        Destroy(_circle);
    }

    public IEnumerator Score(int score)
    {
        if (score > 2) 
        { 
            _textAnoncerGO.SetActive(true);
            _textAnoncer.text = textVariants[Random.Range(0, textVariants.Count)];
            _textAnoncerGO.transform.DOLocalMoveY(0.8f, 0.7f);
        }
        _flyScoreGO.SetActive(true);
        _flyScore.text = $"+{score}";
        _flyScore.DOColor(new Color(1, 1, 1, 0), 0.7f);
        _flyScoreGO.transform.DOLocalMoveY(1.3f, 0.7f);

        yield return new WaitForSeconds(0.7f);
        Destroy(_flyScoreGO);
        Destroy(_textAnoncerGO);
    }

    public IEnumerator Destroyer()
    {
        transform.DOScale(new Vector3(0.1f, 0.1f, 0), 0.3f);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
