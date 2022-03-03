using UnityEngine;
using UnityEngine.UI;

// top 1 situation for observer/subscription or events, that will be done in a dirty way instead
public class UI_TowerTradeButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [System.NonSerialized] public TowerBase callingTowerBase;
    private int callingTowerCost;

    private Image buttonImage;
    [SerializeField] private Sprite buySprite;
    [SerializeField] private Sprite sellSprite;

    private TMPro.TMP_Text costText;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        costText = GetComponentInChildren<TMPro.TMP_Text>(true);
    }

    // intended to only be enabled by towers
    private void OnEnable()
    {

        if (callingTowerBase.isBought == false)
        {
            // tower not bought, use full price and buy image
            callingTowerCost = callingTowerBase.towerCost;
            buttonImage.sprite = buySprite;
            costText.text = "-" + callingTowerCost.ToString();
        }
        else
        {
            // tower bought, for sell price use half the tower price, and sell image
            callingTowerCost = callingTowerBase.towerCost / 2;
            buttonImage.sprite = sellSprite;
            costText.text = "+" + callingTowerCost.ToString();
        }
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

                gameObject.SetActive(false);
            }
        }

        else
        {
            // tower bought, try to sell
            gameManager.ModifyPlayerValue(gameManager.playerMoney, GameManager.Modification.INCREASE, callingTowerCost);
            callingTowerBase.isBought = false;

            gameObject.SetActive(false);
        }
    }
}
