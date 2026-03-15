namespace AbstractFabricc;

public class IProneFactory : IDeviceFactory
{
    public ILaptop CreateLaptop() => new IProneLaptop();
    public ISmartphone CreateSmartphone() => new IProneSmartphone();
    public IEBook CreateEBook() => new IProneEBook();
    public INetbook CreateNetbook() => new IProneNetbook();
}