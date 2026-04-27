public class Spear(IHero hero) : InventoryDecorator(hero)
{
    public override string GetName() => hero.GetName() + " Spear";
}