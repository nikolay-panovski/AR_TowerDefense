using UnityEngine;

// Manages references to global player HP, player money, and interactions to modify them
// (enemy death, enemy on goal, tower buying, tower selling).

[CreateAssetMenu(fileName = "GameManagerObject", menuName = "Custom Game Objects/Game Manager Object")]
public class GameManager : ScriptableObject
{
    public IntValue playerHP;
    public IntValue playerMoney;

    [Tooltip("Once an enemy is this close to its active waypoint, it will update and start moving to the next one. Same value for all enemies, set this in the FloatValue.")]
    public FloatValue minDistToWaypoint;

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
