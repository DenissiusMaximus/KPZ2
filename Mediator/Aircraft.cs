namespace DesignPatterns.Mediator
{
    public class Aircraft
    {
        public string Name;
        private ICommandCentre? _commandCentre;

        public Aircraft(string name, int size)
        {
            this.Name = name;
        }

        public void SetCommandCentre(ICommandCentre commandCentre)
        {
            _commandCentre = commandCentre;
        }

        public void Land()
        {
            Console.WriteLine($"Aircraft {this.Name} ініціює процес посадки.");
            _commandCentre?.RequestLanding(this);
        }

        public void TakeOff()
        {
            Console.WriteLine($"Aircraft {this.Name} ініціює процес зльоту.");
            _commandCentre?.RequestTakeOff(this);
        }
    }
}