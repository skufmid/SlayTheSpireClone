using UnityEngine;
using UnityEngine.UI;

public class CardEntity : MonoBehaviour
{
    [Header("Card Info")]
    [SerializeField] private CardType cardType;
    [SerializeField] private CardColor cardColor;
    [SerializeField] private CardRarity cardRarity;
    [SerializeField] private Sprite cardArt;

    [Header("Database")]
    [SerializeField] private CardFrameDatabase visualDatabase;

    [Header("UI References")]
    [SerializeField] private SpriteRenderer backgroundImage;
    [SerializeField] private SpriteRenderer frameImage;
    [SerializeField] private SpriteRenderer bannerImage;
    [SerializeField] private SpriteRenderer typeIconImage;
    [SerializeField] private SpriteRenderer cardOrbImage;
    [SerializeField] private SpriteRenderer cardArtImage;

    private void Awake()
    {
        RefreshVisual();
    }

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

        if (cardArtImage != null)
            cardArtImage.sprite = cardArt;
    }
}