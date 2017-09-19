using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Antlr4.StringTemplate;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace StringTemplate.MSBuild
{
    public class StringTemplateTask : Task
    {
        private readonly List<ITaskItem> _compileFiles = new List<ITaskItem>();

        public ITaskItem[] TemplateFiles { get; set; }

        [Output]
        public ITaskItem[] CompileFiles => _compileFiles.ToArray();
        
        public override bool Execute()
        {
            if (TemplateFiles == null)
            {
                return true;
            }

            foreach (var file in TemplateFiles)
            {
                var path = file.GetMetadata("FullPath");
                
                // If the file wasn't assigned an outpath (ex. TestClass.cs.stg) continue
                var outpath = GetOutputPath(path);
                if (outpath == null)
                {
                    continue;
                }

                // Render the file and write
                Log.LogCommandLine(MessageImportance.High, "Rendering " + path);

                if (!TryRenderTemplate(path, out var content))
                {
                    Log.LogError("Error rendering " + path);
                    return false;
                }

                File.WriteAllText(outpath, content);
                _compileFiles.Add(new TaskItem(outpath));
            }
            
            return true;
        }
        
        public bool TryRenderTemplate(string templatePath, out string content)
        {
            var uri = new Uri(templatePath, UriKind.Absolute);
            var group = new TemplateGroupFile(uri, Encoding.Default, '<', '>');

            var render = group.GetInstanceOf("render");
            if (render == null)
            {
                content = "";
                return false;
            }

            content = render.Render();
            return true;
        }
        
        public static string GetOutputPath(string filepath)
        {
            var renderedName = Path.GetFileNameWithoutExtension(filepath);
            if (Path.GetExtension(renderedName) == string.Empty)
            {
                return null;
            }
            
            var path = Path.GetDirectoryName(filepath);
            return Path.Combine(path, renderedName);
        }
    }
}
