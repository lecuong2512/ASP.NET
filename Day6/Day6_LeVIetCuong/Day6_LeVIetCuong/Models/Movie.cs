using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day6_LeVIetCuong.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phim")]
        [StringLength(200, ErrorMessage = "Tên phim không được vượt quá 200 ký tự")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn ngày phát hành")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Thời lượng phải lớn hơn 0")]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thể loại hợp lệ")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre? Genre { get; set; }
    }
}
