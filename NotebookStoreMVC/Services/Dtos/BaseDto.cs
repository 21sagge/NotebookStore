using System.Xml.Serialization;

namespace NotebookStoreMVC.Services;

[XmlInclude(typeof(CpuDto))]
[XmlInclude(typeof(DisplayDto))]
public class BaseDto
{
    public int Id { get; set; }
}
