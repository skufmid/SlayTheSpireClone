using UnityEngine;
using UnityEngine.UI;

public class CardEntity : MonoBehaviour
{
    [Header("Card Info")]
    [SerializeField] private CardType cardType;
    [SerializeField] private CardColor cardColor;
    [SerializeField] private CardRarity cardRarity;

    [Header("Database")]
    [SerializeField] private CardFrameDatabase visualDatabase;

    [Header("UI References")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image frameImage;
    [SerializeField] private Image bannerImage;
    [SerializeField] private Image typeIconImage;
    [SerializeField] private Image cardOrbImage;

    public void SetCardVisual(CardType type, CardColor color, CardRarity rarity)
    {
        cardType = type;
        cardColor = color;
        cardRarity = rarity;

        RefreshVisual();
    }

    public void RefreshVisual()
    {
        if (visualDatabase == null)
        {
            Debug.LogWarning($"{name}: CardVisualDatabase가 연결되지 않았습니다.");
            return;
        }

        if (backgroundImage != null)
            backgroundImage.sprite = visualDatabase.GetBackground(cardType, cardColor);

        if (frameImage != null)
            frameImage.sprite = visualDatabase.GetFrame(cardType, cardRarity);

        if (bannerImage != null)
            bannerImage.sprite = visualDatabase.GetBanner(cardRarity);

        if (typeIconImage != null)
            typeIconImage.sprite = visualDatabase.GetTypeIcon(cardRarity);

        if (cardOrbImage != null)
            cardOrbImage.sprite = visualDatabase.GetCardOrb(cardColor);
    }

    public void SetCardType(CardType type)
    {
        cardType = type;
        RefreshVisual();
    }

    public void SetCardColor(CardColor color)
    {
        cardColor = color;
        RefreshVisual();
    }

    public void SetCardRarity(CardRarity rarity)
    {
        cardRarity = rarity;
        RefreshVisual();
    }

    private void Awake()
    {
        RefreshVisual();
    }

    private void OnValidate()
    {
        if (visualDatabase == null) return;
        RefreshVisual();
    }
}