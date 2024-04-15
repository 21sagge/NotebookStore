using System.Xml.Serialization;

namespace NotebookStoreMVC.Services;

[XmlInclude(typeof(CpuDto))]
[XmlInclude(typeof(DisplayDto))]
[XmlInclude(typeof(MemoryDto))]
[XmlInclude(typeof(StorageDto))]
[XmlInclude(typeof(ModelDto))]
[XmlInclude(typeof(BrandDto))]
[XmlInclude(typeof(NotebookDto))]
public class BaseDto
{
    public int Id { get; set; }
}
