namespace MundoDeDisney.MundoDeDisney.Core.QueryFilters
{
    public class CharacterQueryFilter
    {
        /// <summary>
        /// Search by name
        /// </summary>        
        public string? Name { get; set; }
        /// <summary>
        /// Search by age
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// Search by movie
        /// </summary>
        public int? MovieID { get; set; }
        public int PageSize { get; set; } = 1;
        public int PageNumber { get; set; }=10;
    }
}
