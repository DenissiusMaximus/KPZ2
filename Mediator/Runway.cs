namespace DesignPatterns.Mediator
{
    public class Runway
    {
        public readonly Guid Id = Guid.NewGuid();
        public bool IsBusy { get; set; } = false;
        
        private ICommandCentre? _commandCentre;

        public void SetCommandCentre(ICommandCentre commandCentre)
        {
            _commandCentre = commandCentre;
        }

        public void HighLightRed()
        {
            Console.WriteLine($"Runway {this.Id} is busy!");
        }

        public void HighLightGreen()
        {
            Console.WriteLine($"Runway {this.Id} is free!");
        }
    }
}