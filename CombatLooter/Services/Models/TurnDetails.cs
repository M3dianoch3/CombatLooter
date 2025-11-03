namespace CombatLooter.Services.Models
{
    /// <summary>
    /// Class representing the details of a turn in combat.
    /// </summary>
    public class TurnDetails
    {
        public int TurnId { get; set; }
        public int TurnNumber { get; set; }
        public string ActingBeingName { get; set; }
        public string ActionDescription { get; set; }
        public double ActionAmount { get; set; }
        public string TargetBeingName { get; set; }
        public string ResultingTargetStatus { get; set; }

    }
}
