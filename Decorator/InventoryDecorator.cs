public abstract class InventoryDecorator(IHero hero) : IHero
{
    protected readonly IHero _hero = hero;
    
    public virtual string GetName() => hero.GetName();
}