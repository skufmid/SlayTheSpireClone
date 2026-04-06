using System;
using UnityEngine;


public interface IEffect
{
    public string Text { get; }

    public void Execute(object context);
}