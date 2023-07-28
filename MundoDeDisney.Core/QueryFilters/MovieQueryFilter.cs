namespace MundoDeDisney.MundoDeDisney.Core.QueryFilters
{
    public class MovieQueryfilter
    {
        /// <summary>
        /// Search by title
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Search by genre
        /// </summary>
        public int? GenerID { get; set; }
        /// <summary>
        /// Must indicate ASC or DESC to search in ascending or descending order
        /// </summary>
        public string? Order { get; set; }
    }
}
