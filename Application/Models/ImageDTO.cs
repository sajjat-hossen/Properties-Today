namespace Application.Models
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsUploaded { get; set; }
        public PropertyDTO Property { get; set; }
    }
}
