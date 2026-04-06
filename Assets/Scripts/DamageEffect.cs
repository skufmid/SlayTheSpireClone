using UnityEngine;

[SerializeField]
public class DamageEffect : EffectBase
{
    public int amount;

    public override void Execute(object context)
    {
        //context.PrimaryTarget.TakeDamage(amount);
    }
}