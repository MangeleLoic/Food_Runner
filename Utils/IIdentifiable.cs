namespace FoodRunner.Utils
{
    /// <summary>
    /// Interfaccia che definisce una proprietà Id per identificare univocamente un oggetto.
    /// Qualsiasi classe che intende essere gestita da IdGenerator deve implementarla.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Identificativo numerico univoco dell'oggetto.
        /// </summary>
        int Id { get; set; }
    }
}
