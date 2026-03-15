namespace AbstractFabricc;

public interface IDeviceFactory
{
    ILaptop CreateLaptop();
    ISmartphone CreateSmartphone();
    IEBook CreateEBook();
    INetbook CreateNetbook();
}