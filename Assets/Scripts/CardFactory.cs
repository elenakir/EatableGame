using UnityEngine;

public class CardFactory : MonoBehaviour
{
    [SerializeField] private Card _prefab;

    public Card CreateCard(Transform parent)
    {
        return Instantiate(_prefab, parent);
    }
}
