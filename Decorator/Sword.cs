public class Sword(IHero hero) : InventoryDecorator(hero)
{
    public override string GetName() => hero.GetName() + " Sword";
}