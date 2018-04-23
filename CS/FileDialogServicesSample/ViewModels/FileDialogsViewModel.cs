using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.IO;
using System.Linq;

namespace FileDialogServicesSample.ViewModels
{
    [POCOViewModel]
    public class FileDialogsViewModel {
        #region Common properties
        public virtual string Filter { get; set; }
        public virtual int FilterIndex { get; set; }
        public virtual string Title { get; set; }
        public virtual bool DialogResult { get; protected set; }
        public virtual string ResultFileName { get; protected set; }
        public virtual string FileBody { get; set; }
        #endregion

        #region SaveFileDialogService specific properties
        public virtual string DefaultExt { get; set; }
        public virtual string DefaultFileName { get; set; }
        public virtual bool OverwritePrompt { get; set; }
        #endregion

        protected ISaveFileDialogService SaveFileDialogService { get { return this.GetService<ISaveFileDialogService>(); } }
        protected IOpenFileDialogService OpenFileDialogService { get { return this.GetService<IOpenFileDialogService>(); } }

        public FileDialogsViewModel() {
            Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            FilterIndex = 1;
            Title = "Custom Dialog Title";
            DefaultExt = "txt";
            DefaultFileName = "Document1";
            OverwritePrompt = true;
        }

        public void Open() {
            OpenFileDialogService.Filter = Filter;
            OpenFileDialogService.FilterIndex = FilterIndex;
            DialogResult = OpenFileDialogService.ShowDialog();
            if (!DialogResult) {
                ResultFileName = string.Empty;
            } else {
                IFileInfo file = OpenFileDialogService.Files.First();
                ResultFileName = file.Name;
                using (var stream = file.OpenText()) {
                    FileBody = stream.ReadToEnd();
                }
            }
        }

        public void Save() {
            SaveFileDialogService.DefaultExt = DefaultExt;
            SaveFileDialogService.DefaultFileName = DefaultFileName;
            SaveFileDialogService.Filter = Filter;
            SaveFileDialogService.FilterIndex = FilterIndex;
            DialogResult = SaveFileDialogService.ShowDialog();
            if (!DialogResult) {
                ResultFileName = string.Empty;
            } else {
                using (var stream = new StreamWriter(SaveFileDialogService.OpenFile())) {
                    stream.Write(FileBody);
                }
            }
        }

    }
}