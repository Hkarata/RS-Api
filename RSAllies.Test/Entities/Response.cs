using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Test.Entities
{
    [Table("Responses", Schema = "Test")]
    [PrimaryKey("UserId")]
    internal class Response
    {
        public Guid UserId { get; set; }

        public List<SelectedChoice>? SelectedChoices { get; set; }
    }
}
