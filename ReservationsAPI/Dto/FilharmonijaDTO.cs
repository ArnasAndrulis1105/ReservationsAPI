using System.Xml.Serialization;

namespace ReservationsAPI.Dto
{
    [XmlRoot("Filharmonija")]
    public class FilharmonijaDto
    {
        [XmlElement("Hall")]
        public HallDto Hall { get; set; }
    }
}
