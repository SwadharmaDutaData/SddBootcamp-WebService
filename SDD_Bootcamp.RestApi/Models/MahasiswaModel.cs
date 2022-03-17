using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDD_Bootcamp.RestApi.Models
{
    [Table("Mahasiswa")]
    public class MahasiswaModel
    {
        [Key]
        public int Id { get; set; }
        public string Npm { get; set; }
        public string NamaMahasiswa { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string JenisKelamin { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
