namespace SportsStore.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int InemsPerPage { get; set; }
        
        public int CurrentPage { get;set; }
        
        public int TotalPage => (int) Math.Ceiling((decimal) TotalItems / InemsPerPage);
    }
}

