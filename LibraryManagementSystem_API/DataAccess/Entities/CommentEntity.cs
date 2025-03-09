using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CommentEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid BookId { get; set; }

    public string Username { get; set; }

    public float Rating { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDate { get; set; }



}