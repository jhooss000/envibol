namespace Aplicacion.Wrappers
{
    public class PageResponse<T> : Response<T>
    {

        public long PageNumber { get; set; }
        public int PageZise { get; set; }
        public long TotalCount { get; set; }

        public PageResponse(T data, long pageNumber, int pageZise, long totalCount)
        {
            PageNumber = pageNumber;
            PageZise = pageZise;
            TotalCount = totalCount;
            Message = null;
            Succeeded = true;
            Errors = null;
            Data = data;
        }


    }
}
