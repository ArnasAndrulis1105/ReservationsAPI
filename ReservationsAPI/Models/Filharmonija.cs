using System.Xml.Serialization;

namespace ReservationsAPI.Models
{
    [XmlRoot("Filharmonija")]
    public class Filharmonija
    {
        [XmlElement("Hall")]
        public Hall Hall { get; set; }
    }
}
