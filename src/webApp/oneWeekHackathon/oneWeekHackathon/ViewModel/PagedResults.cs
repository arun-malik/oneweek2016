using System.Collections.Generic;

namespace oneWeekHackathon.ViewModel
{
    public class PagedResults<T>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalNumberOfPages { get; set; }
        public int totalNumberOfRecords { get; set; }
        public string nextPageUrl { get; set; }
        public IEnumerable<T> results { get; set; }
    }


}