using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel
{
    public class ChangeCategoryModel
    {
        [Required(ErrorMessage = "É necessário informar o nome")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 40 caracteres")]

        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
    }
}
