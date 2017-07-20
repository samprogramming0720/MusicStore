using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WEB.Infrastructure.Core
{
    public class PagingSet<T>
    {
        public int Page { get; set; }

        public IEnumerable<T> Items { get; set; }

        public int Count
        {
            get
            {
                int ItemCount = 0;
                if(Items != null)
                {
                    ItemCount = Items.Count();
                }
                return ItemCount;
            }
        }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }
    }
}