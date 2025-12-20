using System;
using System.Collections.Generic;

namespace MoeDeloApi.MoeDeloDto.Kontragent
{
    [Serializable]
    public class KontragentDto
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string Okpo { get; set; }
        public string Kpp { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Type { get; set; }
        public string Form { get; set; }
        public bool? IsArchived { get; set; }
        public string LegalAddress { get; set; }
        public string ActualAddress { get; set; }
        public string RegistrationAddress { get; set; }
        public string TaxpayerNumber { get; set; }
        public string AdditionalRegNumber { get; set; }
        public int? SubcontoId { get; set; }
        public string Fio { get; set; }
        public string SignerFio { get; set; }
        public string InFace { get; set; }
        public string Position { get; set; }
        public string InReason { get; set; }
        public string PersonalData { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}