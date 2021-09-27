using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class CardData
{
    [SerializeField] private string _name;
    [SerializeField] private AssetReference _icon;
    [SerializeField] private bool _isEatable;

    public string Name => _name;
    public AssetReference Icon => _icon;
    public bool IsEatable => _isEatable;
}
