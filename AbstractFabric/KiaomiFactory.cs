namespace AbstractFabricc;

public class KiaomiFactory : IDeviceFactory
{
    public ILaptop CreateLaptop() => new KiaomiLaptop();
    public ISmartphone CreateSmartphone() => new KiaomiSmartphone();
    public IEBook CreateEBook() => new KiaomiEBook();
    public INetbook CreateNetbook() => new KiaomiNetbook();
}