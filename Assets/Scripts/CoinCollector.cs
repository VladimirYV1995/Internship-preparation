using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private UnityEvent _сoinCollecting;
    [SerializeField] private Text _scoreInfo;

    private int _score;
    private string _constantInscription;

    private void Awake()
    {
        _score = 0;
        _constantInscription = _scoreInfo.text;
        _scoreInfo.text += _score.ToString();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Coin>())
        {
            _сoinCollecting?.Invoke();
            collider.GetComponent<Coin>().Collect();
            _score++;
            _scoreInfo.text = _constantInscription + _score.ToString();
        }
    }
}