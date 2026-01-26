namespace CombatLooter.Services.Models
{
    /// <summary>
    /// Class representing the details of a turn in combat.
    /// </summary>
    public class TurnDetails
    {
        private readonly List<BeingTurnDetails> _beingTurnDetails;

        /// <summary>
        /// Constructor for TurnDetails class.
        /// </summary>
        public TurnDetails()
        {
            _beingTurnDetails = new List<BeingTurnDetails>();
        }

        /// <summary>
        /// Gets the list of being turn details.
        /// </summary>
        /// <returns></returns>
        public List<BeingTurnDetails> GetBeingTurnDetails()
        {
            return  _beingTurnDetails;
        }

        /// <summary>
        /// Gets the number of attacks recorded in this turn.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfAttacks()
        {
            return _beingTurnDetails.Count;
        }

        /// <summary>
        /// Adds a new action to the turn details.
        /// </summary>
        /// <param name="turnNumber">A unique identifier for the turn.</param>
        /// <param name="attackerName">The name of the attacker.</param>
        /// <param name="action">The action performed.</param>
        /// <param name="amountOfAction">The amount of action performed.</param>
        /// <param name="targetName">The name of the target.</param>
        /// <param name="targetStatus">The status of the target after the action.</param>
        public void AddNewAction(int turnNumber, string attackerName, string action, double amountOfAction, string targetName, string targetStatus)
        {
            _beingTurnDetails.Add(new BeingTurnDetails()
            {
                TurnId = Guid.NewGuid(),
                TurnNumber = turnNumber,
                ActingBeingName = attackerName,
                ActionDescription = action,
                ActionAmount = amountOfAction,
                TargetBeingName = targetName,
                ResultingTargetStatus = targetStatus
            });
        }
    }

    public class BeingTurnDetails
    {
        public Guid TurnId { get; set; }
        public int TurnNumber { get; set; }
        public required string ActingBeingName { get; set; }
        public required string ActionDescription { get; set; }
        public double ActionAmount { get; set; }
        public required string TargetBeingName { get; set; }
        public required string ResultingTargetStatus { get; set; }
    }
}
