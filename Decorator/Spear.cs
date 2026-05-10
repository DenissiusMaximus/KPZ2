public class Spear : InventoryDecorator
{
    public Spear(IHero hero) : base(hero) { }

    public override string GetName() => _hero.GetName() + " Spear";
}