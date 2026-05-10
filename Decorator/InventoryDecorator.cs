public abstract class InventoryDecorator : IHero
{
    protected readonly IHero _hero;

    protected InventoryDecorator(IHero hero)
    {
        _hero = hero;
    }

    public virtual string GetName() => _hero.GetName();
}