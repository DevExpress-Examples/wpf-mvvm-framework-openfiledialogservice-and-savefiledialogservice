using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.IO;
using System.Linq;

namespace FileDialogServicesSample.ViewModels {
    public class FileDialogsViewModel : ViewModelBase {
        #region Common properties
        public string Filter { get { return GetValue<string>(); } set { SetValue(value); } }
        public int FilterIndex { get { return GetValue<int>(); } set { SetValue(value); } }
        public string Title { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool DialogResult { get { return GetValue<bool>(); } protected set { SetValue(value); } }
        public string ResultFileName { get { return GetValue<string>(); } protected set { SetValue(value); } }
        public string FileBody { get { return GetValue<string>(); } set { SetValue(value); } }
        #endregion

        #region SaveFileDialogService specific properties
        public string DefaultExt { get { return GetValue<string>(); } set { SetValue(value); } }
        public string DefaultFileName { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool OverwritePrompt { get { return GetValue<bool>(); } set { SetValue(value); } }
        #endregion

        protected ISaveFileDialogService SaveFileDialogService { get { return GetService<ISaveFileDialogService>(); } }
        protected IOpenFileDialogService OpenFileDialogService { get { return GetService<IOpenFileDialogService>(); } }

        public FileDialogsViewModel() {
            Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            FilterIndex = 1;
            Title = "Custom Dialog Title";
            DefaultExt = "txt";
            DefaultFileName = "Document1";
            OverwritePrompt = true;
        }
        [Command]
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
        [Command]
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
