using UnityEngine;

public class BuffDame : StatusEffect
{
    public BuffDame(Monster target,  float value, float duration) : base(target, value, duration)
    {
        type = EffectType.Buff;
        name = StatusEffectName.BuffDame;
        this.value = value;
    }
    public BuffDame(Character character, float value, float duration) : base(character, value, duration)
    {
        type = EffectType.Buff;
        name = StatusEffectName.BuffDame;
        this.value = value;
    }

    public override bool Cooldown(float deltaTime)
    {
        cdTotal.Update(deltaTime);
        if (cdTotal.isFinished)
        {
            character.listStatusEffect.Remove(this);
            target.listStatusEffect.Remove(this);
            return true;
        }
        return false;
    }
}
