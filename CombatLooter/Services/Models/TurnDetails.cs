namespace CombatLooter.Services.Models
{
    /// <summary>
    /// Class representing the details of a turn in combat.
    /// </summary>
    public class TurnDetails
    {
        private readonly List<BeingTurnDetails> _beingTurnDetails;

        /// <summary>
        /// 
        /// </summary>
        public TurnDetails()
        {
            _beingTurnDetails = new List<BeingTurnDetails>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BeingTurnDetails> GetBeingTurnDetails()
        {
            return  _beingTurnDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfAttacks()
        {
            return _beingTurnDetails.Count;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="turnNumber"></param>
        /// <param name="attackerName"></param>
        /// <param name="action"></param>
        /// <param name="amountOfAction"></param>
        /// <param name="targetName"></param>
        /// <param name="targetStatus"></param>
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
