using IDBM_Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace IDBM_Final.ViewModels
{
    public class EpisodeViewModel : INotifyPropertyChanged
    {
        private readonly ImdbContext _context = new ImdbContext();

        private int _episodeNumber;
        private int _seasonNumber;
        private string _titleId;
        private string _primaryTitle;
        private int _totalEpisodes;
        private string _searchText;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Episode> Episodes { get; set; }

        public int EpisodeNumber
        {
            get { return _episodeNumber; }
            set
            {
                _episodeNumber = value;
                OnPropertyChanged("EpisodeNumber");
            }
        }

        public int SeasonNumber
        {
            get { return _seasonNumber; }
            set
            {
                _seasonNumber = value;
                OnPropertyChanged("SeasonNumber");
            }
        }

        public string TitleId
        {
            get { return _titleId; }
            set
            {
                _titleId = value;
                OnPropertyChanged("TitleId");
            }
        }

        public string PrimaryTitle
        {
            get { return _primaryTitle; }
            set
            {
                _primaryTitle = value;
                OnPropertyChanged("PrimaryTitle");
            }
        }

        public int TotalEpisodes
        {
            get { return _totalEpisodes; }
            set
            {
                _totalEpisodes = value;
                OnPropertyChanged("TotalEpisodes");
            }
        }



        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                ApplyFilters();
            }
        }

        private void ApplyFilters()
        {
            var query = _context.Episodes
                .Include(e => e.Title)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(e => e.Title.PrimaryTitle.Contains(SearchText));
            }

            Episodes.Clear();
            foreach (var episode in query)
            {
                Episodes.Add(episode);

            }
        }
            public EpisodeViewModel()
        {
            Episodes = new ObservableCollection<Episode>();
            LoadEpisodes();
        }
        private void LoadEpisodes()
        {
            var data = _context.Episodes
                .Include(e => e.Title)
                .ToList();
            foreach (var episode in data) {
                Episodes.Add(episode);
            }
            TotalEpisodes = Episodes.Count;




        }
    }
}
