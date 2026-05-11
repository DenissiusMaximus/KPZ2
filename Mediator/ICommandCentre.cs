namespace DesignPatterns.Mediator
{
    public interface ICommandCentre
    {
        void RegisterRunway(Runway runway);
        void RegisterAircraft(Aircraft aircraft);
        void RequestLanding(Aircraft aircraft);
        void RequestTakeOff(Aircraft aircraft);
    }
}