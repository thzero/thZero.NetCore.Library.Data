/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Data
Copyright (C) 2016-2019 thZero.com

<development [at] thzero [dot] com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 * ------------------------------------------------------------------------- */

using System;
using System.Collections.Generic;
using System.Linq;

namespace thZero
{
    public static class PerformSearch
    {
        #region Public Methods
        public static IEnumerable<T> ApplyPagination<T>(IEnumerable<T> list, SearchFilter filter)
        {
            if ((filter != null) && filter.PaginationCount.HasValue)
            {
                list = list.Skip(filter.PaginationStart.HasValue ? filter.PaginationStart.Value : 0);
                list = list.Take(filter.PaginationCount.Value);
            }

            return list;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static SearchResult<TResult> Filter<TResult, TFilter>(TFilter searchFilter, Func<TFilter, IEnumerable<TResult>> dataAction) where TFilter : SearchFilter
        {
            Enforce.AgainstNull<Func<TFilter, IEnumerable<TResult>>>(dataAction, "dataAction");

            IEnumerable<TResult> data = dataAction(searchFilter);

            if ((searchFilter != null) && searchFilter.PaginationCount.HasValue)
            {
                data = data.Skip(searchFilter.PaginationStart.HasValue ? searchFilter.PaginationStart.Value : 0);
                data = data.Take(searchFilter.PaginationCount.Value);
            }

            SearchResult<TResult> results = new SearchResult<TResult>(data);
            return results;
        }
        #endregion
    }

    [Serializable]
    public class SearchResult<T>
    {
        public SearchResult(IEnumerable<T> items)
        {
            Items = items;
        }

        public SearchResult(IEnumerable<T> items, int count)
        {
            Count = count;
            Items = items;
        }

        #region Public Properties
        public int Count
        {
            get => (_count.HasValue ? _count.Value : Items.Count());
            set => _count = value;
        }

        public IEnumerable<T> Items { get; private set; }

        public int? PaginationCount { get; set; }

        public int? PaginationStart { get; set; }
        #endregion

        #region Fields
        private int? _count;
        #endregion
    }
}
