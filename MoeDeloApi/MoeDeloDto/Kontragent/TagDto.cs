using System;

namespace MoeDeloApi.MoeDeloDto.Kontragent
{
    [Serializable]
    public class TagDto
    {
        public int? Id { get; set; }
        public int? EntityId { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool? IsSystem { get; set; }
        public int? UsageCount { get; set; }
    }
}