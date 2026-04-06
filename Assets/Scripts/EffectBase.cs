using System;
using UnityEngine;

[Serializable]
public abstract class EffectBase
{
    public abstract void Execute(object context);
}