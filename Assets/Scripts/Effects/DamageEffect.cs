using System;
using UnityEngine;

[Serializable, SerializeField]
public class DamageEffect : IEffect
{
    [SerializeField] private int amount;
    public  string Text {
        get { return $"피해를 {amount} 줍니다."; }
     }
    
    public int Amount {
        get {  return amount; }
        private set { amount = value; } }

    public void Execute(object context)
    {
        //context.PrimaryTarget.TakeDamage(amount);
    }
}