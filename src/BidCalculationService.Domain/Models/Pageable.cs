namespace BidCalculationService.Domain.Models
{
    public class Pageable<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }
        public T[] Items { get; set; }

        public Pageable()
        {
            Items = Array.Empty<T>();
        }
    }
}
