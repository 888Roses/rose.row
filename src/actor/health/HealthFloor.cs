namespace rose.row.actor.health
{
    /// <summary>
    /// Represents a health portion.
    /// </summary>
    public enum HealthFloor
    {
        /// <summary>
        /// Goes from 0 to <see cref="CustomActorHealth.k_LowestHealthFloorThreshold"/>.
        /// </summary>
        Lowest = 2,

        /// <summary>
        /// Goes from <see cref="CustomActorHealth.k_LowestHealthFloorThreshold"/> to
        /// <see cref="CustomActorHealth.k_MiddleHealthFloorThreshold"/>.
        /// </summary>
        Middle = 1,

        /// <summary>
        /// Goes from <see cref="CustomActorHealth.k_MiddleHealthFloorThreshold"/> to 100 and above.
        /// </summary>
        Highest = 0
    }
}