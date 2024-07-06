using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialWebApp.Models;
using SocialWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using SocialWebApp.ViewModels;
using Microsoft.Identity.Client;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Identity;

namespace SocialWebApp.Controllers
{
    [Authorize]
    public class PostsController: Controller
    {
        AppDbContext context;
        IWebHostEnvironment environment;
        public PostsController(AppDbContext context, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.context = context;
        }
        public IActionResult Index()
        {
            var posts = context.Posts.OrderByDescending(i => i.PostId).ToList(); 
            return View(posts);
        }
        public IActionResult Create()
        {   
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostVM postVM)
        {
            if (postVM.ImageFile != null)
            {
/*                if (!ModelState.IsValid)
                {
                    return View(postVM);
                }*/
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(postVM.ImageFile!.FileName);

                string imageFulPath = environment.WebRootPath + "/posts/" + newFileName;
                using (var stream = System.IO.File.Create(imageFulPath))
                {
                    postVM.ImageFile.CopyTo(stream);
                }
            }
            Post post = new Post()
            {
                Title = postVM.Title,
                Description = postVM.Description,
                ImageUrl = ""
            };

            context.Posts.Add(post);
            context.SaveChanges();

            return RedirectToAction("Index", "Posts");
        }
        public IActionResult Delete(int id)
        {
            var post = context.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Posts");
            }
            context.Posts.Remove(post);
            foreach (var comment in context.Comment)
            {
                if (comment.PostId == post.PostId)
                {
                    context.Comment.Remove(comment);
                }
            }
            context.SaveChanges(true);

            return RedirectToAction("Index", "Posts");
        }
        public IActionResult Edit(int id)
        {
            var post = context.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Posts");
            }

            var postVM = new PostVM()
            {
                Description = post.Description,
                Title = post.Title,
            };
            ViewData["postId"] = post.PostId;
            

            return View(postVM);
        }
        [HttpPost]
        public IActionResult Edit(int id, PostVM postVM)
        {
            var post = context.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Posts");
            }
            post.Title = postVM.Title;
            post.Description = postVM.Description;
            context.SaveChanges();
            return RedirectToAction("Index", "Posts");
        }
        public IActionResult CreateComment(int id)
        {
            var post = context.Posts.Find(id);
            if (post == null)   
            {
                return RedirectToAction("Index", "Posts");
            }
            var comment = new Comment();
            ViewData["PostId"] = post.PostId;
            ViewData["Description"] = post.Description;
            ViewData["Title"] = post.Title;
            return View(comment);
        }
        [HttpPost]
        public IActionResult CreateComment(int id, Comment comment)
        {
            var post = context.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Posts");
            }
            comment.PostId = post.PostId;
            post.Comments = new List<Comment>();//?
            post.Comments.Add(comment);//?

            context.Comment.Add(comment);
            context.SaveChanges();
            return RedirectToAction("Index", "Posts");
        }
        public IActionResult ListOfComments(int id)
        {
            var post = context.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("Index", "Posts");
            }
            //IEnumerable<Comment> comments = from x in context.Comment where x.PostId == post.PostId select x;
            var comments = new List<Comment>();
            foreach (var comment in context.Comment)
            {
                if (comment.PostId == post.PostId)
                {
                    comments.Add(comment);
                }
            }
            ViewBag.Comments = comments;
            return PartialView("ListOfComments", comments);
        }
    }
}
