using System.Collections.Generic;

namespace MoeDeloApi.MoeDeloDto.Mony
{
    public class ApiPageResponseDto<T>
    {
        public List<T> Data { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public int? TotalCount { get; set; }
    }
}