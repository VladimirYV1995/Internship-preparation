using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private UnityEvent _foundCoin;
    [SerializeField] private Text _text;

    private int _score;
    private string _defaultheadline;
    private void Awake()
    {
        _score = 0;
        _defaultheadline = _text.text;
        _text.text += _score.ToString();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Coin>())
        {
            _foundCoin?.Invoke();
            collider.GetComponent<Coin>().Collect();
            _score++;
            _text.text = _defaultheadline + _score.ToString();
        }
    }
}
