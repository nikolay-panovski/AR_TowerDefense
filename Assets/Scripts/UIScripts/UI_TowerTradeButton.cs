using UnityEngine;

// top 1 situation for observer/subscription or events, that will be done in a dirty way instead
public class UI_TowerTradeButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [System.NonSerialized] public TowerBase callingTowerBase;
    private int callingTowerCost;

    private TMPro.TMP_Text costText;

    // intended to only be enabled by towers
    private void OnEnable()
    {
        if (costText == null)
        {
            costText = GetComponentInChildren<TMPro.TMP_Text>(true);
        }

        if (callingTowerBase.isBought == false)
        {
            // tower not bought, use full price
            callingTowerCost = callingTowerBase.towerCost;
        }
        else
        {
            // tower bought, for sell price use half the tower price
            callingTowerCost = callingTowerBase.towerCost / 2;
        }
            
        costText.text = callingTowerCost.ToString();
    }

    private void OnDisable()
    {
        callingTowerBase = null;
    }

    // call this from OnClick event
    public void TryMakeTransaction()
    {
        if (callingTowerBase.isBought == false)
        {
            // tower not bought, try to buy
            if (gameManager.playerMoney.value >= callingTowerCost)
            {
                // enough money, actually buying
                gameManager.ModifyPlayerValue(gameManager.playerMoney, GameManager.Modification.DECREASE, callingTowerCost);
                callingTowerBase.isBought = true;

                // TODO: send some signal back to tower to change its state (material or model)

                gameObject.SetActive(false);
            }
        }

        else
        {
            // tower bought, try to sell
            gameManager.ModifyPlayerValue(gameManager.playerMoney, GameManager.Modification.INCREASE, callingTowerCost);
            callingTowerBase.isBought = false;

            // TODO: send some signal back to tower to change its state (material or model)

            gameObject.SetActive(false);
        }
    }
}
