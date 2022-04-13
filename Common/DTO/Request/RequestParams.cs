﻿namespace Common.DTO.Request
{
    public class RequestParams
    {
        const int maxPageSize = 30;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }

        }
    }
}
