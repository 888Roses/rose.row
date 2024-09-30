using System.Collections.Generic;
using System.Xml.Serialization;

namespace rose.row.data.localisation.serialisation
{
    [XmlRoot(ElementName = "data")]
    public class LanguageKey
    {
        [XmlElement(ElementName = "value")] public string value { get; set; }
        [XmlAttribute(AttributeName = "name")] public string name { get; set; }
        [XmlText] public string text { get; set; }
    }

    [XmlRoot(ElementName = "root")]
    public class LanguageRoot
    {
        [XmlElement(ElementName = "data")] public List<LanguageKey> data { get; set; }
        [XmlAttribute(AttributeName = "language")] public string language { get; set; }
        [XmlText] public string text { get; set; }
    }
}