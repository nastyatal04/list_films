using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Kino
{
    public class Movie
    {
        public Movie(int id, string title, string poster, int year, string country, double rating, List<string> genres)
        {
            this.id = id;
            this.title = title;
            this.poster = poster;
            this.year = year;
            this.country = country;
            this.rating = rating;
            this.genres = genres;
        }

        public int id { get; set; }
        /// <summary>
        /// Название фильма.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Постер фильма.
        /// </summary>
        public string poster { get; set; }
        /// <summary>
        /// Год выпуска фильма.
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// Страны, работавшие над фильмом.
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Рейтинг фильма.
        /// </summary>
        public double rating { get; set; }
        /// <summary>
        /// Жанр фильма.
        /// </summary>
        public List<string> genres { get; set; }

        
        public string GetGeners()
        {
            string s = "";
            foreach (var item in genres) { s += item + " "; }
            return s;
        }

        //{"id":146,
        //"title":"Отряд самоубийц",
        //"poster":"https://static.kinoafisha.info/k/movie_posters/220/upload/movie_posters/7/6/8/8169867/009843dc1d71334581ef2589db73fe24.jpeg",
        //"year":2016,
        //"country":"США",
        //"rating":7.9,
        //"genres":["боевик","фантастика"]}
    }
}
