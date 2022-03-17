using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDD_Bootcamp.RestApi.Models
{
    [Table("TblJurusan")]
    public class TblJurusanModel
    {
        [Key]
        public int JurusanId { get; set; }
        public string NamaJurusan { get; set; }
        public string DeskripsiJurusan { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
