using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.MediaBarEnhanced.Configuration
{
    /// <summary>
    /// Plugin configuration.
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public int ShuffleInterval { get; set; } = 7000;
        public int RetryInterval { get; set; } = 500;
        public int MinSwipeDistance { get; set; } = 50;
        public int LoadingCheckInterval { get; set; } = 100;
        public int MaxPlotLength { get; set; } = 360;
        public int MaxMovies { get; set; } = 20;
        public int MaxTvShows { get; set; } = 20;
        public int MaxItems { get; set; } = 20;
        public int MaxParentalRating { get; set; } = 0;
        public int MaxDaysRecent { get; set; } = 0;
        public int PreloadCount { get; set; } = 3;
        public int FadeTransitionDuration { get; set; } = 500;
        public int MaxPaginationDots { get; set; } = 15;
        public bool ShowPaginationDots { get; set; } = true;
        public bool ForceSlideCounter { get; set; } = false;
        public bool SlideAnimationEnabled { get; set; } = true;
        public bool EnableVideoBackdrop { get; set; } = true;
        public bool UseSponsorBlock { get; set; } = true;
        public string SponsorBlockCategories { get; set; } = "intro,outro,preview";
        public bool PreferLocalTrailers { get; set; } = false;
        public bool RandomizeLocalTrailers { get; set; } = false;
        /// <summary>
        /// When true, start trailer/backdrop video playback at a random time within the usable range
        /// instead of always at the beginning (better variety on short slideshow dwell times).
        /// </summary>
        public bool RandomTrailerStartOffset { get; set; } = true;
        /// <summary>
        /// Minimum start position as percent of usable trailer duration (0-100). Default 10.
        /// </summary>
        public int RandomTrailerStartMinPercent { get; set; } = 10;
        /// <summary>
        /// Maximum start position as percent of usable trailer duration (0-100). Default 75.
        /// </summary>
        public int RandomTrailerStartMaxPercent { get; set; } = 75;
        public bool PreferLocalBackdrops { get; set; } = false;
        public bool RandomizeThemeVideos { get; set; } = false;
        public bool WaitForTrailerToEnd { get; set; } = true;
        public bool StartMuted { get; set; } = true;
        public int DefaultTrailerVolume { get; set; } = 40;
        public bool FullWidthVideo { get; set; } = true;
        public bool EnableMobileVideo { get; set; } = false;
        public bool ShowTrailerButton { get; set; } = true;
        public bool EnableLoadingScreen { get; set; } = true;
        public bool EnableKeyboardControls { get; set; } = true;
        public bool AlwaysShowArrows { get; set; } = false;
        public bool HideArrowsOnMobile { get; set; } = true;
        public string CustomMediaIds { get; set; } = "";
        public bool EnableCustomMediaIds { get; set; } = true;
        public bool EnableSeasonalContent { get; set; } = false;
        public bool ExcludeSeasonalContent { get; set; } = true;
        public string SeasonalSections { get; set; } = "[]";
        public bool IsEnabled { get; set; } = true;
        public bool EnableClientSideSettings { get; set; } = true;
        public bool ApplyLimitsToCustomIds { get; set; } = false;
        public bool IncludeWatchedContent { get; set; } = false;
        public string SortBy { get; set; } = "Random";
        public string SortOrder { get; set; } = "Ascending";
        public int BackdropVideoDelay { get; set; } = 0;
        public int TrailerStartOffset { get; set; } = 0;
        public bool ConstrainPlotWidth { get; set; } = false;
        
        public bool EnableCustomOverlay { get; set; } = false;
        public string CustomOverlayText { get; set; } = "";
        public string CustomOverlayImageUrl { get; set; } = "";
        public string CustomOverlayStyle { get; set; } = "Shadowed";
        public string CustomOverlayImageStyle { get; set; } = "None";
        public string CustomOverlayPriority { get; set; } = "Image";
        
        public int CustomOverlayPositionX { get; set; } = 0;
        public int CustomOverlayPositionY { get; set; } = 0;
        public int CustomOverlayScale { get; set; } = 100;
 
        public string MobileCompactMode { get; set; } = "Original";
        public string ClientMenuLocation { get; set; } = "Navbar";
        public string ClientMenuLocationMobile { get; set; } = "Sidebar";

        public string TransitionEffect { get; set; } = "Fade";
        public bool ShowProgressBar { get; set; } = true;
        public string ProgressBarLocation { get; set; } = "Dots";
        public string CustomPlaylists { get; set; } = "[]";
        public string ExcludedLibraries { get; set; } = "";
        public bool OnlyLocalTrailers { get; set; } = false;
        public bool YoYoProgressBar { get; set; } = true;
    }
}
