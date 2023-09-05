using System;

namespace HelloMonoWorld.Game.Entity.Attributes;

public class AttributeModifier
{
    public Guid Guid { get; private set; }
    public Operation ModiferOperation { get; private set; }
    public float Value { get; private set; }

    public AttributeModifier(Guid guid, Operation modiferOperation, float value)
    {
        this.Guid = guid;
        this.ModiferOperation = modiferOperation;
        this.Value = value;
    }

    public enum Operation
    {
        Addition,
        MultiplyBase
    }
}