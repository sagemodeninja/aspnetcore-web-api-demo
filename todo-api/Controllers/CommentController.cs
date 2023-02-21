using Microsoft.AspNetCore.Mvc;
using todo_api.Classes;

namespace todo_api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private static List<Comment> _comments = new();

    [HttpGet(Name = "GetComments")]
    public IActionResult Get()
    {
        return Ok(_comments);
    }

    [HttpPost(Name = "CreateComments")]
    public IActionResult Create([FromBody] Comment comment)
    {
        if (_comments.Any(c => c.Id == comment.Id))
            return BadRequest($"A comment with id: {comment.Id} already exists.");

        _comments.Add(comment);
        return Ok();
    }

    [HttpPatch(Name = "UpdateComments")]
    public IActionResult Update(int id, [FromBody] Comment newComment)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);

        if (comment is null)
        {
            return NotFound();
        }

        comment.Timestamp = newComment.Timestamp;
        comment.Content = newComment.Content;

        return Ok();
    }

    [HttpDelete(Name = "DeleteComments")]
    public IActionResult Delete(int id)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == id);

        if (comment is null)
        {
            return NotFound();
        }

        _comments.Remove(comment);

        return Ok();
    }
}