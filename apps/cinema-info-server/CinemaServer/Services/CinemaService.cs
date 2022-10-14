﻿using CinemaServer.Data;
using CinemaServer.Data.Convertor;
using CinemaServer.Data.DTO;
using CinemaServer.Data.DTO.InterfaceDTO;
using CinemaServer.Data.Entities;
using CinemaServer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CinemaServer.Services
{

    public class CinemaService:ICinemaService
    {
        private readonly AppDbContext Context;
        public CinemaService (AppDbContext appDb)
        {
            Context = appDb;
        }
        
        public List<IMovieMainPageInfoDTO<ITagDTO>> MainCinema()
        {
            MovieConvertor MC = new();            
            var list = Context.Movies
                .Include(x => x.Sessions)
                .Include(x => x.Tags)
                .Where(x => x.Sessions.Count > 0)
                .Where(x => x.Sessions.Where(x=>x.ShowEndDate>DateTime.Now).Count()>0)
                .ToList();

            List<IMovieMainPageInfoDTO<ITagDTO>> ListDTO =new();            
            foreach (Movie movie in list)
            {
               ListDTO.Add(MC.Convert(movie));
            }
            return ListDTO;
        }
        public void AddMovie(Movie movie)
        {
            List<Tag> AddingNewTags = movie.Tags.Where(x => x.Id == 0).ToList();
                        
            if (AddingNewTags.Count > 0)
            {
                Context.Tags.AddRange(AddingNewTags);
                Context.SaveChanges();
            }
            movie.DateCreate = DateTime.Now;
            Context.Movies.Update(movie);
            Context.SaveChanges();            
        }
        public void AddRandomMovie()
        {
            string nameimg = "NameImg";
            Random random = new Random();
            Movie movie = new Movie();
            movie.Name = random.Next(0, 10000).ToString();
            movie.Description = random.Next(0, 1000).ToString();
            movie.DateCreate = DateTime.Now;
            movie.NameImg = nameimg + random.Next(0, 9999999) + ".png";
            AddMovie(movie);
        }
        public List<ITagDTO> AllTags()
        {
            return new(Context.Tags.ToList());
        }
        public List<IHallDTO> AllHall()
        {
            return new(Context.Halls.ToList());
        }
        public List<IMovieMainPageInfoDTO<ITagDTO>> AllMovie()
        {
            var list = Context.Movies.ToList();
            MovieConvertor MC = new();
            List<IMovieMainPageInfoDTO<ITagDTO>> ListDTO = new();
            foreach (Movie movie in list)
            {
                ListDTO.Add(MC.Convert(movie));
            }
            return ListDTO;
        }
    }
}
