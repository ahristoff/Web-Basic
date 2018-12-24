
using SimpleMvc.Framework.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleMvc.Framework.ViewEngine
{
    public class View : IRenderable
    {
        public const string BaseLayoutFileName = "Layout";

        public const string ContentPlaceHolder = "{{{content}}}";

        public const string FileExtention = ".html";

        public const string LocalErrorPath = "\\SimpleMvc.Framework\\Errors\\error.html";

        private readonly string templateFullQualifiedName;

        private readonly IDictionary<string, string> viewData;

        public View(string templateFullQualifiedName, IDictionary<string, string> viewData)
        {
            this.templateFullQualifiedName = templateFullQualifiedName;
            this.viewData = viewData;
        }

        public string Render()
        {
            var fileHtml = this.ReadFile();

            if (this.viewData.Any())
            {
                foreach (var x in this.viewData)
                {
                    fileHtml = fileHtml.Replace($"{{{{{{{x.Key}}}}}}}", x.Value);
                }
            }

            return fileHtml;
        }

        private string ReadFile()
        {
            var layoutHtml = this.ReadLayoutFile();

            var templateFullFilePath = this.templateFullQualifiedName + FileExtention;

            if (!File.Exists(templateFullFilePath))
            {
                var errorPath = this.GetErrorPath();
                var errorHtml = File.ReadAllText(errorPath);
                this.viewData["error"] = $"The requested view ({templateFullFilePath}) could not be found";

                return errorHtml;
            }

            var templateHtml = File.ReadAllText(templateFullFilePath);

            return layoutHtml.Replace(ContentPlaceHolder, templateHtml);
        }

        private string ReadLayoutFile()
        {
            var layoutHtml = string.Format(
                "{0}\\{1}{2}",
                MvcContext.Get.ViewFolders,
                BaseLayoutFileName,
                FileExtention);

            if (!File.Exists(layoutHtml))
            {
                var errorPath = this.GetErrorPath();
                var errorHtml = File.ReadAllText(errorPath);
                this.viewData["error"] = "Layout view does not exist!";

                return errorHtml;
            }

            return File.ReadAllText(layoutHtml);
        }

        private string GetErrorPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory);
            var parentDirectoryPath = parentDirectory.FullName;

            return $"{parentDirectoryPath}{LocalErrorPath}";
        }
    }
}
