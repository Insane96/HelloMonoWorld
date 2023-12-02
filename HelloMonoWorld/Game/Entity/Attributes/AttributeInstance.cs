using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloMonoWorld.Game.Entity.Attributes;

public class AttributeInstance
{
    public Attribute Attribute { get; }

    private float _baseValue;
    public float BaseValue
    {
        get => this._baseValue;
        set
        {
            this._baseValue = value;
            this._needsCaching = true;
        }
    }

    private float _value;
    public float Value
    {
        get
        {
            if (this._needsCaching)
            {
                this.CalculateValue();
                this._needsCaching = false;
            }
            return this._value;
        }
        private set => this._value = value;
    }

    private List<AttributeModifier> AttributeModifiers { get; }

    private bool _needsCaching;

    public AttributeInstance(Attribute attribute, float baseValue)
    {
        this.Attribute = attribute;
        this.BaseValue = baseValue;
        this.Value = baseValue;
        this.AttributeModifiers = new List<AttributeModifier>();
    }

    public AttributeInstance(Attribute attribute)
    {
        this.Attribute = attribute;
        this.BaseValue = attribute.DefaultValue;
        this.Value = attribute.DefaultValue;
        this.AttributeModifiers = new List<AttributeModifier>();
    }

    public bool AddModifier(AttributeModifier attributeModifier)
    {
        if (this.AttributeModifiers.Any(a => a.Guid.Equals(attributeModifier.Guid)))
            return false;
        this.AttributeModifiers.Add(attributeModifier);
        this._needsCaching = true;
        return true;
    }

    public bool RemoveModifier(AttributeModifier attributeModifier) => this.AttributeModifiers.Remove(attributeModifier);
    public bool RemoveModifier(Guid modifierGuid) => this.AttributeModifiers.RemoveAll(a => a.Guid.Equals(modifierGuid)) > 0;

    private void CalculateValue()
    {
        float value = this.BaseValue;
        foreach (var modifiersByOperation in this.AttributeModifiers.GroupBy(modifier => modifier.ModiferOperation))
        {
            if (modifiersByOperation.Key == AttributeModifier.Operation.Addition)
            {
                value += modifiersByOperation.Sum(attributeModifier => attributeModifier.Value);
            }
            else if (modifiersByOperation.Key == AttributeModifier.Operation.MultiplyBase)
            {
                float finalMultiplier = modifiersByOperation.Sum(attributeModifier => attributeModifier.Value);
                value += value * finalMultiplier;
            }
        }

        this.Value = value;
    }

    public override string ToString()
    {
        return $"AttributeInstance{{Attribute: {this.AttributeModifiers}, BaseValue: {this.BaseValue}, Value: {this.Value}, Modifiers: {this.AttributeModifiers}}}";
    }
}