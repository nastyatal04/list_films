using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Kino
{
    public partial class MainWindow : Window
    {
        List<Movie> movies = new List<Movie>();
        List<Movie> filteredMovies = new List<Movie>();
        int page = 1;//текущая страница
        int maxPage = 29;
        public MainWindow()
        {
            InitializeComponent();
            UploadMovies(GetAdres());//список фильмов
            OutputMovieCards(movies);
            GenresExpander.Items.Add("Все жанры");
            movies.SelectMany(movie => movie.genres)
                .Distinct()
                .ToList()
                .ForEach(genre => GenresExpander.Items.Add(genre));
            CountryExpander.Items.Add("Все страны");
            movies.SelectMany(movie => movie.country.Split(" / "))
                .Distinct()
                .ToList()
                .ForEach(country => CountryExpander.Items.Add(country));
            YearsMovie.Items.Add("Все годы");
            movies.Select(movie => movie.year)
                .Distinct()
                .ToList()
                .ForEach(year => YearsMovie.Items.Add(year));
        }

        //Маленькие и тупые
        //private void SetGanresList()
        //{
        //    genresMovie.Sort();//сортируем список жанров            
        //    foreach(var g in genresMovie)
        //    {
        //        TextBlock textBlock = new TextBlock();
        //        textBlock.Margin = new Thickness(20, 0, 0, 0);
        //        textBlock.FontSize = 14;
        //        textBlock.Text = g;
        //        GanresExpander.Children.Add(textBlock);
        //    }                
        //}
        //private void GanresSearch(Movie movie)
        //{
        //    bool flag = false;
        //    //проходимся по жанрам переданного фильма, если встречаетсчя жанр, которого нет в списке жанров, то добавляем его туда
        //    foreach (string gm in movie.genres)
        //    {
        //        foreach (string gs in genresMovie)
        //            if (gs == gm)
        //            {
        //                flag = true;
        //                break;
        //            }
        //        if (!flag)
        //            genresMovie.Add(gm);
        //    }
        //}

        /// <summary>
        /// Получаем список филмов с сервера.
        /// </summary>
        /// <returns></returns>
        private void UploadMovies(string adres)
        {
            WebClient client = new WebClient();
            client.BaseAddress = "http://192.168.88.22:8080/";
            string json = client.DownloadString(adres);
            movies = JsonSerializer.Deserialize<List<Movie>>(json);
        }

        private void OutputMovieCards(List<Movie> moviesList)
        {
            //здесь смена страниц
            MoviesPanel.Children.Clear();
            if (moviesList.Count != 0)
                foreach (Movie movie in moviesList)
                {
                    MovieView movieView = new MovieView(movie);
                    MoviesPanel.Children.Add(movieView);
                }
            else
                MoviesPanel.Children.Add(new TextBlock { Text = "Ничего не найдено", Margin = new Thickness(20, 0, 0, 0), FontSize = 16 });
        }

        private List<Movie> FilteredMovies(int yearF, float ratingF, string countryF, string genreF, string nameF)
        {
            filteredMovies = new List<Movie>(movies);
            if (yearF != 0)
                filteredMovies.RemoveAll(movie => movie.year != yearF);
            if (genreF != "")
                filteredMovies.RemoveAll(movie => !movie.genres.Contains(genreF));
            if (countryF != "")
                filteredMovies.RemoveAll(movie => !movie.country.Contains(countryF));
            if (nameF != "")
                filteredMovies.RemoveAll(movie => !movie.title.ToLower().Contains(nameF.ToLower()));
            filteredMovies.RemoveAll(movie => movie.rating < ratingF);
            return filteredMovies;
        }

        private void ListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (MoviesPanel == null)
                return;
            int yearF = 0;
            float ratingF = (float)RatngSlider.Value;
            string genreF = "", countryF = "", nameF = "";
            //Получаем выбранный год
            if (YearsMovie.SelectedItem != null && Convert.ToString(YearsMovie.SelectedItem) != "Все годы")
                yearF = Convert.ToInt32(YearsMovie.SelectedItem);

            //Получаем выбранный жанр
            if (GenresExpander.SelectedItem != null && Convert.ToString(GenresExpander.SelectedItem) != "Все жанры")
                genreF = Convert.ToString(GenresExpander.SelectedItem);

            //Получаем выбранную страну
            if (CountryExpander.SelectedItem != null && Convert.ToString(CountryExpander.SelectedItem) != "Все страны")
                countryF = Convert.ToString(CountryExpander.SelectedItem);

            //Получаем введённое название фильма
            if (SearchMovie != null)
                nameF = SearchMovie.Text;

            OutputMovieCards(FilteredMovies(yearF, ratingF, countryF, genreF, nameF));
        }

        private string GetAdres()
        {
            return "movies/" + page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if(Convert.ToInt32(Pages.Text)<=maxPage && Convert.ToInt32(Pages.Text) >= 1)
                page = (button.Content == ">") ? page-1 : page+1;
            Pages.Text = page.ToString();
            UploadMovies("movies/" + page.ToString());
            OutputMovieCards(movies);
        }
    }
}