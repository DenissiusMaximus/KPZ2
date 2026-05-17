namespace DesignPatterns.Mediator
{
    public class CommandCentre : ICommandCentre
    {
        private List<Runway> _runways = new List<Runway>();
        private List<Aircraft> _aircrafts = new List<Aircraft>();
        
        private Dictionary<Aircraft, Runway> _aircraftRunwayMap = new Dictionary<Aircraft, Runway>();

        public CommandCentre(Runway[] runways, Aircraft[] aircrafts)
        {
            foreach (var r in runways) RegisterRunway(r);
            foreach (var a in aircrafts) RegisterAircraft(a);
        }

        public void RegisterRunway(Runway runway)
        {
            _runways.Add(runway);
            runway.SetCommandCentre(this);
        }

        public void RegisterAircraft(Aircraft aircraft)
        {
            _aircrafts.Add(aircraft);
            aircraft.SetCommandCentre(this);
        }

        public void RequestLanding(Aircraft aircraft)
        {
            Console.WriteLine($"Командний центр: Aircraft {aircraft.Name} запитує дозвіл на посадку.");
            
            var freeRunway = _runways.FirstOrDefault(r => !r.IsBusy);

            if (freeRunway != null)
            {
                freeRunway.IsBusy = true;
                _aircraftRunwayMap[aircraft] = freeRunway; 
                Console.WriteLine($"Командний центр: Дозвіл надано. Aircraft {aircraft.Name} сідає на Runway {freeRunway.Id}.");
                freeRunway.HighLightRed();
            }
            else
            {
                Console.WriteLine($"Командний центр: У посадці відмовлено. Немає вільних смуг для {aircraft.Name}.");
            }
        }

        public void RequestTakeOff(Aircraft aircraft)
        {
            Console.WriteLine($"Командний центр: Aircraft {aircraft.Name} запитує дозвіл на зліт.");
            
            if (_aircraftRunwayMap.TryGetValue(aircraft, out Runway? runway))
            {
                runway.IsBusy = false;
                _aircraftRunwayMap.Remove(aircraft);
                Console.WriteLine($"Командний центр: Aircraft {aircraft.Name} злетів зі смуги {runway.Id}.");
                runway.HighLightGreen();
            }
            else
            {
                Console.WriteLine($"Командний центр: Помилка. Aircraft {aircraft.Name} зараз не знаходиться на злітній смузі.");
            }
        }
    }
}