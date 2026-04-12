namespace AbstractFabricc;

public static class Program
{
	public static void Main()
	{
		IDeviceFactory factory = new IProneFactory();

		var laptop = factory.CreateLaptop();
		var smartphone = factory.CreateSmartphone();
		var ebook = factory.CreateEBook();
		var netbook = factory.CreateNetbook();

		laptop.Work();
		smartphone.Call();
		ebook.Read();
		netbook.Browse();
	}
}


