using FaceKid.Entities;

namespace FaceKid.Services.Interfaces
{
    public interface IPostService
    {
        Task AddPostAsync(Post post);
        Task<List<Post>> GetAllPostAsync();
    }
}
