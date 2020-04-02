using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    [SerializeField] private UnityEvent _сoinCollecting;
    [SerializeField] private Text _scoreInfoTemplate;

    private int _score;
    private string _constantInscription;

    private void Awake()
    {
        _score = 0;
        _constantInscription = _scoreInfoTemplate.text;
        _scoreInfoTemplate.text += _score.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Coin>(out Coin coin))
        {
            _сoinCollecting?.Invoke();
            coin.Collect();
            _score++;
            _scoreInfoTemplate.text = _constantInscription + _score.ToString();
        }
    }
}