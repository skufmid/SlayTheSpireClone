using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Model")]
    [SerializeField] private CardModel model;

    [Header("Database")]
    [SerializeField] private CardFrameDatabase visualDatabase;

    [Header("Renderers")]
    [SerializeField] private SpriteRenderer backgroundImage;
    [SerializeField] private SpriteRenderer frameImage;
    [SerializeField] private SpriteRenderer bannerImage;
    [SerializeField] private SpriteRenderer typeIconImage;
    [SerializeField] private SpriteRenderer cardOrbImage;
    [SerializeField] private SpriteRenderer cardArtImage;

    private BattleCardPresenter presenter;
    private Vector3 originalPosition;

    public CardModel Model => model;

    public void Initialize(CardModel model, BattleCardPresenter presenter)
    {
        this.model = model;
        this.presenter = presenter;

        Refresh();
    }

    public void Refresh()
    {
        if (model == null || visualDatabase == null)
            return;

        if (backgroundImage != null)
            backgroundImage.sprite = visualDatabase.GetBackground(model.CardType, model.CardColor);

        if (frameImage != null)
            frameImage.sprite = visualDatabase.GetFrame(model.CardType, model.CardRarity);

        if (bannerImage != null)
            bannerImage.sprite = visualDatabase.GetBanner(model.CardRarity);

        if (typeIconImage != null)
            typeIconImage.sprite = visualDatabase.GetTypeIcon(model.CardRarity);

        if (cardOrbImage != null)
            cardOrbImage.sprite = visualDatabase.GetCardOrb(model.CardColor);

        if (cardArtImage != null)
            cardArtImage.sprite = model.CardImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        presenter?.OnCardClicked(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        presenter?.OnCardBeginDrag(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        presenter?.OnCardDragging(this, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        presenter?.OnCardEndDrag(this, eventData, originalPosition);
    }

    public void SetWorldPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public void ResetPosition(Vector3 position)
    {
        transform.position = position;
    }
}