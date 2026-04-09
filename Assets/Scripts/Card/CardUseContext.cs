using System;
using System.Collections.Generic;

[Serializable]
public class CardUseContext
{
    public CardModel Card;
    public object User;
    public List<object> Targets = new();

    public int AvailableEnergy;
    public Action<int> SpendEnergy;

    public bool IsPlayable = true;
}