using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using MediaBrowser.Common.Configuration;
using Jellyfin.Plugin.MediaBarEnhanced.Helpers;

namespace Jellyfin.Plugin.MediaBarEnhanced
{
    /// <summary>
    /// Handles the injection of the MediaBarEnhanced script into the Jellyfin web interface.
    /// </summary>
    public class ScriptInjector
    {
        private readonly IApplicationPaths _appPaths;
        private readonly ILogger<ScriptInjector> _logger;
        public const string ScriptTag = "<script src=\"../MediaBarEnhanced/Resources/mediaBarEnhanced.js\" defer></script>";
        public const string CssTag = "<link rel=\"stylesheet\" href=\"../MediaBarEnhanced/Resources/mediaBarEnhanced.css\" />";
        public const string ScriptMarker = "</body>";
        public const string CssMarker = "</head>";

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInjector"/> class.
        /// </summary>
        /// <param name="appPaths">The application paths.</param>
        /// <param name="logger">The logger.</param>
        public ScriptInjector(IApplicationPaths appPaths, ILogger<ScriptInjector> logger)
        {
            _appPaths = appPaths;
            _logger = logger;
        }

        /// <summary>
        /// Injects the script tag into index.html if it's not already present.
        /// </summary>
        public void Inject()
        {
            try
            {
                var webPath = GetWebPath();
                if (string.IsNullOrEmpty(webPath))
                {
                    _logger.LogWarning("Could not find Jellyfin web path. Script injection skipped. Attempting fallback.");
                    RegisterFileTransformation();
                    return;
                }

                var indexPath = Path.Combine(webPath, "index.html");
                if (!File.Exists(indexPath))
                {
                    _logger.LogWarning("index.html not found at {Path}. Script injection skipped. Attempting fallback.", indexPath);
                    RegisterFileTransformation();
                    return;
                }

                var content = File.ReadAllText(indexPath);
                var injectedJS = false;
                var injectedCSS = false;

                if (!content.Contains(ScriptTag))
                {
                    var index = content.IndexOf(ScriptMarker, StringComparison.OrdinalIgnoreCase);
                    if (index != -1)
                    {
                        content = content.Insert(index, ScriptTag + Environment.NewLine);
                        injectedJS = true;
                    }
                }
                
                if (!content.Contains(CssTag))
                {
                     var index = content.IndexOf(CssMarker, StringComparison.OrdinalIgnoreCase);
                     if (index != -1)
                     {
                         content = content.Insert(index, CssTag + Environment.NewLine);
                         injectedCSS = true;
                     }
                }

                if (injectedJS || injectedCSS)
                {
                    File.WriteAllText(indexPath, content);

                    if (injectedJS && injectedCSS)
                    {
                        _logger.LogInformation("MediaBarEnhanced script injected into index.html.");
                    }
                    else if (injectedJS)
                    {
                        _logger.LogInformation("MediaBarEnhanced JS script injected into index.html. But CSS was already present or could not be injected.");
                    }
                    else
                    {
                        _logger.LogInformation("MediaBarEnhanced CSS injected into index.html. But JS script was already present or could not be injected.");
                    }
                }
                else
                {
                    _logger.LogInformation("MediaBarEnhanced script and CSS already present in index.html. Or could not be injected.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("Unauthorized access when attempting to inject script into index.html. Automatic injection failed. Attempting fallback now...");
                RegisterFileTransformation();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error injecting MediaBarEnhanced resources. Attempting fallback.");
                RegisterFileTransformation();
            }
        }

        /// <summary>
        /// Removes the script tag from index.html.
        /// </summary>
        public void Remove()
        {
            UnregisterFileTransformation();

            try
            {
                var webPath = GetWebPath();
                if (string.IsNullOrEmpty(webPath))
                {
                    return;
                }

                var indexPath = Path.Combine(webPath, "index.html");
                if (!File.Exists(indexPath))
                {
                    return;
                }

                var content = File.ReadAllText(indexPath);
                var modified = false;

                if (content.Contains(ScriptTag))
                {
                    content = content.Replace(ScriptTag + Environment.NewLine, "").Replace(ScriptTag, "");
                    modified = true;
                }
                
                if (content.Contains(CssTag))
                {
                    content = content.Replace(CssTag + Environment.NewLine, "").Replace(CssTag, "");
                    modified = true;
                }

                if (modified)
                {
                    File.WriteAllText(indexPath, content);
                    _logger.LogInformation("MediaBarEnhanced script removed from index.html.");
                } else
                {
                    _logger.LogInformation("MediaBarEnhanced script not found in index.html. No removal necessary.");
                }
            }
            catch (UnauthorizedAccessException uaEx)
            {
                _logger.LogError(uaEx, "Unauthorized access when trying to remove MediaBarEnhanced script. Check file permissions.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing MediaBarEnhanced script.");
            }
        }

        private string? GetWebPath()
        {
            var prop = _appPaths.GetType().GetProperty("WebPath", BindingFlags.Instance | BindingFlags.Public);
            return prop?.GetValue(_appPaths) as string;
        }

        private void RegisterFileTransformation()
        {
            _logger.LogInformation("MediaBarEnhanced Fallback. Registering file transformations.");
            
            List<JObject> payloads = new List<JObject>();

            {
                JObject payload = new JObject();
                payload.Add("id", "0dfac9d7-d898-4944-900b-1c1837707279"); 
                payload.Add("fileNamePattern", "index.html");
                payload.Add("callbackAssembly", GetType().Assembly.FullName);
                payload.Add("callbackClass", typeof(TransformationPatches).FullName);
                payload.Add("callbackMethod", nameof(TransformationPatches.IndexHtml));
                
                payloads.Add(payload);
            }

            Assembly? fileTransformationAssembly =
                AssemblyLoadContext.All.SelectMany(x => x.Assemblies).FirstOrDefault(x =>
                    x.FullName?.Contains(".FileTransformation") ?? false);

            if (fileTransformationAssembly != null)
            {
                Type? pluginInterfaceType = fileTransformationAssembly.GetType("Jellyfin.Plugin.FileTransformation.PluginInterface");

                if (pluginInterfaceType != null)
                {
                    foreach (JObject payload in payloads)
                    {
                        pluginInterfaceType.GetMethod("RegisterTransformation")?.Invoke(null, new object?[] { payload });
                    }
                    _logger.LogInformation("File transformations registered successfully.");
                }
                else
                {
                    _logger.LogWarning("FileTransformation plugin found but PluginInterface type missing.");
                }
            }
            else
            {
                 _logger.LogWarning("FileTransformation plugin assembly not found. Fallback failed.");
            }
        }
        
        private void UnregisterFileTransformation()
        {
            try 
            {
                Assembly? fileTransformationAssembly =
                    AssemblyLoadContext.All.SelectMany(x => x.Assemblies).FirstOrDefault(x =>
                        x.FullName?.Contains(".FileTransformation") ?? false);

                if (fileTransformationAssembly != null)
                {
                    Type? pluginInterfaceType = fileTransformationAssembly.GetType("Jellyfin.Plugin.FileTransformation.PluginInterface");

                    if (pluginInterfaceType != null)
                    {
                        Guid id = Guid.Parse("0dfac9d7-d898-4944-900b-1c1837707279");
                        pluginInterfaceType.GetMethod("RemoveTransformation")?.Invoke(null, new object?[] { id });
                        _logger.LogInformation("File transformation unregistered successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error attempting to unregister file transformation. It might not have been registered.");
            }
        }
    }
}
