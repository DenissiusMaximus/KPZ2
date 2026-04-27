IHero myHero = new Paladin();

myHero = new Sword(myHero);

myHero = new Spear(myHero);

myHero = new Sword(myHero);

Console.WriteLine(myHero.GetName());