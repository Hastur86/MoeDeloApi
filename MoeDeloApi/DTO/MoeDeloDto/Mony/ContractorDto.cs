using System;

namespace MoeDeloApi.MoeDeloDto.Mony
{
    [Serializable]
    public class ContractorDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
    }
}