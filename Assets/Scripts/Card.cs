using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;

    public Image Icon
    {
        get => _image;
        set => _image = value;
    }

    public string Name
    {
        get => _name.text;
        set => _name.text = value;
    }
}
