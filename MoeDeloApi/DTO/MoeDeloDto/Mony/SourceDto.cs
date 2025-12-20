using System;

namespace MoeDeloApi.MoeDeloDto.Mony
{
    [Serializable]
    public class SourceDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
    }
}