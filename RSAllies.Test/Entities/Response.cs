using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
