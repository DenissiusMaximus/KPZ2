namespace AbstractFabricc;

public class BalaxyFactory : IDeviceFactory
{
    public ILaptop CreateLaptop() => new BalaxyLaptop();
    public ISmartphone CreateSmartphone() => new BalaxySmartphone();
    public IEBook CreateEBook() => new BalaxyEBook();
    public INetbook CreateNetbook() => new BalaxyNetbook();
}