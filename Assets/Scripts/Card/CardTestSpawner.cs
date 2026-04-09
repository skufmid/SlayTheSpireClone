using UnityEngine;
using System.Collections.Generic;

public class CardTestSpawner : MonoBehaviour
{
    
    [SerializeField] private List<CardData> cardDataList;
    [SerializeField] private CardView cardViewPrefab;
    [SerializeField] private BattleCardPresenter battleCardPresenter;

    private void Start()
    {
        foreach (var cardData in cardDataList)
        {
            CardModel cardModel = new CardModel(cardData, false);
            CardView cardView = Instantiate(cardViewPrefab, Vector3.zero, Quaternion.identity);

            cardView.Initialize(cardModel, battleCardPresenter);
            battleCardPresenter.RegisterCard(cardView);
            // Position the card views in a row for testing
            cardView.transform.position = new Vector3((cardDataList.IndexOf(cardData) - cardDataList.Count / 2) * 2.0f, 0, 0);
        }
    }
}