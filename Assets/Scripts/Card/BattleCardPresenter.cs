using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardPresenter : MonoBehaviour
{
    [Header("Runtime")]
    [SerializeField] private List<CardView> registeredCardViews = new();

    private CardView selectedCardView;
    private CardView draggingCardView;

    public void RegisterCard(CardView cardView)
    {
        if (cardView == null)
            return;

        if (!registeredCardViews.Contains(cardView))
            registeredCardViews.Add(cardView);
    }

    public void UnregisterCard(CardView cardView)
    {
        if (cardView == null)
            return;

        registeredCardViews.Remove(cardView);

        if (selectedCardView == cardView)
            selectedCardView = null;

        if (draggingCardView == cardView)
            draggingCardView = null;
    }

    public void OnCardClicked(CardView cardView)
    {
        if (cardView == null)
            return;

        selectedCardView = cardView;

        // 필요하면 선택 연출/UI 표시
        Debug.Log($"카드 클릭: {cardView.Model.CardName}");
    }

    public void OnCardBeginDrag(CardView cardView, PointerEventData eventData)
    {
        if (cardView == null)
            return;

        selectedCardView = cardView;
        draggingCardView = cardView;

        Debug.Log($"드래그 시작: {cardView.Model.CardName}");
    }

    public void OnCardDragging(CardView cardView, PointerEventData eventData)
    {
        if (cardView == null || draggingCardView != cardView)
            return;

        Vector3 screenPoint = eventData.position;
        screenPoint.z = 10f;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        cardView.SetWorldPosition(worldPoint);
    }

    public void OnCardEndDrag(CardView cardView, PointerEventData eventData, Vector3 originalPosition)
    {
        if (cardView == null || draggingCardView != cardView)
            return;

        draggingCardView = null;

        object target = FindDropTarget(eventData);
        bool success = TryPlayCard(cardView, target);

        if (!success)
        {
            cardView.ResetPosition(originalPosition);
        }
    }

    public bool CanPlayCard(CardModel model, CardUseContext context)
    {
        if (model == null || context == null)
            return false;

        if (!context.IsPlayable)
            return false;

        if (model.CurrentCost >= 0 && context.AvailableEnergy < model.CurrentCost)
            return false;

        if (model.TargetType != TargetType.None)
        {
            if (context.Targets == null || context.Targets.Count == 0)
                return false;

            if (model.TargetCount > 0 && context.Targets.Count > model.TargetCount)
                return false;
        }

        return true;
    }

    public bool PlayCard(CardModel model, CardUseContext context)
    {
        if (!CanPlayCard(model, context))
            return false;

        if (model.CurrentCost >= 0)
            context.SpendEnergy?.Invoke(model.CurrentCost);

        context.Card = model;

        List<IEffect> effects = model.GetAllEffects();
        foreach (var effect in effects)
        {
            if (effect == null)
                continue;

            effect.Execute(context);
        }

        return true;
    }

    private bool TryPlayCard(CardView cardView, object target)
    {
        CardUseContext context = CreateUseContext(cardView.Model, target);

        bool success = PlayCard(cardView.Model, context);

        if (success)
        {
            Debug.Log($"카드 사용 성공: {cardView.Model.CardName}");

            // TODO:
            // 1. 손패에서 제거
            // 2. 버림더미/소멸더미 이동
            // 3. 카드 오브젝트 처리
        }
        else
        {
            Debug.Log($"카드 사용 실패: {cardView.Model.CardName}");
        }

        return success;
    }

    private CardUseContext CreateUseContext(CardModel model, object target)
    {
        CardUseContext context = new CardUseContext
        {
            Card = model,
            User = GetCurrentPlayer(),
            AvailableEnergy = GetCurrentEnergy(),
            SpendEnergy = SpendEnergy,
            IsPlayable = true
        };

        if (target != null)
            context.Targets.Add(target);

        return context;
    }

    private object FindDropTarget(PointerEventData eventData)
    {
        // TODO:
        // 레이캐스트 결과를 바탕으로 적, 아군, 바닥 등을 판정해서 반환
        return null;
    }

    private object GetCurrentPlayer()
    {
        // TODO:
        // 실제 플레이어 객체 반환
        return null;
    }

    private int GetCurrentEnergy()
    {
        // TODO:
        // 실제 전투 에너지 반환
        return 3;
    }

    private void SpendEnergy(int amount)
    {
        // TODO:
        // 실제 전투 에너지 차감
        Debug.Log($"에너지 {amount} 사용");
    }
}