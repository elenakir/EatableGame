using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Bundle Data", menuName = "Card Bundle Data")]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private List<CardData> _cardData;

    public List<CardData> CardData => _cardData;
}
