using FaceKid.Entities;

namespace FaceKid.ViewModels
{
    public class PostIndexViewModel
    {
        public CreatePostViewModel NewPost { get; set; } = new();
        public List<Post> Posts { get; set; } = new();
    }
}
