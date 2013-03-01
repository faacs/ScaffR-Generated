namespace DemoApplication.Core.Interfaces.Paging
{
    using System.Collections.Generic;

    public interface IPage<T>
    {
        int CurrentPage { get; set; }
        int PagesCount { get; set; }
        int PageSize { get; set; }
        int Count { get; set; }
        IList<T> Entities { get; set; }
    }
}