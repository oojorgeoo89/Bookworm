using System;
namespace BookStore.Models.Helpers
{
    public class PageInfo
    {
        private const int maxSize = 100;

        public int Page { get; set; }
        public string OrderBy { get; set; }
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value < 1)
                    _size = 1;
                else if (value > 100)
                    _size = 100;
                else
                    _size = value;
            }
        }

        private int _size;

        public PageInfo()
        {
            Page = 0;
            OrderBy = "Id";
            Size = 12;
        }
    }
}
