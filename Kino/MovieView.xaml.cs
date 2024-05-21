using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kino
{
    //Возврат одного случайного фильма
    //http://192.168.88.22:8080/movies/random
    //Возврат всех фильмов
    //http://192.168.88.22:8080/movies/all
    //Документация
    //http://192.168.88.22:8080/docs#/
    //192.168.88.22:3000/code

    public partial class MovieView : UserControl
    {
        public MovieView(Movie movie)
        {
            InitializeComponent();
            NameMovie.Text = movie.title;
            GenresMovie.Content = string.Join(", ", movie.genres);//жанры
            RatingMovie.Content = "IMDb: " + movie.rating;//рейтинг
            YearMovie.Content = movie.year;//год
            CountryMovie.Content = movie.country;//страны
            PosterMovie.Source = new BitmapImage(new Uri(movie.poster));
            IdMovie.Text = (movie.id + 1).ToString();
        }
           
    }
}
