using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    public int healthCount;
    public int secondsCount;
    public int maxCombo;
}
