namespace HelloMonoWorld.Game.Entity.Attributes;

public class Attribute
{
    public string Id { get; private set; }
    public float DefaultValue { get; private set; }

    public Attribute(string id, float defaultValue)
    {
        this.Id = id;
        this.DefaultValue = defaultValue;
    }

    public override string ToString()
    {
        return $"Attribute{{Id: {this.Id}, DefaultValue: {this.DefaultValue}}}";
    }
}