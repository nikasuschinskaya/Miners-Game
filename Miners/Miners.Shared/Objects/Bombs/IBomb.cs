namespace Miners.Shared.Objects.Bombs
{
    public interface IBomb
    {
        /// <summary>Gets or sets the radius.</summary>
        /// <value>The radius.</value>
        int Radius { get; set; }

        /// <summary>Gets or sets the damage.</summary>
        /// <value>The damage.</value>
        int Damage { get; set; }
    }
}