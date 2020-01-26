﻿namespace IRunes.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using IRunes.App.ViewModels.Albums;
    using IRunes.Models;
    using IRunes.Services;
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes.Http;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Mapping;
    using SIS.MvcFramework.Result;

    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }

        [Authorize]
        public IActionResult All()
        {
            ICollection<Album> allAlbums = this.albumService.GetAllAlbums();

            if (allAlbums.Count != 0)
            {
                return this.View(allAlbums.Select(ModelMapper.ProjectTo<AlbumAllViewModel>).ToList());
            }

            return this.View(new List<AlbumAllViewModel>());
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost(ActionName = "Create")]
        public IActionResult CreateConfirm()
        {
            var name = ((ISet<string>)this.Request.FormData["name"]).FirstOrDefault();
            var cover = ((ISet<string>)this.Request.FormData["cover"]).FirstOrDefault();

            var album = new Album
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Cover = cover,
                Price = 0m
            };

            this.albumService.CreateAlbum(album);

            return this.Redirect("/Albums/All");
        }

        [Authorize]
        public IActionResult Details()
        {
            var albumId = this.Request.QueryData["id"].ToString();
            var albumFromDb = this.albumService.GetAlbumById(albumId);

            var albumDetailsViewModel = ModelMapper.ProjectTo<AlbumDetailsViewModel>(albumFromDb);

            if (albumFromDb == null)
            {
                return this.Redirect("/Albums/All");
            }

            return this.View(albumDetailsViewModel);
        }
    }
}
