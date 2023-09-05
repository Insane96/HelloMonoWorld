using System.Collections.Generic;

namespace HelloMonoWorld.Game.Entity.Attributes;

public class Attributes
{
    public static readonly List<Attribute> AttributesList = new();
    
    public static readonly Attribute Cooldown = CreateAttribute("cooldown", 1f);
    public static readonly Attribute MaxHealth = CreateAttribute("max_health", 5f);
    public static readonly Attribute MovementSpeed = CreateAttribute("movement_speed", 100f);
    public static readonly Attribute KnockbackResistance = CreateAttribute("knockback_resistance", 0.1f);
    public static readonly Attribute Damage = CreateAttribute("damage", 1f);
    public static readonly Attribute DamageInterval = CreateAttribute("damage_interval", 1f);
    public static readonly Attribute Knockback = CreateAttribute("knockback", 0f);

    private static Attribute CreateAttribute(string id, float defaultValue)
    {
        Attribute attribute = new Attribute(id, defaultValue);
        AttributesList.Add(attribute);
        return attribute;
    }
}