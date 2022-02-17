using UnityEngine;

// Manages references to global player HP, player money, and interactions to modify them
// (enemy death, enemy on goal, tower buying, tower selling).

[CreateAssetMenu(fileName = "GameManagerObject", menuName = "Custom Game Objects/Game Manager Object")]
public class GameManager : ScriptableObject
{
    public IntValue playerHP;
    public IntValue playerMoney;
    //public IntValue timeBetweenWaves;
    public System.Random random { get; } = new System.Random();

    public enum Modification
    {
        INCREASE,
        DECREASE
    }

    public void ModifyPlayerValue(IntValue value, Modification modification, int byValue)
    {
        switch (modification)
        {
            case Modification.INCREASE:
                value.value += byValue;
                break;
            case Modification.DECREASE:
                value.value -= byValue;
                break;
            default:
                break;
        }
    }
}
