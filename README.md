# Jellyfin Media Bar Enhanced Plugin

**Media Bar Enhanced (MBE)** is a plugin for Jellyfin that introduces a customizable and interactive media bar to your dashboard view on Jellyfin web.

This plugin is a fork and enhancement of the original [Media Bar by MakD](https://github.com/MakD/Jellyfin-Media-Bar) and my previous work on [Jellyfin-Featured-Content-Bar](https://github.com/CodeDevMLH/Jellyfin-Featured-Content-Bar), but can be installed as plugin for easier installation and management/configuration.

![logo](https://raw.githubusercontent.com/CodeDevMLH/jellyfin-plugin-media-bar-enhanced/main/logo.png)

---

## Table of Contents
- [Jellyfin Media Bar Enhanced Plugin](#jellyfin-media-bar-enhanced-plugin)
  - [Table of Contents](#table-of-contents)
  - [Overview](#overview)
  - [Features](#features)
    - [New Features \& Enhancements](#new-features--enhancements)
    - [Core Features](#core-features)
  - [Installation](#installation)
  - [Client Compatibility](#client-compatibility)
  - [Content Display Priority Hierarchy](#content-display-priority-hierarchy)
  - [Configuration](#configuration)
    - [General Settings](#general-settings)
    - [Custom Content](#custom-content)
    - [Custom Overlay](#custom-overlay)
    - [How to get the IDs:](#how-to-get-the-ids)
    - [Advanced Settings](#advanced-settings)
      - [Time Settings](#time-settings)
      - [Content Sorting](#content-sorting)
      - [Content Limits \& Library Filters](#content-limits--library-filters)
  - [Build The Plugin By Yourself](#build-the-plugin-by-yourself)
  - [Troubleshooting](#troubleshooting)
    - [Effects Not Showing](#effects-not-showing)
    - [Docker Permission Issues](#docker-permission-issues)
    - [YouTube "Sign in to confirm you're not a bot" message instead of trailers](#youtube-sign-in-to-confirm-youre-not-a-bot-message-instead-of-trailers)
  - [Uninstall](#uninstall)
  - [Credits](#credits)
  - [Contributing](#contributing)

---

## Overview
![demo](https://github.com/user-attachments/assets/3a01b886-1a96-4dd1-abf6-e9c3b054bfde)

Expand to get more impressions:

<details>
<summary>Desktop Layout</summary> 

<img width="1919" height="990" alt="dashboard trailer button" src="https://github.com/user-attachments/assets/a3c3c8a8-7925-473c-b15f-fc9695cb573f" />
Normal mode like the original with additional trailer button
<br><br><br>

<img width="1920" height="993" alt="modal_desktop" src="https://github.com/user-attachments/assets/9087f43d-cd9d-4581-a7e0-404b75bc8e02" />
Trailer modal
<br><br><br>

<img width="1919" height="996" alt="admin_mbe" src="https://github.com/user-attachments/assets/ed04ceb5-e401-4dc6-ad53-022d8bb26e99" />
Excerpt from the config: E.g. here you can simply add your items that should be displayed
<br><br>
</details>

<details>
<summary>Client side settings menu</summary>

| **Mobile Client Menu** | **Client Menu** |
|:---:|:---:|
| ![Mobile Client Menu](https://github.com/user-attachments/assets/a0e3912b-2fd3-4364-a9b9-235831b2b028) | ![Client Menu](https://github.com/user-attachments/assets/3a5f8715-09aa-4f97-9bb6-8c58059bc308) |

Supported languages (English, German, Spanish, French, Italian)
</details>

<details>
<summary>Mobile Layout</summary> 


<br>If trailer on mobile is enabled...
| **Demo Mobile** | **Demo Mobile 16:9** |
|:---:|:---:|
| ![demo_mobile](https://github.com/user-attachments/assets/d11a7ed0-ceb7-43c3-9b22-09510251e0aa) | ![demo_mobile_16-9](https://github.com/user-attachments/assets/4116c3ab-0de3-4928-82e9-938802e06aa3) |

<br><br><br>

| **Normal mode like the original with additional trailer button** | **Trailer modal in portrait mode** |
|:---:|:---:|
| ![demo_mobile](https://github.com/user-attachments/assets/f0a0cc0d-f019-45f5-96c8-a5de14bf92ba) | ![trailer_modal_mobile](https://github.com/user-attachments/assets/944f9b82-9c9b-411f-883b-877b65ed933f) |

<br><br><br>

| **Compact 16:9 mode** | **Classic 4:3 mode** |
|:---:|:---:|
| ![Compact 16:9 mode](https://github.com/user-attachments/assets/216cff8d-900d-4add-b6cf-b782cfc2fb6e) | ![Classic 4:3 mode](https://github.com/user-attachments/assets/9d6e6139-c381-45cb-9671-a7686e85efdd) |

</details>


## Features

This plugin builds upon the original Media Bar with new capabilities and improvements:

### New Features & Enhancements
*   **Personalized Recommendation Engine**:
    *   Integrates per-user recommendations tailored to watch history via Jellyfin's Suggestions API.
    *   Includes automatic fallback to standard library content if a user has no watch history.
*   **Cinematic Video Backdrops**:
    *   Full-width background video playback for YouTube and local video streams.
    *   Smart trailer selection prioritizes official main trailers over teasers and clips.
    *   **SponsorBlock & Offsets**: Automatically skip sponsored segments, intro logos, and trim end credits.
    *   **Sync Page Backdrop**: Ambiently mirrors the active slide backdrop into Jellyfin's full page background.
    *   **Hover Audio Fade**: Hover over the media bar to smoothly fade sound in while muted without changing persistent mute state.
*   **Title Logo Controls & Fallbacks**:
    *   **Require Title Logo Filter**: Optional toggle to only include media with dedicated transparent logos, bypassing text fallbacks when desired.
    *   TV Series, Season, and Episode slides automatically inherit parent Season or Series local trailers, logos, genres, and ratings when direct assets are missing.
*   **Per-Library Granular Control**:
    *   Choose specifically which libraries appear in Media Bar and which libraries are allowed to autoplay video trailers.
*   **Mobile-Optimized Experience**:
    *   **16:9 Wide** & **4:3 Classic** compact aspect ratio modes designed specifically for mobile portrait screens.
    *   Responsive layouts with vertical metadata stacking and touch swipe gestures.
*   **Per-Device Client-Side Settings**:
    *   Allows individual users on your server to override volume, aspect ratio, audio fade, recommendations, and library filters on their own device.
    *   Choose where the client-side configuration button appears (Navbar header gear icon, Sidebar navigation drawer, or Both).
    *   Supporting 5 languages (English, German, Spanish, French, Italian).
*   **Custom Content & Seasonal Scheduling**:
    *   Display custom BoxSets, Playlists, or dynamic filters like `genre:Action` and `tag:Christmas`.
    *   Schedule automated seasonal holiday themes with custom date ranges and priority rules.
*   **Enhanced Controls**:
    *   Keyboard shortcuts (Arrow keys to navigate, Space to pause, M to mute).
*   **Override Trailers**: Manually specify a custom trailer URL for any item via the Custom Media IDs list.
*   **Customizable Floating Overlays**:
    *   Display custom branding, holiday greetings, or promotional badges.
    *   Choose from 15+ animated CSS styles (Neon, Cyberpunk Glitch, Matrix, VHS, etc.) with custom image upload support.
*  **Many more settings**: Just install the plugin and check out the settings in the Jellyfin admin panel to see all the options.

### Core Features
*   **Immersive Slideshow**: Rotates through your media library.
*   **Metadata Display**: Shows title, rating, year, and plot summary.
*   **Direct Play**: Click "Play" to start watching immediately.
*   **Details View**: Click "Info" to jump to the item's detail page.
*   **Add To Favorites**: Click the heart to add the item to your favorites.
*   **Customize**: Change the plugins behavior through the Jellyfin admin panel.

## Installation

This plugin is based on Jellyfin Version `10.11.x`

1.  Open your **Jellyfin Dashboard**.
2.  Navigate to **Plugins** > **Repositories**.
3.  Click the **+** button to add a new repository.
4.  Enter a name for the repo and paste the following URL:
    ```
    https://raw.githubusercontent.com/CodeDevMLH/jellyfin-plugin-manifest/refs/heads/main/manifest.json
    ```
5.  Click **Save**.
6.  Go to the **Catalog** tab.
7.  Find **Media Bar Enhanced** (Under **General**) and install it.
8.  **Restart your Jellyfin server.**
9.  **Refresh your browser** (Ctrl+F5) to load the new interface elements.

## Client Compatibility

Because this plugin relies on injecting JavaScript and CSS into the web interface, it works best on clients that use the web wrapper.

| Client Platform | Status | Notes |
| :--- | :---: | :--- |
| **Web Browsers** (Firefox, Chrome etc.) | ✅ | Direct JS injection |
| **Jellyfin Media Player** (Windows/Linux/macOS) | ✅ | Uses jellyfin web |
| **Android App** | ✅ | Uses a web wrapper |
| **iOS App** | ✅ | Uses a web wrapper |
| **Android TV / Fire TV** | ❌ | **Not supported.** Uses a native Java/Kotlin UI. |
| **Tizen OS** | ❌ | **Not supported.** Uses a native UI. |
| **Roku** | ❌ | **Not supported.** Uses a native UI. |
| **Swiftfin** (iOS/tvOS) | ❌ | **Not supported.** Uses a native Swift UI. |
| **Kodi** (via Jellyfin Addon) | ❌ | **Not supported.** Uses Kodi's native skinning engine. |

## Content Display Priority Hierarchy

Media Bar Enhanced supports multiple content sources (from automated per-user recommendations and seasonal holiday themes to custom box sets and random library rotations). When multiple sources are active, they are evaluated in a defined order of priority:

| Priority | Level | Source | Description |
| :---: | :---: | :--- | :--- |
| **1** | 👤 Client | **Forced Custom Playlist** | When a user explicitly selects a playlist from the client settings menu (e.g. `Playlist: Marvel`), it takes top priority on their device. |
| **2** | 👤 Client | **Personalized Recommendations** | When a user turns ON *Personalized Recommendations* in their client menu, it overrides server-wide static custom IDs and shows suggestions tailored to their watch history. |
| **3** | 🖥️ Server | **Seasonal Content Mode** | Scheduled seasonal sections (e.g. Halloween, Christmas) automatically activate when enabled and the current calendar date matches the configured period. |
| **4** | 🖥️ Server | **Custom Media IDs** | Fixed list of item IDs, collections, `genre:`, or `tag:` filters configured by the server administrator. |
| **5** | 🖥️ Server | **Server Default Recommendations** | When *Enable Personalized Recommendations* is turned ON globally in the Admin Dashboard, users on `Default` receive watch-history recommendations. |
| **6** | 🖥️ Server | **Random / Recent Library Content** | Default dynamic rotation of library items (with parental rating, age limit, and library inclusion/exclusion filters applied). |

*Note: If a user has no watch history (0 played items), recommendations automatically fall back to standard library content to ensure the Media Bar is never empty.*


## Configuration

Configure the plugin via **Dashboard** > **Plugins** > **Media Bar Enhanced**.

> [!NOTE]
> You must refresh your browser window (F5 or Ctrl+R) after saving changes for them to take effect.

### General Settings
*   **Enable Media Bar Enhanced Plugin**: Master switch to toggle the plugin.
*   **Enable Video Backdrops**: Dynamically plays trailers in the background.
*   **Sync Page Backdrop**: Mirrors the active slide background image into Jellyfin's page background.
*   **Wait For Trailer To End**: Prevents slide transition until the video finishes.
*   **Enable Trailer on Mobile**: Specific setting to allow video playback on mobile devices (disabled by default to save data/battery).
*   **Show Trailer Button**: Adds a button to open the trailer in a popup modal if video backdrops are disabled (e.g. on mobile if trailers are disabled there).
*   **Prefer Local Trailers**: If enabled, local trailers will be preferred over remote (YouTube) trailers.
*   **Prefer Local Backdrops / Theme Videos**: If enabled, local backdrop videos (Theme Videos) will be preferred over remote and local trailers.
*   **Mobile Aspect Ratio**: Set a global default layout format (`Original` 65vh, `16:9` Compact, `4:3` Classic) for mobile portrait clients.
*   **Client Settings Menu Location**: Set where users can access the settings popup (`Navbar`, `Sidebar`, or `Both`).
*   **Default Trailer Volume**: Select the global default trailer volume (10% - 100%).

### Custom Content
Define exactly what shows up in your bar.

*   **Enable Custom Media IDs**: Restrict the slideshow to a specific list of IDs.
    *   **Dynamic Filtering**: Use `genre:Name` or `tag:Name` to pull all matching items from your library. Mix and match with regular IDs.
    *   **Manual Trailer Override**: Add `[YouTube_URL]` or `[Jellyfin_ID]` after an ID to force a specific trailer/video.
    *   Example ID: `a1b2c3d4e5... [https://www.youtube.com/watch?v=VIDEO_ID]`
    *   Example ID: `z1b2c3d4e5... [Jellyfin_ID]`
    *   **Example Mixed List**:
        ```
        genre:Action                            <-- All Action movies
        tag:Christmas                           <-- All Christmas tagged items
        a1b2c3d4e5f6...                         <-- Plays local item video
        6bdu812812hd... [https://youtu.be/...]  <-- Item metadata + Custom YouTube Trailer
        12h44h124sf7... [hdc78127z4ff...]       <-- Item metadata + Custom Jellyfin Trailer/Video etc.
        ```
    *   **Automatic ID Extraction**: Simply paste full Jellyfin item URLs (e.g., `.../details?id=XXXX...`) and the IDs will be automatically extracted.
    *   Example Collection Name: `Halloween Collection [https://...] | My Description` (Note: Use `|` to separate description from name if using a name instead of an ID)
*   **Custom Playlists**: Enter playlist names or IDs to directly display items from Jellyfin playlists.
*   **Apply Limits to Custom IDs**: If enabled, the "Content Limits" (see below) will also apply to your Custom Media IDs list. By default, custom lists show all listed items regardless of limits.
*   **Enable Seasonal Content Mode**: Advanced date-based scheduling.
    *   **GUI Configuration**: You can easily add "Seasons" via the **Add Season** button.
    *   **Active Period**: Select the Start and End Day/Month for each season.
    *   **Media IDs**: Enter the list of IDs/Filters for that season.
    *   **Overlay Overrides**: Specify custom overlay text/images just for this season.
    *   **Priority**: Rules are evaluated top-to-bottom. The first matching one wins.

### Custom Overlay
Add a global hover element to your slideshow for special events, seasonal greetings etc.
    *   **Overlay Text**: Display messages like "Happy Holidays!" or "New Movies!".
    *   **Overlay Image**: Upload or link a floating image/logo.
    *   **Animated Styles**: 15+ CSS-driven effects including Cinematic Glow, Cyberpunk Glitch, Matrix and more.
    *   **Fine-Tuning**: Adjust X/Y position and Scale.

### How to get the IDs:
Simply copy the URL of an item in the web interface and paste it into the "Custom Media IDs" field. The plugin will handle the rest!
Alternatively, check the URL: `.../web/#/details?id=YOUR_ITEM_ID_IS_HERE&...`

### Advanced Settings
*   **Slide Animations**: Enable/disable the "Zoom In" effect.
*   **Transition Effect**: Choose between `Fade`, `Slide`, or `None` for slide transitions.
*   **Show Progress Bar**: Display an animated progress bar indicating time remaining on the active slide (`Top`, `Bottom`, or `Disabled`).
*   **Client-Side Settings**: Allow users to customize their own experience by enabling client-side overrides for certain settings.
*   **Randomize Backdrop Video/Local Trailer**: If multiple videos are available, randomly select one instead of always using the first, if backdrop videos or local trailers are enabled.
*   **Use SponsorBlock**: Skips non-content segments in YouTube trailers (if the data exists).
*   **Start Muted**: Videos start without sound (user can unmute). On most devices necessary for autoplay!.
*   **Hover Audio Fade**: While muted, hovering over the media bar smoothly fades sound in; leaving fades sound out. Safe against browser autoplay restrictions and does not change the persistent mute button state. Default is disabled.
*   **Full Width Video**: Stretches video to cover the entire width (good for desktop, crop on mobile).
*   **Constrain Plot Width**: Aligns description text left to match logo width, preventing it from crossing the entire screen (also allows 3 lines of text instead of 2).
*   **Enable Loading Screen**: Enable/disable the loading indicator while the bar initializes.
*   **Always Show Arrow Navigation Buttons**: Keeps navigation arrows visible instead of hiding them on mouse leave.
*   **Hide Arrows on Mobile**: Disable arrow buttons on touch devices to prioritize swipe gestures.
*   **Enable Keyboard Controls**:
    *   `Left`/`Right`: Change slide
    *   `Space`: Pause/Play slideshow
    *   `M`: Mute/Unmute video

#### Time Settings
*   **Shuffle Interval (ms)**: Time each slide is displayed before transitioning to the next (only active on trailer slides if "Wait For Trailer To End" is disabled).
*   **Backdrop Video Delay (ms)**: Time to wait before playing background videos (leaves static backdrop visible longer).
*   **Trailer Start Offset (ms)**: Skip the first part of every trailer, e.g. a studio logo. YouTube trailers can only be skipped in whole seconds, and SponsorBlock still wins if it skips further. Default is `0` (disabled).
*   **Trailer End Offset (ms)**: Trim the end of every trailer, e.g. end credits. Default is `0` (disabled).
*   **Random Trailer Start Position**: Start each backdrop trailer at a random point (between 10% and 75% of usable length). Only active when "Wait For Trailer To End" is disabled to improve variety on short slideshow rotations. Default is enabled when "Wait For Trailer To End" is disabled.

#### Content Sorting
Customize the order of slides in the Media Bar.

*   **Sort By**: Choose criteria like *Original*, *Random*, *Premiere Date*, *Production Year*, *Critic Rating*, *Community Rating*, *Name*, or *Runtime*.
*   **Sort Order**: Ascending or Descending.
*   **Note**: Sorting applies to both server-fetched content AND Custom Media IDs. Select **Original** to preserve the exact order of your Custom Media IDs list.
*   **Max Parental Rating**: Items exceeding this age rating (e.g., 0, 12, 16, 18) will be excluded.
*   **Max Days Recent**: Only show items added to your library in the last X days.
*   **Include Watched Content**: If enabled, the random slideshow will also include items that you have already watched (by default, watched items are excluded from random selection to keep the content fresh).

#### Content Limits & Library Filters
Fine-tune performance by limiting the number of items fetched from the server and filtering libraries.

*   **Total Max Items**: Maximum total items to fetch (combined).
*   **Max Movies**: Maximum movies to include (for random selection).
*   **Max Tv Shows**: Maximum TV shows to include (for random selection).
*   **Libraries to Include in Media Bar**: Multi-select library checkboxes to include or exclude specific libraries from the Media Bar.
*   **Libraries Allowed to Play Trailer Backdrops**: Select specifically which libraries may autoplay video backdrops (unselected libraries will show static backdrops).
*   **Preload Count**: Number of slides to preload for smooth transitions (does not apply to low end devices like TVs or iPhones to prevent performance issues).
    *   *Intelligent Preloading*: The plugin uses a safe preloading strategy that respects this count but handles small lists gracefully to avoid playback issues.
*   **Show Pagination Dots**: Toggle the dots/counter visibility.
*   **Max Pagination Dots**: Maximum number of dots before switching to the counter (e.g., 3/20).
*   **Max Plot Length**: Limit the length of the metadata description, also limitied to 3 lines with the "Constrain Plot Width" option or otherwise 2 lines.

## Build The Plugin By Yourself

If you want to build the plugin yourself:

1.  Clone the repository.
2.  Ensure you have the .NET SDK installed (NET 8 or 9 depending on your Jellyfin version).
3.  Run the build command:
    ```powershell
    dotnet build Jellyfin.Plugin.MediaBarEnhanced/Jellyfin.Plugin.MediaBarEnhanced.csproj --configuration Release --output bin/Publish
    ```
4.  The compiled DLL and resources will be in bin/Publish.

## Troubleshooting

### Effects Not Showing
1. **Verify plugin installation**:
   - Check that the plugin appears in the jellyfin admin panel
   - Ensure that the plugin is enabled and active

2. **Clear browser cache**:
   - Force refresh browser (Ctrl+F5)
   - Clear jellyfin web client cache (--> mostly you have to clear the whole browser cache)

### Docker Permission Issues
If you encounter the message `Access was denied when attempting to inject script into index.html. Automatic direct injection failed. Automatic direct insertion failed. The system will now attempt to use the File Transformation plugin.` in the log or similar permission errors in Docker:

**Option 1: Use File Transformation Plugin (Recommended)**

Media Bar Enhanced now automatically detects and uses the [File Transformation](https://github.com/IAmParadox27/jellyfin-plugin-file-transformation) plugin (v2.5.0.0+) if it's installed. This eliminates permission issues by transforming content at runtime without modifying files on disk.

**Installation Steps:**
1. Install the File Transformation plugin from the Jellyfin plugin catalog
2. Restart Jellyfin
3. Media Bar Enhanced will automatically detect and use it (no configuration needed)
4. Check logs to confirm: Look for "Successfully registered transformation with File Transformation plugin"

**Benefits:**
- No file permission issues in Docker environments
- Works with read-only web directories
- Survives Jellyfin updates without re-injection
- No manual file modifications required

**Option 2: Fix File Permissions**
```bash
# Find the actual index.html location
docker exec -it jellyfin find / -name index.html

# Fix ownership (replace 'jellyfin' with your container name and adjust user:group if needed)
docker exec -it --user root jellyfin chown jellyfin:jellyfin /jellyfin/jellyfin-web/index.html

# Restart container
docker restart jellyfin
```

**Option 3: Manual Volume Mapping**
```bash
# Extract index.html from container
docker cp jellyfin:/jellyfin/jellyfin-web/index.html /path/to/jellyfin/config/index.html

# Add to docker-compose.yml volumes section:
volumes:
  - /path/to/jellyfin/config/index.html:/jellyfin/jellyfin-web/index.html
```

### YouTube "Sign in to confirm you're not a bot" message instead of trailers
If YouTube trailers stop playing and show a "Sign in" or bot check message (only happend to me during heavy testing):
1. This is usually due to loading too many trailers in a short period from the same IP address.
2. **Solution**: Use local trailers instead, or request a new IP address from your ISP (e.g., via your router reconnect) or wait for the temporary block to expire.
3. To prevent this, increase the **Shuffle Interval** or use **Backdrop Video Delay** to reduce the frequency of YouTube requests.

## Uninstall
To cleanly uninstall the plugin and ensure all injected scripts (Direct Injection or File Transformation) are removed from the Jellyfin web interface:

1.  Go to the **Plugin Settings** of Media Bar Enhanced.
2.  Uncheck **Enable Media Bar Enhanced**.
3.  Click **Save**. This triggers the cleanup of the `index.html` file or unregisters the file transformation.
4.  Restart Jellyfin to ensure all changes take effect.
5.  Go to **Dashboard** -> **Plugins**.
6.  Select **Uninstall** from the menu of the Media Bar Enhanced plugin.
7.  Restart Jellyfin again.

## Credits

This project is based on the original [Jellyfin Media Bar by MakD](https://github.com/MakD/Jellyfin-Media-Bar) and incorporates concepts from [IAmParadox27's plugin fork](https://github.com/IAmParadox27/jellyfin-plugin-media-bar). Thanks for their work!

Also, special thanks to IAmParadox27 for the [File Transformation plugin](https://github.com/IAmParadox27/jellyfin-plugin-file-transformation) which this plugin can optionally use for improved Docker compatibility.

## Contributing

Feel free to contribute to this project by creating pull requests or reporting issues.

> [!NOTE]
> **Guidelines for Pull Requests:** Please do **not** change version numbers in `manifest.json` or `.csproj` files in your Pull Requests. New releases and version updates are created exclusively by the repository maintainers.
