using System;
using Engine;

namespace TowerDefense.Registry;

public static partial class EnemiesRegistry
{
    public class RegistryObject<T> where T : GameObject
    {
        public string Id { get; private set; }
        public Func<T> Instantiator { get; private set; }

        public RegistryObject(string id, Func<T> instantiator)
        {
            this.Id = id;
            this.Instantiator = instantiator;
        }

        public T Create()
        {
            return this.Instantiator.Invoke();
        }
    }
}