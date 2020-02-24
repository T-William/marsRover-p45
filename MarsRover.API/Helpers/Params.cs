using System.Collections.Generic;

namespace MarsRover.API.Helpers
{
    public class Params
    {
        public Params()
        {
            SearchFields = new Dictionary<string, string>();
        }
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public Dictionary<string, string> SearchFields { get; set; }

        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
    }
}