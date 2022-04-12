using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class ImageModel
    {
        [Key]
        public Guid ImageId { get; set; } = Guid.NewGuid();

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }
    }
}
