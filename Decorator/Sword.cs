public class Sword : InventoryDecorator
{
    public Sword(IHero hero) : base(hero) { }

    public override string GetName() => _hero.GetName() + " Sword";
}