using FaceKid.Entities;
using FaceKid.Services.Interfaces;
using FaceKid.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FaceKid.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FaceKid.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IValidator<CreatePostViewModel> _validator;
        private readonly ILogger<PostController> _logger;

        private readonly IHubContext<PostHub> _hubContext;
        public PostController(IPostService postService, IValidator<CreatePostViewModel> validator,
            ILogger<PostController> logger,
            IHubContext<PostHub> hubContext)
        {
            _postService = postService;
            _validator = validator;
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new PostIndexViewModel
            {
                Posts = await _postService.GetAllPostAsync()
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "NewPost")] CreatePostViewModel model) 
        {
            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var pageModel = new PostIndexViewModel
                {
                    NewPost = model,
                    Posts = await _postService.GetAllPostAsync()
                };

                return View("Index", pageModel);
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var post = new Post
            {
                Content = model.Content.Trim(),
                SentAt = DateTime.UtcNow,
                UserId = userId,
            };

            await _postService.AddPostAsync(post);

            await _hubContext.Clients.All.SendAsync("ReceivePost",
                new
                {
                    id = post.Id,
                    content = post.Content,
                    sentAt = post.SentAt,
                    fullName = User.Identity?.Name
                });

            _logger.LogInformation("Kullanıcı post paylaştı! PostId: {PostId}", post.Id);

            return RedirectToAction(nameof(Index));

        }


    }
}
