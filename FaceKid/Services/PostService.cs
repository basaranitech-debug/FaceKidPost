using FaceKid.Data;
using FaceKid.Entities;
using FaceKid.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FaceKid.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<PostService> _logger;

        public PostService(ApplicationDbContext context, ILogger<PostService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddPostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Yeni post oluşturuldu PostId: {PostId}", post.Id);

        }

        public async Task<List<Post>> GetAllPostAsync()
        {
            return await _context.Posts.Include(p => p.User)
                .OrderByDescending(p => p.SentAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
