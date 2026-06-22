using System;
using Jellyfin.Plugin.MediaBarEnhanced.Model;

namespace Jellyfin.Plugin.MediaBarEnhanced.Helpers
{
    public static class TransformationPatches
    {
        public static string IndexHtml(PatchRequestPayload payload)
        {
            // Always return original content if something fails or is null
            string? originalContents = payload?.Contents;
            
            if (string.IsNullOrEmpty(originalContents))
            {
                return originalContents ?? string.Empty;
            }

            try 
            {
                // Safety Check: If plugin is disabled, do nothing
                if (MediaBarEnhancedPlugin.Instance?.Configuration?.IsEnabled != true)
                {
                    return originalContents;
                }

                // Use StringBuilder for efficient modification (conceptually similar to stream processing)
                var builder = new System.Text.StringBuilder(originalContents);

                // Inject Script if missing
                if (!originalContents.Contains(ScriptInjector.ScriptTag))
                {
                    var scriptIndex = originalContents.LastIndexOf(ScriptInjector.ScriptMarker, StringComparison.OrdinalIgnoreCase);
                    if (scriptIndex != -1)
                    {
                        builder.Insert(scriptIndex, ScriptInjector.ScriptTag + Environment.NewLine);
                    }
                }
                
                // Inject CSS if missing
                if (!originalContents.Contains(ScriptInjector.CssTag))
                {
                    var cssIndex = originalContents.LastIndexOf(ScriptInjector.CssMarker, StringComparison.OrdinalIgnoreCase);
                    if (cssIndex != -1)
                    {
                        builder.Insert(cssIndex, ScriptInjector.CssTag + Environment.NewLine);
                    }
                }
                
                return builder.ToString();
            }
            catch
            {
                // On error, return original content to avoid breaking the UI
                return originalContents;
            }
        }
    }
}
