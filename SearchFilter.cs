﻿/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Data
Copyright (C) 2016-2022 thZero.com

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

namespace thZero
{
    public abstract class SearchFilter
    {
        public SearchFilter()
        {
            Columns = new List<SearchFilterSort>();
        }

        #region Public Properties
        public ICollection<SearchFilterSort> Columns { get; private set; }
        public string Id { get; set; }
        public int? PaginationCount { get; set; }
        public int? PaginationStart { get; set; }
        public object Tag { get; set; }
        #endregion

        #region Constants
        public const string ColumnSortId = "Id";
        #endregion
    }

    [Serializable]
    public class SearchFilterSort
    {
        public SearchFilterSort()
        {
            Direction = SearchFilterSortDirection.Ascending;
        }

        #region Public Properties
        public string Column { get; set; }
        public SearchFilterSortDirection Direction { get; set; }
        #endregion
    }

    public enum SearchFilterSortDirection
    {
        Ascending,
        Descending
    }
}
